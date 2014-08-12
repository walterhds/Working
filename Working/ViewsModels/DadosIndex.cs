using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominio.Entidades;

namespace Working.ViewsModels
{
    public class DadosIndex
    {
        public IList<Compromisso> Compromissos { get; set; }
        public IList<Job> Jobs { get; set; }
        public Funcionario Funcionario { get; set; }
    }
}