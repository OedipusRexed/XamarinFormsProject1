using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using DoToo.Models;
using DoToo.Repositories;
using Xamarin.Forms;

namespace DoToo.ViewModels
{
    public class ItemViewModel : ViewModel
    {
        private ToDoItemRepository repository;
        public ToDoItem Item { get; set; }

        public ItemViewModel(ToDoItemRepository repository)
        {
            this.repository = repository;
            Item = new ToDoItem() { Due = DateTime.Now.AddDays(1) };
        }

        public ICommand Save => new Command(async () =>
        {
            await repository.AddOrUpdate(Item);
            await Navigation.PopAsync();
        });

    }
}
