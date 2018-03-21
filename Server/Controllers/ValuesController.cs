using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotnetCoreServer.Models;
using MySql.Data.MySqlClient;

namespace DotnetCoreServer.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        IUserDao userDao;
        public ValuesController(IUserDao userDao)
        {
            this.userDao = userDao;
        }
        
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public User Get(long id)
        {
            //IUserDao userDao;
            //User user = new User();
            User user = userDao.GetUser(id);
            return user;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
