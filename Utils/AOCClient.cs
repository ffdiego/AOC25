using System;
using System.Net;

namespace AOC24.Utils;

public class AOCClient : IDisposable
{
    public const string baseURI = "https://adventofcode.com/2025/";
    private const string envSessao = "aocsession";
    private string pastaCacheInputs;
    private HttpClient httpClient;
    public AOCClient()
    {
        CookieContainer cookieContainer = new CookieContainer();
        cookieContainer.Add(
            new Uri(baseURI),
            new Cookie("session", Environment.GetEnvironmentVariable(envSessao) ?? throw new ArgumentNullException("PATH:envSessao"))
        );

        HttpClientHandler httpClientHandler = new HttpClientHandler()
        {
            CookieContainer = cookieContainer
        };

        httpClient = new HttpClient(httpClientHandler)
        {
            BaseAddress = new Uri(baseURI)
        };

        pastaCacheInputs = Path.Combine(Directory.GetCurrentDirectory(), "cacheinputs/");
        if (!Directory.Exists(pastaCacheInputs))
        {
            Directory.CreateDirectory(pastaCacheInputs);
        }
    }

    public async Task<string> GetInputAsync(int dia, bool forcaRefreshNoCache = false)
    {
        string nomeArquivoCache = $"{pastaCacheInputs}/{dia}.txt";

        if (File.Exists(nomeArquivoCache) && !forcaRefreshNoCache)
        {
            return File.ReadAllText(nomeArquivoCache);
        }

        var resposta = await httpClient.GetAsync($"day/{dia}/input");

        if (!resposta.IsSuccessStatusCode)
        {
            throw new Exception($"Falha ao obter o input do dia {dia}");
        }

        string respostaConteudo = (await resposta.Content.ReadAsStringAsync()).TrimEnd();

        File.WriteAllText(nomeArquivoCache, respostaConteudo);

        return respostaConteudo;
    }

    public void Dispose()
    {
        httpClient.Dispose();
    }
}
