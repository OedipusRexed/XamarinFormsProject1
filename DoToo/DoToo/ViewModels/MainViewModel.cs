using DoToo.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DoToo.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private readonly ToDoItemRepository repository;

        public MainViewModel(ToDoItemRepository repository)
        {
            this.repository = repository;
            Task.Run(async () => await LoadData());
        }
        
        private async Task LoadData()
        {

        }
    }
}
