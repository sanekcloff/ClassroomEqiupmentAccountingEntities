using ClassroomEquipmentAccountingEntities.Models;
using ClassroomEquipmentAccountingWindowsApp.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ClassroomEquipmentAccountingWindowsApp.ViewModels
{
    public class UserManagerWindowViewModel : ViewModelBase
    {
        private bool _isAddOperation = false;
        public UserManagerWindowViewModel()
        {
            UserToEdit = new User();
            _isAddOperation = true;
            UserSaveCommand = new RelayCommand(_ => SaveUser());
            InitializeData();
        }
        public UserManagerWindowViewModel(User? userToEdit)
        {
            if (userToEdit == null)
            {
                UserToEdit = new User();
                _isAddOperation = true;
            }
            else
            {
                UserToEdit = userToEdit!;
                _isAddOperation = false;
            }
            UserSaveCommand = new RelayCommand(_ => SaveUser());
            InitializeData();
        }
        public User UserToEdit { get => field; set => Set(ref field, in value, nameof(UserToEdit)); }
        #region Main Fields
        public string Login { get => field; set => Set(ref field, in value, nameof(Login)); }
        public string Password { get => field; set => Set(ref field, in value, nameof(Password)); }
        public string Firstname { get => field; set => Set(ref field, in value, nameof(Firstname)); }
        public string Lastname { get => field; set => Set(ref field, in value, nameof(Lastname)); }
        public string Middlename { get => field; set => Set(ref field, in value, nameof(Middlename)); }
        #endregion
        #region Tags
        public string SelectedTag { get => field; set => Set(ref field, in value, nameof(SelectedTag)); }

        public List<string> PermissionTags { get; } = new List<string>() { "Не выбрано", "Зав. Аудиторией", "Администратор" };
        #endregion
        #region Permission

        #region Classrooms
        public bool ClassroomsViewing { get => field; set => Set(ref field, in value, nameof(ClassroomsViewing)); }
        public bool ClassroomsAdd { get => field; set => Set(ref field, in value, nameof(ClassroomsAdd)); }
        public bool ClassroomsEdit { get => field; set => Set(ref field, in value, nameof(ClassroomsEdit)); }
        public bool ClassroomsDelete { get => field; set => Set(ref field, in value, nameof(ClassroomsDelete)); }
        #endregion

        #region Users
        public bool UsersViewing { get => field; set => Set(ref field, in value, nameof(UsersViewing)); }
        public bool UsersAdd { get => field; set => Set(ref field, in value, nameof(UsersAdd)); }
        public bool UsersEdit { get => field; set => Set(ref field, in value, nameof(UsersEdit)); }
        public bool UsersDelete { get => field; set => Set(ref field, in value, nameof(UsersDelete)); }
        #endregion

        #region Equipments
        public bool EquipmentsViewing { get => field; set => Set(ref field, in value, nameof(EquipmentsViewing)); }
        public bool EquipmentsAdd { get => field; set => Set(ref field, in value, nameof(EquipmentsAdd)); }
        public bool EquipmentsEdit { get => field; set => Set(ref field, in value, nameof(EquipmentsEdit)); }
        public bool EquipmentsDelete { get => field; set => Set(ref field, in value, nameof(EquipmentsDelete)); }
        #endregion

        #region Categories
        public bool CategoriesViewing { get => field; set => Set(ref field, in value, nameof(CategoriesViewing)); }
        public bool CategoriesAdd { get => field; set => Set(ref field, in value, nameof(CategoriesAdd)); }
        public bool CategoriesEdit { get => field; set => Set(ref field, in value, nameof(CategoriesEdit)); }
        public bool CategoriesDelete { get => field; set => Set(ref field, in value, nameof(CategoriesDelete)); }
        #endregion

        #region RepairRequest
        public bool RepairRequestViewing { get => field; set => Set(ref field, in value, nameof(RepairRequestViewing)); }
        public bool RepairRequestAdd { get => field; set => Set(ref field, in value, nameof(RepairRequestAdd)); }
        public bool RepairRequestEdit { get => field; set => Set(ref field, in value, nameof(RepairRequestEdit)); }
        public bool RepairRequestDelete { get => field; set => Set(ref field, in value, nameof(RepairRequestDelete)); }
        public bool RepairRequestInventoryCreation { get => field; set => Set(ref field, in value, nameof(RepairRequestInventoryCreation)); }
        #endregion

        #endregion
        public RelayCommand UserSaveCommand { get; }

        private User ConfigurateUser()
        {
            var user = UserToEdit ?? new User();

            user.Login = Login?.Trim() ?? string.Empty;

            if (!string.IsNullOrEmpty(Password))
            {
                user.PasswordHash = PasswordEncoder.Hash(Password);
            }

            user.FirstName = Firstname?.Trim() ?? string.Empty;
            user.LastName = Lastname?.Trim() ?? string.Empty;
            user.MiddleName = Middlename?.Trim() ?? string.Empty;

            user.Tag = SelectedTag == PermissionTags[1] ? Tag.Manager :
                       SelectedTag == PermissionTags[2] ? Tag.Admin : Tag.None;

            Permission permissions = 0;

            if (ClassroomsViewing) permissions |= Permission.ViewingClassrooms;
            if (ClassroomsAdd) permissions |= Permission.AddClassrooms;
            if (ClassroomsEdit) permissions |= Permission.UpdateClassrooms;
            if (ClassroomsDelete) permissions |= Permission.RemoveClassrooms;

            if (UsersViewing) permissions |= Permission.ViewingUsers;
            if (UsersAdd) permissions |= Permission.AddUsers;
            if (UsersEdit) permissions |= Permission.UpdateUsers;
            if (UsersDelete) permissions |= Permission.RemoveUsers;

            if (EquipmentsViewing) permissions |= Permission.ViewingEquipments;
            if (EquipmentsAdd) permissions |= Permission.AddEquipments;
            if (EquipmentsEdit) permissions |= Permission.UpdateEquipments;
            if (EquipmentsDelete) permissions |= Permission.RemoveEquipments;

            if (CategoriesViewing) permissions |= Permission.ViewingCategories;
            if (CategoriesAdd) permissions |= Permission.AddCategories;
            if (CategoriesEdit) permissions |= Permission.UpdateCategories;
            if (CategoriesDelete) permissions |= Permission.RemoveCategories;

            if (RepairRequestViewing) permissions |= Permission.ViewingRequests;
            if (RepairRequestAdd) permissions |= Permission.AddRequests;
            if (RepairRequestEdit) permissions |= Permission.UpdateRequests;
            if (RepairRequestDelete) permissions |= Permission.RemoveRequests;
            if (RepairRequestInventoryCreation) permissions |= Permission.CreatingInventory;

            user.Permissions = permissions;

            return user;
        }
        private void SaveUser()
        {
            // Валидация логина
            if (string.IsNullOrWhiteSpace(Login))
            {
                MessageBox.Show("Логин не может быть пустым", "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Валидация пароля при добавлении нового пользователя
            if (_isAddOperation && string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Пароль для нового пользователя обязателен", "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var user = ConfigurateUser();

                if (_isAddOperation)
                {
                    AppCore.Instance.AppDbContext.Users.Add(user);
                }
                else
                {
                    AppCore.Instance.AppDbContext.Users.Update(user);
                }

                AppCore.Instance.AppDbContext.SaveChanges();

                // Если мы редактировали текущего пользователя в сессии — обновим ссылку на него
                if (!_isAddOperation && AppCore.Instance.CurrentUser != null && AppCore.Instance.CurrentUser.Id == user.Id)
                {
                    var refreshed = AppCore.Instance.AppDbContext.Users.Find(user.Id);
                    if (refreshed != null)
                        AppCore.Instance.CurrentUser = refreshed;
                }

                // Обновим состояние команд/кнопок в UI
                CommandManager.InvalidateRequerySuggested();

                MessageBox.Show(
                    _isAddOperation ? "Пользователь успешно добавлен" : "Пользователь успешно обновлен",
                    "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                // Закрыть окно после успешного сохранения
                Application.Current.Windows.OfType<Window>()
                    .FirstOrDefault(w => w.DataContext == this)?
                    .Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void InitializeData()
        {
            Login = UserToEdit.Login;
            // skip password
            Firstname = UserToEdit.FirstName;
            Lastname = UserToEdit.LastName;
            Middlename = UserToEdit.MiddleName;

            SelectedTag = UserToEdit.Tag == Tag.None ? PermissionTags[0] : UserToEdit.Tag == Tag.Manager ? PermissionTags[1] : PermissionTags[2];

            if (UserToEdit.Permissions.HasFlag(Permission.ViewingClassrooms))
            {
                ClassroomsViewing = true;
            }
            if (UserToEdit.Permissions.HasFlag(Permission.AddClassrooms))
            {
                ClassroomsAdd = true;
            }
            if (UserToEdit.Permissions.HasFlag(Permission.UpdateClassrooms))
            {
                ClassroomsEdit = true;
            }
            if (UserToEdit.Permissions.HasFlag(Permission.RemoveClassrooms))
            {
                ClassroomsDelete = true;
            }
            if (UserToEdit.Permissions.HasFlag(Permission.ViewingUsers))
            {
                UsersViewing = true;
            }
            if (UserToEdit.Permissions.HasFlag(Permission.AddUsers))
            {
                UsersAdd = true;
            }
            if (UserToEdit.Permissions.HasFlag(Permission.UpdateUsers))
            {
                UsersEdit = true;
            }
            if (UserToEdit.Permissions.HasFlag(Permission.RemoveUsers))
            {
                UsersDelete = true;
            }
            if (UserToEdit.Permissions.HasFlag(Permission.ViewingEquipments))
            {
                EquipmentsViewing = true;
            }
            if (UserToEdit.Permissions.HasFlag(Permission.AddEquipments))
            {
                EquipmentsAdd = true;
            }
            if (UserToEdit.Permissions.HasFlag(Permission.UpdateEquipments))
            {
                EquipmentsEdit = true;
            }
            if (UserToEdit.Permissions.HasFlag(Permission.RemoveEquipments))
            {
                EquipmentsDelete = true;
            }
            if (UserToEdit.Permissions.HasFlag(Permission.ViewingCategories))
            {
                CategoriesViewing = true;
            }
            if (UserToEdit.Permissions.HasFlag(Permission.AddCategories))
            {
                CategoriesAdd = true;
            }
            if (UserToEdit.Permissions.HasFlag(Permission.UpdateCategories))
            {
                CategoriesEdit = true;
            }
            if (UserToEdit.Permissions.HasFlag(Permission.RemoveCategories))
            {
                CategoriesDelete = true;
            }
            if (UserToEdit.Permissions.HasFlag(Permission.ViewingRequests))
            {
                RepairRequestViewing = true;
            }
            if (UserToEdit.Permissions.HasFlag(Permission.AddRequests))
            {
                RepairRequestAdd = true;
            }
            if (UserToEdit.Permissions.HasFlag(Permission.UpdateRequests))
            {
                RepairRequestEdit = true;
            }
            if (UserToEdit.Permissions.HasFlag(Permission.RemoveRequests))
            {
                RepairRequestDelete = true;
            }
            if (UserToEdit.Permissions.HasFlag(Permission.CreatingInventory))
            {
                RepairRequestInventoryCreation = true;
            }
        }
    }
}