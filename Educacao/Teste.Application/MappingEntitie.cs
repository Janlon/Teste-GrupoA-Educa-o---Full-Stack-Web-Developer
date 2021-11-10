using AutoMapper;
using Teste.Application.DTOs;
using Teste.Domain.Entities;

namespace Teste.Application
{
    public class MappingEntitie : Profile
    {
        public MappingEntitie()
        {
            //Aluno
            CreateMap<Aluno, AlunoDTO>();
            CreateMap<AlunoDTO, Aluno>();
        }
    }
}
