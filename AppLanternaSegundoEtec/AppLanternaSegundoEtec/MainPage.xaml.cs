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

            Carrega_Info_Bateria();
        }

        private void btnOnOff_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (lanterna_ligada)
                {
                    // desligar
                    btnOnOff.Source = ImageSource.FromResource("AppLanternaSegundoEtec.Image.botao-desligado.jpg");
                    lanterna_ligada = false;

                    Flashlight.TurnOffAsync();
                    Vibration.Vibrate(TimeSpan.FromMilliseconds(300));

                }
                else
                {
                    // ligar
                    btnOnOff.Source = ImageSource.FromResource("AppLanternaSegundoEtec.Image.botao-ligado.jpg");
                    lanterna_ligada = true;

                    Flashlight.TurnOnAsync();
                    Vibration.Vibrate(TimeSpan.FromMilliseconds(300));
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Ops", ex.Message, "OK :(");
            }
        }

        private void Carrega_Info_Bateria()
        {
            try
            {
                if (CrossBattery.IsSupported)
                {
                    CrossBattery.Current.BatteryChanged -= Mudanca_Status_Bateria;
                    CrossBattery.Current.BatteryChanged += Mudanca_Status_Bateria;

                }
                else
                {
                    throw new Exception("Sem permissão para dados da bateria.");
                }

            }
            catch (Exception ex)
            {
                DisplayAlert("Ops", ex.Message, "Ok");
            }
        }


        private void Mudanca_Status_Bateria(object sender, Plugin.Battery.Abstractions.BatteryChangedEventArgs e)
        {
            // O que mudou?
            // Carga Remancente d Bateria
            // Status da Bateria
            // Fonte de Energia
            try
            {
                lbl_carga.Text = e.RemainingChargePercent.ToString() + "%";

                // Escolha - Caso

                // Qual é o status da bateria? Carregando? Descarregando?
                switch (e.Status)
                {
                    case Plugin.Battery.Abstractions.BatteryStatus.Charging:
                        lbl_status.Text = "Carregando";
                        break;

                    case Plugin.Battery.Abstractions.BatteryStatus.Discharging:
                        lbl_status.Text = "Descarregando";
                        break;

                    case Plugin.Battery.Abstractions.BatteryStatus.Full:
                        lbl_status.Text = "Carregada";
                        break;

                    case Plugin.Battery.Abstractions.BatteryStatus.NotCharging:
                        lbl_status.Text = "Sem Carregar";
                        break;

                    case Plugin.Battery.Abstractions.BatteryStatus.Unknown:
                        lbl_status.Text = "Desconhecido";
                        break;
                }

                // Qual é a fonte de energia da bateria?
                switch(e.PowerSource)
                {
                    case Plugin.Battery.Abstractions.PowerSource.Ac:
                        lbl_fonte.Text = "Carregador";
                        break;

                    case Plugin.Battery.Abstractions.PowerSource.Battery:
                        lbl_fonte.Text = "Bateria";
                        break;

                    case Plugin.Battery.Abstractions.PowerSource.Usb:
                        lbl_fonte.Text = "USB";
                        break;

                    case Plugin.Battery.Abstractions.PowerSource.Wireless:
                        lbl_fonte.Text = "Sem fio";
                        break;

                    case Plugin.Battery.Abstractions.PowerSource.Other:
                        lbl_fonte.Text = "Outro";
                        break;
                }
                

            }
            catch (Exception ex)
            {
                DisplayAlert("Ops", ex.Message, "Ok");
            }
        }



    }
}
