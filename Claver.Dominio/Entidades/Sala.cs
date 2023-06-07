using Claver.Dominio.Entidades.Base;

namespace Claver.Dominio.Entidades
{
    public class Sala : ClasseBase
    {
        public Sala()
        {
            Turmas = new HashSet<TurmaSala>();
        }
        public string NumeroSala { get; set; }
        // public int Capacidade { get; set; }
        public ICollection<TurmaSala> Turmas { get; set; }
    }
}