using DoToo.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DoToo.Views;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Linq;
using DoToo.Models;

namespace DoToo.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private readonly ToDoItemRepository repository;
        public ObservableCollection<ToDoItemViewModel> Items { get; set; }
        public string FilterText => ShowAll ? "All" : "Active";
        public ICommand ToggleFilter => new Command(async () =>
        {
            ShowAll = !ShowAll;
            await LoadData();
        });

        public MainViewModel(ToDoItemRepository repository)
        {
            repository.OnItemAdded += (sender, item) =>
            Items.Add(CreateToDoItemViewModel(item));

            repository.OnItemUpdated += (sender, item) =>
            Task.Run(async () => await LoadData());
            this.repository = repository;
            Task.Run(async () => await LoadData());
        }
        
        private async Task LoadData()
        {
            var items = await repository.GetItems();

            if (!ShowAll)
            {
                items = items.Where(x => x.Completed == false).ToList();
            }
            var itemViewModels = items.Select(i => CreateToDoItemViewModel(i));
            Items = new ObservableCollection<ToDoItemViewModel>(itemViewModels);
        }

        private ToDoItemViewModel CreateToDoItemViewModel(ToDoItem item)
        {
            var itemViewModel = new ToDoItemViewModel(item);
            itemViewModel.ItemStatusChanged += ItemStatusChanged;
            return itemViewModel;
        }

        private void ItemStatusChanged(object sender, EventArgs e)
        {
            if (sender is ToDoItemViewModel item)
            {
                if (!ShowAll && item.Item.Completed)
                {
                    Items.Remove(item);
                }

                Task.Run(async () => await repository.UpdateItem(item.Item));
            }
        }

        public bool ShowAll { get; set; }
        public ICommand AddItem => new Command(async () =>
        {
            var itemView = Resolver.Resolve<ItemView>();
            await Navigation.PushAsync(itemView);
        });

        public ToDoItemViewModel SelectedItem
        {
            get { return null; }
            set
            {
                Device.BeginInvokeOnMainThread(async () => await NavigateToItem(value));
                RaisePropertyChanged(nameof(SelectedItem));
            }
        }

        private async Task NavigateToItem(ToDoItemViewModel item)
        {
            if (item == null)
            {
                return;
            }

            var itemView = Resolver.Resolve<ItemView>();
            var vm = itemView.BindingContext as ItemViewModel;
            vm.Item = item.Item;

            await Navigation.PushAsync(itemView);
        }
    }
}
