using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominio.Entidades;

namespace Working.ViewsModels
{
    public class DadosContasPagarIndex
    {
        public IList<ContasPagar> ContasPagar { get; set; } 
        public IList<ParcelasPagar> ParcelasPagar { get; set; } 
    }
}