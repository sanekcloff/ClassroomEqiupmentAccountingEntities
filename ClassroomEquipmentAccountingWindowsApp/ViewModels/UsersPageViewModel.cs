using ClassroomEquipmentAccountingEntities.Models;
using ClassroomEquipmentAccountingWindowsApp.Core;
using ClassroomEquipmentAccountingWindowsApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ClassroomEquipmentAccountingWindowsApp.ViewModels
{
    public class UsersPageViewModel : ViewModelBase
    {
        public UsersPageViewModel()
        {
            Users = new ObservableCollection<User>(AppCore.Instance.AppDbContext.Users);
            ManageUserCommand = new RelayCommand(action =>
            {
                new UserMangerWindow(SelectedUser).ShowDialog();
            });
        }

        public User SelectedUser { get; set => Set(ref field, in value, nameof(SelectedUser)); }
        public ObservableCollection<User> Users { get; set=>Set(ref field, in value, nameof(Users)); }

        public RelayCommand ManageUserCommand { get; }

    }
}
