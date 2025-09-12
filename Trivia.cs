using Newtonsoft.Json.Linq;
using Spectre.Console;

using System.Diagnostics;

public class TriviaGame
{
    public List<Question> Questions { get; set; } = new List<Question>();
    public List<Player> Players { get; set; } = new List<Player>();

    public int Turn { get; set; }

    public int NumQuestions { get; set; }

    public int NumPlayers { get; set; }

    public Difficulties Difficulty { get; set; }

    public Categories Category { get; set; }


    public TriviaGame(int numQuestions = 10, int numPlayers = 1, string difficultyInput = "All", string categoryInput = "All")
    {
        NumQuestions = numQuestions;
        NumPlayers = numPlayers;

        if (!Enum.TryParse(difficultyInput, true, out Difficulties difficulty))
        {
            Console.WriteLine("Invalid difficulty. Defaulting to Easy.");
            difficulty = Difficulties.easy;
        }
        Difficulty = difficulty;

        if (!Enum.TryParse(categoryInput, true, out Categories category))
        {
            Console.WriteLine("Invalid category. Defaulting to Easy.");
            category = Categories.All;
        }
        Category = category;
    }

    public bool IsOver() => Turn >= Questions.Count;


    public async Task MakeGame()
    {

        for (int i = 0; i < NumPlayers; i++)
        {
            AnsiConsole.Write(
                 new Panel(new Markup($"[bold yellow]ENTER PLAYER {i+1}'S NAME:[/]"))
                    .Border(BoxBorder.Square)
                    .BorderStyle(new Style(Color.Yellow))
    );
            string? name = Console.ReadLine();
            Players.Add(new Player(name));
        }

        using var client = new HttpClient();
        string url = $"https://opentdb.com/api.php?amount={NumQuestions}";

        if (Category != Categories.All)
        {
            url += $"&category={(int)Category}";
        }

        if (Difficulty != Difficulties.all)
        {
            url += $"&difficulty={Difficulty}";
        }

        url += "&type=multiple";

        string json = await client.GetStringAsync(url);

        var data = Newtonsoft.Json.Linq.JObject.Parse(json);
        var results = data["results"];

        Questions = new List<Question>();

        foreach (var q in results)
        {
            string correct = System.Net.WebUtility.HtmlDecode((string)q["correct_answer"]);

            var incorrect = new List<string>();
            foreach (var a in q["incorrect_answers"])
            {
                incorrect.Add(System.Net.WebUtility.HtmlDecode((string)a));
            }

            var options = new List<string>(incorrect);
            Random random = new Random();
            int randomInsert = random.Next(0, 4);

            if (randomInsert == 3)
            {
                options.Add(correct);
            }
            else
            {
                options.Insert(randomInsert, correct);
            }

            string questionText = System.Net.WebUtility.HtmlDecode((string)q["question"]);
            Questions.Add(new Question(
                  questionText,
                  correct,
                  options.ToArray(),
                  this.Category,
                  this.Difficulty
              ));

        }
    }
    
    public bool MakeGuess(string guess, Player player, Question question)
    {
        if (!new[] { "1", "2", "3", "4" }.Contains(guess))
        {
            return false;
        }

        int guessInt = int.Parse(guess) - 1;

        bool isCorrect = question.Options[guessInt] == question.CorrectAnswer;
        player.updateScore(isCorrect);
        return isCorrect;
    }
}


