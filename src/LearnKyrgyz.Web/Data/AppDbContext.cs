using Microsoft.EntityFrameworkCore;

namespace LearnKyrgyz.Web.Data;
public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}
