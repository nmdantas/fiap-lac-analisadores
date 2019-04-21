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

            entrada.ArquivoFonte = new FileInfo(@"C:\Development\FIAP\entrada.txt");

            AnalisadorLexicoSaida saida = analisador.Processar(entrada, tokens);
            saida.GerarArquivos(entrada.ArquivoFonte);

            Console.WriteLine("Fim");

            Console.Read();
        }
    }
}
