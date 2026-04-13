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
    public class ClassroomsPageViewModel : ViewModelBase
    {
        public ClassroomsPageViewModel()
        {
            Items = new ObservableCollection<Classroom>(AppCore.Instance.AppDbContext.Classrooms);
            ItemsView = CollectionViewSource.GetDefaultView(Items);

            SortOptions = new List<string> { "По умолчанию", "Id" };

            // Предметно-ориентированные фильтры для кабинетов
            FilterOptions = new List<string> { "Все", "Пустые (без оборудования)", "С оборудованием" };

            AddCommand = new RelayCommand(_ => OpenClassroomManager(null), _ => CanAdd);
            DoubleClickCommand = new RelayCommand(_ => OpenClassroomManager(SelectedItem), _ => SelectedItem != null && CanEdit);
            DeleteCommand = new RelayCommand(_ => DeleteSelected(), _ => SelectedItem != null && CanDelete);
            RefreshCommand = new RelayCommand(_ => Refresh());

            SelectedSort = "По умолчанию";
            SelectedFilter = FilterOptions[0];
        }

        public ObservableCollection<Classroom> Items { get => field; set => Set(ref field, in value, nameof(Items)); }
        public ICollectionView ItemsView { get; }

        public Classroom SelectedItem { get => field; set => Set(ref field, in value, nameof(SelectedItem)); }

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

        public bool CanAdd => AppCore.Instance.CurrentUser?.Permissions.HasFlag(Permission.AddClassrooms) ?? false;
        public bool CanEdit => AppCore.Instance.CurrentUser?.Permissions.HasFlag(Permission.UpdateClassrooms) ?? false;
        public bool CanDelete => AppCore.Instance.CurrentUser?.Permissions.HasFlag(Permission.RemoveClassrooms) ?? false;

        private void OpenClassroomManager(Classroom? entity)
        {
            new ClassroomMangerWindow(entity).ShowDialog();
            Refresh();
        }

        private void Refresh()
        {
            Items.Clear();
            foreach (var it in AppCore.Instance.AppDbContext.Classrooms)
                Items.Add(it);
            ItemsView.Refresh();
        }

        private void ApplyFilter()
        {
            ItemsView.Filter = obj =>
            {
                if (obj is not Classroom c) return false;

                bool matchesSearch = string.IsNullOrWhiteSpace(SearchText)
                    || (c.ToString() ?? string.Empty).Contains(SearchText, StringComparison.OrdinalIgnoreCase);

                bool matchesFilter = SelectedFilter == "Все"
                    || (SelectedFilter == "Пустые (без оборудования)" && c.IsEquipmentAreEmpty)
                    || (SelectedFilter == "С оборудованием" && !c.IsEquipmentAreEmpty);

                return matchesSearch && matchesFilter;
            };
            ItemsView.Refresh();
        }

        private void ApplySort()
        {
            ItemsView.SortDescriptions.Clear();
            if (SelectedSort == "Id")
                ItemsView.SortDescriptions.Add(new SortDescription(nameof(Classroom.Id), ListSortDirection.Ascending));
        }

        private void DeleteSelected()
        {
            if (SelectedItem == null) return;

            var result = MessageBox.Show(
                $"Вы действительно хотите удалить кабинет \"{SelectedItem}\"?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes) return;

            try
            {
                AppCore.Instance.AppDbContext.Classrooms.Remove(SelectedItem);
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
