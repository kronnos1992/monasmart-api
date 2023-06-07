using Claver.Dominio.Contratos.Geral;
using Claver.Dominio.DTOS.AnoLectivo;
using Claver.Dominio.Entidades;

namespace Claver.Aplicacao.Implemetancao;

public class ImplementarAnoLectivo
{
    private readonly IUnidadeTrabalho _ut;
    // variaveis necessarias
    DateTime _dataAtual = DateTime.Now;
    // private readonly IMapper _mapper;
    public ImplementarAnoLectivo( IUnidadeTrabalho ut)
    {
        _ut = ut;
        // _mapper = mapper;
    }

    // inserir novo candidato
    #region Inserir AnoLectivo   
    public async Task<AnoLectivo> Adicionar(AddAnoLectivoDTO anoLectivo)
    {
    
        // mapeamento
        var _AnoLectivo = new AnoLectivo{
            Id = Guid.NewGuid(),
            AnoAcademico= anoLectivo.AnoAcademico.Trim().ToUpper(),
            Ativo = anoLectivo.Ativo,
            DataRegistro = _dataAtual,
            DataRevisao = null
        };
        // tratamento de erros
        var AnoLectivos = await _ut.AnoLectivo.BuscarAnoLectivo(anoLectivo.AnoAcademico);
        // Nome nao pode ser nulo
        if ( anoLectivo.AnoAcademico== string.Empty  || anoLectivo.AnoAcademico== null || anoLectivo.AnoAcademico== "string" || anoLectivo.AnoAcademico== "STRING")
        {
            throw new NullReferenceException("AnoLectivo mal inserido, inseira o AnoLectivo como deve ser.");
        }
        else{
            // verificar se o AnoLectivo ja exisate
            try
            {
                var _anoLectivos = await this.ExibirPorNome(anoLectivo.AnoAcademico.Trim().ToUpper());
                if (_anoLectivos.Count() > 0)
                {
                    throw new NullReferenceException("Erro N. ICM-0002: A AnoLectivo que tentou inserir ja existe.");
                }
                else{
                    return await _ut.AnoLectivo.Inserir(_AnoLectivo);
                }
            }
            catch (Exception ex)
            {
                throw new NullReferenceException(ex.Message);
            }
        }  
    
    }

    #endregion
    
    // Exibir por nome
    #region Exibir por nome 
        public async Task<IEnumerable<AnoLectivo>> ExibirPorNome(string nome)
        {
            
            try
            {
                var AnoLectivo =await _ut.AnoLectivo.BuscarAnoLectivo(nome.Trim().ToUpper());
                return AnoLectivo.ToList(); 
            }
            catch (System.Exception)
            {
                throw new ArgumentNullException("Nenhum registro encontrado.");
            }
        }        
    #endregion  

    // Exibir todos AnoLectivos
    #region Exibir tudo
        public async Task<IEnumerable<AnoLectivo>> ExibirTudo()
        {
            try
            {
                var anoLectivo =await _ut.AnoLectivo.BuscarTodos();
                return anoLectivo.ToList(); 
            }
            catch (System.Exception)
            {
                throw new NullReferenceException("Nenhum registro encontrado.");
            }
        }
    #endregion 

    // atualizar AnoLectivo
    #region Atualizar AnoLectivo
        public async Task<AnoLectivo> Atualizar(UpdateAnoLectivoDTO anoLectivo)
        {
            // AnoLectivo a ser atualizado 
            var buscarAnoLectivo =await  _ut.AnoLectivo.BuscarPorId(anoLectivo.Id);

            if (buscarAnoLectivo == null)
            {
                throw new NullReferenceException("AnoLectivo nao encontrado, por favor cadastre.");
            }

            buscarAnoLectivo.DataRevisao = _dataAtual;
            buscarAnoLectivo.AnoAcademico= anoLectivo.AnoAcademico.Trim().ToUpper();
            buscarAnoLectivo.Ativo = anoLectivo.Ativo;

            // Nome nao pode ser nulo
            if ( buscarAnoLectivo.AnoAcademico.Trim().ToUpper()== string.Empty  
            || buscarAnoLectivo.AnoAcademico.Trim().ToUpper()== null 
            || buscarAnoLectivo.AnoAcademico.Trim().ToUpper()== "string" || buscarAnoLectivo.AnoAcademico.Trim().ToUpper()== "STRING")
            {
                throw new NullReferenceException("Erro N. ICM-0001: AnoLectivo mal inserido, inseira o AnoLectivo como deve ser.");
            }
            else{
                try
                {
                    return await _ut.AnoLectivo.Atualizar(buscarAnoLectivo);
                }
                catch (System.Exception ex)
                {
                    throw new NullReferenceException(ex.Message);
                }
            }  
        }
    #endregion

    // eliminar AnoLectivo
    #region Eliminar AnoLectivo
        public async Task<bool> Eliminar(Guid id)
        {
            // AnoLectivo a ser atualizado 
            var buscarAnoLectivo =await  _ut.AnoLectivo.BuscarPorId(id);

            if (buscarAnoLectivo == null)
            {
                throw new NullReferenceException("AnoLectivo nao encontrado, por favor cadastre.");
            }
            // verificar se o nome ja exisate
            try
            {
                await _ut.AnoLectivo.Eliminar(id);
                return true;
            }
            catch (System.Exception ex)
            {
                throw new NullReferenceException(ex.Message);
            } 
        }
    #endregion

    // Salvar no banco
    #region Salvar no Banco
        public async Task<int> Persistir()
        {
            return await _ut.Finalizar();
        }
    #endregion  
}