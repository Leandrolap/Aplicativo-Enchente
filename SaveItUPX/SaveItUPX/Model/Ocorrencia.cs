using System;
using System.Collections.Generic;
using System.Text;

namespace SaveItUPX.Model
{
    public class Ocorrencia
    {
        public string Id { get; set; }
        public string Rota { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Data { get; set; }
        public string Tipo { get; set; }
        public string UsuarioPost { get; set; }
    }
}
