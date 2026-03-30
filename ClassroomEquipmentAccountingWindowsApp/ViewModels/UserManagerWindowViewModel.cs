using ClassroomEquipmentAccountingEntities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassroomEquipmentAccountingWindowsApp.ViewModels
{
    public class UserManagerWindowViewModel : ViewModelBase
    {
        public UserManagerWindowViewModel() { }
        public UserManagerWindowViewModel(User? userToEdit)
        {
            if (userToEdit == null)
                UserToEdit = new User();
            else
                UserToEdit = userToEdit!;
            InitializeData();
        }
        public User UserToEdit { get; set => Set(ref field, in value, nameof(UserToEdit)); }
        #region Main Fields
        public string Login { get; set => Set(ref field, in value, nameof(Login)); }
        public string Password { get; set => Set(ref field, in value, nameof(Password)); }
        public string Firstname { get; set => Set(ref field, in value, nameof(Firstname)); }
        public string Lastname { get; set => Set(ref field, in value, nameof(Lastname)); }
        public string Middlename { get; set => Set(ref field, in value, nameof(Middlename)); }
        #endregion
        #region Tags
        public string SelectedTag { get; set => Set(ref field, in value, nameof(SelectedTag)); }

        public List<string> Tags = new List<string>() { "Не выбрано", "Зав. Аудиторией", "Администратор" };
        #endregion
        #region Permission

        #region Classrooms
        public bool ClassroomsViewing;
        public bool ClassroomsAdd;
        public bool ClassroomsEdit;
        public bool ClassroomsDelete;
        #endregion

        #region Users
        public bool UsersViewing;
        public bool UsersAdd;
        public bool UsersEdit;
        public bool UsersDelete;
        #endregion

        #region Equipments
        public bool EquipmentsViewing;
        public bool EquipmentsAdd;
        public bool EquipmentsEdit;
        public bool EquipmentsDelete;
        #endregion

        #region Categories
        public bool CategoriesViewing;
        public bool CategoriesAdd;
        public bool CategoriesEdit;
        public bool CategoriesDelete;
        #endregion

        #region RepairRequest
        public bool RepairRequestViewing;
        public bool RepairRequestAdd;
        public bool RepairRequestEdit;
        public bool RepairRequestDelete;
        public bool RepairRequestInventoryCreation;
        #endregion

        #endregion

        private void InitializeData()
        {
            Login = UserToEdit.Login;
            // skip password
            Firstname = UserToEdit.FirstName;
            Lastname = UserToEdit.LastName;
            Middlename = UserToEdit.MiddleName;

            SelectedTag = UserToEdit.Tag == Tag.None ? Tags[0] : UserToEdit.Tag == Tag.Manager ? Tags[1] : Tags[2];

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