using AutoMapper;
using Claver.Dominio.DTOS.CursoDTOs;
using Claver.Dominio.DTOS.InscricaoDTOs;
using Claver.Dominio.Entidades;

namespace Claver.Aplicacao.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            // CreateMap<Inscricao, AddCandidatoDTO>().ReverseMap()
        }
    }
}