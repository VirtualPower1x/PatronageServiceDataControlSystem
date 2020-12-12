namespace PatronageServiceDataControlSystem
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PatronageServiceModel : DbContext
    {
        public PatronageServiceModel()
            : base("name=PatronageServiceConfig")
        {
        }

        public virtual DbSet<Клиент> Клиент { get; set; }
        public virtual DbSet<Сотрудник> Сотрудник { get; set; }
        public virtual DbSet<Талон_ухода_за_клиентом> Талон_ухода_за_клиентом { get; set; }
        public virtual DbSet<Услуга> Услуга { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Клиент>()
                .HasMany(e => e.Талон_ухода_за_клиентом)
                .WithRequired(e => e.Клиент)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Сотрудник>()
                .Property(e => e.Заработная_плата)
                .HasPrecision(19, 2);

            modelBuilder.Entity<Сотрудник>()
                .HasMany(e => e.Талон_ухода_за_клиентом)
                .WithRequired(e => e.Сотрудник)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Талон_ухода_за_клиентом>()
                .Property(e => e.Сумма_к_оплате)
                .HasPrecision(19, 2);

            modelBuilder.Entity<Услуга>()
                .Property(e => e.Цена_услуги)
                .HasPrecision(19, 2);

            modelBuilder.Entity<Услуга>()
                .HasMany(e => e.Талон_ухода_за_клиентом)
                .WithRequired(e => e.Услуга)
                .WillCascadeOnDelete(false);
        }
    }
}
