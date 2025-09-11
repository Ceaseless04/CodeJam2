public class TriviaGame
{
    public List<Question> Questions { get; set; }

    public List<Player> Players { get; set; }
    public int Turn { get; set; }

    public int NumQuestions { get; set; }

    public int NumPlayers { get; set; }

    public Difficulties Difficulty { get; set; }

    public Categories Category { get; set; }
    
    public bool IsOver => turn >= Questions.Count;
    // show game over game.IsOver

    public TriviaGame(int numQuestions=10, int numPlayers=1, string difficultyInput="Easy", string categoryInput="All")
    {
        NumQuestions = numQuestions;
        NumPlayers = numPlayers;

        if (!Enum.TryParse(difficultyInput, true, out Difficulties difficulty))
        {
            Console.WriteLine("Invalid difficulty. Defaulting to Easy.");
            difficulty = Difficulties.Easy;
        }
        Difficulty = difficulty;

        if (!Enum.TryParse(difficultyInput, true, out Categories category))
        {
            Console.WriteLine("Invalid category. Defaulting to Easy.");
            category = Categories.All;
        }
        Category = category;
    }




public void MakeGame()
    {
        using var client = new HttpClient();
        string url = $"https://opentdb.com/api.php?amount=10";
       
        var response = await client.GetStringAsync(url);
        responseTask.Wait();
        string response = responseTask.Result;

     

       // public bool IsOver() => turn 
    }