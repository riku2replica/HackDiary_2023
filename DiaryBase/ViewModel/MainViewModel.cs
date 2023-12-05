using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
//using GoogleGson.Annotations;
using Microsoft.Maui.Controls.Compatibility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DiaryBase
{
    public partial class MainViewModel : INotifyPropertyChanged
    {
        readonly IList<DiaryData> source;

        public ObservableCollection<DiaryData> Entries { get; private set; }
        public IList<DiaryData> EmptyEntries { get; private set; }

        public DiaryData PreviousEntry { get; set; }
        public DiaryData CurrentEntry { get; set; }
        public DiaryData CurrentItem { get; set; }
        public int PreviousPosition { get; set; }
        public int CurrentPosition { get; set; }
        public int Position { get; set; }

        public ICommand FilterCommand => new Command<string>(FilterItems);
        public ICommand ItemChangedCommand => new Command<DiaryData>(ItemChanged);
        public ICommand PositionChangedCommand => new Command<int>(PositionChanged);
        public ICommand DeleteCommand => new Command<DiaryData>(RemoveMonkey);
        //public ICommand FavoriteCommand => new Command<DiaryData>(FavoriteMonkey);

        public MainViewModel()
        {
            source = new List<DiaryData>();
            CreateDummyCollection();

            CurrentItem = Entries.Skip(1).FirstOrDefault();
            OnPropertyChanged("CurrentItem");
            Position = 0;
            OnPropertyChanged("Position");
        }

        void CreateDummyCollection()
        {
            source.Add(new DiaryData
            {
                ID = "3",
                Title = "Good Day",
                Entry = "Good day comes after a bad day just like rainbow after the rain.",
                Date = "3/1/2020",
                Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg",
                Mood = "Recovering Good",
            });

            source.Add(new DiaryData
            {
                ID = "2",
                Title = "Bad Omen",
                Entry = "Something went wrong today, I'm not sure what should it be.",
                Date = "2/1/2020",
                Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg",
                Mood = "Bad",
            });

            source.Add(new DiaryData
            {
                ID = "1",
                Title = "My First Ever Entry",
                Entry = "This is the first evert Entry of the diary",
                Date = "1/1/2020",
                Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg",
                Mood = "Neutral",
            });

            Entries = new ObservableCollection<DiaryData>(source);
        }

        [RelayCommand]
        async void Add()
        {
            await Shell.Current.DisplayAlert("Warning", "To be added", "OK");
            return;
        }

        [RelayCommand]
        async void Tap()
        {
            //Debug
            //await Shell.Current.DisplayAlert("Test", CurrentPosition.ToString(), "OK");
            String sEnc = String.Empty;
            //Json.Encode(Entries[CurrentPosition])
            String s = String.Empty;

            //s = Entries[CurrentPosition].ToString();
            DiaryData dSelected = Entries[CurrentPosition];            
            s = dSelected.ToString();

            //Convert.ToBase64CharArray(Entries[CurrentPosition]);
            if (!String.IsNullOrEmpty(s))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(s);
                sEnc = Convert.ToBase64String(bytes);
            }
            await Shell.Current.GoToAsync($"{nameof(DetailPage)}?Text={sEnc}");
            //return;
        }

        [RelayCommand]
        async void First()
        {
            Position = 0;
            //PositionChanged(Position);
            OnPropertyChanged("Position");
            await Shell.Current.DisplayAlert("Warning","First","OK");
            return;
        }

        [RelayCommand]
        async void Last()
        {
            Position = 2;
            //PositionChanged(Position);
            OnPropertyChanged("Position");
            await Shell.Current.DisplayAlert("Warning","Last","OK");
            return;
        }

        void FilterItems(string filter)
        {
            var filteredItems = source.Where( entry => entry.ID.ToLower().Contains(filter.ToLower())).ToList();
            foreach (var entry in source)
            {
                if (!filteredItems.Contains(entry))
                {
                    Entries.Remove(entry);
                }
                else
                {
                    if (!Entries.Contains(entry))
                    {
                        Entries.Add(entry);
                    }
                }
            }
        }

        void ItemChanged(DiaryData item)
        {
            PreviousEntry = CurrentEntry;
            CurrentEntry = item;
            OnPropertyChanged("PreviousMonkey");
            OnPropertyChanged("CurrentMonkey");
        }

        void PositionChanged(int position)
        {
            PreviousPosition = CurrentPosition;
            CurrentPosition = position;
            OnPropertyChanged("PreviousPosition");
            OnPropertyChanged("CurrentPosition");
        }

        void RemoveMonkey(DiaryData monkey)
        {
            if (Entries.Contains(monkey))
            {
                Entries.Remove(monkey);
            }
        }

        /*
        void FavoriteMonkey(DiaryData monkey)
        {
            monkey.IsFavorite = !monkey.IsFavorite;
        }
        */

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
