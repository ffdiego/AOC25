using AOC24.Utils;

namespace AOC24.Solucoes;

public class Dia3 : ISolucionador
{
    public string SolucaoParte1(string input)
    {
        input = "987654321111111\n811111111111119\n234234234234278\n818181911112111";
        var soma = input.Split('\n').Sum(linha =>
        {
            var lista = Parser.ListaDeIntsDeUnicoDigito(linha);
            var maior = MaiorNumeroComNDigitos(lista, 2);
            var maior2 = MaiorNumeroComNDigitos(linha, 2);

            return maior;
        });


        return soma.ToString();
    }

    public string SolucaoParte2(string input)
    {
        var soma = input.Split('\n').Sum(linha =>
        {
            var lista = Parser.ListaDeIntsDeUnicoDigito(linha);
            var maior = MaiorNumeroComNDigitos(linha, 12);

            return maior;
        });

        return soma.ToString();
    }

    private long MaiorNumeroComNDigitos(IEnumerable<int> ints, int quantidadeDigitos)
    {
        if (quantidadeDigitos == 1)
        {
            return ints.Max();
        }

        if (ints.Count() == 1)
        {
            return ints.First();
        }

        long maiorNumero = 0;

        for (int i = 0; i < ints.Count() - 1; i++)
        {
            long numero = ints.ElementAt(i) * (long)Math.Pow(10, (quantidadeDigitos - 1));

            if (quantidadeDigitos > 1)
            {
                numero += MaiorNumeroComNDigitos(ints.Skip(i + 1), quantidadeDigitos - 1);
            }

            if (numero > maiorNumero)
            {
                maiorNumero = numero;
            }
        }

        return maiorNumero;
    }

    private int MaiorNumeroCom2Digitos(string input)
    {
        List<int> ints = Parser.ListaDeIntsDeUnicoDigito(input);

        int maiorNumero = -1;
        for (int i = 0; i < ints.Count - 1; i++)
        {
            int numero = ints[i] * 10 + ints.Skip(i + 1).Max();

            if (numero > maiorNumero)
            {
                maiorNumero = numero;
            }
        }

        return maiorNumero;
    }

    private int MaiorNumero(string input) => input.Select(c => c & 0b1111).Max();
}
