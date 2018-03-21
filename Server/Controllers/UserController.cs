using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotnetCoreServer.Models;

namespace DotnetCoreServer.Controllers
{
    [Route("[controller]/[action]")]
    //[Route("[controller]/[action]")]
    public class UserController : Controller
    {
        IUserDao userDao;
        public UserController(IUserDao userDao){
            this.userDao = userDao;
        }

        // GET User/Info?UserID=42
        [HttpGet]
        public UserResult Info(int UserID)
        {
            UserResult result = new UserResult();
            result.Data = userDao.GetUser(UserID);
            result.ResultCode = 1;
            result.Message = "OK";
            return result;
        }


        // GET user/get/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            User user = userDao.GetUser(id);
            return user;
        }
    }
}
