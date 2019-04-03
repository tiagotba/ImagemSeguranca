using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApiImagemSegurança.Models;

namespace WebApiImagemSegurança.Context
{
    public class BD_Context : DbContext
    {
        public BD_Context() : base("name=ImagemSegurancaDBConnectionString")
        { }

        public DbSet<Portao> Portoes { get; set; }
        public DbSet<Camera> Cameras { get; set; }
        public DbSet<EventosDispositivo> EventosDispositivos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove();

            modelBuilder.Entity<Portao>().HasKey<int>(p => p.idPortao);
            modelBuilder.Entity<Portao>().ToTable("TB_PORTAO");

            modelBuilder.Entity<Camera>().HasKey<int>(c => c.idCamera);
            modelBuilder.Entity<Camera>().ToTable("TB_CAMERA");

            modelBuilder.Entity<EventosDispositivo>().ToTable("TB_EVENTOS_DISPOSITIVOS");

            modelBuilder.Entity<EventosDispositivo>()
            .HasRequired(p => p.Portao)
            .WithMany(g => g.eventos)
            .HasForeignKey(s => s.idPortao);

            modelBuilder.Entity<EventosDispositivo>()
           .HasRequired(c => c.Camera)
           .WithMany(g => g.eventos)
           .HasForeignKey(s => s.idCamera);


            base.OnModelCreating(modelBuilder);

        }
    }
}
