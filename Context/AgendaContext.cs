using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _ProjetoMVC_FInal.Models;
using Microsoft.EntityFrameworkCore;

namespace _ProjetoMVC_FInal.Context
{
    public class AgendaContext : DbContext
    {
        public AgendaContext(DbContextOptions<AgendaContext> options) : base(options)
        {

        }

        public DbSet<Contato> Contatos { get; set; }

    }
}