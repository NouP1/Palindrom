
class Program
{

    static HttpClient httpClient = new HttpClient();
    static async Task Main()
    {
        Console.WriteLine("Введите путь к папке: ");
        string storagePath = Console.ReadLine();

        if (Directory.Exists(storagePath))
        {
            Environment.CurrentDirectory = storagePath;
            Console.WriteLine("Путь к папке:" + storagePath);
            var fileStorage = new List<string[]>();
            try
            {
                var filePaths = Directory.GetFiles(storagePath, "*.txt"); 

                foreach (string path in filePaths)
                {
                    string[] fileLines = File.ReadAllLines(path);
                    fileStorage.Add(fileLines);
                }
                foreach (string[] fileLines in fileStorage)
                {
                    foreach (string fileLine in fileLines)
                    {

                        StringContent content = new(fileLine);
                        using var response = await httpClient.PostAsync("http://localhost:5135/", content);
                        string responseText = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(responseText);
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e}");
            }

        }

        else
        {
            Console.WriteLine("Указанной папки не существует.");
        }
        Console.WriteLine("Нажмите любую клавишу для выхода.");
        Console.ReadKey();

    }


}