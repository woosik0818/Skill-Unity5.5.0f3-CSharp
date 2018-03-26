using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotnetCoreServer.Models;

namespace DotnetCoreServer.Controllers
{
    [Route("[controller]/[action]")]
    public class UpgradeController : Controller
    {

        IUpgradeDao upgradeDao;
        IUserDao userDao;

        public UpgradeController(IUpgradeDao upgradeDao, IUserDao userDao)
        {
            this.upgradeDao = upgradeDao;
            this.userDao = userDao;
        }

        // POST Upgrade/Execute
        [HttpPost]
        public ResultBase Execute([FromBody] UpgradeRequest request)
        {
            ResultBase result = new ResultBase();
            User user = this.userDao.GetUser(request.UserID);

            user.UserID = request.UserID;
            user.Level = request.Level;
            user.StatPoint = request.StatPoint;
            user.Str = request.Str;
            user.Dex = request.Dex;
            user.Int = request.Int;
            user.Con = request.Con;
            user.MaxExperience = request.MaxExperience;
            user.Experience = request.Experience;
            user.Money = request.Money;

            this.userDao.UpdateUser(user);

            result.ResultCode = 1;
            result.Message = "Success";

            return result;

        }
    }
}
