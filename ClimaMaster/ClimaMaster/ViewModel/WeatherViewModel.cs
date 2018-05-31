using ClimaMaster.Model;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ClimaMaster.ViewModel
{
      public class WeatherViewModel:Notificable
    {
        #region Atributos
        private string ubicacion;
        private string pais;
        private string resultTerm;
        private string region;
        private string ultimaActualizacion;
        private string clima;
        private string tempereatura;
        private ImageSource imagen;
        #endregion

   

        #region Propiedades
        public string Ubicacion
        {
            get
            {
                return ubicacion;
            }
            set
            {
                SetValue(ref ubicacion, value);
            }
        }

        private void SetValue(ref string ubicacion, string value)
        {
            throw new NotImplementedException();
        }

        public string Pais
        {
            get
            {
                return pais;
            }
            set
            {
                SetValue(ref pais, value);
            }
        }
        public string ResultTerm
        {
            get
            {
                return resultTerm;
            }
            set
            {
                SetValue(ref resultTerm, value);
            }
        }
        public string Region
        {
            get
            {
                return region;
            }
            set
            {
                SetValue(ref region, value);
            }
        }
        public string UltimaActualizacion
        {
            get
            {
                return ultimaActualizacion;
            }
            set
            {
                SetValue(ref ultimaActualizacion, value);
            }
        }
        public string Clima
        {
            get
            {
                return clima;
            }
            set
            {
                SetValue(ref clima, value);
            }
        }
        public string Temperatura
        {
            get
            {
                return tempereatura;
            }
            set
            {
                SetValue(ref tempereatura, value);
            }
        }
        public ImageSource Image
        {
            get
            {
                return imagen;
            }
            set
            {
                SetValue(ref imagen, value);
            }
        }


        #endregion

        private void SetValue(ref ImageSource imagen, ImageSource value)
        {
            throw new NotImplementedException();
        }

        #region Comandos
        public ICommand BuscarCommand
        {
            get
            {
                return new RelayCommand(Buscar);
            }
        }

        private async void Buscar()
        {
            HttpClient Cliente = new HttpClient();
            Cliente.BaseAddress = new Uri(ObtenerUrl());
            var response = await Cliente.GetAsync(Cliente.BaseAddress);
            response.EnsureSuccessStatusCode();
            var jsonResult = response.Content.ReadAsStringAsync().Result;
            var Weathermodel = Weather.FromJson(jsonResult);

            FijarValores(Weathermodel);
        }

        private void FijarValores(Weather weathermodel)
        {
            Ubicacion = weathermodel.Query.Results.Channel.Location.City;
            pais = weathermodel.Query.Results.Channel.Location.Country;
            Region = weathermodel.Query.Results.Channel.Location.Region;
            UltimaActualizacion = weathermodel.Query.Results.Channel.Item.Condition.Date;
            clima = weathermodel.Query.Results.Channel.Description;
            var code = weathermodel.Query.Results.Channel.Item.Condition.Text;
            var imglink = $"http://l.yimg.com/a/i/us/we/52/29{code}.gif" ;
            Image = ImageSource.FromUri(new Uri(imglink));

        }

        private string ObtenerUrl()
        {   
            string serviceUrl = $"https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22{ResultTerm}%22)&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";
            return serviceUrl;
        }
        #endregion
        #region Constructores

        public WeatherViewModel()
        {

        }
        #endregion
    }
}
