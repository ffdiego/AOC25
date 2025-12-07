using AOC24.Solucoes;
using AOC25.Models.Comuns;
using System;
using System.Collections.Generic;
using System.Text;

namespace AOC24.Solucoes;

class TakyonSimulator : Mapa
{
    private const char splitter = '^';
    private const char vacuo = '.';
    private HashSet<Posicao> takyons;
    private int alturaSimulada;
    public int Splits { get; private set; }
    public TakyonSimulator(string input) : base(input, vazio: vacuo)
    {
        this.takyons = [.. this.PegaTodasOcorrenciasDe('S').Select(s => (Posicao)s)];
        this.alturaSimulada = 0;
    }

    public void SimulaAteOFim()
    {
        while(alturaSimulada < this.Altura)
        {
            AvancaEstado();
            alturaSimulada++;
        }
    } 

    public void AvancaEstado()
    {
        HashSet<Posicao> posicaoAtualizada = [];

        foreach (Posicao takyon in takyons)
        {
            Posicao aFrente = (takyon.X, takyon.Y + 1);
            
            switch (this.GetItem(aFrente))
            {
                case vacuo:
                    this.SetItem(aFrente, 'T');
                    posicaoAtualizada.Add(aFrente);
                    break;
                case splitter:
                    Splits++;
                    Posicao takyonFilhoL = (aFrente.X - 1, aFrente.Y);
                    Posicao takyonFilhoR = (aFrente.X + 1, aFrente.Y);

                    posicaoAtualizada.Add(takyonFilhoL);
                    posicaoAtualizada.Add(takyonFilhoR);

                    this.SetItem(takyonFilhoL, 'T');
                    this.SetItem(takyonFilhoR, 'T');
                    break;
                default:
                    break;
            }
        }

        this.takyons = posicaoAtualizada;
    }
}

public class Dia7 : ISolucionador
{
    public string SolucaoParte1(string input)
    {
        //input = ".......S.......\n...............\n.......^.......\n...............\n......^.^......\n...............\n.....^.^.^.....\n...............\n....^.^...^....\n...............\n...^.^...^.^...\n...............\n..^...^.....^..\n...............\n.^.^.^.^.^...^.\n...............";
        var simulador = new TakyonSimulator(input);

        simulador.SimulaAteOFim();

        return simulador.Splits.ToString();
    }
}
