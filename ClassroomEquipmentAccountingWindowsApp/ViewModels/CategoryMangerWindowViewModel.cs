using ClassroomEquipmentAccountingEntities.Models;
using ClassroomEquipmentAccountingWindowsApp.Core;
using System;
using System.Windows;

namespace ClassroomEquipmentAccountingWindowsApp.ViewModels
{
    public class CategoryMangerWindowViewModel : ViewModelBase
    {
        private bool _isAdd = false;
        public CategoryMangerWindowViewModel(Category? category)
        {
            _isAdd = category == null;
            Entity = category ?? new Category();
            Title = Entity.Title;
            Description = Entity.Description;
            SaveCommand = new RelayCommand(_ => Save(), _ => CanSave);
        }

        public Category Entity { get => field; set => Set(ref field, in value, nameof(Entity)); }
        public string Title { get => field; set => Set(ref field, in value, nameof(Title)); }
        public string Description { get => field; set => Set(ref field, in value, nameof(Description)); }

        public RelayCommand SaveCommand { get; }

        public bool CanSave => _isAdd ? (AppCore.Instance.CurrentUser?.Permissions.HasFlag(Permission.AddCategories) ?? false)
                                    : (AppCore.Instance.CurrentUser?.Permissions.HasFlag(Permission.UpdateCategories) ?? false);

        private void Save()
        {
            if (!CanSave)
            {
                MessageBox.Show("Нет прав на сохранение категории", "Доступ запрещён", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                Entity.Title = Title?.Trim() ?? string.Empty;
                Entity.Description = Description;

                if (_isAdd)
                    AppCore.Instance.AppDbContext.Categories.Add(Entity);
                else
                    AppCore.Instance.AppDbContext.Categories.Update(Entity);

                AppCore.Instance.AppDbContext.SaveChanges();

                MessageBox.Show("Категория сохранена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.DataContext == this)?.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
