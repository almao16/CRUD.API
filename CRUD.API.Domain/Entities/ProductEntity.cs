﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD.API.Domain.Entities
{
    public class ProductEntity
    {
        public int IdProduct { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public bool Active { get; set; }
    }
}
