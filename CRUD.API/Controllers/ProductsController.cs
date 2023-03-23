using Autofac;
using AutoMapper;
using CRUD.API.BL.Container;
using CRUD.API.BL.Products;
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
    public class ProductsController : Controller
    {
        private readonly IConfiguration Configuration;
        private readonly IMapper Mapper;

        public ProductsController(IConfiguration configuration, IMapper mapper)
        {
            Configuration = configuration;
            Mapper = mapper;
        }

       
        [Route("Create")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(GuidResExample), 201)]
        [SwaggerResponseExample(201, typeof(GuidResExample))]
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateDTO request)
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


                    ProductEntity productEntity = Mapper.Map<ProductEntity>(request);

                    string name = await scope.Resolve<IBLProducts>().CreateAsync(productEntity);

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
        [ProducesResponseType(typeof(ProductGetResExample), 200)]
        [SwaggerResponseExample(200, typeof(ProductGetResExample))]
        [HttpGet]
        public async Task<IActionResult> Get([Required] int IdProduct)
        {
            OperationResult<ProductDTO> operationResult = new OperationResult<ProductDTO>();


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
                    ProductEntity productEntity = await scope.Resolve<IBLProducts>().GetAsync(IdProduct);

                    if (productEntity != null)
                    {
                        operationResult.Data = Mapper.Map<ProductDTO>(productEntity);
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
        [ProducesResponseType(typeof(ProductGetListResExample), 200)]
        [SwaggerResponseExample(200, typeof(ProductGetListResExample))]
        [HttpGet]
        public async Task<IActionResult> GetList(
        [Required][Range(1, int.MaxValue)] int Page,
        [Required][Range(1, int.MaxValue)] int Rows,
        [StringLength(50)] string Name)
        {
            OperationResult<GenericList<ProductsDTO>> operationResult = new OperationResult<GenericList<ProductsDTO>>();
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
                    IList<ProductsEntity> items = await scope.Resolve<IBLProducts>().GetListAsync(Page, Rows, Name);

                    if (items != null)
                    {
                        operationResult.Data = new GenericList<ProductsDTO>
                        {
                            CurrentPage = Page,
                            Rows = Rows,
                            TotalRows = items.FirstOrDefault().TotalRows,
                            Items = Mapper.Map<List<ProductsDTO>>(items)
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
        [ProducesResponseType(typeof(ProductGetResExample), 200)]
        [SwaggerResponseExample(200, typeof(ProductGetResExample))]
        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDTO request)
        {
            OperationResult<ProductDTO> operationResult = new OperationResult<ProductDTO>();
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
                    ProductEntity _producEntity = await scope.Resolve<IBLProducts>().GetAsync(request.IdProduc);
                    if (_producEntity == null)
                    {
                        throw new Exception(Functions.FormatError(Constants.MESSAGE_ERROR_NOT_FOUND, Enums.Entity.USUARIO.ToString()));
                    }



                    ProductEntity producEntity = Mapper.Map<ProductEntity>(request);
                    Console.WriteLine("Este es el reques", request);
                    Console.WriteLine(producEntity);
                    if (!await scope.Resolve<IBLProducts>().UpdateAsync(producEntity))
                    {
                        Console.WriteLine("Este fue el error");
                        throw new Exception(Functions.FormatError(Constants.MESSAGE_ERROR_UPDATE, Enums.Entity.USUARIO.ToString()));
                    }

                    producEntity = await scope.Resolve<IBLProducts>().GetAsync(producEntity.IdProduct);

                    if (producEntity != null)
                    {
                        operationResult.Data = Mapper.Map<ProductDTO>(producEntity);
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
        public async Task<IActionResult> Delete([Required] int IdProduct)
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


                    if (!await scope.Resolve<IBLProducts>().DeleteAsync(IdProduct))
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
