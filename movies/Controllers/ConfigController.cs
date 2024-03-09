using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace movies.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ConfigController : ControllerBase
	{
		private readonly IConfiguration _configuration;
		private readonly IOptions<AttachmentOptions> _options;

		public ConfigController(IConfiguration configuration,IOptions<AttachmentOptions> options)
        {
			_configuration = configuration;
			_options = options;
			var value = options.Value;
		}
		[HttpGet] 
		public IActionResult Get() 
		{
			var config = new
			{
				AllowedHosts = _configuration["AllowedHosts"],
				Default = _configuration["Logging:LogLevel:Default"],
				SignInfo = _configuration["SignInfo"],
				AttachmentOptions = _options.Value
			};
			return Ok(config);
		}
    }
}
