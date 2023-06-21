using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Maui.Markup.LeftToRight;
using MauiApp2.Models;
using MauiApp2.ViewModels;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using System.Collections.ObjectModel;
using System.Linq;

namespace MauiApp2;

public partial class MainPage : ContentPage
{
    readonly MainViewModel sharedCollectionViewModel;
    internal Polygon SortingIcon;
    public MainPage()
    {
        InitializeComponent();        
        sharedCollectionViewModel = new MainViewModel();
        BindingContext =  sharedCollectionViewModel;
        LoadFilters();
    }   

    public void LoadFilters()
    {
        SortingIcon = new();
        Grid DateTimeGrid = new()
        {
            Margin = new Thickness(50),
            RowDefinitions =
            {
                new RowDefinition{Height=50},
            },
            ColumnDefinitions=
            {
                new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(GridLength.Star),               
            }
        };
        Grid ListGrid = new()
        {
            Margin = new Thickness(10, 5, 10, 10),           
            RowDefinitions =
            {
                 new RowDefinition(GridLength.Star),
            },
            ColumnDefinitions =
            {
                new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(GridLength.Star),
            }
        };       
        DateTimeGrid.Children.Add(new DateLabel("From Date").Row(0).Column(0));
        DateTimeGrid.Children.Add(new FromDatetime(sharedCollectionViewModel).Bind(DatePicker.DateProperty, nameof(sharedCollectionViewModel.UserEntryFromDate)).Row(0).Column(0));
        DateTimeGrid.Children.Add(new DateLabel("To Date").Row(0).Column(1));
        DateTimeGrid.Children.Add(new ToDatetime(sharedCollectionViewModel).Bind(DatePicker.DateProperty, nameof(sharedCollectionViewModel.UserEntryToDate)).Row(0).Column(1));
        DateTimeGrid.Children.Add(new MyEntry(sharedCollectionViewModel).Bind(Entry.TextProperty, nameof(sharedCollectionViewModel.UserEntryText)).Row(0).Column(2));

        //ListGrid.Children.Add(new MyPickerView(sharedCollectionViewModel).Bind(Picker.BindingContextProperty, nameof(sharedCollectionViewModel.UserItems), BindingMode.TwoWay).Row(0).Column(0));
        ListGrid.Children.Add(new MyCollectionView(sharedCollectionViewModel).Bind(ItemsView.ItemsSourceProperty, nameof(sharedCollectionViewModel.Products), BindingMode.TwoWay).Row(0).Column(0));
        ListGrid.Children.Add(new MyEventsCollectionView(sharedCollectionViewModel).Bind(ItemsView.ItemsSourceProperty, nameof(sharedCollectionViewModel.Events), BindingMode.TwoWay).Row(0).Column(1));
        ListGrid.Children.Add(new MyCardCollectionView(sharedCollectionViewModel).Bind(ItemsView.ItemsSourceProperty, nameof(sharedCollectionViewModel.Cards), BindingMode.TwoWay).Row(0).Column(2));
        ListGrid.Children.Add(new ClearButtion(sharedCollectionViewModel).Bind(ItemsView.ItemsSourceProperty, nameof(sharedCollectionViewModel.UserItems), BindingMode.TwoWay).Row(0).Column(3));

        Grid MainGrid = new()
        {           
            RowDefinitions =
            {
                new RowDefinition(GridLength.Star),
                new RowDefinition(GridLength.Star),
                new RowDefinition(GridLength.Star),
            },
        };
        MainGrid.Children.Add(DateTimeGrid.Column(0));
        MainGrid.Children.Add(ListGrid.Row(1));
        MainGrid.Children.Add(_dataGrid1.Row(2));

        Content = new Grid
        {
            Children = {
               MainGrid
            }
        };      
    }
    class MyEntry : Entry
    {
        readonly MainViewModel _sharedCollectionViewModel;
        public MyEntry(MainViewModel sharedCollectionViewModel)
        {
            WidthRequest = 250;
            Placeholder = "Search Product";
            _sharedCollectionViewModel = sharedCollectionViewModel;            
            TextChanged += OnMyEditTextChanged;
            Margin = 10;
            BackgroundColor = Colors.LightBlue;
        }
        private void OnMyEditTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                _sharedCollectionViewModel.Teams = new ObservableCollection<Team>(_sharedCollectionViewModel.SaveHistoryUserItems);
            }

