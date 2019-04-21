using Fiap.Lac.Analisadores.Modelos;
using Fiap.Lac.Analisadores.Modelos.ES;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Fiap.Lac.Analisadores
{
    public class AnalisadorLexico : IAnalisador<AnalisadorLexicoEntrada, AnalisadorLexicoSaida>
    {
        private const string FormatoItemFluxoTokenComPosicao = "<{0},{1}>";
        private const string FormatoItemFluxoTokenSemPosicao = "<{0}>";

        private int controleAlocacaoMemoria;

        #region Propriedades 

        public IList<Simbolo> TabelaSimbolos { get; set; }

        public IList<string> FluxoTokens { get; set; }

        #endregion

        public AnalisadorLexico()
        {
            TabelaSimbolos = new List<Simbolo>();
            FluxoTokens = new List<string>();
        }

        private void AdicionarSimbolo(string lexema, Token token, Posicao posicao)
        {
            int indiceSimbolo = -1;
            Simbolo simbolo = new Simbolo(lexema, token);

            for (int i = 0; i < TabelaSimbolos.Count && token.DeveEstarTabelaSimbolos; i++)
            {
                if (TabelaSimbolos[i].Lexema == simbolo.Lexema &&
                    TabelaSimbolos[i].Token.Nome == simbolo.Token.Nome)
                {
                    indiceSimbolo = i;

                    break;
                }
            }

            // Simbolo ja adicionado na tabela
            if (indiceSimbolo > -1 && token.DeveEstarTabelaSimbolos)
            {
                simbolo = TabelaSimbolos[indiceSimbolo];

                simbolo.Ocorrencias.Add(posicao);
            }
            else if (token.DeveEstarTabelaSimbolos)
            {
                simbolo.Endereco = ++controleAlocacaoMemoria;
                simbolo.Ocorrencias.Add(posicao);

                TabelaSimbolos.Add(simbolo);
            }


            if (simbolo.Endereco > -1)
            {
                FluxoTokens.Add(string.Format(FormatoItemFluxoTokenComPosicao, token.Nome, simbolo.Endereco));
            }
            else
            {
                FluxoTokens.Add(string.Format(FormatoItemFluxoTokenSemPosicao, lexema));
            }
        }

        public AnalisadorLexicoSaida Processar(AnalisadorLexicoEntrada dadosEntrada, IList<Token> tokens)
        {
            using (StreamReader leitor = new StreamReader(dadosEntrada.ArquivoFonte.OpenRead()))
            {
                int numeroLinha = 0;
                int numeroColuna = 0;

                while (!leitor.EndOfStream)
                {
                    string linha = leitor.ReadLine();

                    for (int i = 0; i < linha.Length; i++)
                    {
                        char caracter = linha[i];

                        numeroColuna = i;

                        for (int j = 0; j < tokens.Count; j++)
                        {
                            Token token = tokens[j];

                            if (token.Padrao.IsMatch(caracter.ToString()))
                            {
                                int tamanhoLexema = 0;

                                while (token.Padrao.IsMatch(caracter.ToString()))
                                {
                                    i += 1;
                                    tamanhoLexema++;

                                    if (i < linha.Length)
                                    {
                                        caracter = linha[i];
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                                string lexema = linha.Substring(numeroColuna, tamanhoLexema);

                                AdicionarSimbolo(lexema, token, new Posicao(numeroLinha, numeroColuna));

                                i -= 1;

                                break;
                            }
                        }
                    }

                    numeroLinha++;
                }
            }

            AnalisadorLexicoSaida saida = new AnalisadorLexicoSaida(FluxoTokens, TabelaSimbolos);

            return saida;
        }
    }
}
