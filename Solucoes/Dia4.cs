using AOC24.Utils;
using AOC25.Models.Comuns;

namespace AOC24.Solucoes;

internal class MapaRolos : Mapa
{
    public MapaRolos(string input) : base(input)
    {
        
    }

    public int PapeisAcessiveis()
    {
        int papeis = 0;

        foreach (var coord in this.PegaTodasOcorrenciasDe('@'))
        {
            int rolosAdjacentes = this.PosicoesAdjacentes(coord.x, coord.y)
                .Count(pos => this.GetItem(pos.x, pos.y) == '@');
            if (rolosAdjacentes < 4)
            {
                papeis++;
            }
        }

        return papeis;
    }
}

public class Dia4 : ISolucionador
{
    public string SolucaoParte1(string input)
    {
        var mapa = new MapaRolos(input);

        return mapa.PapeisAcessiveis().ToString();
    }

    public string SolucaoParte2(string input)
    {
        
        return "";
    }


}
