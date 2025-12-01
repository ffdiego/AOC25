using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC25.Models.Comuns;

public record Posicao(int X, int Y)
{
    public static implicit operator Posicao((int x, int y) tuple)
        => new Posicao(tuple.x, tuple.y);

    public static implicit operator (int x, int y)(Posicao posicao)
        => (posicao.X, posicao.Y);
}