            else
            {
                _sharedCollectionViewModel.Teams = new ObservableCollection<Team>(_sharedCollectionViewModel.SaveHistoryUserItems.Where(i => i.Name.ToLower().Contains(e.NewTextValue.ToLower())));

            }
        }
    }

    class FromDatetime : DatePicker
    {
        readonly MainViewModel _sharedCollectionViewModel;
        public FromDatetime(MainViewModel sharedCollectionViewModel)
        {
            _sharedCollectionViewModel = sharedCollectionViewModel;
            DateSelected += OnMyEditTextChanged;
        }
        private void OnMyEditTextChanged(object sender, DateChangedEventArgs e)
        {          
            _sharedCollectionViewModel.Teams = new ObservableCollection<Team>(_sharedCollectionViewModel.SaveHistoryUserItems.Where(i => i.CreatedDate >= DateOnly.FromDateTime(e.NewDate.Date)));
        }
    }
    class DateLabel: Label
    {
        public DateLabel(string name)
        {
            Margin = 10;
            Text = name;
            WidthRequest = 100;
            BackgroundColor = Colors.LightBlue;
            HorizontalTextAlignment = TextAlignment.Center;
            VerticalTextAlignment= TextAlignment.Center;
        }
    }
    class ToDatetime : DatePicker
    {
        readonly MainViewModel _sharedCollectionViewModel;
        public ToDatetime(MainViewModel sharedCollectionViewModel)
        {
            _sharedCollectionViewModel = sharedCollectionViewModel;
             DateSelected += OnDateSelected;
        }
        private void OnDateSelected(object sender, DateChangedEventArgs e)
        {
            _sharedCollectionViewModel.Teams = new ObservableCollection<Team>(_sharedCollectionViewModel.SaveHistoryUserItems.Where(i => i.CreatedDate <= DateOnly.FromDateTime(e.NewDate.Date)));
        }
    }
    class MyCollectionView : CollectionView
    {
        readonly MainViewModel _sharedCollectionViewModel;
        public MyCollectionView(MainViewModel sharedCollectionViewModel)
        {
            _sharedCollectionViewModel = sharedCollectionViewModel;
            SelectionMode = SelectionMode.Multiple;           
            BackgroundColor = Colors.LightBlue;
            
            Header = "Select Product";
            SelectionChanged += HandleCollectionViewSelectionChanged;

            ItemsLayout = new GridItemsLayout(orientation: ItemsLayoutOrientation.Vertical);
            ItemTemplate = new DataTemplate(() =>
            {
                VerticalStackLayout VerticalView = new VerticalStackLayout { Spacing = 50, Padding = new Thickness(0, 0, 0,0) ,IsVisible=true}.BackgroundColor(Colors.LightGray);
                Label nameLabel = new Label { }.Left().TextColor(Colors.Black);
                nameLabel.SetBinding(Label.TextProperty, "Name"); 
                
                VerticalView.Children.Add(nameLabel);                
                return VerticalView;               
            });
        }
        void HandleCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = (Product)e.CurrentSelection.FirstOrDefault();
            if (list != null) _sharedCollectionViewModel.Teams = new ObservableCollection<Team>(_sharedCollectionViewModel.SaveHistoryUserItems.Where(i => i.Name == list.Name));
        }
    }
    class MyEventsCollectionView : CollectionView
    {
        readonly MainViewModel _sharedCollectionViewModel;
        public MyEventsCollectionView(MainViewModel sharedCollectionViewModel)
        {
            _sharedCollectionViewModel = sharedCollectionViewModel;
            SelectionMode = SelectionMode.Multiple;
            BackgroundColor = Colors.LightBlue;
            Header = "Select Event";
            SelectionChanged += HandleCollectionViewSelectionChanged;

            ItemsLayout = new GridItemsLayout(orientation: ItemsLayoutOrientation.Vertical);
            ItemTemplate = new DataTemplate(() =>
            {
                VerticalStackLayout VerticalView = new VerticalStackLayout { Spacing = 0, Padding = new Thickness(0, 0, 0, 0) }.BackgroundColor(Colors.LightGray);
                Label nameLabel = new Label { }.Left().TextColor(Colors.Black);
                nameLabel.SetBinding(Label.TextProperty, "Name");
                VerticalView.Children.Add(nameLabel);               
                return VerticalView;                
            });
        }
        void HandleCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = (Event)e.CurrentSelection.FirstOrDefault();
            if (list != null) _sharedCollectionViewModel.Teams = new ObservableCollection<Team>(_sharedCollectionViewModel.SaveHistoryUserItems.Where(i => i.Event == list.Name));
        }
    }    

    class MyCardCollectionView : CollectionView
    {
        readonly MainViewModel _sharedCollectionViewModel;
        internal Polygon SortingIcon;
        internal View SortingIconContainer { get; }
        public MyCardCollectionView(MainViewModel sharedCollectionViewModel)
        {
            SortingIcon = new();             
            SortingIconContainer = new ContentView
            {
                IsVisible = false,
                Content = SortingIcon,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                InputTransparent = true,
            };

            _sharedCollectionViewModel = sharedCollectionViewModel;
            SelectionMode = SelectionMode.Multiple;
            BackgroundColor = Colors.LightBlue;
            Header = "Select Card";
            SelectionChanged += HandleCollectionViewSelectionChanged;

            ItemsLayout = new GridItemsLayout(orientation: ItemsLayoutOrientation.Vertical);
            ItemTemplate = new DataTemplate(() =>
            {
                VerticalStackLayout VerticalView = new VerticalStackLayout { Spacing = 0, Padding = new Thickness(0, 0, 0, 0) }.BackgroundColor(Colors.LightGray);
                Label nameLabel = new Label { }.Left().TextColor(Colors.Black);
                nameLabel.SetBinding(Label.TextProperty, "Name");
                VerticalView.Children.Add(nameLabel);
                return VerticalView;
            });
        }
        void HandleCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Card list = (Card) e.CurrentSelection.FirstOrDefault();
            if (list!=null) _sharedCollectionViewModel.Teams = new ObservableCollection<Team>(_sharedCollectionViewModel.SaveHistoryUserItems.Where(i => i.CardNo == list.Name));
            ;
        }
    }

    class ClearButtion: Button
    {
        readonly MainViewModel _sharedCollectionViewModel;
        public ClearButtion(MainViewModel sharedCollectionViewModel) 
        {
            Text = "Clear All";
            TextColor = Colors.Red;            
            _sharedCollectionViewModel = sharedCollectionViewModel;
            Clicked += ButtonClicked;            
        }
        void ButtonClicked(object sender, EventArgs e)
        {
            _sharedCollectionViewModel.Teams = _sharedCollectionViewModel.SaveHistoryUserItems;
        }
    }

    class MyPickerView : Picker
    {
        readonly MainViewModel _sharedCollectionViewModel;
        public MyPickerView(MainViewModel sharedCollectionViewModel)
        {
            _sharedCollectionViewModel = sharedCollectionViewModel;
             //= SelectionMode.Multiple;
            BackgroundColor = Colors.LightBlue;
            Title = "Select Product";
            SelectedIndexChanged += OnPickerSelectedIndexChanged;
            ItemDisplayBinding = new Binding("Name");
            //ItemsLayout = new GridItemsLayout(orientation: ItemsLayoutOrientation.Vertical);
            //ItemTemplate = new DataTemplate(() =>
            //{
            //    VerticalStackLayout VerticalView = new VerticalStackLayout { Spacing = 50, Padding = new Thickness(10, 10, 10, 10) }.BackgroundColor(Colors.LightGray);
            //    Label nameLabel = new Label { }.Left().TextColor(Colors.Black);
            //    nameLabel.SetBinding(Label.TextProperty, "Name");

            //    VerticalView.Children.Add(nameLabel);
            //    return VerticalView;
            //});
        }
        void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            Team list = (Team)sender;
            if (list != null) _sharedCollectionViewModel.Teams = new ObservableCollection<Team>(_sharedCollectionViewModel.SaveHistoryUserItems.Where(i => i.Name == list.Name));
            ;
        }
    }
}


