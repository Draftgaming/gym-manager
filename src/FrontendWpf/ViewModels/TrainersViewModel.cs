using DataAccess.Models;

using FrontendWpf.Services;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FrontendWpf.ViewModels

{
    public class TrainersViewModel : INotifyPropertyChanged
    {
        private readonly TrainerService _svc = new();
        public ObservableCollection<Trainer> Trainers { get; } = new ObservableCollection<Trainer>();

        private Trainer _selected;
        public Trainer SelectedTrainer
        {
            get => _selected;
            set { _selected = value; OnPropertyChanged(); }
        }

        public ICommand LoadCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }

        public TrainersViewModel()
        {
            LoadCommand = new AsyncRelayCommand(LoadAsync);
            AddCommand = new AsyncRelayCommand(AddAsync, () => SelectedTrainer != null);
            UpdateCommand = new AsyncRelayCommand(UpdateAsync, () => SelectedTrainer != null);
            DeleteCommand = new AsyncRelayCommand(DeleteAsync, () => SelectedTrainer != null);
        }

        private async Task LoadAsync()
        {
            var list = await _svc.GetAllAsync();
            Trainers.Clear();
            foreach (var t in list) Trainers.Add(t);
        }

        private async Task AddAsync()
        {
            var created = await _svc.CreateAsync(SelectedTrainer!);
            Trainers.Add(created);
        }

        private async Task UpdateAsync()
        {
            await _svc.UpdateAsync(SelectedTrainer!);
            await LoadAsync();
        }

        private async Task DeleteAsync()
        {
            await _svc.DeleteAsync(SelectedTrainer!.TrainerId);
            Trainers.Remove(SelectedTrainer!);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string p = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
    }
}
