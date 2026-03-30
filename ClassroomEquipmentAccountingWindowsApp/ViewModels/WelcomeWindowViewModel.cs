using ClassroomEquipmentAccountingWindowsApp.Core;
using ClassroomEquipmentAccountingWindowsApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace ClassroomEquipmentAccountingWindowsApp.ViewModels
{
    public class WelcomeWindowViewModel : ViewModelBase
    {
        public WelcomeWindowViewModel()
        {
            Login = string.Empty;
            Password = string.Empty;
            LoginCommand = new(lc =>
            {
                var sameLoginUser = AppCore.Instance.AppDbContext.Users.FirstOrDefault(u => u.Login == Login);
                if (sameLoginUser != null && PasswordEncoder.Verify(sameLoginUser.PasswordHash, Password))
                {
                    MessageBox.Show("Успешный вход!");
                    AppCore.Instance.CurrentUser = sameLoginUser;
                    new GeneralWindow().ShowDialog();
                }
                else
                {
                    MessageBox.Show($"Пользователь с логином {Login} не найден или указан неверный пароль!");
                }
            });
            RegistrationCommand = new(rc =>
            {
                new RegistrationWindow().ShowDialog();
            });
        }

        public string Login { get; set => Set(ref field, in value, nameof(Login)); }
        public string Password { get; set => Set(ref field, in value, nameof(Password)); }

        public RelayCommand LoginCommand { get; }
        public RelayCommand RegistrationCommand { get; }
    }
}
