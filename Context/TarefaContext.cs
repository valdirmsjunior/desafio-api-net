using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using desafio_api_net.Models;
using Microsoft.EntityFrameworkCore;

namespace desafio_api_net.Context
{
    public class TarefaContext : DbContext
    {
        public TarefaContext(DbContextOptions<TarefaContext> options) : base(options)
        {

        }

        public DbSet<Tarefa> Tarefas { get; set; }
    }
}

