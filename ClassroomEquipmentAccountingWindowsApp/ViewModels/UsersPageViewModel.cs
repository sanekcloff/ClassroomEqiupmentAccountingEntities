using ClassroomEquipmentAccountingEntities.Models;
using ClassroomEquipmentAccountingWindowsApp.Core;
using ClassroomEquipmentAccountingWindowsApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace ClassroomEquipmentAccountingWindowsApp.ViewModels
{
    public class UsersPageViewModel : ViewModelBase
    {
        public UsersPageViewModel()
        {
            Users = new ObservableCollection<User>(AppCore.Instance.AppDbContext.Users);
            UsersView = CollectionViewSource.GetDefaultView(Users);

            AddUserCommand = new RelayCommand(_ => OpenUserManagerWindow(null));
            DoubleClickCommand = new RelayCommand(_ => OpenUserManagerWindow(SelectedUser), _ => SelectedUser != null);
            DeleteUserCommand = new RelayCommand(_ => DeleteUser(), _ => SelectedUser != null);

            SortOptions = new List<string> { "Логин", "Фамилия", "Имя", "Тэг" };
            TagFilterOptions = new List<string> { "Все", "Не выбрано", "Зав. Аудиторией", "Администратор" };

            SelectedSort = SortOptions[0];
            SelectedTagFilter = TagFilterOptions[0];
        }

        public ObservableCollection<User> Users { get; set => Set(ref field, in value, nameof(Users)); }
        public ICollectionView UsersView { get; }

        public User SelectedUser { get; set => Set(ref field, in value, nameof(SelectedUser)); }

        public RelayCommand AddUserCommand { get; }
        public RelayCommand DoubleClickCommand { get; }
        public RelayCommand DeleteUserCommand { get; }

        public List<string> SortOptions { get; }
        public List<string> TagFilterOptions { get; }

        public string SearchText
        {
            get => field;
            set
            {
                if (Set(ref field, in value, nameof(SearchText)))
                    ApplyFilter();
            }
        }

        public string SelectedSort
        {
            get => field;
            set
            {
                if (Set(ref field, in value, nameof(SelectedSort)))
                    ApplySort();
            }
        }

        public string SelectedTagFilter
        {
            get => field;
            set
            {
                if (Set(ref field, in value, nameof(SelectedTagFilter)))
                    ApplyFilter();
            }
        }

        private void OpenUserManagerWindow(User? userToEdit)
        {
            new UserMangerWindow(userToEdit).ShowDialog();
            RefreshUsers();
        }

        private void RefreshUsers()
        {
            Users.Clear();
            foreach (var user in AppCore.Instance.AppDbContext.Users)
                Users.Add(user);
            UsersView.Refresh();
        }

        private void ApplyFilter()
        {
            UsersView.Filter = obj =>
            {
                if (obj is not User user) return false;
                bool matchesSearch = string.IsNullOrWhiteSpace(SearchText)
                    || (user.Login?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false)
                    || (user.FirstName?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false)
                    || (user.LastName?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false)
                    || (user.MiddleName?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false);

                bool matchesTag = SelectedTagFilter == "Все"
                    || (SelectedTagFilter == "Не выбрано" && user.Tag == Tag.None)
                    || (SelectedTagFilter == "Зав. Аудиторией" && user.Tag == Tag.Manager)
                    || (SelectedTagFilter == "Администратор" && user.Tag == Tag.Admin);

                return matchesSearch && matchesTag;
            };
            UsersView.Refresh();
        }

        private void ApplySort()
        {
            UsersView.SortDescriptions.Clear();
            if (SelectedSort == "Логин")
                UsersView.SortDescriptions.Add(new SortDescription(nameof(User.Login), ListSortDirection.Ascending));
            else if (SelectedSort == "Фамилия")
                UsersView.SortDescriptions.Add(new SortDescription(nameof(User.LastName), ListSortDirection.Ascending));
            else if (SelectedSort == "Имя")
                UsersView.SortDescriptions.Add(new SortDescription(nameof(User.FirstName), ListSortDirection.Ascending));
            else if (SelectedSort == "Тэг")
                UsersView.SortDescriptions.Add(new SortDescription(nameof(User.Tag), ListSortDirection.Ascending));
        }

        private void DeleteUser()
        {
            if (SelectedUser == null) return;

            var result = MessageBox.Show(
                $"Вы действительно хотите удалить пользователя \"{SelectedUser.Login}\"?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    AppCore.Instance.AppDbContext.Users.Remove(SelectedUser);
                    AppCore.Instance.AppDbContext.SaveChanges();
                    Users.Remove(SelectedUser);
                    SelectedUser = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
