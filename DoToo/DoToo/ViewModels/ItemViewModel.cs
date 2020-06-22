using System;
using System.Collections.Generic;
using System.Text;
using DoToo.Models;
using DoToo.Repositories;
using Xamarin.Forms;

namespace DoToo.ViewModels
{
    public class ItemViewModel : ViewModel
    {
        private ToDoItemRepository repository;

        public ItemViewModel(ToDoItemRepository repository)
        {
            this.repository = repository;
        }
    }
}
