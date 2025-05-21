using System.Text;
using jdelgadoS6A.Models;
using Newtonsoft.Json;

namespace jdelgadoS6A.Views;

public partial class vActEli : ContentPage
{
    public vActEli(Booking datos)
    {
        InitializeComponent();
        txtId.Text = datos.id.ToString();
        txtNombre.Text = datos.name;
        txtDescripcion.Text = datos.descripcion;
        chkEstado.IsChecked = datos.estado;
    }

    private const string URL = "http://localhost:8082/booking/";
    private HttpClient client = new HttpClient();



    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {
        try
        {

            var parametros = new
            {
                id = txtId.Text,
                name = txtNombre.Text,
                descripcion = txtDescripcion.Text,
                estado = chkEstado.IsChecked ? true : false
            };
            string jsonString = JsonConvert.SerializeObject(parametros);
            HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(URL + "update", content);

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                Navigation.PushAsync(new vCrud());
                DisplayAlert("Éxito", "Booking actualizado correctamente", "OK");
            }
            else
            {
                DisplayAlert("Error", "No se ha podido actualizar el booking", "OK");
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", "No se ha podido actualizar el booking: " + ex.Message, "OK");
        }
    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        try
        {
            string bookingId = txtId.Text;
            if (string.IsNullOrEmpty(bookingId))
            {
                DisplayAlert("Error", "Por favor, ingrese un ID de booking", "OK");
                return;
            }

            HttpResponseMessage response = await client.DeleteAsync(URL + "delete/" + bookingId);

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                Navigation.PushAsync(new vCrud());
                DisplayAlert("Éxito", "Booking eliminado correctamente", "OK");
            }
            else
            {
                DisplayAlert("Error", "No se ha podido eliminar el booking", "OK");
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", "No se ha podido eliminar el booking: " + ex.Message, "OK");
        }
    }

}