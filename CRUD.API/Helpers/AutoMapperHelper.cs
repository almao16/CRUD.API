using AutoMapper;
using CRUD.API.Domain.Entities;
using CRUD.API.DTOs;

namespace CRUD.API.Helpers
{
    public class AutoMapperHelper : Profile
    {
        public AutoMapperHelper()
        {

            //USERS
            CreateMap<UserEntity, UserDTO>();
            CreateMap<UsersEntity, UsersDTO>();
            CreateMap<UserCreateDTO, UserEntity>();
            CreateMap<UserUpdateDTO, UserEntity>();

            //CLIENTS
            CreateMap<ClientEntity, ClientDTO>();
            CreateMap<ClientsEntity, ClientsDTO>();
            CreateMap<ClientCreateDTO, ClientEntity>();
            CreateMap<ClientUpdateDTO, ClientEntity>();

            //PRODUCTS
            CreateMap<ProductEntity, ProductDTO>();
            CreateMap<ProductsEntity, ProductsDTO>();
            CreateMap<ProductCreateDTO, ProductEntity>();
            CreateMap<ProductUpdateDTO, ProductEntity>();




        }

    }
}
