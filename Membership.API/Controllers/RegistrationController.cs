using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Membership.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Membership.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly ILogger<RegistrationController> _logger;
        private readonly MembershipRepository _repository;

        public RegistrationController(ILogger<RegistrationController> logger,
            MembershipRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }


    }
}