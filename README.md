# Linguagens, Autômatos e Computabilidade
Projeto para criação de analisadores:
 - Léxico
 - Sintáticos
 - Semânticos

## Analisador Léxico
Analisa um arquivo de entrada contendo expressões matemáticas e gera o fluxo de tokens e a tabela de símbolos.
Tokens admitidos no analisador: 
```c#
IList<Token> tokens = new List<Token>()
{
	new Token() { Nome = "number", Descricao = "constantes numericas", Padrao = new Regex("[0-9]"), DeveEstarTabelaSimbolos = true },
	new Token() { Nome = "operator", Descricao = "operadores aritmeticos", Padrao = new Regex("[+|\\-|*|/]"), DeveEstarTabelaSimbolos = false },
	new Token() { Nome = "assignment", Descricao = "atribuicao de valores", Padrao = new Regex("[=]"), DeveEstarTabelaSimbolos = false },
	new Token() { Nome = "end_of_statement", Descricao = "fim de instrucao", Padrao = new Regex("[;]"), DeveEstarTabelaSimbolos = false },
	new Token() { Nome = "open_parentheses", Descricao = "abertura parenteses", Padrao = new Regex("[(]"), DeveEstarTabelaSimbolos = false },
	new Token() { Nome = "close_parentheses", Descricao = "fechamento parenteses", Padrao = new Regex("[)]"), DeveEstarTabelaSimbolos = false }
};
```
