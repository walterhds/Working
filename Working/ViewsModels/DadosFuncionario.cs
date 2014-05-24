using System.Linq;
using Dominio.Entidades;
using System.Collections.Generic;
using Percistencia.Ado;

namespace Working.ViewsModels
{
    public class DadosFuncionario
    {
        public Funcionario Funcionario { get; set; }
        public IList<Cargo> ListaCargo { get; set; }
        public IEnumerable<Situacao> Situacao { get; set; }
    }
}