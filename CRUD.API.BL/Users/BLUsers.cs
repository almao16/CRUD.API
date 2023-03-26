using Autofac;
using CRUD.API.BL.Utils;
using CRUD.API.DAL.Common;
using CRUD.API.DAL.Container;
using CRUD.API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.API.BL.Users
{
    public class BLUsers : IBLUsers
    {
        public async Task<string> CreateAsync(UserEntity user)
        {
            string retValue = string.Empty;

            string password = Functions.CreateMD5(user.Password);
            try
            {
                object parammeters = new
                {
                    user.FullName,
                    user.Email,
                    user.StartDate,
                    user.Salary,
                    user.Status,
                    password,
                    user.Role
                };

                using (var scope = DALContainer._container.BeginLifetimeScope())
                {
                    retValue = await scope.Resolve<IDALCommon>().ExecuteScalarAsync(ConstantsProcedures.USERS_CREATE, parammeters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retValue;
        }

        public async Task<bool> DeleteAsync(int IdUser)
        {
            bool retValue = false;
            try
            {
                object parammeters = new
                {
                    IdUser
                };

                using (var scope = DALContainer._container.BeginLifetimeScope())
                {
                    retValue = await scope.Resolve<IDALCommon>().ExecuteAsync(ConstantsProcedures.USERS_DELETE, parammeters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retValue;
        }

        public async Task<UserEntity> GetAsync(int IdUser)
        {
            UserEntity retValue = null;
            try
            {
                object parammeters = new
                {
                    IdUser
                };

                using (var scope = DALContainer._container.BeginLifetimeScope())
                {
                    retValue = await scope.Resolve<IDALCommon>().GetAsync<UserEntity>(ConstantsProcedures.USERS_GET, parammeters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retValue;
        }

        public async Task<IList<UsersEntity>> GetListAsync(int Page, int Rows, string Name)
        {
            List<UsersEntity> retValue = null;
            try
            {
                object parammeters = new
                {
                    Page,
                    Rows,
                    Name
                };

                using (var scope = DALContainer._container.BeginLifetimeScope())
                {
                    retValue = (List<UsersEntity>)await scope.Resolve<IDALCommon>().GetListAsync<UsersEntity>(ConstantsProcedures.USERS_GET_LIST, parammeters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retValue;
        }

        public async Task<bool> UpdateAsync(UserEntity user)
        {
            bool retValue = false;
            try
            {
                object parammeters = new
                {
                    IdUser = user.IdUser,
                    user.FullName,
                    user.Email,
                    user.StartDate,
                    user.Salary,
                    user.Status,
                    user.Role
                };

                using (var scope = DALContainer._container.BeginLifetimeScope())
                {
                    retValue = await scope.Resolve<IDALCommon>().ExecuteAsync(ConstantsProcedures.USERS_UPDATE, parammeters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retValue;
        }

        public async Task<int> ValidateIdentityAsync(string email)
        {


            string retValue = string.Empty;
            int user = 0;
            try
            {
                object parammeters = new
                {
                    email
                };

                using (var scope = DALContainer._container.BeginLifetimeScope())
                {
                    retValue = await scope.Resolve<IDALCommon>().ExecuteScalarAsync(ConstantsProcedures.USERS_VALIDATE_IDENTITY, parammeters);
                    user = int.Parse(retValue);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }

        public async Task<bool> ValidatePasswordAsync(string email, string pass)
        {
            bool retValue = false;
            string password = Functions.CreateMD5(pass);
            try
            {
                object parammeters = new
                {
                    email,
                    password
                };

                using (var scope = DALContainer._container.BeginLifetimeScope())
                {
                    if (!string.IsNullOrEmpty(await scope.Resolve<IDALCommon>().ExecuteScalarAsync(ConstantsProcedures.USERS_VALIDATE_PASSWORD, parammeters)))
                    {
                        retValue = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retValue;
        }

        public async Task<bool> UpdatePasswordAsync(UserEntity user, string newPassword)
        {
            bool retValue = false;
            try
            {
                object parammeters = new
                {
                    idUser = user.IdUser,
                    newPassword
                };

                using (var scope = DALContainer._container.BeginLifetimeScope())
                {
                    retValue = await scope.Resolve<IDALCommon>().ExecuteAsync(ConstantsProcedures.USERS_UPDATE_PASSWORD, parammeters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retValue;
        }


       

    }

}
