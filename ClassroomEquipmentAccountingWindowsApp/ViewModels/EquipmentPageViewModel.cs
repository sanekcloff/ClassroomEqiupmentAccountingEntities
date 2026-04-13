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
    public class EquipmentPageViewModel : ViewModelBase
    {
        private readonly Dictionary<string, Status> _statusMap = new()
        {
            { "В эксплуатации", Status.Expoitation },
            { "На ремонте", Status.Repair },
            { "В резерве", Status.Reserve },
            { "Снят с эксплуатации", Status.Decommissioned }
        };

        public EquipmentPageViewModel()
        {
            Items = new ObservableCollection<Equipment>(AppCore.Instance.AppDbContext.Equipments);
            ItemsView = CollectionViewSource.GetDefaultView(Items);

            SortOptions = new List<string> { "По умолчанию", "Id" };

            // Фильтры по предметной области — статус, можно расширить по категории/аудитории
            FilterOptions = new List<string> { "Все", "В эксплуатации", "На ремонте", "В резерве", "Снят с эксплуатации" };

            AddCommand = new RelayCommand(_ => OpenEquipmentManager(null), _ => CanAdd);
            DoubleClickCommand = new RelayCommand(_ => OpenEquipmentManager(SelectedItem), _ => SelectedItem != null && CanEdit);
            DeleteCommand = new RelayCommand(_ => DeleteSelected(), _ => SelectedItem != null && CanDelete);
            RefreshCommand = new RelayCommand(_ => Refresh());

            SelectedSort = "По умолчанию";
            SelectedFilter = FilterOptions[0];
        }

        public ObservableCollection<Equipment> Items { get => field; set => Set(ref field, in value, nameof(Items)); }
        public ICollectionView ItemsView { get; }

        public Equipment SelectedItem { get => field; set => Set(ref field, in value, nameof(SelectedItem)); }

        public RelayCommand AddCommand { get; }
        public RelayCommand DoubleClickCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand RefreshCommand { get; }

        public List<string> SortOptions { get; }
        public List<string> FilterOptions { get; }

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

        public string SelectedFilter
        {
            get => field;
            set
            {
                if (Set(ref field, in value, nameof(SelectedFilter)))
                    ApplyFilter();
            }
        }

        public bool CanAdd => AppCore.Instance.CurrentUser?.Permissions.HasFlag(Permission.AddEquipments) ?? false;
        public bool CanEdit => AppCore.Instance.CurrentUser?.Permissions.HasFlag(Permission.UpdateEquipments) ?? false;
        public bool CanDelete => AppCore.Instance.CurrentUser?.Permissions.HasFlag(Permission.RemoveEquipments) ?? false;

        private void OpenEquipmentManager(Equipment? entity)
        {
            new EquipmentMangerWindow(entity).ShowDialog();
            Refresh();
        }

        private void Refresh()
        {
            Items.Clear();
            foreach (var it in AppCore.Instance.AppDbContext.Equipments)
                Items.Add(it);
            ItemsView.Refresh();
        }

        private void ApplyFilter()
        {
            ItemsView.Filter = obj =>
            {
                if (obj is not Equipment e) return false;

                bool matchesSearch = string.IsNullOrWhiteSpace(SearchText)
                    || (e.ToString() ?? string.Empty).Contains(SearchText, StringComparison.OrdinalIgnoreCase);

                bool matchesFilter = SelectedFilter == "Все";

                if (!matchesFilter && _statusMap.TryGetValue(SelectedFilter ?? string.Empty, out var status))
                {
                    matchesFilter = e.Status == status;
                }

                return matchesSearch && matchesFilter;
            };
            ItemsView.Refresh();
        }

        private void ApplySort()
        {
            ItemsView.SortDescriptions.Clear();
            if (SelectedSort == "Id")
                ItemsView.SortDescriptions.Add(new SortDescription(nameof(Equipment.Id), ListSortDirection.Ascending));
        }

        private void DeleteSelected()
        {
            if (SelectedItem == null) return;

            var result = MessageBox.Show(
                $"Вы действительно хотите удалить оборудование \"{SelectedItem}\"?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes) return;

            try
            {
                AppCore.Instance.AppDbContext.Equipments.Remove(SelectedItem);
                AppCore.Instance.AppDbContext.SaveChanges();
                Items.Remove(SelectedItem);
                SelectedItem = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
