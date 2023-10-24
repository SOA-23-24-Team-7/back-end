namespace Explorer.API.Controllers
{

    [Route("api/blog")]
    public class BlogController : BaseApiController
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService authenticationService)
        {
            _blogService = authenticationService;
        }


        [Authorize(Policy = "userPolicy")]
        [HttpPost("create")]
        public ActionResult<BlogResponseDto> Create([FromBody] BlogResponseDto blog)
        {
            var result = _blogService.Create(blog);
            return CreateResponse(result);
        }

        [HttpGet]
        public ActionResult<PagedResult<BlogResponseDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _blogService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<BlogResponseDto> Get(int id)
        {
            var result = _blogService.Get(id);
            return CreateResponse(result);
        }

    }
}
