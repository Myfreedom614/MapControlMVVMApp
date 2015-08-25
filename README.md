# MapControlMVVMApp
This sample tell you how to use MapControl control in a Windows 10 Universal App with MVVM pattern

##Trigger method from ViewModel

[MapControl.TrySetViewAsync()](https://msdn.microsoft.com/en-us/library/windows/apps/xaml/dn637062.aspx) method help us to set the view of the map displayed in the MapControl:

    private async void SetMapView(Geopoint point)  
    {  
      await MainMap.TrySetViewAsync(point, 15,0,0,Windows.UI.Xaml.Controls.Maps.MapAnimationKind.Bow);  
    }  

With the help of [MVVMLight Messenger](https://msdn.microsoft.com/en-us/magazine/JJ694937.aspx), we can trigger it from the viewmodel easily

Register the MainPage.xaml.cs to the default messenger provided by MVVMLight

    Messenger.Default.Register<Geopoint>(this, Constants.SetMapViewToken, SetMapView); 

Used a string Constants.SetMapViewToken as a token to identify the message and assigned SetMapView as an action

    internal class Constants
    {  
        public static string SetMapViewToken = "SetMapView";  
    }
