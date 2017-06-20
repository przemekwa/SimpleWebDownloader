using System;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace SimpleWebDownloader
{
    class Program
    {
        private static HttpClient Client = new HttpClient();

        static void Main(string[] args)
        {
            Console.Title = "SimpleWebDownloader";

            if (args.Length != 2)
            {
                throw new ArgumentException("Brak jednego z parametrów. Nazwa pliku z linkami, nazwa katalogu do utworzenia");
            }

            if (!Directory.Exists(args[1]))
            {
                Directory.CreateDirectory(args[1]);
            }

            var direcotry = new DirectoryInfo(args[1]);

            var fileStream = new FileStream(args[0], FileMode.Open);


            using (var sr = new StreamReader(fileStream))
            {
                var line = "";

                while ((line = sr.ReadLine()) != null)
                {
                    var uri = line.Replace("imgbox.com", "i.imgbox.com").Replace("http", "https");

                    var fileName = line.Split('/').ToList().Last();

                    var result =  Client.GetByteArrayAsync(uri);

                    File.WriteAllBytes(Path.Combine(direcotry.FullName, $"{fileName}.jpg"), result.Result);
                }

            }

                Console.WriteLine("SimpleWebDownloader");
            Console.ReadKey();
        }
    }
}