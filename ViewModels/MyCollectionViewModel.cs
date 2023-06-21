using MauiApp2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp2.ViewModels
{
    public class MyCollectionViewModel : BindableObject
    {

        private string userEntryText;
        private DateTime fromdateTime;
        private DateTime toDateTime;
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
                        UserItems = new ObservableCollection<MyModel>(SaveHistoryUserItems.Where(i => i.Name.ToLower().Contains(UserEntryText.ToLower())));
                    }
                    else
                    {

                        UserItems = new ObservableCollection<MyModel>(SaveHistoryUserItems);
                    }
                }
            }
        }

        public DateTime UserEntryFromDate
        {
            get
            {
                return fromdateTime;
            }
            set
            {
                fromdateTime = value;
                OnPropertyChanged(nameof(UserEntryFromDate));
                UserItems = new ObservableCollection<MyModel>(SaveHistoryUserItems.Where(i => i.FromDate >= fromdateTime));
                //}
                //else
                //{

                //    UserItems = new ObservableCollection<MyModel>(SaveHistoryUserItems);
                //}

            }
        }

        public DateTime UserEntryToDate
        {
            get
            {
                return toDateTime;
            }
            set
            {
                toDateTime = value;
                OnPropertyChanged(nameof(UserEntryToDate));
                UserItems = new ObservableCollection<MyModel>(SaveHistoryUserItems.Where(i => i.ToDate <= toDateTime));
                //}
                //else
                //{

                //    UserItems = new ObservableCollection<MyModel>(SaveHistoryUserItems);
                //}

            }
        }

        private ObservableCollection<MyModel> userItems;

        public ObservableCollection<MyModel> UserItems
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

        private ObservableCollection<MyModel> saveHistoryUserItems;
        public ObservableCollection<MyModel> SaveHistoryUserItems
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

        public MyCollectionViewModel()
        {
            //this list is from the api or any where like db 
            UserItems = new ObservableCollection<MyModel>
            {
                new MyModel{Name = "Alex", Address = "Tx,NSW", FromDate=DateTime.MinValue,ToDate=DateTime.Today},
                new MyModel{Name = "Bran", Address = "Gh,Mexico", FromDate=DateTime.MinValue,ToDate=DateTime.Today},
                new MyModel{Name = "Asif", Address = "Gh,Mexico", FromDate=DateTime.MaxValue,ToDate=DateTime.Today},
                new MyModel{Name = "Mira", Address = "Gh,Mexico", FromDate=DateTime.MinValue,ToDate=DateTime.MaxValue},
                new MyModel{Name = "Vira", Address = "Gh,Mexico", FromDate=DateTime.MaxValue,ToDate=DateTime.MaxValue},
                new MyModel{Name = "Hira", Address = "Gh,Mexico", FromDate=DateTime.MaxValue,ToDate=DateTime.Today},
                new MyModel{Name = "Gira", Address = "Gh,Mexico", FromDate=DateTime.MaxValue,ToDate=DateTime.MaxValue},
                new MyModel{Name = "Giva", Address = "Gh,Mexico", FromDate=DateTime.MaxValue,ToDate=DateTime.Today},
                new MyModel{Name = "Khira",Address = "Gh,Mexico", FromDate=DateTime.MaxValue,ToDate=DateTime.MaxValue},
            };

            //this is history list which saves the current list items
            SaveHistoryUserItems = new ObservableCollection<MyModel>(UserItems);

        }
    }
}
