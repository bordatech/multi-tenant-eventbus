using System;
using System.Collections.Generic;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.Api.Tenancy;

namespace Sample.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ICapPublisher _capPublisher;
        private readonly ITenantService _tenantService;
        private readonly ILogger _logger;

        public TestController(ICapPublisher capPublisher, ITenantService tenantService, ILogger<TestController> logger)
        {
            _capPublisher = capPublisher;
            _tenantService = tenantService;
            _logger = logger;
        }

        [HttpPost("publish")]
        public IActionResult Publish()
        {
            string message = "sample message";
            Guid tenantId = Guid.NewGuid();
            
            _capPublisher.Publish("sample.message", message, new Dictionary<string, string>()
            {
                {nameof(ITenantService.TenantId), tenantId.ToString()}
            });
            
            _logger.LogInformation($"Message published: {message}");
            _logger.LogInformation($"TenantId: {tenantId}");
            
            return Ok("Message published");
        }

        [CapSubscribe("sample.message")]
        public void Subscribe(string body)
        {
            _logger.LogInformation($"Message arrived: {body}");
            _logger.LogInformation($"TenantId: {_tenantService.TenantId}");
        }
    }
}