using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WhatsMyIp.Controllers
{
    public class IpDetails
    {
    public string Ip {get; set;}
    public string Hostname {get; set;}
    public string City {get; set;}
    public string Region {get; set;}
    public string Country {get; set;}
    public string Loc {get; set;}
    public string Org {get; set;}
    public string Timezone {get; set;}
    public string Readme {get; set;}
}
    
    [ApiController]
    [Route("ip")]
    public class WhatsMyIpController : ControllerBase
    {
        private readonly ILogger<WhatsMyIpController> _logger;

        public const string IpUri = "https://ipinfo.io/";
        
        public WhatsMyIpController(ILogger<WhatsMyIpController> logger)
        {
            _logger = logger;
        }

        
        [HttpGet]
        public async Task<IpDetails?> Get()
        {

            try
            {
                using var httpclient = new HttpClient();
                var ipDetails = await httpclient.GetFromJsonAsync<IpDetails>(IpUri);
                _logger.LogInformation(JsonSerializer.Serialize(ipDetails));
                return ipDetails;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                throw;
            }
        }
        
        [HttpGet("test")]
        public string Getasd()
        {

            try
            {
                return "Hello world 2";
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                throw;
            }
        }
    }
}