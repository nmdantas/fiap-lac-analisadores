# Linguagens, Autômatos e Computabilidade
Projeto para criação de analisadores
 - Léxico
 - Sintáticos
 - Semânticos

## Analisador Léxico
Analisa um arquivo de entrada contendo expressões matemáticas e gera o fluxo de tokens e a tabela de símbolos. Tokens admitidos no analisador 
```c#
IList<Token> tokens = new List<Token>()
{
	new Token() { Nome = "number", Descricao = "constantes numericas", Padrao = new Regex("[0-9]"), DeveEstarTabelaSimbolos = true },
	new Token() { Nome = "operator", Descricao = "operadores aritmeticos", Padrao = new Regex("[+|\\-|*|/]"), DeveEstarTabelaSimbolos = true },
	new Token() { Nome = "assignment", Descricao = "atribuicao de valores", Padrao = new Regex("[=]"), DeveEstarTabelaSimbolos = false },
	new Token() { Nome = "end_of_statement", Descricao = "fim de instrucao", Padrao = new Regex("[;]"), DeveEstarTabelaSimbolos = false },
	new Token() { Nome = "open_parentheses", Descricao = "abertura parenteses", Padrao = new Regex("[(]"), DeveEstarTabelaSimbolos = false },
	new Token() { Nome = "close_parentheses", Descricao = "fechamento parenteses", Padrao = new Regex("[)]"), DeveEstarTabelaSimbolos = false }
};
```

### Execução
Para execução é necessário ter instalado o ``Visual Studio`` (versão 2017 ou superior) ou ``Visual Code`` + ``.NET Core 2.0``  

1. *Abrir o Visual Studio*
![Primeiro Passo](/imagens/passo-01.png)

2. *Escolher o arquivo '.sln' (Solution) para a abrir os projetos*
![Segundo Passo](/imagens/passo-02.png)

3. *Abrir o menu lateral 'Solution Explorer'*
![Terceiro Passo](/imagens/passo-03.png)

4. *Clicar com o botão direito no projeto 'Fiap.Lac.Apresentacao' e selecionar a opção 'Set as StartUp Project'*
![Quarto Passo](/imagens/passo-04.png)

5. *Selecionar a opção 'Start Without Debungging' no menu Debug*
![Quinto Passo](/imagens/passo-05.png)

6. *Agora para testar o projeto basta seguir as orientações na tela* =)
![Último Passo](/imagens/passo-06.png)
