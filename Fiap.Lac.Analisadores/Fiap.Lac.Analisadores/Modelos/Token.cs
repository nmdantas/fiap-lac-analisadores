using System.Text.RegularExpressions;

namespace Fiap.Lac.Analisadores.Modelos
{
    public class Token
    {
        public string Nome { get; set; }

        public Regex Padrao { get; set; }

        public string Descricao { get; set; }

        public bool DeveEstarTabelaSimbolos { get; set; }
    }
}
