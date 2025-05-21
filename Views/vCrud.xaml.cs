using System.Collections.ObjectModel;
using jdelgadoS6A.Models;
using Newtonsoft.Json;

namespace jdelgadoS6A.Views;

public partial class vCrud : ContentPage
{
    private const string URL = "http://localhost:8082/booking/";
    private HttpClient client = new HttpClient();
    private ObservableCollection<Booking> bookingTemp;



    public vCrud()
    {
        InitializeComponent();
        mostarBookig();
    }

    public async void mostarBookig()
    {
        var content = await client.GetStringAsync(URL + "views");
        List<Booking> lista = JsonConvert.DeserializeObject<List<Booking>>(content);
        bookingTemp = new ObservableCollection<Booking>(lista);
        lvBooking.ItemsSource = bookingTemp;


    }

    private void btnInsertar_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new vAñadirBooking());
    }

    private void lvBooking_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var objBooking = (Booking)e.SelectedItem;
        Navigation.PushAsync(new vActEli(objBooking));
    }
}