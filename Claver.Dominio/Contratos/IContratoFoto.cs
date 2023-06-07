using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Claver.Dominio.Contratos
{
    public interface IContratoFoto
    {
        Task<byte[]> BuscarFotoAsync(string filename);
        Task<string> SalvarFotoAsync(byte[] photo);
    }
}