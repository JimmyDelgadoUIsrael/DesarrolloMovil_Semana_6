using System.Text;
using Newtonsoft.Json;

namespace jdelgadoS6A.Views;

public partial class vAñadirBooking : ContentPage
{
    public vAñadirBooking()
    {
        InitializeComponent();
    }
    private async void btnGuardar_Clicked(object sender, EventArgs e)
    {
        try
        {
            HttpClient client = new HttpClient();


            var parametros = new
            {
                name = txtNombre.Text,
                descripcion = txtDescripcion.Text,
                estado = chkEstado.IsChecked ? true : false
            };
            string jsonString = JsonConvert.SerializeObject(parametros);

            HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("http://localhost:8082/booking/add", content);

            string result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);

            Navigation.PushAsync(new vCrud());


        }
        catch (Exception)
        {
            DisplayAlert("Error", "No se ha podido añadir el booking", "OK");
            throw;
        }
    }
}