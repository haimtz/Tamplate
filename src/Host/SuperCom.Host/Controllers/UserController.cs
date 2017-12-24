using Super.User.Identity;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SuperCom.Host.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        
        public IList<UserModle> Get()
        {
            return _userRepository.GetUsers();
        }

        
        public UserModle Get(string id)
        {
            var guid = Guid.Parse(id);
            return _userRepository.GetUser(guid);
        }

        
        public void Post([FromBody]UserModle user)
        {
            _userRepository.AddUser(user);
        }

        
        public void Put(string id,[FromBody]UserModle user)
        {
            var guid = Guid.Parse(id);
            user.Id = guid;
            _userRepository.UpdateUser(user);
        }

        
        public void Delete(string id)
        {
            var guid = Guid.Parse(id);
            _userRepository.DeleteUser(guid);
        }
    }
}
