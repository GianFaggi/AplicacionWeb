using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;


        namespace AplicacionWeb.Models
    {
        public class JugadoresCLS
        {
            public int id { get; set; }
            [DisplayName("Nombre")]
            public string name { get; set; }
            [DisplayName("Apellido")]
            public string lastName { get; set; }
            [DisplayName("Equipo")]
            public string team { get; set; }
            [DisplayName("Posicion")]
            public string position { get; set; }

        }

    }


