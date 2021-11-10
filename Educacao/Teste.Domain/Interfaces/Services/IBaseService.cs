﻿using System.Collections.Generic;
using Teste.Domain.Entities;

namespace Teste.Domain.Interfaces.Services
{
    public interface IBaseService<T> where T : BaseEntitie
    {
        int Create(T entitie);
        void Delete(int id);
        void Delete(T entitie);
        void Edit(T entitie);
        T GetId(int id);
        IEnumerable<T> GetAll();
    }
}
