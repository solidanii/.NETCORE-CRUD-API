using crud_asp_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace crud_asp_api.DAL
{
    public class BranchDbContext : DbContext
    {
        public BranchDbContext(DbContextOptions options) : base(options)
        {
        }
        //add branch property to communicate with database
        public DbSet<branch> Branches { get; set; }
    }

}
