using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Fiap.Lac.Analisadores.Modelos.ES
{
    public class AnalisadorLexicoSaida
    {
        #region Constantes

        private const string FormatoNomeArquivoEntrada        = "{0}/{1}-entrada.txt";
        private const string FormatoNomeArquivoFluxoTokens    = "{0}/{1}-fluxo-tokens.txt";
        private const string FormatoNomeArquivoTabelaSimbolos = "{0}/{1}-tabela-simbolos.txt";
        private const string FormatoCabecalhoArquivoTabelaSimbolos =
@"+======================================================================================================+
| END     LEXEMA     TOKEN               PADRAO                              OCORRENCIA                |
+======================================================================================================+
";
        private const string FormatoSeparadorLinhaArquivoTabelaSimbolos = "+------------------------------------------------------------------------------------------------------+";
        private const string FormatoRodapeArquivoTabelaSimbolos = "+======================================================================================================+";

        private const int TamanhoCampoEndereco = 8;
        private const int TamanhoCampoLexema = 11;
        private const int TamanhoCampoToken = 20;
        private const int TamanhoCampoPadrao = 36;
        private const int TamanhoCampoOcorrencia = 25;

        #endregion

        public string Identificador { get; }

        public IList<string> FluxoTokens { get; internal set; }

        public IList<Simbolo> TabelaSimbolos { get; internal set; }

        public FileInfo ArquivoEntrada { get; internal set; }

        public FileInfo ArquivoFluxoTokens { get; internal set; }

        public FileInfo ArquivoTabelaSimbolos { get; internal set; }

        public AnalisadorLexicoSaida(IList<string> fluxoTokens, IList<Simbolo> tabelaSimbolos)
        {
            Identificador = Guid.NewGuid().ToString().Substring(0, 8);

            this.FluxoTokens = fluxoTokens;
            this.TabelaSimbolos = tabelaSimbolos;
        }

        public void GerarArquivos(FileInfo arquivoEntrada)
        {
            string diretorioRaiz = arquivoEntrada.DirectoryName;
            string nomeCompletoArquivoEntrada = string.Format(FormatoNomeArquivoEntrada, diretorioRaiz, Identificador);
            string nomeCompletoArquivoFluxoTokens = string.Format(FormatoNomeArquivoFluxoTokens, diretorioRaiz, Identificador);
            string nomeCompletoArquivoTabelaSimbolos = string.Format(FormatoNomeArquivoTabelaSimbolos, diretorioRaiz, Identificador);

            ArquivoEntrada = new FileInfo(nomeCompletoArquivoEntrada);
            ArquivoFluxoTokens = new FileInfo(nomeCompletoArquivoFluxoTokens);
            ArquivoTabelaSimbolos = new FileInfo(nomeCompletoArquivoTabelaSimbolos);

            arquivoEntrada.CopyTo(ArquivoEntrada.FullName);

            using (FileStream stream = ArquivoFluxoTokens.OpenWrite())
            {
                string fluxoCompleto = string.Join(" ", FluxoTokens);
                byte[] buffer = Encoding.UTF8.GetBytes(fluxoCompleto);

                stream.Write(buffer, 0, buffer.Length);
            }

            using (FileStream stream = ArquivoTabelaSimbolos.OpenWrite())
            {
                StringBuilder compositorSaida = new StringBuilder(FormatoCabecalhoArquivoTabelaSimbolos);

                for (int i = 0; i < TabelaSimbolos.Count; i++)
                {
                    string endereco = TabelaSimbolos[i].Endereco.ToString().PadRight(TamanhoCampoEndereco, ' ');
                    string lexema = TabelaSimbolos[i].Lexema.PadRight(TamanhoCampoLexema, ' ');
                    string token = TabelaSimbolos[i].Token.Nome.PadRight(TamanhoCampoToken, ' ');
                    string padrao = TabelaSimbolos[i].Token.Descricao.PadRight(TamanhoCampoPadrao, ' ');
                    string ocorrencia = string.Join(",", TabelaSimbolos[i].Ocorrencias).PadRight(TamanhoCampoOcorrencia, ' ');

                    compositorSaida.AppendLine(string.Format("| {0}{1}{2}{3}{4} |", endereco, lexema, token, padrao, ocorrencia));

                    // Nao é a ultima linha
                    if (i < TabelaSimbolos.Count - 1)
                    {
                        compositorSaida.AppendLine(FormatoSeparadorLinhaArquivoTabelaSimbolos);
                    }
                }

                compositorSaida.AppendLine(FormatoRodapeArquivoTabelaSimbolos);

                byte[] buffer = Encoding.UTF8.GetBytes(compositorSaida.ToString());

                stream.Write(buffer, 0, buffer.Length);
            }
        }
    }
}
