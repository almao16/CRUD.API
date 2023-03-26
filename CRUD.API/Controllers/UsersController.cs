using Autofac;
using AutoMapper;
using CRUD.API.BL.Container;
using CRUD.API.BL.Users;
using CRUD.API.BL.Utils;
using CRUD.API.Domain.Entities;
using CRUD.API.Domain.General;
using CRUD.API.DTOs;
using CRUD.API.Helpers;
using CRUD.API.Swagger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.API.Controllers
{

    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IConfiguration Configuration;
        private readonly IMapper Mapper;

        public UsersController(IConfiguration configuration, IMapper mapper)
        {
            Configuration = configuration;
            Mapper = mapper;
        }


        [Route("Create")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(GuidResExample), 201)]
        [SwaggerResponseExample(201, typeof(GuidResExample))]
        [HttpPost]
        public async Task<IActionResult> Create(UserCreateDTO request)
        {
            OperationResult<string> operationResult = new OperationResult<string>();

           

                try
                {
                    if (!ModelState.IsValid)
                    {
                        operationResult.Err = true;
                        operationResult.Message = ErrorHelper.ErrorsToString(ModelState);
                        return StatusCode((int)HttpStatusCode.BadRequest, operationResult);
                    }

                    using (var scope = BLContainer._container.BeginLifetimeScope())
                    {


                        UserEntity userEntity = Mapper.Map<UserEntity>(request);

                        string email = await scope.Resolve<IBLUsers>().CreateAsync(userEntity);

                        if (string.IsNullOrEmpty(email))
                        {
                            throw new Exception(Functions.FormatError(Constants.MESSAGE_ERROR_CREATE, Enums.Entity.USUARIO.ToString()));
                        }

                        operationResult.Data = email;
                    }
                }
                catch (Exception ex)
                {
                    operationResult.Err = true;
                    operationResult.Message = ex.Message;
                    return StatusCode((int)HttpStatusCode.InternalServerError, operationResult);
                }
          

            return StatusCode((int)HttpStatusCode.Created, operationResult);
        }


        [Route("Get")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserGetResExample), 200)]
        [SwaggerResponseExample(200, typeof(UserGetResExample))]
        [HttpGet]
        public async Task<IActionResult> Get([Required] int IdUser)
        {
            OperationResult<UserDTO> operationResult = new OperationResult<UserDTO>();


            try
            {
                if (!ModelState.IsValid)
                {
                    operationResult.Err = true;
                    operationResult.Message = ErrorHelper.ErrorsToString(ModelState);
                    return StatusCode((int)HttpStatusCode.BadRequest, operationResult);
                }

                using (var scope = BLContainer._container.BeginLifetimeScope())
                {
                    UserEntity userEntity = await scope.Resolve<IBLUsers>().GetAsync(IdUser);

                    if (userEntity != null)
                    {
                        operationResult.Data = Mapper.Map<UserDTO>(userEntity);
                    }
                }
            }
            catch (Exception ex)
            {
                operationResult.Err = true;
                operationResult.Message = ex.Message;
                return StatusCode((int)HttpStatusCode.InternalServerError, operationResult);
            }



            return StatusCode((int)HttpStatusCode.OK, operationResult);
        }

        [Route("GetList")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserGetListResExample), 200)]
        [SwaggerResponseExample(200, typeof(UserGetListResExample))]
        [HttpGet]
        public async Task<IActionResult> GetList(
        [Required][Range(1, int.MaxValue)] int Page,
        [Required][Range(1, int.MaxValue)] int Rows,
        [StringLength(50)] string Name)
        {
            OperationResult<GenericList<UsersDTO>> operationResult = new OperationResult<GenericList<UsersDTO>>();


            try
            {
                if (!ModelState.IsValid)
                {
                    operationResult.Err = true;
                    operationResult.Message = ErrorHelper.ErrorsToString(ModelState);
                    return StatusCode((int)HttpStatusCode.BadRequest, operationResult);
                }

                using (var scope = BLContainer._container.BeginLifetimeScope())
                {
                    IList<UsersEntity> items = await scope.Resolve<IBLUsers>().GetListAsync(Page, Rows, Name);

                    if (items != null)
                    {
                        operationResult.Data = new GenericList<UsersDTO>
                        {
                            CurrentPage = Page,
                            Rows = Rows,
                            TotalRows = items.FirstOrDefault().TotalRows,
                            Items = Mapper.Map<List<UsersDTO>>(items)
                        };


                    }
                }
            }
            catch (Exception ex)
            {
                operationResult.Err = true;
                operationResult.Message = ex.Message;
                return StatusCode((int)HttpStatusCode.InternalServerError, operationResult);
            }



            return StatusCode((int)HttpStatusCode.OK, operationResult);
        }


        [Route("Update")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserGetResExample), 200)]
        [SwaggerResponseExample(200, typeof(UserGetResExample))]
        [HttpPut]
        public async Task<IActionResult> Update(UserUpdateDTO request)
        {
            OperationResult<UserDTO> operationResult = new OperationResult<UserDTO>();
           
                try
                {
                    if (!ModelState.IsValid)
                    {
                        operationResult.Err = true;
                        operationResult.Message = ErrorHelper.ErrorsToString(ModelState);
                        return StatusCode((int)HttpStatusCode.BadRequest, operationResult);
                    }

                    using (var scope = BLContainer._container.BeginLifetimeScope())
                    {
                        UserEntity _userEntity = await scope.Resolve<IBLUsers>().GetAsync(request.IdUser);
                        if (_userEntity == null)
                        {
                            throw new Exception(Functions.FormatError(Constants.MESSAGE_ERROR_NOT_FOUND, Enums.Entity.USUARIO.ToString()));
                        }



                        UserEntity userEntity = Mapper.Map<UserEntity>(request);

                        if (!await scope.Resolve<IBLUsers>().UpdateAsync(userEntity))
                        {
                            throw new Exception(Functions.FormatError(Constants.MESSAGE_ERROR_UPDATE, Enums.Entity.USUARIO.ToString()));
                        }

                        userEntity = await scope.Resolve<IBLUsers>().GetAsync(userEntity.IdUser);

                        if (userEntity != null)
                        {
                            operationResult.Data = Mapper.Map<UserDTO>(userEntity);
                        }
                    }
                }
                catch (Exception ex)
                {
                    operationResult.Err = true;
                    operationResult.Message = ex.Message;
                    return StatusCode((int)HttpStatusCode.InternalServerError, operationResult);
                }
            return StatusCode((int)HttpStatusCode.OK, operationResult);
        }


        
        [Route("Delete")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(GuidResExample), 200)]
        [SwaggerResponseExample(200, typeof(GuidResExample))]
        [HttpDelete]
        public async Task<IActionResult> Delete([Required] int IdUser)
        {
            OperationResult<string> operationResult = new OperationResult<string>();
                try
                {
                    if (!ModelState.IsValid)
                    {
                        operationResult.Err = true;
                        operationResult.Message = ErrorHelper.ErrorsToString(ModelState);
                        return StatusCode((int)HttpStatusCode.BadRequest, operationResult);
                    }

                    using (var scope = BLContainer._container.BeginLifetimeScope())
                    {


                        if (!await scope.Resolve<IBLUsers>().DeleteAsync(IdUser))
                        {
                            throw new Exception(Functions.FormatError(Constants.MESSAGE_ERROR_DELETE, Enums.Entity.USUARIO.ToString()));
                        }

                        operationResult.Data = Constants.MESSAGE_TRANSACTION_SUCCESSFUL;
                    }
                }
                catch (Exception ex)
                {
                    operationResult.Err = true;
                    operationResult.Message = ex.Message;
                    return StatusCode((int)HttpStatusCode.InternalServerError, operationResult);
                }

            return StatusCode((int)HttpStatusCode.OK, operationResult);
        }


        [AllowAnonymous]
        [Route("Authenticate")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TokenResExample), 200)]
        [SwaggerResponseExample(200, typeof(TokenResExample))]
        [HttpPost]
        public async Task<IActionResult> Authenticate(UserAuthenticateDTO request)
        {
            OperationResult<string> operationResult = new OperationResult<string>();
            try
            {
                if (!ModelState.IsValid)
                {
                    operationResult.Err = true;
                    operationResult.Message = ErrorHelper.ErrorsToString(ModelState);
                    return StatusCode((int)HttpStatusCode.BadRequest, operationResult);
                }

                using (var scope = BLContainer._container.BeginLifetimeScope())
                {
                    UserEntity userEntity = null;
                    int user;
                    string userss = string.Empty;

                    user = await scope.Resolve<IBLUsers>().ValidateIdentityAsync(request.Email);
                    userss = user.ToString();

                    if ((user == 0) | userss.Equals(null))
                    {
                        throw new Exception(Functions.FormatError(Constants.MESSAGE_ERROR_EMAIL, Enums.Entity.PASSWORD.ToString()));
                    }

                    if (!await scope.Resolve<IBLUsers>().ValidatePasswordAsync(request.Email, request.Password))
                    {
                        throw new Exception(Functions.FormatError(Constants.MESSAGE_ERROR_PASSWORD, Enums.Entity.PASSWORD.ToString()));
                    }

                    userEntity = await scope.Resolve<IBLUsers>().GetAsync(user);

                    operationResult.Data = GenerateToken(userEntity, request.RememberMe);
                }
            }
            catch (Exception ex)
            {
                operationResult.Err = true;
                operationResult.Message = ex.Message;
                return StatusCode((int)HttpStatusCode.InternalServerError, operationResult);
            }
            return StatusCode((int)HttpStatusCode.OK, operationResult);
        }



        [Route("UpdatePassword")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(StringResExample), 200)]
        [SwaggerResponseExample(200, typeof(StringResExample))]
        [HttpPut]
        public async Task<IActionResult> UpdatePassword(UserUpdatePasswordDTO request)
        {
            OperationResult<string> operationResult = new OperationResult<string>();
            try
            {
                if (!ModelState.IsValid)
                {
                    operationResult.Err = true;
                    operationResult.Message = ErrorHelper.ErrorsToString(ModelState);
                    return StatusCode((int)HttpStatusCode.BadRequest, operationResult);
                }

                using (var scope = BLContainer._container.BeginLifetimeScope())
                {
                    int idUserLanding = request.IdUser;

                    if (!await scope.Resolve<IBLUsers>().ValidatePasswordAsync(request.Email, request.OldPassword))
                    {
                        throw new Exception(Functions.FormatError(Constants.MESSAGE_ERROR_PASSWORD, Enums.Entity.PASSWORD.ToString()));
                    }


                    UserEntity _userEntity = await scope.Resolve<IBLUsers>().GetAsync(idUserLanding);

                    if (_userEntity == null)
                    {
                        throw new Exception(Functions.FormatError(Constants.MESSAGE_ERROR_NOT_FOUND, Enums.Entity.USUARIO.ToString()));
                    }

                    if (!await scope.Resolve<IBLUsers>().UpdatePasswordAsync(_userEntity, Functions.CreateMD5(request.NewPassword)))
                    {
                        throw new Exception(Functions.FormatError(Constants.MESSAGE_ERROR_UPDATE, Enums.Entity.PASSWORD.ToString()));
                    }

                    operationResult.Data = Constants.MESSAGE_TRANSACTION_SUCCESSFUL;
                }
            }
            catch (Exception ex)
            {
                operationResult.Err = true;
                operationResult.Message = ex.Message;
                return StatusCode((int)HttpStatusCode.InternalServerError, operationResult);
            }
            return StatusCode((int)HttpStatusCode.OK, operationResult);
        }


        private string GenerateToken(UserEntity userEntity, bool rememberMe)
        {
            byte[] jwtKey = Encoding.ASCII.GetBytes(Configuration.GetValue<string>(Constants.CONFIG_CRUD_JWT_KEY));

            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaim(new Claim("idUser", userEntity.IdUser.ToString(), ClaimValueTypes.Integer32));
            claimsIdentity.AddClaim(new Claim("fullNmae", userEntity.FullName ?? string.Empty));
            claimsIdentity.AddClaim(new Claim("email", userEntity.Email ?? string.Empty));
            claimsIdentity.AddClaim(new Claim("role", userEntity.Role ?? string.Empty));
            claimsIdentity.AddClaim(new Claim("active", userEntity.Active.ToString(), ClaimValueTypes.Boolean));

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = rememberMe ? DateTime.UtcNow.AddMonths(1) : DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(jwtKey), SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken createdToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(createdToken);
        }

    }
}
