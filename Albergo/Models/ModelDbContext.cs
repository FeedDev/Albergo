using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Albergo.Models
{
    public partial class ModelDbContext : DbContext
    {
        public ModelDbContext()
            : base("name=ModelDbContext")
        {
        }

        public virtual DbSet<Camera> Camera { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<PivotPrenotazioneServizio> PivotPrenotazioneServizio { get; set; }
        public virtual DbSet<Prenotazione> Prenotazione { get; set; }
        public virtual DbSet<Servizio> Servizio { get; set; }
        public virtual DbSet<Tipologia> Tipologia { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Camera>()
                .HasMany(e => e.Prenotazione)
                .WithRequired(e => e.Camera)
                .HasForeignKey(e => e.IdCameraFk)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cliente>()
                .HasMany(e => e.Prenotazione)
                .WithRequired(e => e.Cliente)
                .HasForeignKey(e => e.IdClienteFk)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Prenotazione>()
                .Property(e => e.Anno)
                .IsFixedLength();

            modelBuilder.Entity<Prenotazione>()
                .Property(e => e.Caparra)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Prenotazione>()
                .Property(e => e.Tariffa)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Prenotazione>()
                .HasMany(e => e.PivotPrenotazioneServizio)
                .WithRequired(e => e.Prenotazione)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Servizio>()
                .Property(e => e.NomeServizio)
                .IsFixedLength();

            modelBuilder.Entity<Servizio>()
                .Property(e => e.Prezzo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Servizio>()
                .HasMany(e => e.PivotPrenotazioneServizio)
                .WithRequired(e => e.Servizio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tipologia>()
                .HasMany(e => e.Prenotazione)
                .WithRequired(e => e.Tipologia)
                .HasForeignKey(e => e.IdTipologiaFk)
                .WillCascadeOnDelete(false);
        }
    }
}
