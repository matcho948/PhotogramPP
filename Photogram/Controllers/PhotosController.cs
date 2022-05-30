using Microsoft.AspNetCore.Mvc;
using Photogram.Data;
using Photogram.Models;

namespace Photogram.Controllers
{
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IPhotogramRepo _repo;
        public PhotosController(IPhotogramRepo repo)
        {
            _repo = repo;
        }
        
        [HttpGet("/GetAllPhotos")]
        public async Task<ActionResult<IEnumerable<Photos>>> GetAllPhotos()
        {
            var photos = _repo.GetAllPhotos();
            if(photos == null)
                return NotFound();
            return Ok(photos);
        }
        [HttpGet("/GetUserPhotos/{id}")]
        public async Task<ActionResult<IEnumerable<Photos>>> GetUserPhotos(int id)
        {
            var photos = _repo.GetUserWithPhotosById(id).Photos;
            if(photos == null)
                return BadRequest();
            return Ok(photos);
        }
        [HttpGet("/GetPhotoById/{id}")]
        public async Task<ActionResult<Photos>> GetPhotoById(int id)
        {
            var photo = _repo.getPhotoById(id);
            if (photo == null)
                return BadRequest();
            return Ok(photo.PhotoUrl);
        }
        [HttpPost("AddNewPhoto")]
        public async Task<ActionResult> AddNewPhoto(String URL, int userId,string description)
        {
            try
            {
                if (URL == null)
                    return BadRequest();
                var photo = new Photos(URL,description);
                await _repo.addNewPhoto(userId, photo);
                return Ok(photo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPatch("/SetProfilePhoto/{id}")]
        public async Task<ActionResult> SetProfilePhoto([FromBody] int id)
        {
            try
            {
                if (id == null)
                    return BadRequest();

                var photo = _repo.getPhotoById(id);
                _repo.setProfilePhoto(photo);
                return Ok(photo);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPatch("/DeleteProfilePhoto/{id}")]
        public async Task<ActionResult> DeleteProfilePhoto([FromBody] int id)
        {
            try
            {
                if (id == null)
                    return BadRequest();

                var photo = _repo.getPhotoById(id);
                _repo.deleteProfilePhoto(photo);
                return Ok(photo);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("/DeletePhoto/{id}")]
        public async Task<ActionResult> DeletePhoto([FromBody] int id)
        {
            if (id == null)
                return NotFound();

            var photo = _repo.getPhotoById(id);
            _repo.deletePhoto(photo);
            return Ok();

            
        }
    }

}
