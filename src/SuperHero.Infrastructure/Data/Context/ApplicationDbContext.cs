using Microsoft.EntityFrameworkCore;

namespace SuperHero.Infrastructure.Data.Context;

public class ApplicationDbContext : BaseApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
}