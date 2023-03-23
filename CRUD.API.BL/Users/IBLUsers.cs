using CRUD.API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.API.BL.Users
{
    public interface IBLUsers
    {

        public Task<string> CreateAsync(UserEntity user);
        public Task<bool> DeleteAsync(int IdUser);
        public Task<UserEntity> GetAsync(int IdUser);
        public Task<IList<UsersEntity>> GetListAsync(int Page, int Rows, string Name);
        public Task<bool> UpdateAsync(UserEntity user);
        public Task<int> ValidateIdentityAsync(string email);
        public Task<bool> UpdatePasswordAsync(UserEntity user, string NewPassword);
        public Task<bool> ValidatePasswordAsync(string email, string Password);

    }
}
