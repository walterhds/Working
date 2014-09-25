using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominio.Entidades;

namespace Working.ViewsModels
{
    public class DadosContratoIndex
    {
        public IList<Contrato> Contratos { get; set; } 
        public IList<AlteracaoContrato> Alteracoes { get; set; } 
    }
}