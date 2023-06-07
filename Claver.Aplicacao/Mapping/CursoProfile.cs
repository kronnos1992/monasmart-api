using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Claver.Dominio.DTOS.CursoDTOs;
using Claver.Dominio.Entidades;

namespace Claver.Aplicacao.Mapping
{
    public class CursoProfile:Profile
    {
        public CursoProfile()
        {
            // Mapping Curso informations
            CreateMap<AddCursoDTO, Curso>()
                .ForMember(dest => dest.NomeCurso, map => map.MapFrom(
                    src => src.NomeCurso.Trim().ToUpper()
                ))
                 .ForMember(dest => dest.Sigla, map => map.MapFrom(
                    src => src.Sigla.Trim().ToUpper()
                ))
                .ReverseMap();

            CreateMap<Curso, GetCursoDTO>()
                .ForMember(dest => dest.NomeCurso, map => map.MapFrom(
                    src => src.NomeCurso
                ))
                .ForMember(dest => dest.Sigla, map => map.MapFrom(
                    src => src.Sigla
                ))
                 .ForMember(dest => dest.DataRegistro, map => map.MapFrom(
                    src => src.DataRegistro
                ))
                 .ForMember(dest => dest.DataRevisao, map => map.MapFrom(
                    src => src.DataRevisao
                ))
                .ReverseMap(); 

            // Mapping Curso informations
            CreateMap<AtualizarCursoDTO, Curso>()
                .ForMember(dest => dest.NomeCurso, map => map.MapFrom(
                    src => src.NomeCurso.Trim().ToUpper()
                ))
                .ForMember(dest => dest.Sigla, map => map.MapFrom(
                    src => src.Sigla.Trim().ToUpper()
                ))
                .ReverseMap();

                   
            
        }
    }
}