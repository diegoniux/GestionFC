using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace GestionFC.ViewModels.Share
{
    public class ViewModelBase: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handle = PropertyChanged;
            if (handle != null)
                handle(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task<string> getLocation()
        {
            string response = string.Empty;
            try
            {
                var location = await Geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    //Timeout = TimeSpan.FromSeconds(30)
                });
                response = $"{location.Latitude}, {location.Longitude}";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }


    }
}
