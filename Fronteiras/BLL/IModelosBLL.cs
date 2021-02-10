using Entidades;
using Fronteiras.DTOs;
using System.Collections.Generic;

namespace Fronteiras.BLL
{
    public interface IModelosBLL
    {
        Modelos Adicionar(ModelosDTO Modelo);
        List<ModelosDTO> ObterTodos();
        Modelos ObterPorId(int Modelo);
        bool Remover(int Modelo);
    }
}
