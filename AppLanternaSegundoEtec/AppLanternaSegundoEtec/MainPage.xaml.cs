using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using Plugin.Battery;

namespace AppLanternaSegundoEtec
{
    public partial class MainPage : ContentPage
    {
        bool lanterna_ligada = false;

        public MainPage()
        {
            InitializeComponent();

            btnOnOff.Source = ImageSource.FromResource("AppLanternaSegundoEtec.Image.botao-desligado.jpg");
        }

        private void btnOnOff_Clicked(object sender, EventArgs e)
        {
            try
            {
                if(lanterna_ligada)
                {
                    // desligar
                    btnOnOff.Source = ImageSource.FromResource("AppLanternaSegundoEtec.Image.botao-desligado.jpg");
                    lanterna_ligada = false;

                    Flashlight.TurnOffAsync();
                    Vibration.Vibrate(TimeSpan.FromMilliseconds(300));

                } else
                {
                    // ligar
                    btnOnOff.Source = ImageSource.FromResource("AppLanternaSegundoEtec.Image.botao-ligado.jpg");
                    lanterna_ligada = true;

                    Flashlight.TurnOnAsync();
                    Vibration.Vibrate(TimeSpan.FromMilliseconds(300));
                }             
            } catch(Exception ex)
            {
                DisplayAlert("Ops", ex.Message, "OK :(" );
            }
        }
    }
}
