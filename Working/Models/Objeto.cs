using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominio.Entidades;

namespace Working.Models
{
    public class Objeto
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string Cliente { get; set; }
        public SituacaoJob Situacao { get; set; }
        public string DataCriacao { get; set; }
        public string DataEstimativa { get; set; }
        public string HorasNecessarias { get; set; }
        public string QuantidadeHora { get; set; }
        public string Data { get; set; }
        public string Hora { get; set; }
        public string Descricao { get; set; }
        public string Alterado { get; set; }
        public string Telefone { get; set; }
        public string Contato { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public IList<Fornecedor> Fornecedor { get; set; }
        public int idFuncionario { get; set; }
        public int IdLogado { get; set; }
        public IList<Peca> Pecas { get; set; }
        public string Fase { get; set; }
        public bool TemAcesso { get; set; }
    }
}