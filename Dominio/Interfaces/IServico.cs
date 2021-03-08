using SistemaVenda.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Interfaces
{
    public interface IServico<TEntidade> where TEntidade: class
    {
        IEnumerable<TEntidade> Buscar();
        TEntidade Buscar(int id);
        void Criar(TEntidade cliente);
        void Deletar(int id);
    }
}
