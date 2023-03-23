using Autofac;
using AutoMapper;
using CRUD.API.BL.Clients;
using CRUD.API.BL.Container;
using CRUD.API.BL.Utils;
using CRUD.API.Domain.Entities;
using CRUD.API.Domain.General;
using CRUD.API.DTOs;
using CRUD.API.Helpers;
using CRUD.API.Swagger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CRUD.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientsController : Controller
    {
        private readonly IConfiguration Configuration;
        private readonly IMapper Mapper;

        public ClientsController(IConfiguration configuration, IMapper mapper)
        {
            Configuration = configuration;
            Mapper = mapper;
        }


        [Route("Create")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(GuidResExample), 201)]
        [SwaggerResponseExample(201, typeof(GuidResExample))]
        [HttpPost]
        public async Task<IActionResult> Create(ClientCreateDTO request)
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


                    ClientEntity productEntity = Mapper.Map<ClientEntity>(request);

                    string name = await scope.Resolve<IBLClients>().CreateAsync(productEntity);

                    if (string.IsNullOrEmpty(name))
                    {
                        throw new Exception(Functions.FormatError(Constants.MESSAGE_ERROR_CREATE, Enums.Entity.USUARIO.ToString()));
                    }

                    operationResult.Data = name;
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
        [ProducesResponseType(typeof(ClientGetResExample), 200)]
        [SwaggerResponseExample(200, typeof(ClientGetResExample))]
        [HttpGet]
        public async Task<IActionResult> Get([Required] int IdClient)
        {
            OperationResult<ClientDTO> operationResult = new OperationResult<ClientDTO>();


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
                    ClientEntity clientEntity = await scope.Resolve<IBLClients>().GetAsync(IdClient);

                    if (clientEntity != null)
                    {
                        operationResult.Data = Mapper.Map<ClientDTO>(clientEntity);
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
        [ProducesResponseType(typeof(ClientGetListResExample), 200)]
        [SwaggerResponseExample(200, typeof(ClientGetListResExample))]
        [HttpGet]
        public async Task<IActionResult> GetList(
        [Required][Range(1, int.MaxValue)] int Page,
        [Required][Range(1, int.MaxValue)] int Rows,
        [StringLength(50)] string Name)
        {
            OperationResult<GenericList<ClientsDTO>> operationResult = new OperationResult<GenericList<ClientsDTO>>();
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
                    IList<ClientsEntity> items = await scope.Resolve<IBLClients>().GetListAsync(Page, Rows, Name);

                    if (items != null)
                    {
                        operationResult.Data = new GenericList<ClientsDTO>
                        {
                            CurrentPage = Page,
                            Rows = Rows,
                            TotalRows = items.FirstOrDefault().TotalRows,
                            Items = Mapper.Map<List<ClientsDTO>>(items)
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
        [ProducesResponseType(typeof(ClientGetResExample), 200)]
        [SwaggerResponseExample(200, typeof(ClientGetResExample))]
        [HttpPut]
        public async Task<IActionResult> Update(ClientUpdateDTO request)
        {
            OperationResult<ClientDTO> operationResult = new OperationResult<ClientDTO>();
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
                    ClientEntity _clientEntity = await scope.Resolve<IBLClients>().GetAsync(request.IdClient);
                    if (_clientEntity == null)
                    {
                        throw new Exception(Functions.FormatError(Constants.MESSAGE_ERROR_NOT_FOUND, Enums.Entity.USUARIO.ToString()));
                    }



                    ClientEntity clientEntity = Mapper.Map<ClientEntity>(request);

                    if (!await scope.Resolve<IBLClients>().UpdateAsync(clientEntity))
                    {
                        throw new Exception(Functions.FormatError(Constants.MESSAGE_ERROR_UPDATE, Enums.Entity.USUARIO.ToString()));
                    }

                    clientEntity = await scope.Resolve<IBLClients>().GetAsync(clientEntity.IdClient);

                    if (clientEntity != null)
                    {
                        operationResult.Data = Mapper.Map<ClientDTO>(clientEntity);
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
        public async Task<IActionResult> Delete([Required] int IdClient)
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


                    if (!await scope.Resolve<IBLClients>().DeleteAsync(IdClient))
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

    }

}
