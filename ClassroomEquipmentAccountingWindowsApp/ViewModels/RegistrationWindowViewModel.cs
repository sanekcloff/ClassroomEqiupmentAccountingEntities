using ClassroomEquipmentAccountingEntities.Core;
using ClassroomEquipmentAccountingEntities.Models;
using ClassroomEquipmentAccountingWindowsApp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassroomEquipmentAccountingWindowsApp.ViewModels
{
    public class RegistrationWindowViewModel : ViewModelBase
    {
        public RegistrationWindowViewModel()
        {
            Login = string.Empty;
            Password = string.Empty;
            Firstname = string.Empty;
            Lastname = string.Empty;
            Middlename = string.Empty;

            RegisterCommand = new(rc =>
            {
                var newUser = new User(Login,PasswordEncoder.Hash(Password),Firstname,Lastname,Middlename);

                var isExist = AppCore.Instance.AppDbContext.Users.Any(u => u.Equals(newUser));

                if (!isExist)
                {
                    AppCore.Instance.AppDbContext.Users.Add(newUser);
                    AppCore.Instance.AppDbContext.SaveChanges();
                }
            });
        }
        public string Login { get; set => Set(ref field, in value, nameof(Login)); }
        public string Password { get; set => Set(ref field, in value, nameof(Password)); }
        public string Firstname { get; set => Set(ref field, in value, nameof(Firstname)); }
        public string Lastname { get; set => Set(ref field, in value, nameof(Lastname)); }
        public string Middlename { get; set => Set(ref field, in value, nameof(Middlename)); }

        public RelayCommand RegisterCommand { get; }
    }
}
