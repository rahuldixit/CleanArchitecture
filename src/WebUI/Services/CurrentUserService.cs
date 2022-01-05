using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CaWorkshop.Application.Infrastructure.Identity;

namespace CaWorkshop.WebUI.Services
{
    public class CurrentUserService : ICurrentUserService

    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? UserId => _httpContextAccessor.HttpContext?
                                .User?
                                .FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
