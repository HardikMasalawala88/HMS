using HMS.Data.ContextModels;
using HMS.Data.FormModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Services.Interface
{
    public interface IUserService
    {
        User GetUserDetails(LoginFormModel loginUser);
        UserFM Create(UserFM userFM);
    }
}
