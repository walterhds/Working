using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Working.Models
{
    public class ObjetoFuncionarioDisponibilidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string HorasDisponiveis { get; set; }
        public double HorasDouble { get; set; }
        public string PrimeiraData { get; set; }
        public string SegundaData { get; set; }
    }
}