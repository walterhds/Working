using System.ComponentModel;

namespace Dominio.Entidades
{
    public enum Situacao
    {
        Ativo,
        [Description("Férias")]
        Ferias,
        [Description("Licença Maternidade")]
        LicencaMaternidade,
        [Description("Licença Paternidade")]
        LicencaPaternidade,
        Desligado,
        [Description("Atestado Médico")]
        AtestadoMedico
    }
}
