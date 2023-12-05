//using Android.Database;
//using AndroidX.Activity;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
//using Xamarin.Google.Crypto.Tink.Subtle;

namespace DiaryBase
{
    [QueryProperty("Text", "Text")]
    public partial class DetailViewModel : ObservableObject
    {
        [ObservableProperty]
        string text;

        //[ObservableProperty]
        //public string ID
        public string ID { get; private set; }
        public string Title { get; private set; }
        public string Entry { get; private set; }
        public string Date { get; private set; }
        public string Image { get; private set; }
        public string Mood { get; private set; }
        public string Commentary { get; private set; }

        public DetailViewModel()
        {
            if(Text != null)
            {
                if(!DecryptQuery(Text))
                {
                    Shell.Current.DisplayAlert("Error", "Parsing", "OK");
                }
            }
        }

        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

        bool DecryptQuery(string sInput)
        {
            bool bReturn = false;
            if (!String.IsNullOrEmpty(sInput))
            {
                byte[] byteVal = Convert.FromBase64String(sInput);
                string sRaw = byteVal.ToString();
                if (sRaw != null && sRaw.Length > 0)
                {
                    DiaryData dData = (DiaryData)sRaw;
                    if (dData != null)
                    {
                        ID = dData.ID;
                        Title = dData.Title;
                        Entry = dData.Entry;
                        Date = dData.Date;
                        Image = dData.Image;
                        Mood = dData.Mood;
                        if(!bReturn) bReturn = true;
                    }
                }
            }
            return bReturn;
        }
    }
}
