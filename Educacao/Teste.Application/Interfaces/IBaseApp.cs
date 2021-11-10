using System.Collections.Generic;
using Teste.Application.DTOs;
using Teste.Domain.Entities;

namespace Teste.Application.Interfaces
{
    public interface IBaseApp<TEntitie, TEntitieDTO> where TEntitie : BaseEntitie where TEntitieDTO : BaseDTO
    {
        int Create(TEntitieDTO entitieDTO);
        void Delete(int id);
        void Delete(TEntitieDTO entitieDTO);
        void Edit(TEntitieDTO entitieDTO);
        TEntitieDTO GetId(int id);
        IEnumerable<TEntitieDTO> GetAll();
    }
}

