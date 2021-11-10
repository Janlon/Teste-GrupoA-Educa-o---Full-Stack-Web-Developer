using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Teste.Domain.Entities;
using Teste.Domain.Interfaces.Repositories;
using Teste.Infra.Data.Contexts;

namespace Teste.Infra.Data.Repositories
{
    public class BaseRepositorie<T> : IBaseRepositorie<T> where T : BaseEntitie
    {
        public Context Context { get; set; }
        public BaseRepositorie(Context context)
        {
            Context = context;
        }
        public int Create(T entitie)
        {
            Context.InitTransacao();
            Context.Set<T>().Add(entitie);
            Context.Entry(entitie).State = EntityState.Added;
            Context.SendChanges();
            return entitie.Id;
        }
        public void Delete(int id)
        {
            T entitie = GetId(id);
            if (entitie != null)
            {
                Context.InitTransacao();
                Context.Set<T>().Remove(entitie);
                Context.SendChanges();
            }
        }
        public void Delete(T entitie)
        {
            Context.InitTransacao();
            Context.Set<T>().Remove(entitie);
            Context.SendChanges();
        }
        public void Edit(T entitie)
        {
            Context.InitTransacao();
            Context.Set<T>().Attach(entitie);
            Context.Entry(entitie).State = EntityState.Modified;
            Context.SendChanges();
        }
        public IEnumerable<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }
        public T GetId(int id)
        {
            return Context.Set<T>().Find(id);
        }
    }
}
