﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD.API.Domain.Entities
{
    public class UsersEntity
    {
        public int IdUser { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime StartDate { get; set; }
        public float Salary { get; set; }
        public string Status { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool Active { get; set; }
        public int TotalRows { get; set; }
    }
}
