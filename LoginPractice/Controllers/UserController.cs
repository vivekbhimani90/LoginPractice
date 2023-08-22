using LoginPractice.Models;
using LoginPractice.Models.Dto;
using LoginPractice.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LoginPractice.Controllers
{
    [Route("api/UserAuth")]
    [ApiController]
    public class UserController : Controller
    {

        private readonly IUserRepository _userRepo;
        protected APIResponse _response;
       

        public UserController(IUserRepository userRepository)
        {
            _userRepo = userRepository;
            _response = new();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            var loginResponse = await _userRepo.Login(model);
            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessage.Add("UserName or Password is Invalid.");
                return Ok(_response);
            }

            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = loginResponse;
            return Ok(_response);

        }

        [HttpPost("register")]

        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO model)
        {
            bool IfUserNameUnique = _userRepo.IsUniueUser(model.UserName);

            if (!IfUserNameUnique)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessage.Add("UserName Already Exits..");
                return BadRequest(_response);

            }

            var user = await _userRepo.Register(model);
            if (user == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessage.Add("While registering error..");
                return BadRequest(_response);
            }

            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            return Ok(_response);
        }
    }
}
