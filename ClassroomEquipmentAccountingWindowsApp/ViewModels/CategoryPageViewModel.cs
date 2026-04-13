using ClassroomEquipmentAccountingEntities.Models;
using ClassroomEquipmentAccountingWindowsApp.Core;
using ClassroomEquipmentAccountingWindowsApp.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Collections.Generic;

namespace ClassroomEquipmentAccountingWindowsApp.ViewModels
{
    public class CategoryPageViewModel : ViewModelBase
    {
        public CategoryPageViewModel()
        {
            Items = new ObservableCollection<Category>(AppCore.Instance.AppDbContext.Categories);
            ItemsView = CollectionViewSource.GetDefaultView(Items);

            SortOptions = new List<string> { "По умолчанию", "Id" };

            // Фильтры по предметной области для категорий
            FilterOptions = new List<string> { "Все", "С описанием", "Без описания" };

            AddCommand = new RelayCommand(_ => OpenCategoryManager(null), _ => CanAdd);
            DoubleClickCommand = new RelayCommand(_ => OpenCategoryManager(SelectedItem), _ => SelectedItem != null);
            DeleteCommand = new RelayCommand(_ => DeleteSelected(), _ => SelectedItem != null);
            RefreshCommand = new RelayCommand(_ => Refresh());

            SelectedSort = "По умолчанию";
            SelectedFilter = FilterOptions[0];
        }

        public ObservableCollection<Category> Items { get => field; set => Set(ref field, in value, nameof(Items)); }
        public ICollectionView ItemsView { get; }

        public Category SelectedItem { get => field; set => Set(ref field, in value, nameof(SelectedItem)); }

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

        // Права доступа
        public bool CanAdd => AppCore.Instance.CurrentUser?.Permissions.HasFlag(Permission.AddCategories) ?? false;
        public bool CanEdit => AppCore.Instance.CurrentUser?.Permissions.HasFlag(Permission.UpdateCategories) ?? false;
        public bool CanDelete => AppCore.Instance.CurrentUser?.Permissions.HasFlag(Permission.RemoveCategories) ?? false;

        private void OpenCategoryManager(Category? entity)
        {
            new CategoryMangerWindow(entity).ShowDialog();
            Refresh();
        }

        private void Refresh()
        {
            Items.Clear();
            foreach (var it in AppCore.Instance.AppDbContext.Categories)
                Items.Add(it);
            ItemsView.Refresh();
        }

        private void ApplyFilter()
        {
            ItemsView.Filter = obj =>
            {
                if (obj is not Category c) return false;

                bool matchesSearch = string.IsNullOrWhiteSpace(SearchText)
                    || (c.ToString() ?? string.Empty).Contains(SearchText, StringComparison.OrdinalIgnoreCase);

                bool matchesFilter = SelectedFilter == "Все"
                    || (SelectedFilter == "С описанием" && !string.IsNullOrWhiteSpace(c.Description))
                    || (SelectedFilter == "Без описания" && string.IsNullOrWhiteSpace(c.Description));

                return matchesSearch && matchesFilter;
            };
            ItemsView.Refresh();
        }

        private void ApplySort()
        {
            ItemsView.SortDescriptions.Clear();
            if (SelectedSort == "Id")
                ItemsView.SortDescriptions.Add(new SortDescription(nameof(Category.Id), ListSortDirection.Ascending));
            else if (SelectedSort == "Название")
                ItemsView.SortDescriptions.Add(new SortDescription(nameof(Category.Title), ListSortDirection.Ascending));
        }

        private void DeleteSelected()
        {
            if (SelectedItem == null) return;

            var result = MessageBox.Show(
                $"Вы действительно хотите удалить категорию \"{SelectedItem}\"?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes) return;

            try
            {
                AppCore.Instance.AppDbContext.Categories.Remove(SelectedItem);
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
