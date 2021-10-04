using HMS.Data.ContextModels;
using HMS.Data.FormModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Services.Interface
{
    public interface ILoginUserService
    {
        //IEnumerable<User> GetUserDetails(UserFormModel loginUser);
        IEnumerable<User> GetUserDetails(LoginFormModel loginUser);
    }
}
