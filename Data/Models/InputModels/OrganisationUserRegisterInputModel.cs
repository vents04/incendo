using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.InputModels
{
    public class OrganisationUserRegisterInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        //TODO:add ther registering properties
    }
}