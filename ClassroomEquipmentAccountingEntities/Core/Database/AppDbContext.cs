using ClassroomEquipmentAccountingEntities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassroomEquipmentAccountingEntities.Core.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
            Database.EnsureCreated();
        }
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

            // Seed data (по миграциям). Обеспечено минимум по 5 записей в каждой таблице.
            var seedDate = new DateTime(2000, 12, 3);

            // Users (5)
            modelBuilder.Entity<User>()
                .HasData(new User("login", "500:Nj5sBszKuiPv9Syy3eV4sg==:D0Z5twb40MEuBiqfdHZbRKVWgZwK4PAeR3XB/VCAjlM=", "Виталий", "Быстров", "Петрович", Permission.Administrator, Tag.Admin) { Id = 1, RowCreatedAt = new DateTime(2000, 12, 3), RowUpdateAt = new DateTime(2000, 12, 3) });

            // Categories (5)
            modelBuilder.Entity<Category>().HasData(
                new
                {
                    Id = 1,
                    Title = "Компьютеры",
                    Description = "Компьютерное оборудование",
                    IsHidden = false,
                    RowCreatedAt = seedDate,
                    RowUpdateAt = seedDate
                },
                new
                {
                    Id = 2,
                    Title = "Принтеры",
                    Description = "Печатающие устройства",
                    IsHidden = false,
                    RowCreatedAt = seedDate,
                    RowUpdateAt = seedDate
                },
                new
                {
                    Id = 3,
                    Title = "Проекторы",
                    Description = "Проекционное оборудование",
                    IsHidden = false,
                    RowCreatedAt = seedDate,
                    RowUpdateAt = seedDate
                },
                new
                {
                    Id = 4,
                    Title = "Сеть",
                    Description = "Сетевое оборудование",
                    IsHidden = false,
                    RowCreatedAt = seedDate,
                    RowUpdateAt = seedDate
                },
                new
                {
                    Id = 5,
                    Title = "Мебель",
                    Description = "Кабинетная мебель",
                    IsHidden = false,
                    RowCreatedAt = seedDate,
                    RowUpdateAt = seedDate
                }
            );

            // Classrooms (5) — ссылки на менеджеров (Users.Id)
            modelBuilder.Entity<Classroom>().HasData(
                new
                {
                    Id = 1,
                    Number = "101",
                    Specialization = "Компьютерный кабинет",
                    ManagerId = 1,
                    IsHidden = false,
                    RowCreatedAt = seedDate,
                    RowUpdateAt = seedDate
                },
                new
                {
                    Id = 2,
                    Number = "102",
                    Specialization = "Лаборатория сетей",
                    ManagerId = 1,
                    IsHidden = false,
                    RowCreatedAt = seedDate,
                    RowUpdateAt = seedDate
                },
                new
                {
                    Id = 3,
                    Number = "201",
                    Specialization = "Кабинет программирования",
                    ManagerId = 1,
                    IsHidden = false,
                    RowCreatedAt = seedDate,
                    RowUpdateAt = seedDate
                },
                new
                {
                    Id = 4,
                    Number = "202",
                    Specialization = "Мультимедийный кабинет",
                    ManagerId = 1,
                    IsHidden = false,
                    RowCreatedAt = seedDate,
                    RowUpdateAt = seedDate
                },
                new
                {
                    Id = 5,
                    Number = "301",
                    Specialization = "Резервный кабинет",
                    ManagerId = 1,
                    IsHidden = false,
                    RowCreatedAt = seedDate,
                    RowUpdateAt = seedDate
                }
            );

            // Equipments (5) — ссылки на CategoryId и ClassroomId
            modelBuilder.Entity<Equipment>().HasData(
                new
                {
                    Id = 1,
                    SerialNumber = "SN-CPU-0001",
                    InventoryNumber = "INV-0001",
                    CategoryId = 1,
                    ClassroomId = 1,
                    Status = Status.Expoitation,
                    Model = "Dell OptiPlex 3080",
                    CommissioningDate = new DateTime(2020, 1, 15),
                    WaranityEndDate = new DateTime(2023, 1, 15),
                    IsHidden = false,
                    RowCreatedAt = seedDate,
                    RowUpdateAt = seedDate
                },
                new
                {
                    Id = 2,
                    SerialNumber = "SN-PRT-0002",
                    InventoryNumber = "INV-0002",
                    CategoryId = 2,
                    ClassroomId = 1,
                    Status = Status.Expoitation,
                    Model = "HP LaserJet Pro",
                    CommissioningDate = new DateTime(2019, 6, 1),
                    WaranityEndDate = new DateTime(2022, 6, 1),
                    IsHidden = false,
                    RowCreatedAt = seedDate,
                    RowUpdateAt = seedDate
                },
                new
                {
                    Id = 3,
                    SerialNumber = "SN-PJT-0003",
                    InventoryNumber = "INV-0003",
                    CategoryId = 3,
                    ClassroomId = 4,
                    Status = Status.Expoitation,
                    Model = "Epson EB-X05",
                    CommissioningDate = new DateTime(2021, 9, 10),
                    WaranityEndDate = new DateTime(2024, 9, 10),
                    IsHidden = false,
                    RowCreatedAt = seedDate,
                    RowUpdateAt = seedDate
                },
                new
                {
                    Id = 4,
                    SerialNumber = "SN-NET-0004",
                    InventoryNumber = "INV-0004",
                    CategoryId = 4,
                    ClassroomId = 2,
                    Status = Status.Expoitation,
                    Model = "Cisco Switch 2960",
                    CommissioningDate = new DateTime(2018, 3, 5),
                    WaranityEndDate = new DateTime(2021, 3, 5),
                    IsHidden = false,
                    RowCreatedAt = seedDate,
                    RowUpdateAt = seedDate
                },
                new
                {
                    Id = 5,
                    SerialNumber = "SN-FUR-0005",
                    InventoryNumber = "INV-0005",
                    CategoryId = 5,
                    ClassroomId = 5,
                    Status = Status.Expoitation,
                    Model = "Учебный стол Model-A",
                    CommissioningDate = new DateTime(2017, 11, 20),
                    WaranityEndDate = new DateTime(2020, 11, 20),
                    IsHidden = false,
                    RowCreatedAt = seedDate,
                    RowUpdateAt = seedDate
                }
            );

            // RepairRequests (5)
            modelBuilder.Entity<RepairRequest>().HasData(
                new
                {
                    Id = 1,
                    StartDate = new DateTime(2026, 5, 1),
                    EndDate = (DateTime?)null,
                    Description = "Не включается компьютер",
                    IsHidden = false,
                    RowCreatedAt = seedDate,
                    RowUpdateAt = seedDate
                },
                new
                {
                    Id = 2,
                    StartDate = new DateTime(2026, 4, 10),
                    EndDate = new DateTime(2026, 4, 15),
                    Description = "Заменить картридж принтера",
                    IsHidden = false,
                    RowCreatedAt = seedDate,
                    RowUpdateAt = seedDate
                },
                new
                {
                    Id = 3,
                    StartDate = new DateTime(2026, 3, 20),
                    EndDate = (DateTime?)null,
                    Description = "Проектор не переключает вход",
                    IsHidden = false,
                    RowCreatedAt = seedDate,
                    RowUpdateAt = seedDate
                },
                new
                {
                    Id = 4,
                    StartDate = new DateTime(2026, 2, 5),
                    EndDate = new DateTime(2026, 2, 7),
                    Description = "Проблемы в сети, пакетные потери",
                    IsHidden = false,
                    RowCreatedAt = seedDate,
                    RowUpdateAt = seedDate
                },
                new
                {
                    Id = 5,
                    StartDate = new DateTime(2026, 1, 12),
                    EndDate = (DateTime?)null,
                    Description = "Поломка стола",
                    IsHidden = false,
                    RowCreatedAt = seedDate,
                    RowUpdateAt = seedDate
                }
            );

            // RepairRequestEquipment (5) — привязки RepairRequest -> Equipment
            modelBuilder.Entity<RepairRequestEquipment>().HasData(
                new
                {
                    Id = 1,
                    RepairRequestId = 1,
                    EquipmentId = 1,
                    IsHidden = false,
                    RowCreatedAt = seedDate,
                    RowUpdateAt = seedDate
                },
                new
                {
                    Id = 2,
                    RepairRequestId = 2,
                    EquipmentId = 2,
                    IsHidden = false,
                    RowCreatedAt = seedDate,
                    RowUpdateAt = seedDate
                },
                new
                {
                    Id = 3,
                    RepairRequestId = 3,
                    EquipmentId = 3,
                    IsHidden = false,
                    RowCreatedAt = seedDate,
                    RowUpdateAt = seedDate
                },
                new
                {
                    Id = 4,
                    RepairRequestId = 4,
                    EquipmentId = 4,
                    IsHidden = false,
                    RowCreatedAt = seedDate,
                    RowUpdateAt = seedDate
                },
                new
                {
                    Id = 5,
                    RepairRequestId = 5,
                    EquipmentId = 5,
                    IsHidden = false,
                    RowCreatedAt = seedDate,
                    RowUpdateAt = seedDate
                }
            );
        }
    }
}
