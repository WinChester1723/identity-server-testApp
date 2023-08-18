
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MvcClient.Identity;

namespace MvcClient.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        
    }
}