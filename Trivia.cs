public class TriviaGame
{
    public List<Question> Questions { get; set; } = new();
    public int turn { get; set; } = 0;

    public void MakeGame()
    {
        using var client = new HttpClient();
        string url = $"https://opentdb.com/api.php?amount=10";
       
        var response = await client.GetStringAsync(url);
        responseTask.Wait();
        string response = responseTask.Result;

     

       // public bool IsOver() => turn 
    }