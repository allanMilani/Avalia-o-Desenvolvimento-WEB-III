using Aula_03.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aula_03.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Editora> TBEditora { get; set; }
        public DbSet<ClassificacaoLivro> TBClassificacaoLivro { get; set; }
        public DbSet<Livro> TBLivro { get; set; }
    }
}
