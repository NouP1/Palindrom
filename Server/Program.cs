var builder = WebApplication.CreateBuilder();
var app = builder.Build();
SemaphoreSlim semaphore=new(3);
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
Thread.Sleep(1000);


  if(name==finaltext)
            {
                semaphore.Release();
                string result = name +"  палиндром (Вышел из очереди)";
                return result;
            }
            else
            {
                semaphore.Release();
                string result = name +" не палиндром (Вышел из очереди)";
                return result;          
            }

});
app.Run();


