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
        [HttpPost("/AddNewPhoto")]
        public async Task <ActionResult> AddNewPhoto()
        {
            
        }
    }
}
