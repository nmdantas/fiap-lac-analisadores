using System.Collections.Generic;

namespace Fiap.Lac.Analisadores.Modelos
{
    public class Simbolo
    {
        public int Endereco { get; set; }

        public string Lexema { get; set; }

        public Token Token { get; set; }

        public IList<Posicao> Ocorrencias { get; set; }

        public Simbolo()
        {
            Endereco = -1; // Indica que nao ha alocacao de memoria
            Ocorrencias = new List<Posicao>();
        }

        public Simbolo(string lexema, Token token) : this()
        {
            this.Lexema = lexema;
            this.Token = token;
        }
    }
}
