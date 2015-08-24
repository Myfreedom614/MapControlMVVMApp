using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI.Popups;

namespace MapControlMVVMApp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public const string MyLocationPropertyName = "MyLocation";

        private Geoposition _myLocation = null;

        public Geoposition MyLocation
        {
            get
            {
                return _myLocation;
            }

            set
            {
                if (_myLocation == value)
                {
                    return;
                }


                _myLocation = value;
                RaisePropertyChanged(MyLocationPropertyName);
            }
        }

        public const string GetMyLocationCommandPropertyName = "GetMyLocationCommand";

        private RelayCommand _getMyLocationCommand = null;

        public RelayCommand GetMyLocationCommand
        {
            get
            {
                return _getMyLocationCommand;
            }

            set
            {
                if (_getMyLocationCommand == value)
                {
                    return;
                }


                _getMyLocationCommand = value;
                RaisePropertyChanged(GetMyLocationCommandPropertyName);
            }
        }

        public MainViewModel()
        {
            GetMyLocationCommand = new RelayCommand(GetMyLocation);
        }

        private async void ToggleProgressBar(bool toggle, string message = "")
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                var statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
                
                if (toggle)
                {
                    statusBar.ProgressIndicator.Text = message;
                    await statusBar.ProgressIndicator.ShowAsync();
                }
                else
                {
                    await statusBar.ProgressIndicator.HideAsync();
                }
            }
            
        }

        private async void GetMyLocation()
        {
            try
            {
                ToggleProgressBar(true, "Getting your location");

                Geolocator locator = new Geolocator();

                var location = await locator.GetGeopositionAsync(TimeSpan.FromMilliseconds(1), TimeSpan.FromSeconds(60));
                MyLocation = location;

                Messenger.Default.Send<Geopoint>(MyLocation.Coordinate.Point, Constants.SetMapViewToken);

                ToggleProgressBar(false);
            }
            catch (Exception ex)
            {
                MessageDialog Dialog = new MessageDialog("Location detection failed");
                Dialog.ShowAsync();
            }
        }
    }
}
