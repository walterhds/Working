using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominio.Entidades;

namespace Working.ViewsModels
{
    public class ObjetoJobRelatorios
    {
        public IList<Job> Jobs { get; set; } 
        public string PrimeiraData { get; set; }
        public string SegundaData { get; set; }
    }
}