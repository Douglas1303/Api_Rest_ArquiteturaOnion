using Infra.CrossCutting.AppSettings;
using Microsoft.EntityFrameworkCore;
using Poc.Domain.Entities;

namespace Infra.Data.Context
{
    public class DevEventsDbContext : DbContext
    {
        public DevEventsDbContext(DbContextOptions<DevEventsDbContext> options) : base(options)
        {
        }

        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<EventModel> Events { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<SubscriptionModel> Subscription { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DevEventsDbContext).Assembly); 

            modelBuilder.Entity<EventModel>()
                .Property(e => e.Titulo)
                .IsRequired()
                    .HasMaxLength(300);

            modelBuilder.Entity<EventModel>()
                .Property(e => e.Descricao)
                  .IsRequired()
                    .HasMaxLength(300);

            modelBuilder.Entity<CategoryModel>()
                .Property(e => e.Descricao)
                    .HasMaxLength(300);

            modelBuilder.Entity<EventModel>()
               .HasOne(e => e.Categoria)
               .WithMany()
               .HasForeignKey(e => e.CategoriaId)
                .IsRequired(false);

            modelBuilder.Entity<EventModel>()
                .HasMany(e => e.Usuarios);


            modelBuilder.Entity<SubscriptionModel>()
               .HasKey(i => i.Id);

            modelBuilder.Entity<SubscriptionModel>()
                .HasOne(i => i.Evento)
                .WithMany(e => e.Inscricoes)
                .HasForeignKey(i => i.EventoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SubscriptionModel>()
                .HasOne(i => i.Usuario)
                .WithMany(e => e.Inscricoes)
                .HasForeignKey(i => i.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            //N : N 
            //modelBuilder.Entity<EventUserModel>()
            //    .HasKey(x => new { x.EventoId, x.UsuarioId}); 
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(GetAppSetting.GetConnection(DefaultKeys.DevEvents_Domain())); 
        //}
    }
}