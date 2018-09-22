using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace medic.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [EnumDataType(typeof(Tipo))]
        [Required]


        public Tipo Type { get; set; }

        public enum Tipo
        {
            admin = 0,
            medico =1,
            paciente=2
            
        }
    }
}
