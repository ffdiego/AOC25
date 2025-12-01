using Microsoft.Extensions.DependencyInjection;
using AOC25.Core;


internal class Program
{
    private static async Task Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddSingleton<AOCClient>()
            .AddSingleton<GerenciadorDeSolucoes>()
            .AddSingleton<Application>()
            .BuildServiceProvider();
        
        var app = serviceProvider.GetRequiredService<Application>();
        await app.Run("1");
    }
}

internal class Application 
{
    private readonly GerenciadorDeSolucoes gerenciadorDeSolucoes;

    public Application(GerenciadorDeSolucoes gerenciadorDeInput) 
    {
        this.gerenciadorDeSolucoes = gerenciadorDeInput;
    }

    public async Task Run(string? arg) 
    {
        List<int> diasARodar = [];

        if (string.IsNullOrWhiteSpace(arg)) 
        {
            int diaMaisRecente = this.gerenciadorDeSolucoes.DiasComSolucao().Max();

            diasARodar.Add(diaMaisRecente);
        } 

        else if (arg == "all")
        {
            diasARodar.AddRange(this.gerenciadorDeSolucoes.DiasComSolucao());
        }

        else if (int.TryParse(arg, out int dia))
        {
            diasARodar.Add(dia);
        }

        foreach(int dia in diasARodar)
        {
            await this.gerenciadorDeSolucoes.ObtemSolucaoDoDia(dia);
        }
    }

}