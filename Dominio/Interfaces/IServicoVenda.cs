﻿using SistemaVenda.Dominio.DTO;
using SistemaVenda.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Interfaces
{
    public interface IServicoVenda : IServico<Venda>
    {
        IEnumerable<GraficoViewModel> ListaGrafico();
    }
}
