using AOC24.Utils;

namespace AOC24.Solucoes;

public class Dia1 : ISolucionador
{
    public string SolucaoParte1(string input)
    {
        Dial dial = new(input);

        return dial.Cliques.ToString();
    }

    public string SolucaoParte2(string input)
    {
        Dial dial = new(input, contaCaminhos: true);

        return (dial.Cliques).ToString();
    }

    public class Dial
    {
        public int Posicao { get; private set; } = 50;
        public int Cliques { get; private set; } = 0;

        private void L()
        {
            Posicao--;
            if (Posicao < 0)
            {
                Posicao = 99;
            }
        }

        private void R()
        {
            Posicao++;
            if (Posicao > 99)
            {
                Posicao = 0;
            }
        }

        public void Rotacionar(string input, bool contaCaminhos)
        {
            Action rotacionar = (input[0] == 'R') ? R : L;
            int quantidadeRotacoes = int.Parse(input.Substring(1));

            for (int i = 0; i < quantidadeRotacoes; i++)
            {
                rotacionar();

                if (Posicao == 0 && contaCaminhos)
                {
                    Cliques++;
                }
            }

            if (Posicao == 0 && !contaCaminhos)
            {
                Cliques++;
            }
        }

        public Dial(string input, bool contaCaminhos = false)
        {
            foreach (var linha in Parser.LinhasDeTexto(input))
            {
                Rotacionar(linha, contaCaminhos);
            }
        }

    }
}
