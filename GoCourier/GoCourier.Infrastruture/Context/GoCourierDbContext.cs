using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GoCourier.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoCourier.infrastructure.Context
{
    public class GoCourierContext : DbContext
    {
        public GoCourierContext(DbContextOptions<GoCourierContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Envio> Envios { get; set; }

        public DbSet<Notificacion> Notificaciones { get; set; }


    }

}