using System;
class Program
{
    static async Task Main(string[] args)
    {
        //// have a cool console WELCOME TO TRIVIA sign or something
        //Console.ForegroundColor = ConsoleColor.Cyan;
        //Console.WriteLine("┌───────────────────────────────┐");
        //Console.WriteLine("│      Welcome to Trivia!       │");
        //Console.WriteLine("└───────────────────────────────┘\n");
        //Console.ResetColor();

        // loop for game
        // while(!game.IsOver())
        
        var game = new TriviaGame(3);

        await game.MakeGame();

        foreach (var q in game.Questions)
        {
            Console.WriteLine("Question: " + q.Text);
            for (int i = 0; i < q.Options.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {q.Options[i]}");
            }
        }
    }
}