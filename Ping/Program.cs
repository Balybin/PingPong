
using Pong.Model;
using System.Text;
using System.Text.Json;

HttpClient client = new HttpClient();

if (Environment.GetCommandLineArgs().Length == 1)
{
    Console.WriteLine("Ожидаемый ввод:");
    Console.WriteLine("url");
    Console.WriteLine("getById/getByUserId/delete/create/edit/");
    Console.WriteLine("data id/id/id/User Message/Id User Message Status");
    Console.ReadLine();
    return;
}
string url = Environment.GetCommandLineArgs()[1];
string command = Environment.GetCommandLineArgs()[2];
if (command == "getById")
{
    if(Environment.GetCommandLineArgs().Length != 4) { Console.WriteLine("Wrong num of arguments"); return; }
    string data = Environment.GetCommandLineArgs()[3];
    if (int.TryParse(data, out int id))
    {
        System.Uri uri = new Uri(url);
        client.BaseAddress = uri;
        var content = await client.GetStringAsync("/Message/getById/" + id);
        Console.WriteLine("Result:");
        Console.WriteLine(content);
        return;
    }
    Console.WriteLine("cant parse id arg");
    return;
}
else if (command == "getByUserId")
{
    if (Environment.GetCommandLineArgs().Length != 4) { Console.WriteLine("Wrong num of arguments"); return; }
    string data = Environment.GetCommandLineArgs()[3];
    if (int.TryParse(data, out int id))
    {
        System.Uri uri = new Uri(url);
        client.BaseAddress = uri;
        var content = await client.GetStringAsync("/Message/getByUserId/" + id);
        Console.WriteLine("Result:");
        Console.WriteLine(content);
        return;
    }
    Console.WriteLine("cant parse id arg");
    return;
}
else if (command == "delete")
{
    if (Environment.GetCommandLineArgs().Length != 4) { Console.WriteLine("Wrong num of arguments"); return; }
    string data = Environment.GetCommandLineArgs()[3];
    if (int.TryParse(data, out int id))
    {
        Uri uri = new Uri(url);
        client.BaseAddress = uri;
        var response = await client.DeleteAsync("/Message/delete/" + id);
        Console.WriteLine("Result:");
        Console.WriteLine(response);
        return;
    }
    Console.WriteLine("cant parse id arg");
    return;
}
else if (command == "create")
{
    if (Environment.GetCommandLineArgs().Length != 5) { Console.WriteLine("Wrong num of arguments"); return; }
    string userId = Environment.GetCommandLineArgs()[3];
    if (string.IsNullOrEmpty(userId)) { Console.WriteLine("Cant read arg userId"); return; }
    string message = Environment.GetCommandLineArgs()[4];
    if (string.IsNullOrEmpty(message)) { Console.WriteLine("Cant read arg messagge"); return; }
    if (!int.TryParse(userId, out int id)) { Console.WriteLine("Cant parse int UserId"); return; }
    Uri uri = new Uri(url);
    client.BaseAddress = uri;
    var content = new CreateMessageObject { Message = message, User = id };
    var response = await client.PostAsync("​Message​/create".Replace("\x200B", ""), new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json"));
    Console.WriteLine(response);
    return;
}
else if (command == "edit")
{
    if (Environment.GetCommandLineArgs().Length != 7) { Console.WriteLine("Wrong num of arguments"); return; }
    string idStr = Environment.GetCommandLineArgs()[3];
    if (string.IsNullOrEmpty(idStr)) { Console.WriteLine("Cant read arg id"); return; }
    if (!int.TryParse(idStr, out int id)) { Console.WriteLine("Cant parse arg id"); return; }
    string userStr = Environment.GetCommandLineArgs()[4];
    if (string.IsNullOrEmpty(userStr)) { Console.WriteLine("Cant read arg user"); return; }
    if (!int.TryParse(idStr, out int user)) { Console.WriteLine("Cant parse arg user"); return; }
    string messageStr = Environment.GetCommandLineArgs()[5];
    if (string.IsNullOrEmpty(messageStr)) { Console.WriteLine("Cant read arg message"); return; }
    string statusStr = Environment.GetCommandLineArgs()[6];
    if (string.IsNullOrEmpty(statusStr)) Console.WriteLine("Cant read arg status");
    if (!int.TryParse(idStr, out int status)) { Console.WriteLine("Cant parse arg status"); return; }
    Uri uri = new Uri(url);
    client.BaseAddress = uri;
    var content = new MessageDto { Id = id, Message = messageStr, Status = status, User = user };
    var response = await client.PutAsync("Message/edit".Replace("\x200B", ""),
        new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json"));
    Console.WriteLine(response);
    return;
}
else
{
    Console.WriteLine("Unknown method");
    return;
}






