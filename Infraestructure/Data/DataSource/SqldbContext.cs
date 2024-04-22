using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookLendApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookLendApi.Infraestructure.Data.DataSource
{
    public class SqldbContext: DbContext
    {
        public SqldbContext(DbContextOptions<SqldbContext> options) : base(options) { }
        public DbSet<Books> Book { get; set; }
        public DbSet<Loans> Loan { get; set; }
        public DbSet<LoansDetails> LoansDetails { get; set; }
        public DbSet<Users> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             base.OnModelCreating(modelBuilder);
        }
    }
}