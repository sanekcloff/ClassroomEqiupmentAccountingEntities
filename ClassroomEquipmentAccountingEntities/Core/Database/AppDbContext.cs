using ClassroomEquipmentAccountingEntities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassroomEquipmentAccountingEntities.Core.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<RepairRequest> RepairRequests { get; set; }
        public DbSet<RepairRequestEquipment> RepairRequestEquipment { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CEA_Db;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Classroom>()
                .HasOne(c => c.Manager)
                .WithMany(u => u.Classrooms)
                .HasForeignKey(c => c.ManagerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasData(new User("login", "500:Nj5sBszKuiPv9Syy3eV4sg==:D0Z5twb40MEuBiqfdHZbRKVWgZwK4PAeR3XB/VCAjlM=", "Виталий", "Папич", "Петрович", Permission.Administrator, Tag.Admin) { Id = 1, RowCreatedAt = new DateTime(2000,12,3), RowUpdateAt = new DateTime(2000, 12, 3) });
        }
    }
}
