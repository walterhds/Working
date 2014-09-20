using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominio.Entidades;

namespace Working.ViewsModels
{
    public class ObjetoJob
    {
        public int Id { get; set; }
        public string DataRegistro { get; set; }
        public string Briefing { get; set; }
        public string Descricao { get; set; }
        public string Funcionario { get; set; }
        public string DataCriacao { get; set; }
        public string DataEstimativa { get; set; }
        public string DataEntrega { get; set; }
        public string HorasNecessarias { get; set; }
        public string Cliente { get; set; }
        public string SituacaoJob { get; set; }
        public IList<Peca> Peca { get; set; }
        public IList<Fornecedor> Fornecedor { get; set; } 
        public string Fase { get; set; }
        public string Nome { get; set; }
    }
}