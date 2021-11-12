using AutoMapper;
using System.Collections.Generic;
using Teste.Application.DTOs;
using Teste.Application.Interfaces;
using Teste.Domain.Entities;
using Teste.Domain.Interfaces.Services;

namespace Teste.Application.Services
{
    public class BaseApp<TEntidade, TEntidadeDTO> : IBaseApp<TEntidade, TEntidadeDTO>
             where TEntidade : BaseEntitie
             where TEntidadeDTO : BaseDTO
    {
        protected readonly IBaseService<TEntidade> service;
        protected readonly IMapper iMapper;

        public BaseApp(IMapper iMapper, IBaseService<TEntidade> service)
        {
            this.iMapper = iMapper;
            this.service = service;
        }
        public int Create(TEntidadeDTO entidadeDTO)
        {
            return service.Create(iMapper.Map<TEntidade>(entidadeDTO));
        }

        public void Edit(TEntidadeDTO entidadeDTO)
        {
            service.Edit(iMapper.Map<TEntidade>(entidadeDTO));
        }

        public void Delete(int id)
        {
            service.Delete(id);
        }

        public void Delete(TEntidadeDTO entidadeDTO)
        {
            service.Delete(iMapper.Map<TEntidade>(entidadeDTO));
        }

        public IEnumerable<TEntidadeDTO> GetAll()
        {
            return iMapper.Map<IEnumerable<TEntidadeDTO>>(service.GetAll());
        }

        public TEntidadeDTO GetId(int id)
        {
            return iMapper.Map<TEntidadeDTO>(service.GetId(id));
        }

        public bool FindCpf(string cpf)
        {
            return iMapper.Map<TEntidadeDTO>(service.FindCpf(cpf)) != null;
        }

        public bool FindRa(string ra)
        {
            return iMapper.Map<TEntidadeDTO>(service.FindRa(ra)) != null;
        }
    }
}
