using System;
using System.Collections.Generic;
using System.Text;

namespace SaveItUPX.Model
{
    public class Previsao
    {
        public class Tempo
        {
            public string Title { get; set; }

            public double Temperature { get; set; }

            public string Wind { get; set; }

            public string Humidity { get; set; }

            public string Description { get; set; }
            public string Icon { get; set; }
            public string Icon_url => string.Format("{0}{1}{2}", "https://openweathermap.org/img/wn/", Icon, "@4x.png");

            public Tempo()
            {
                // Como as Labels se vinculam a estes valores vamos defini-los 
                // como uma string vazia no construtor 
                this.Title = " ";
                this.Temperature = 0;
                this.Wind = " ";
                this.Humidity = " ";
                this.Description = " ";
                this.Icon = " ";
            }
        }
    }
}
