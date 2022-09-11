using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ControleDeUsuarios.Models;

namespace ControleDeUsuarios.Data
{
    public class ControleDeUsuariosContext : DbContext
    {
        public ControleDeUsuariosContext (DbContextOptions<ControleDeUsuariosContext> options)
            : base(options)
        {
        }

        public DbSet<ControleDeUsuarios.Models.Usuario> Usuario { get; set; }
    }
}
