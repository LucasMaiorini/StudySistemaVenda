using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Repositorio
{
    public interface IRepositorio<TEntidade> where TEntidade: class
    {
        /// <summary>
        /// Cria uma entidade no DB.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Entidade"></param>
        void Criar (TEntidade Entidade);

        /// <summary>
        /// Busca uma Entidade que corresponda ao ID especificado no argumento.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntidade Buscar(int id);

        /// <summary>
        /// Exclui do DB uma entidade que corresponda ao ID especificado no argumento.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        void Deletar(int id);

        /// <summary>
        /// Retorna uma lista com todas as Entidades do DB.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<TEntidade> Buscar();
    }
}
