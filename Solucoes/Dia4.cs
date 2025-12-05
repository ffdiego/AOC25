using AOC24.Utils;
using AOC25.Models.Comuns;

namespace AOC24.Solucoes;

internal class MapaRolos : Mapa
{
    public MapaRolos(string input) : base(input)
    {
        
    }

    public int RolosAlcancaveis(bool limpa = false)
    {
        int papeis = 0;

        foreach (var coord in this.PegaTodasOcorrenciasDe('@'))
        {
            int rolosAdjacentes = this.PosicoesAdjacentes(coord.x, coord.y)
                .Count(pos => this.GetItem(pos) == '@');
            if (rolosAdjacentes < 4)
            {
                papeis++;
                if (limpa)
                {
                    this.RemoveItem(coord);
                }
            }
        }

        return papeis;
    }

    public int RolosAlcancaveisComLimpeza()
    {
        int papeis = 0;
        int papeisIteracao = 0;

        do
        {
            papeisIteracao = RolosAlcancaveis(limpa: true);
            papeis += papeisIteracao;
        } while (papeisIteracao > 0);

        return papeis;
    }
}

public class Dia4 : ISolucionador
{
    public string SolucaoParte1(string input)
    {
        var mapa = new MapaRolos(input);

        return mapa.RolosAlcancaveis().ToString();
    }

    public string SolucaoParte2(string input)
    {
        var mapa = new MapaRolos(input);

        return mapa.RolosAlcancaveisComLimpeza().ToString();
    }


}
