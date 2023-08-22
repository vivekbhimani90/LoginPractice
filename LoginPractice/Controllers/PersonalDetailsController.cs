using AutoMapper;
using LoginPractice.Data;
using LoginPractice.Models;
using LoginPractice.Models.Dto;
using LoginPractice.Repository.IRepository;
using LoginPractice.Store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace LoginPractice.Controllers
{
    [Route("api/PersonalDetails")]
    [ApiController]
   
    
    public class PersonalDetailsController : Controller
    {

        //private readonly ApplicationDbContext _db;
        private readonly IPersonalDetailsRepository _dbDetails;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public PersonalDetailsController(IPersonalDetailsRepository dbDetails, IMapper mapper)
        {
            _dbDetails = dbDetails;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]

        public async Task<ActionResult<APIResponse>> PersonalDetails()
        {
            try
            {
                IEnumerable<PersonalDetails> model = await _dbDetails.GetAllAsync();

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = _mapper.Map<List<PersonalDetailsDTO>>(model);
                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage =
                      new List<string>() { ex.ToString() };
            }

            return _response;

        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<ActionResult<APIResponse>> PersonalDetail(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }

                //var personalDetail = await _db.UserDetails.FirstOrDefaultAsync(u => u.Id == id);
                var personalDetail = await _dbDetails.GetAsync(u => u.Id == id);

                if (personalDetail == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return (_response);
                }

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = _mapper.Map<PersonalDetails>(personalDetail);
                return Ok(_response);
            }
             catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage =
                      new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<ActionResult<APIResponse>> CreatePersonalDetail([FromBody] PersonalDetailsCreateDTO model)
        {
            try
            {
                if (await _dbDetails.GetAsync(u => u.Name.ToLower() == model.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("CustomError", "Name Is Already Exits..");
                    return BadRequest(ModelState);
                }

                if (model == null)
                {
                    return BadRequest(model);
                }

                //model.Id = PersonalDetailsStore.personalDetails.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
                PersonalDetails details = _mapper.Map<PersonalDetails>(model);
                await _dbDetails.CreateAsync(details);

                _response.StatusCode = HttpStatusCode.Created;
                _response.Result = details;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage =
                      new List<string>() { ex.ToString() };
            }
            return _response;

        }

        [HttpDelete("{id:int}", Name = "RemoveDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<ActionResult<APIResponse>> RemovePersonalDetail(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }

                var personalDetail = await _dbDetails.GetAsync(u => u.Id == id);

                if (personalDetail == null)
                {
                    return NotFound();
                }

                await _dbDetails.RemoveAsync(personalDetail);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage =
                      new List<string>() { ex.ToString() };
            }
            return _response;

        }

        [HttpPut("{id:int}", Name = "UpdateDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<ActionResult<APIResponse>> UpdatePersonalDetail(int id, [FromBody] PersonalDetailsUpdateDTO model)
        {
            if (model == null || id != model.Id)
            {
                return BadRequest();
            }

            // var personalDetail = _db.UserDetails.FirstOrDefault(u => u.Id == id);

            PersonalDetails details = _mapper.Map<PersonalDetails>(model);


            await _dbDetails.UpdateAsync(details);

            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);
        }
    }
}
