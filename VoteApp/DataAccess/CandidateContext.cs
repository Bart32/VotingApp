using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoteApp.Models;

namespace VoteApp.DataAccess
{
    class CandidateContext : DbContext
    {
        public CandidateContext() { }

        public DbSet<Candidate> Candidate { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=localhost;Database=VotingApp;Trusted_Connection=True;MultipleActiveResultsets=True");
        }

    }
}
