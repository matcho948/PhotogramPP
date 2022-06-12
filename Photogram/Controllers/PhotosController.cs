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
        public async Task<ActionResult> SetProfilePhoto(int id)
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
        public async Task<ActionResult> DeleteProfilePhoto(int id)
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
        public async Task<ActionResult> DeletePhoto(int id)
        {
            if (id == null)
                return NotFound();
            var photo = _repo.getPhotoToDeleteById(id);
            _repo.deletePhoto(photo);
            return Ok();


        }
        [HttpGet("/GetProfilePhoto/{id}")]
        public async Task<ActionResult> GetProfilePhoto(int id)
        {
            if (id == null)
                return BadRequest();
            else
            {
                var user = _repo.GetUserWithPhotosById(id);
                foreach(Photos photo in user.Photos)
                {
                    if(photo.IsMainPhoto == true)
                        return Ok(photo.PhotoUrl);

                }
                return NotFound();
            }
        }
        [HttpGet("/GetRecomendedPhotos")]
        public async Task<ActionResult<List<Photos>>> getRecomendedPhotos()
        {
            var idList = new List<int>();
            var numberOfPhotos = _repo.getNumberOfPhotos();
            Random random = new Random();
            var photos = new List<Photos>();
            if (numberOfPhotos < 20)
                photos = _repo.GetAllPhotos().ToList();
            else
            {
                while (photos.Count < 20)
                {
                    var id = random.Next(numberOfPhotos);
                    if (!idList.Contains(id))
                    {
                        var photo = _repo.getPhotoById(id);
                        if (photo != null)
                            photos.Add(photo);
                    }
                    idList.Add(id);
                }
            }
            return Ok(photos);
        }
    }

}
