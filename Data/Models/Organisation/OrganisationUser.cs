﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class OrganisationUser
    {
        public string PasswordHash { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
    }
}