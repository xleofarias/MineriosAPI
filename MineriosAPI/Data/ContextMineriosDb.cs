using Microsoft.EntityFrameworkCore;
using MineriosAPI.Models;
using System.Data;

namespace MineriosAPI.Data
{
    public class ContextMineriosDb : DbContext
    {
        public ContextMineriosDb(DbContextOptions<ContextMineriosDb> options) : base(options) { }

        public DbSet<MineriosModel> Minerios { get; set; }
    }
}
