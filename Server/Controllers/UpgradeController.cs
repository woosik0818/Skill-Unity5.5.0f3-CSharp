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

        public UpgradeController(IUpgradeDao upgradeDao, IUserDao userDao){
            this.upgradeDao = upgradeDao;
            this.userDao = userDao;
        }

        // GET Upgrade/Info
        //[HttpGet]
        //public UpgradeResult Info()
        //{
        //    UpgradeResult result = new UpgradeResult();

        //    result.Data = this.upgradeDao.GetUpgradeInfo();

        //    result.ResultCode = 1;
        //    result.Message = "OK";

        //    return result;
        //}

        //// POST Upgrade/Execute/42
        //[HttpPost("{id}")]
        //public ResultBase Execute(long id, [FromBody] UpgradeRequest request)
        //{
        //    request.UserID = id;
        //    ResultBase result = new ResultBase();
        //    User user = this.userDao.GetUser(request.UserID);
        //    UpgradeData upgradeInfo = null;
        //    if("StatPoint".Equals(request.UpgradeType)){
        //        upgradeInfo = this.upgradeDao.GetUpgradeInfo(request.UpgradeType, user.StatPoint, user.UserID);
        //    }else if("Str".Equals(request.UpgradeType)){
        //        upgradeInfo = this.upgradeDao.GetUpgradeInfo(request.UpgradeType, user.Str, user.UserID);
        //    }else if("Int".Equals(request.UpgradeType)){
        //        upgradeInfo = this.upgradeDao.GetUpgradeInfo(request.UpgradeType, user.Int, user.UserID);
        //    }else if("Con".Equals(request.UpgradeType)){
        //        upgradeInfo = this.upgradeDao.GetUpgradeInfo(request.UpgradeType, user.Con, user.UserID);
        //    }else if ("Dex".Equals(request.UpgradeType))
        //    {
        //        upgradeInfo = this.upgradeDao.GetUpgradeInfo(request.UpgradeType, user.Dex, user.UserID);
        //    }else if ("Level".Equals(request.UpgradeType))
        //    {
        //        upgradeInfo = this.upgradeDao.GetUpgradeInfo(request.UpgradeType, user.Level, user.UserID);
        //    }else if ("Experience".Equals(request.UpgradeType))
        //    {
        //        upgradeInfo = this.upgradeDao.GetUpgradeInfo(request.UpgradeType, user.Experience, user.UserID);
        //    }else if ("MaxExperience".Equals(request.UpgradeType))
        //    {
        //        upgradeInfo = this.upgradeDao.GetUpgradeInfo(request.UpgradeType, user.Experience, user.UserID);
        //    }else if ("Money".Equals(request.UpgradeType))
        //    {
        //        upgradeInfo = this.upgradeDao.GetUpgradeInfo(request.UpgradeType, user.Experience, user.UserID);
        //    }else{
        //        // 유효하지 않은 업그레이드 타입입니다.
        //    }


        //    if("StatPoint".Equals(request.UpgradeType))
        //    {

        //        user.StatPoint = request.stat;

        //    }

        //    else if("Str".Equals(request.UpgradeType))
        //    {
                
        //        user.Str = request.stat;

        //    }

        //    else if("Int".Equals(request.UpgradeType))
        //    {
                
        //        user.Int = request.stat;

        //    }

        //    else if("Con".Equals(request.UpgradeType))
        //    {

        //        user.Con = request.stat;

        //    }

        //    else if ("Dex".Equals(request.UpgradeType))
        //    {

        //        user.Dex = request.stat;

        //    }

        //    else if ("Level".Equals(request.UpgradeType))
        //    {

        //        user.Level = request.stat;
        //    }

        //    else if ("Experience".Equals(request.UpgradeType))
        //    {

        //        user.Experience = request.stat;

        //    }

        //    else if ("MaxExperience".Equals(request.UpgradeType))
        //    {

        //        user.MaxExperience = request.stat;

        //    }

        //    else if ("Money".Equals(request.UpgradeType))
        //    {

        //        user.Money = request.stat;

        //    }

        //    this.userDao.UpdateUser(user);


        //    result.ResultCode = 1;
        //    result.Message = "Success";

        //    return result;

        //}

 

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
