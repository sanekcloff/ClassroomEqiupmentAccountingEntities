using ClassroomEquipmentAccountingEntities.Models.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassroomEquipmentAccountingEntities.Models
{
    public class User : BaseModel
    {
        public User()
        {
            Login = string.Empty;
            PasswordHash = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            MiddleName = string.Empty;
            Permissions = Permission.None;
            Tag = Tag.None;
            Classrooms = new HashSet<Classroom>();
        }
        public User(string login, string passwordHash, string firstName, string lastName, string middleName, Permission permissions = Permission.None, Tag tag = Tag.None)
        {
            Login = login;
            PasswordHash = passwordHash;
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            Permissions = permissions;
            Tag = tag;
            Classrooms = new HashSet<Classroom>();
        }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public Permission Permissions { get; set; }
        public Tag Tag { get; set; }

        public ICollection<Classroom> Classrooms { get; set; }

        public string FullName => $"{LastName} {FirstName} {MiddleName}";
        public string ShortName => $"{LastName} {FirstName[0]}. {MiddleName[0]}.";
        public string TagAsText => Tag switch
        {
            Tag.None => "Отсутвует",
            Tag.Manager => "Зав. Кабинетом",
            Tag.Admin => "Администратор",
            _ => "Неопознаный"
        };

        public override int GetHashCode() => HashCode.Combine(Login);
        public override bool Equals(object? obj) => obj == null || !(obj is User) ? false : GetHashCode() == obj.GetHashCode();
        public override string ToString() => $"[{Id}][{FullName}] - {Login}:{Permissions} (IsHidden = {IsHidden})";
    }
}
