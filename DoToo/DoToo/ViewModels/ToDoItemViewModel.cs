using DoToo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DoToo.ViewModels
{
    public class ToDoItemViewModel : ViewModel
    {
        public ToDoItemViewModel(ToDoItem item) => Item = item;

        public event EventHandler ItemStatusChanged;
        public ToDoItem Item { get; private set; }
        public string StatusText => Item.Completed ? "Reactivate" : "Completed";
        public ICommand ToggleCompleted => new Command((arg) =>
        {
            Item.Completed = !Item.Completed;
            ItemStatusChanged?.Invoke(this, new EventArgs());
        });
    }
}
