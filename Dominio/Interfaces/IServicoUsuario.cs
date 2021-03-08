using SistemaVenda.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Interfaces
{
    public interface IServicoUsuario: IServico<Usuario>
    {
        bool ValidarLogin(string email, string senha);
    }
}
