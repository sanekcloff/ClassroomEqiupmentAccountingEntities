using ClassroomEquipmentAccountingEntities.Models;
using ClassroomEquipmentAccountingWindowsApp.Core;
using ClassroomEquipmentAccountingWindowsApp.Views.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace ClassroomEquipmentAccountingWindowsApp.ViewModels
{
    public class GeneralWindowViewModel : ViewModelBase
    {
        public GeneralWindowViewModel()
        {
            CurrentUser = AppCore.Instance.CurrentUser!;
            CurrentPage = new StarterPage();
            SwitchToUsersPageCommand = new(action =>
            {
                CurrentPage = new UsersPage();
            });
            SwitchToClassroomsPageCommand = new(action =>
            {
                CurrentPage = new ClassroomsPage();
            });
            SwitchToEquipmentsPageCommand = new(action =>
            {
                CurrentPage = new EquipmentPage();
            });
            SwitchToRepaierRequestsPageCommand = new(action =>
            {
                CurrentPage = new RepairRequestsPage();
            });
            SwitchToCategoriesPageCommand = new(action =>
            {
                CurrentPage = new CategoryPage();
            });
        }
        public User CurrentUser { get; init; }

        public Page CurrentPage { get => field; set => Set(ref field, in value, nameof(CurrentPage)); }

        public RelayCommand SwitchToUsersPageCommand { get; }
        public RelayCommand SwitchToClassroomsPageCommand { get; }
        public RelayCommand SwitchToEquipmentsPageCommand { get; }
        public RelayCommand SwitchToRepaierRequestsPageCommand { get; }
        public RelayCommand SwitchToCategoriesPageCommand { get; }

    }
}
