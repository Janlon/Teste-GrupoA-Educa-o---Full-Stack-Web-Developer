using System.Collections.Generic;
using Teste.Domain.Entities;
using Teste.Domain.Interfaces.Repositories;
using Teste.Domain.Interfaces.Services;

namespace Teste.Domain.Services
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntitie
    {
        protected readonly IBaseRepositorie<T> repositorie;

        public BaseService(IBaseRepositorie<T> repositorie)
        {
            this.repositorie = repositorie;
        }

        public int Create(T entitie)
        {
            return repositorie.Create(entitie);
        }

        public void Delete(int id)
        {
            repositorie.Delete(id);
        }

        public void Delete(T entitie)
        {
            repositorie.Delete(entitie);
        }

        public void Edit(T entitie)
        {
            repositorie.Edit(entitie);
        }

        public IEnumerable<T> GetAll()
        {
            return repositorie.GetAll();
        }

        public T GetId(int id)
        {
            return repositorie.GetId(id);
        }

        public bool FindCpf(string cpf)
        {
            return repositorie.FindCpf(cpf);
        }

        public bool FindRa(string ra)
        {
            return repositorie.FindRa(ra);
        }
    }
}
