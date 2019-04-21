using Fiap.Lac.Analisadores.Modelos;
using System.Collections.Generic;

namespace Fiap.Lac.Analisadores
{
    public interface IAnalisador<TDadosEntrada, TDadosSaida>
    {
        TDadosSaida Processar(TDadosEntrada dadosEntrada, IList<Token> tokens);
    }
}
