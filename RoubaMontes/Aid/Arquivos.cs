using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RoubaMontes.Aid
{
    public class Arquivos
    {
        public static void SalvarLogs(string logs)
        {
            // string path = Assembly.GetExecutingAssembly().Location;

            //E:\LocalRepository\c-sharp\RoubaMontes\RoubaMontes\bin\Debug\net8.0\RoubaMontes.dll
            //E:\LocalRepository\c-sharp\RoubaMontes\RoubaMontes\Logs\

            string diretorioDeExecucao = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../"));
            string pastaLogs = Path.Combine(diretorioDeExecucao, "Logs");

            Directory.CreateDirectory(pastaLogs);

            string data = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string arquivo = $"logs_{data}.txt";
            string path = Path.Combine(pastaLogs, arquivo);

            File.WriteAllText(path, logs);

            Console.WriteLine($"Arquivo '{arquivo}' foi salvo na pasta Logs.");
        }
    }
}
