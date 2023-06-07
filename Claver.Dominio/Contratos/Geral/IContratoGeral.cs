namespace Claver.Dominio.Contratos.Geral
{
    public interface IContratoGeral<T> where T: class
    {
        Task<T>Atualizar(T entidade);
        Task Eliminar(Guid id);
        Task<T>Inserir(T entidade);
        Task<IEnumerable<T>>Listar();
        Task<T>Listar(Guid id);
    }
}