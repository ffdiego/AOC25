using AOC24.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AOC25.Models.Comuns;

internal class Mapa
{
    protected List<List<char>> mapa;
    protected char vazio;
    protected int Altura => this.mapa.Count;
    protected int Largura => this.mapa[0].Count;

    public Mapa(List<List<char>> mapa, char vazio = '.')
    {
        this.mapa = mapa;
        this.vazio = vazio;
    }

    public Mapa(string entrada)
    {
        this.mapa = Parser.MatrizDeChars(entrada);
        this.vazio = '.';
    }

    protected Mapa()
    {
        this.vazio = '.';
        this.mapa = [];
    }

    public bool EstaDentroDoMapa(int x, int y) => (y >= 0 && x >= 0 && y < this.mapa.Count && x < this.mapa[y].Count);

    public char GetItem((int x, int y)pos) => GetItem(pos.x, pos.y);
    public char GetItem(int x, int y)
    {
        if (!EstaDentroDoMapa(x, y))
        {
            return default;
        }

        return mapa[y][x];
    }

    public void RemoveItem((int x, int y) pos) => this.mapa[pos.y][pos.x] = this.vazio;

    public void TrocaItem((int x, int y)pos1, (int x, int y) pos2)
    {
        (mapa[pos1.y][pos1.x], mapa[pos2.y][pos2.x]) = (mapa[pos2.y][pos2.x], mapa[pos1.y][pos1.x]);
    }

    public int GetItemInt(int x, int y)
    {
        char c = GetItem(x, y);
        return (c == this.vazio) ? -1 : c & 0b1111;
    }

    public HashSet<(int x, int y)> PegaTodasOcorrenciasDe(char item, bool remove = false)
    {
        HashSet<(int x, int y)> coordenadas = [];

        for (int y = 0; y < this.mapa.Count; y++)
        {
            for (int x = 0; x < this.mapa[0].Count; x++)
            {
                if (this.mapa[y][x]!.Equals(item))
                {
                    coordenadas.Add((x, y));

                    if (remove)
                    {
                        this.mapa[y][x] = vazio;
                    }
                }
            }
        }

        return coordenadas;
    }

    public IEnumerable<(int x, int y)> PosicoesAdjacentes(int x, int y)
    {
        foreach (int dx in range)
        {
            foreach (int dy in range)
            {
                if (dx == 0 && dy == 0)
                {
                    continue;
                }

                (int x, int y)pos = ((x + dx), (y + dy));

                if (EstaDentroDoMapa(pos.x, pos.y))
                {
                    yield return pos;
                }
            }
        }
    }

    private static int[] range = [-1, 0, 1];
}
