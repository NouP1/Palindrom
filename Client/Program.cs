
class Program
{

    static HttpClient httpClient = new HttpClient();
    static async Task Main()
    {
        string storagePath = "words";
        var fileStorage = new List<string[]>();
        try
        {
            var filePaths = Directory.GetFiles(storagePath, "*.txt"); //string[]

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
}