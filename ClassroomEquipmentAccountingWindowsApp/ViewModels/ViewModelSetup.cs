using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Input;
using ClassroomEquipmentAccountingWindowsApp.Core;

namespace ClassroomEquipmentAccountingWindowsApp.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected ViewModelBase()
        {
            // Подписка на событие смены CurrentUser — уведомляем UI о смене прав
            try
            {
                AppCore.Instance.CurrentUserChanged += (_, __) =>
                {
                    // уведомить биндинги (все свойства) и переоценить команды
                    OnPropertyChanged(string.Empty);
                    CommandManager.InvalidateRequerySuggested();
                };
            }
            catch
            {
                // безопасно игнорируем ошибки во время инициализации
            }
        }

        protected bool Set<T>(ref T field,in T value,string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? (_ => true);
        }

        // Подписываемся на CommandManager.RequerySuggested чтобы CanExecute обновлялся автоматически
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute(parameter!);
        }

        public void Execute(object? parameter)
        {
            _execute(parameter!);
        }

        // Вызвать при необходимости вручную (альтернатива — CommandManager.InvalidateRequerySuggested())
        public void RaiseCanExecuteChanged() => CommandManager.InvalidateRequerySuggested();
    }

    public static class PasswordEncoder
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 500;

        public static string Hash(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

            var pbkdf2 = new Rfc2898DeriveBytes(password,salt,Iterations,HashAlgorithmName.SHA256);

            byte[] hash = pbkdf2.GetBytes(HashSize);

            return $"{Iterations}:{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
        }
        public static bool Verify(string hashedPassword, string password)
        {
            var parts = hashedPassword.Split(':');
            if (parts.Length != 3) return false;

            var iterations = int.Parse(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var storedHash = Convert.FromBase64String(parts[2]);

            var encodePass = new Rfc2898DeriveBytes(password,salt,iterations, HashAlgorithmName.SHA256);

            var computedHash = encodePass.GetBytes(storedHash.Length);

            return CryptographicOperations.FixedTimeEquals(computedHash,storedHash);
        }
    }
}
