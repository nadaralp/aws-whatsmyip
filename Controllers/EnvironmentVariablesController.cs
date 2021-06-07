using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WhatsMyIp.Controllers
{
    public class EnvVariables
    {
        public string NameFromEnv {get; set;}
        public string NameFromConfiguration {get; set;}

        public EnvVariables(string nameFromEnv, string nameFromConfiguration)
        {
            NameFromEnv = nameFromEnv;
            NameFromConfiguration = nameFromConfiguration;
        }
    }
    
    [ApiController]
    [Route("env")]
    public class EnvironmentVariablesController : ControllerBase
    {
        private readonly ILogger<EnvironmentVariablesController> _logger;
        private readonly IConfiguration _configuration;

        public EnvironmentVariablesController(ILogger<EnvironmentVariablesController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        
        [HttpGet]
        public EnvVariables Get()
        {

            try
            {
                var envModel = new EnvVariables(
                    Environment.GetEnvironmentVariable("Name") ?? "Didnt find",
                    _configuration.GetValue<string>("Name") ?? "Didnt find"
                );
                return envModel;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                throw;
            }
        }
    }
}