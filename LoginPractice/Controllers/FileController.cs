using LoginPractice.Data;
using LoginPractice.Models;
using LoginPractice.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace LoginPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileController(ApplicationDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: api/File
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // Retrieves all files from the database and returns them as a response.
            var files = await _dbContext.Files.ToListAsync();
            return Ok(files);
        }

        // GET: api/File/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            // Retrieves a file with the given ID from the database and returns it as a response.
            var file = await _dbContext.Files.FindAsync(id);
            if (file == null)
                return NotFound();

            return Ok(file);
        }


        // POST: api/File
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateFilesModel model)
        {
            if (ModelState.IsValid)
            {
                // Extracts the file name from the uploaded file and generates a unique file name.
                var fileName = Path.GetFileName(model.UploadedFile.FileName);
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;

                // Constructs the folder path for uploading and combines it with the unique file name to create the file path.
                var uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Content\\Image");
                var filePath = Path.Combine(uploadFolder, uniqueFileName);

                // Saves the uploaded file to the specified path on the server.
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.UploadedFile.CopyToAsync(stream);
                }

                // Creates a new file model object with the uploaded file details and saves it to the database.
                var fileModel = new FileModel()
                {
                    FileName = fileName,
                    FilePath = filePath,
                    UploadDate = DateTime.UtcNow,
                    PersonName = model.PersonName,
                    City = model.City
                };

                _dbContext.Files.Add(fileModel);
                await _dbContext.SaveChangesAsync();

                return Ok(fileModel);
            }
            else
            {
                // If the model state is not valid, returns a BadRequest response with the model state errors.
                return BadRequest(ModelState);
            }
        }


        // DELETE: api/File/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Retrieves the file with the given ID from the database and deletes it from the server and database.
            var file = await _dbContext.Files.FindAsync(id);
            if (file == null)
                return NotFound();

            System.IO.File.Delete(file.FilePath);
            _dbContext.Files.Remove(file);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/File
        [HttpPut]
        public async Task<IActionResult> Edit(int id, [FromForm] UpdateFilesModel model)
        {
            // Retrieves the file data with the given ID from the database and updates it with the new data, including the uploaded file if provided.
            var data = _dbContext.Files.Find(id);
            if (model.UploadedFile != null)
            {
                if (System.IO.File.Exists(data.FilePath))
                {
                    System.IO.File.Delete(data.FilePath);
                }

                // Extracts the file name from the uploaded file and generates a unique file name.
                var fileName = Path.GetFileName(model.UploadedFile.FileName);
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;

                // Constructs the folder path for uploading and combines it with the unique file name to create the file path.
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Content\\Image");
                string filePath = Path.Combine(uploadFolder, uniqueFileName);

                // Saves the uploaded file to the specified path on the server.
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.UploadedFile.CopyToAsync(stream);
                }

                // Updates the existing file data with the new values and saves the changes to the database.
                data.FileName = fileName;
                data.FilePath = filePath;
                data.UploadDate = DateTime.UtcNow;
                data.PersonName = model.PersonName;
                data.City = model.City;

                _dbContext.Files.Update(data);
                await _dbContext.SaveChangesAsync();
            }

            return Ok(data);
        }

    }
}

