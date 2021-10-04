using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Data.FormModels
{
    public class LoginFormModel 
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
