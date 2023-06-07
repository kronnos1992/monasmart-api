using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Claver.Dominio.Contratos.Geral;
using Claver.Dominio.DTOS.AlunoDTO;
using Claver.Dominio.Entidades;

namespace Claver.Aplicacao.Implemetancao
{
    public class ImplementarMatricula
    {
        private readonly IUnidadeTrabalho _ut;
        private readonly IMapper _mapper;
        DateTime _dataAtual = DateTime.Now;
        //string _regex = @"^[0-9]{9}[a-zA-Z]{2}[0-9]{3}$";
        int i = 0;

        public ImplementarMatricula( IUnidadeTrabalho ut, IMapper mapper)
        {
            _ut = ut;
            _mapper = mapper;
        }

        public async Task Matricular(AddAlunoDTO matricula, Guid candidato, Guid anoLectivo, Guid turma)
        {
            try
            {
                // var _candidato = await _ut.Inscricao.BuscarPorId(candidato);
                var _anoLectivo = await _ut.AnoLectivo.BuscarPorId(anoLectivo);
                var _turma = await _ut.TurmaSala.BuscarPorId(turma);

                // if (_candidato == null)
                //     throw new NullReferenceException("Candidato não encontrado");

                if (_turma == null)
                    throw new NullReferenceException("Turma não encontrada");

                if (_anoLectivo == null)
                    throw new NullReferenceException("Ano lectivo não encontrado");

                var _matricula= new Matricula{
                    AnoLectivo = _anoLectivo,
                    NumeroOrdem = i + 1,
                    Turma = _turma,
                    DataRegistro = _dataAtual,
                    Aluno = new Aluno{ 
                        NumeroCartao = matricula.NumeroCartao,
                        NumeroProcesso = matricula.NumeroProcesso,
                        // Inscricao = _candidato,
                        DataRegistro = _dataAtual
                    },                       
                };

                if (_matricula.NumeroOrdem == _turma.Turma.TotalTurma)
                    throw new Exception("Esta turma ja esá totalmente preenchida.");
                await _ut.Matriculas.Inserir(_matricula);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    
        public async Task<int> Persistir()
        {
            return await _ut.Finalizar();
        }
    }
}