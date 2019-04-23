using Fiap.Lac.Analisadores;
using Fiap.Lac.Analisadores.Modelos;
using Fiap.Lac.Analisadores.Modelos.ES;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Fiap.Lac.Apresentacao
{
    class Program
    {
        #region :: Constantes ::

        private static readonly string Welcome =
@" __/\\\\\\\\\\\\\\\__/\\\\\\\\\\\_____/\\\\\\\\\_____/\\\\\\\\\\\\\___        
| _\/\\\///////////__\/////\\\///____/\\\\\\\\\\\\\__\/\\\/////////\\\_       
|  _\/\\\_________________\/\\\______/\\\/////////\\\_\/\\\_______\/\\\_      
|   _\/\\\\\\\\\\\_________\/\\\_____\/\\\_______\/\\\_\/\\\\\\\\\\\\\/__     
|    _\/\\\///////__________\/\\\_____\/\\\\\\\\\\\\\\\_\/\\\/////////____    
|     _\/\\\_________________\/\\\_____\/\\\/////////\\\_\/\\\_____________   
|      _\/\\\_________________\/\\\_____\/\\\_______\/\\\_\/\\\_____________  
|       _\/\\\______________/\\\\\\\\\\\_\/\\\_______\/\\\_\/\\\_____________ 
|        _\///______________\///////////__\///________\///__\///______________
|         _____________________________________________________________________
|        |                                                                     | 
|________|                                4-ECR                                |
  _______|                                                                     |
   ______|                                 LAC                                 |
    _____|                                                                     |
     ____|                  PROFESSOR FABIO HENRIQUE PIMENTEL                  |
      ___|                  ---------------------------------                  |
       __|                  ------  ANALISADOR LEXICO  ------                  |
        _|                  ---------------------------------                  |
         |_____________________________________________________________________|

";
        private static readonly IList<Token> tokens = new List<Token>()
        {
            new Token() { Nome = "number", Descricao = "constantes numericas", Padrao = new Regex("[0-9]"), DeveEstarTabelaSimbolos = true },
            new Token() { Nome = "operator", Descricao = "operadores aritmeticos", Padrao = new Regex("[+|\\-|*|/]"), DeveEstarTabelaSimbolos = true },
            new Token() { Nome = "assignment", Descricao = "atribuicao de valores", Padrao = new Regex("[=]"), DeveEstarTabelaSimbolos = false },
            new Token() { Nome = "end_of_statement", Descricao = "fim de instrucao", Padrao = new Regex("[;]"), DeveEstarTabelaSimbolos = false },
            new Token() { Nome = "open_parentheses", Descricao = "abertura parenteses", Padrao = new Regex("[(]"), DeveEstarTabelaSimbolos = false },
            new Token() { Nome = "close_parentheses", Descricao = "fechamento parenteses", Padrao = new Regex("[)]"), DeveEstarTabelaSimbolos = false }
        };

        #endregion

        static void Main(string[] args)
        {
            Console.WriteLine(Welcome);

            bool interromperProcesso = false;

            while (!interromperProcesso)
            {
                AnalisadorLexico analisador = new AnalisadorLexico();
                AnalisadorLexicoEntrada entrada = new AnalisadorLexicoEntrada();

                Console.WriteLine("Informe o caminho completo para o arquivo de entrada...");

                string caminhoArquivoEntrada = Console.ReadLine();
                FileInfo arquivoEntrada = null;

                if (!string.IsNullOrWhiteSpace(caminhoArquivoEntrada))
                {
                    arquivoEntrada = new FileInfo(caminhoArquivoEntrada.Replace("\"", ""));
                }

                if (arquivoEntrada != null && arquivoEntrada.Exists)
                {
                    entrada.ArquivoFonte = arquivoEntrada;

                    AnalisadorLexicoSaida saida = analisador.Processar(entrada, tokens);
                    saida.GerarArquivos(entrada.ArquivoFonte);

                    Console.WriteLine("");
                    Console.WriteLine("Processo de análise finalizado, hash de identificação {0}", saida.Identificador);
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("Arquivo de entrada não pode ser localizado");
                }

                Console.WriteLine("Pressione qualquer tecla para continuar ou pressione ESC para sair...");
                Console.WriteLine("");

                interromperProcesso = Console.ReadKey().Key == ConsoleKey.Escape;
            }
        }
    }
}
