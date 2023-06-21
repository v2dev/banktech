using MauiApp2.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp2.DataGrid
{
    class MyEntry : Entry
    {
        readonly MyCollectionViewModel _sharedCollectionViewModel;

        public MyEntry(MyCollectionViewModel sharedCollectionViewModel)
        {
            Placeholder = "Search";
            _sharedCollectionViewModel = sharedCollectionViewModel;
            TextChanged += OnMyEditTextChanged;
        }

        private void OnMyEditTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                _sharedCollectionViewModel.UserItems = new ObservableCollection<Models.MyModel>(_sharedCollectionViewModel.SaveHistoryUserItems);
            }
            else
            {
                _sharedCollectionViewModel.UserItems = new ObservableCollection<Models.MyModel>(_sharedCollectionViewModel.SaveHistoryUserItems.Where(i => i.Name.ToLower().Contains(e.NewTextValue.ToLower())));
            }
        }
    }
}
