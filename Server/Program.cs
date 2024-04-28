
var builder = WebApplication.CreateBuilder();
var app = builder.Build();

int maxReq;
Console.WriteLine("Ведите кол-во одновременно обрабатываемых запросов сервером: ");
while (!int.TryParse(Console.ReadLine(), out maxReq) || maxReq <= 0)
{
    Console.WriteLine("Неккоректное значение...");
}

SemaphoreSlim semaphore = new(maxReq);


app.MapPost("/", async (HttpContext httpContext) =>
{

    await semaphore.WaitAsync();
    Console.WriteLine("Входит в очередь");


    using StreamReader reader = new(httpContext.Request.Body);
    string name = await reader.ReadToEndAsync();
    char[] obrtext = name.ToCharArray();
    Array.Reverse(obrtext);
    string finaltext = new(obrtext);

    Console.WriteLine("Обрабатывается....");
    await Task.Delay(2000);

    string result;
    if (name == finaltext)
    {
        result = name + "  палиндром (Вышел из очереди)";
    }
    else
    {
        result = name + " не палиндром (Вышел из очереди)";
    }
    semaphore.Release();
    await httpContext.Response.WriteAsync(result);

});
app.Run("http://localhost:5135");

