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
        static readonly string Welcome = @"__/\\\\\\\\\\\\\\\__/\\\\\\\\\\\_____/\\\\\\\\\_____/\\\\\\\\\\\\\___        
 _\/\\\///////////__\/////\\\///____/\\\\\\\\\\\\\__\/\\\/////////\\\_       
  _\/\\\_________________\/\\\______/\\\/////////\\\_\/\\\_______\/\\\_      
   _\/\\\\\\\\\\\_________\/\\\_____\/\\\_______\/\\\_\/\\\\\\\\\\\\\/__     
    _\/\\\///////__________\/\\\_____\/\\\\\\\\\\\\\\\_\/\\\/////////____    
     _\/\\\_________________\/\\\_____\/\\\/////////\\\_\/\\\_____________   
      _\/\\\_________________\/\\\_____\/\\\_______\/\\\_\/\\\_____________  
       _\/\\\______________/\\\\\\\\\\\_\/\\\_______\/\\\_\/\\\_____________ 
        _\///______________\///////////__\///________\///__\///______________
";
        static void Main(string[] args)
        {
            IList<Token> tokens = new List<Token>();
            tokens.Add(new Token() { Nome = "number", Descricao = "constantes numericas", Padrao = new Regex("[0-9]"), DeveEstarTabelaSimbolos = true });
            tokens.Add(new Token() { Nome = "operator", Descricao = "operadores aritmeticos", Padrao = new Regex("[+|\\-|*|/]"), DeveEstarTabelaSimbolos = false });
            tokens.Add(new Token() { Nome = "assignment", Descricao = "atribuicao de valores", Padrao = new Regex("[=]"), DeveEstarTabelaSimbolos = false });
            tokens.Add(new Token() { Nome = "end_of_statement", Descricao = "fim de instrucao", Padrao = new Regex("[;]"), DeveEstarTabelaSimbolos = false });
            tokens.Add(new Token() { Nome = "open_parentheses", Descricao = "abertura parenteses", Padrao = new Regex("[(]"), DeveEstarTabelaSimbolos = false });
            tokens.Add(new Token() { Nome = "close_parentheses", Descricao = "fechamento parenteses", Padrao = new Regex("[)]"), DeveEstarTabelaSimbolos = false });
                        
            AnalisadorLexico analisador = new AnalisadorLexico();
            AnalisadorLexicoEntrada entrada = new AnalisadorLexicoEntrada();

            Console.WriteLine(Welcome);
            Console.WriteLine("Informe o caminho completo para o arquivo de entrada:");

            string caminhaArquivoEntrada = Console.ReadLine();
            FileInfo arquivoEntrada = new FileInfo(caminhaArquivoEntrada);

            if (arquivoEntrada.Exists)
            {
                entrada.ArquivoFonte = arquivoEntrada;

                AnalisadorLexicoSaida saida = analisador.Processar(entrada, tokens);
                saida.GerarArquivos(entrada.ArquivoFonte);

                Console.WriteLine("Processo de análise finalizado, hash de identificação {0}", saida.Identificador);
            }
            else
            {
                Console.WriteLine("Arquivo de entrada não pode ser localizado");
            }

            Console.Read();
        }
    }
}
