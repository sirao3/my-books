using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

namespace my_books.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private PublishersService _publishersService;
        private readonly ILogger<PublishersController> _logger;
        public PublishersController(PublishersService publishersService, ILogger<PublishersController> logger)
        {
            _publishersService = publishersService;
            _logger = logger;
        }

        [HttpGet("get-all-publishers")]
        public IActionResult GetAllPublishers(string sortBy, int pageNumber){
            try
            {
                _logger.LogInformation("This is just a log in GetAllPublishers()");
                var result = _publishersService.GetAllPublishers(sortBy, pageNumber);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Sorry, we could not load the publishers");
            }
            
        }

        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody]PublisherVM publisher)
        {
            try
            {
                var newPublisher= _publishersService.AddPublisher(publisher);
                return Created(nameof(AddPublisher),newPublisher);   
            }
            catch(PublisherNameException ex){
                return BadRequest($"{ex.Message}, Publisher name: {ex.PublisherName}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("get-publisher-by-id/{id}")]
        public IActionResult GetPublisherById(int id){
            //throw new Exception("Handled by middleware");
            
            var _response = _publishersService.GetPublisherData(id);
            if(_response != null){
                return Ok(_response);
            }else{
                return NotFound();
            }
            
        }

        [HttpGet("get-publisher-books-with-authors/{id}")]
        public IActionResult GetPublisherData(int id){
            var _response = _publishersService.GetPublisherData(id);
            return Ok(_response);
        }

        [HttpDelete("delete-publisher-by-id/{id}")]
        public IActionResult DeletePublisherById(int id){
            try
            {
                _publishersService.DeletePublisherById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
