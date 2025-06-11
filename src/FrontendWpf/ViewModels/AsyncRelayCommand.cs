using System;

using System.Threading.Tasks;
using System.Windows.Input;

namespace FrontendWpf.ViewModels
{
    // Simple ICommand implementation for async
    public class AsyncRelayCommand(Func<Task> exec, Func<bool> can = null) : ICommand
    {
        private readonly Func<Task> _execute = exec;
        private readonly Func<bool> _canExecute = can;

        public bool CanExecute(object _) => _canExecute?.Invoke() ?? true;
        public async void Execute(object _) => await _execute();
        public event EventHandler CanExecuteChanged;
        public void RaiseCanExecute() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
