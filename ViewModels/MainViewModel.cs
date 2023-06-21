using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using MauiApp2.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Extensions;

namespace MauiApp2.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Team> _teams;
        private Team _selectedItem;
        private bool _isRefreshing;
        private bool _teamColumnVisible = true;
        private bool _wonColumnVisible = true;
        private bool _headerBordersVisible = true;
        private ushort _teamColumnWidth = 70;
       

        public MainViewModel()
        {
            Teams = DummyDataProvider.GetTeams().ToObservableCollection();
            Products = DummyDataProvider.GetProducts().ToObservableCollection();
            Events = DummyDataProvider.GetEvents().ToObservableCollection();
            Cards = DummyDataProvider.GetCards().ToObservableCollection();
            RefreshCommand = new Command(CmdRefresh);
            LoadUserItems();
        }

        private void LoadUserItems()
        {
            //this list is from the api or any where like db 
            UserItems = Teams.ToObservableCollection<Team>();  
            //this is history list which saves the current list items
            SaveHistoryUserItems = new ObservableCollection<Team>(UserItems);
        }

        public ObservableCollection<Team> Teams
        {
            get => _teams;
            set
            {
                _teams = value;
                OnPropertyChanged(nameof(Teams));
            }
        }

        public bool HeaderBordersVisible
        {
            get => _headerBordersVisible;
            set
            {
                _headerBordersVisible = value;
                OnPropertyChanged(nameof(HeaderBordersVisible));
            }
        }

        public bool TeamColumnVisible
        {
            get => _teamColumnVisible;
            set
            {
                _teamColumnVisible = value;
                OnPropertyChanged(nameof(TeamColumnVisible));
            }
        }

        public bool WonColumnVisible
        {
            get => _wonColumnVisible;
            set
            {
                _wonColumnVisible = value;
                OnPropertyChanged(nameof(WonColumnVisible));
            }
        }

        public ushort TeamColumnWidth
        {
            get => _teamColumnWidth;
            set
            {
                _teamColumnWidth = value;
                OnPropertyChanged(nameof(TeamColumnWidth));
            }
        }

        public Team SelectedTeam
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                Debug.WriteLine("Team Selected : " + value?.Name);
            }
        }

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public ICommand RefreshCommand { get; set; }

        private async void CmdRefresh()
        {
            IsRefreshing = true;
            // wait 3 secs for demo
            await Task.Delay(3000);
            IsRefreshing = false;
        }

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        #endregion INotifyPropertyChanged implementation


        private string userEntryText;
        private DateOnly fromdateTime;
        private DateOnly toDateTime;
        public string UserEntryText
        {
            get
            {
                return userEntryText;
            }
            set
            {
                if (userEntryText != value)
                {
                    userEntryText = value;
                    OnPropertyChanged(UserEntryText);

                    if (!string.IsNullOrEmpty(UserEntryText))
                    {
                        UserItems = new ObservableCollection<Team>(SaveHistoryUserItems.Where(i => i.Name.ToLower().Contains(UserEntryText.ToLower())));
                    }
                    else
                    {

                        UserItems = new ObservableCollection<Team>(SaveHistoryUserItems);
                    }
                }
            }
        }

        public DateOnly UserEntryFromDate
        {
            get
            {
                return fromdateTime;
            }
            set
            {
                fromdateTime = value;
                OnPropertyChanged(nameof(UserEntryFromDate));
                UserItems = new ObservableCollection<Team>(SaveHistoryUserItems.Where(i => i.CreatedDate >= fromdateTime));
            }
        }

        public DateOnly UserEntryToDate
        {
            get{ return toDateTime;}
            set
            {
                toDateTime = value;
                OnPropertyChanged(nameof(UserEntryToDate));
                UserItems = new ObservableCollection<Team>(SaveHistoryUserItems.Where(i => i.CreatedDate <= toDateTime));
            }
        }

        private ObservableCollection<Team> userItems;

        public ObservableCollection<Team> UserItems
        {
            get
            {
                return userItems;
            }
            set
            {
                if (userItems != value)
                {
                    userItems = value;
                    OnPropertyChanged(nameof(UserItems));
                }
            }
        }

        private ObservableCollection<Team> saveHistoryUserItems;
        public ObservableCollection<Team> SaveHistoryUserItems
        {
            get
            {
                return saveHistoryUserItems;
            }
            set
            {
                if (saveHistoryUserItems != value)
                {
                    saveHistoryUserItems = value;
                    OnPropertyChanged(nameof(SaveHistoryUserItems));
                }
            }
        }


        //Observable Collections for Products,Events and Cards

        private ObservableCollection<Product> products;
        public ObservableCollection<Product> Products
        {
            get { return products; }
            set
            {              
                    products = value;
                    OnPropertyChanged(nameof(Products));
            }
        }

        private ObservableCollection<Event> events;
        public ObservableCollection<Event> Events
        {
            get { return events; }
            set
            {
                events = value;
                OnPropertyChanged(nameof(Events));
            }
        }

        private ObservableCollection<Card> cards;
        public ObservableCollection<Card> Cards
        {
            get { return cards; }
            set
            {
                cards = value;
                OnPropertyChanged(nameof(Cards));
            }
        }
    }
}
