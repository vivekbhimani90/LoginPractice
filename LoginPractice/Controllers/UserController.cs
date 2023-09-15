using LoginPractice.Helper;
using LoginPractice.Models;
using LoginPractice.Models.Dto;
using LoginPractice.Repository.IRepository;
using LoginPractice.Service;
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
        private readonly IEmailService _emailService;

        public UserController(IUserRepository userRepository, IEmailService emailService)
        {
            _userRepo = userRepository;
            _response = new();
            _emailService = emailService;
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            /*var loginResponse = await _userRepo.Login(model);*/
            
            GenrateOtpResponseDTO loginResponse = await _userRepo.GenrateOtp(model);

            if (loginResponse.User == null || loginResponse.Otp == "")
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessage.Add("Email or Password is Invalid.");
                return Ok(_response);
            }

            HttpContext.Session.SetString("OTP",loginResponse.Otp);
            HttpContext.Session.SetString("Email", model.Email);
           

            Mailrequest mailrequest = new Mailrequest();
            mailrequest.ToEmail = model.Email;
            mailrequest.Subject = "OTP Verification";
            mailrequest.Body = GetHtmlcontent(loginResponse.Otp);
            await _emailService.SendEmailAsync(mailrequest);



            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = loginResponse;
            return Ok(_response);

        }


        [HttpPost("verifyotp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpRequestDTO verifyModel)
        {
            string storedOtp = HttpContext.Session.GetString("OTP");
            string stroredEmail = HttpContext.Session.GetString("Email");

            if (storedOtp == null || storedOtp != verifyModel.Otp)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessage.Add("Invalid OTP.");
                return Ok(_response);
            }

            // Clear the OTP from session after successful verification
            HttpContext.Session.Remove("OTP");

            LoginResponseDTO loginResponse = await _userRepo.VerifyOtp(stroredEmail);

            HttpContext.Session.Remove("Email");


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

        [HttpPost("SendMail")]
        public async Task<IActionResult> SendMail(Mailrequest usermailreq)
        {
            try
            {
                /*Mailrequest mailrequest = new Mailrequest();
                mailrequest.ToEmail = usermailreq.ToEmail;
                mailrequest.Subject = usermailreq.Subject;
                mailrequest.Body = GetHtmlcontent(usermailreq.Body);
                await _emailService.SendEmailAsync(mailrequest);*/
                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
        }




        private string GetHtmlcontent(string Otp)
        {
            string Response = "<div style=\"width:100%;background-color:lightblue;text-align:center;margin:10px\">";
            Response += $"<h1>Verification OTP {Otp}</h1>";
            Response += "<img src=\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTbCDZZfnTfo6P6OVAR4PEbLGDBPs3hkIocmS18c9Y6MA&s\" />";
            Response += "<h2>Valid For 2 Min.</h2>";
            Response += "<a href=\"https://www.youtube.com/channel/UCsbmVmB_or8sVLLEq4XhE_A/join\">Please join membership by click the link</a>";
            Response += "<div><h1> Contact us : +917069450940</h1></div>";
            Response += "</div>";
            return Response;
        }
    }
}



/*[HttpPost("login")]
public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
{
    var loginResponse = await _userRepo.Login(model);

    *//* if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
     {
         _response.StatusCode = HttpStatusCode.BadRequest;
         _response.IsSuccess = false;
         _response.ErrorMessage.Add("UserName or Password is Invalid.");
         return Ok(_response);
     }*//*

    _response.StatusCode = HttpStatusCode.OK;
    _response.IsSuccess = true;
    _response.Result = loginResponse;
    return Ok(_response);

}*/