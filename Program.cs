using Spectre.Console;
using Spectre.Console.Cli;
using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;


class Program
{
    static async Task Main(string[] args)
    {
        var app = new CommandApp();
        app.Configure(config =>
        {
            config.AddCommand<PlayCommand>("play")
                  .WithDescription("Start a trivia game");

            config.AddCommand<CategoriesCommand>("categories")
                  .WithDescription("List all valid trivia categories");

            config.AddCommand<DifficultiesCommand>("difficulties")
                  .WithDescription("List all valid diificulties");
        });

        app.Run(args);
    }

    internal sealed class PlayCommand : AsyncCommand<PlayCommand.Settings>
    {
        public sealed class Settings : CommandSettings
        {

            [CommandOption("-p|--players")]
            [DefaultValue(1)]
            [Description("The number of players")]
            public int NumPlayers { get; init; }
            [CommandOption("-n|--questions")]
            [DefaultValue(10)]
            [Description("The number of questions")]
            public int NumQuestions { get; init; }

            [CommandOption("-d|--difficulty")]
            [DefaultValue("All")]
            [Description("The level of difficulty")]
            public string? Difficulty { get; init; }

            [CommandOption("-c|--category")]
            [DefaultValue("All")]
            [Description("The category of trivia")]
            public string? Category { get; init; }


        }



        public override async Task<int> ExecuteAsync([NotNull] CommandContext context, [NotNull] Settings settings)
        {
            TriviaGame game = new TriviaGame(settings.NumQuestions, settings.NumPlayers, settings.Difficulty, settings.Category);
           
            AnsiConsole.Write(
            new FigletText("WELCOME TO TRIVIA!")
           .Centered()
           .Color(Color.Yellow));

            AnsiConsole.MarkupLine("[green]Let's see how smart you really are![/]\n");

            await game.MakeGame();

            while (!game.IsOver())
            {
                Question currQuestion = game.Questions[game.Turn];
                AnsiConsole.MarkupLine($"\n[blue]{currQuestion.Text}[/]");
                int choice = 1;
                foreach (string option in currQuestion.Options)
                {
                    Console.WriteLine($"{choice}. {option}");
                    choice++;
                }
                foreach (Player player in game.Players)
                {
                    AnsiConsole.Markup($"\n[blue]{player.Name}[/], make your guess: ");
                    string guess = Console.ReadLine();
                    while(!game.MakeGuess(guess, player, currQuestion))
                    {
                        AnsiConsole.MarkupLine("[red]Invalid guess, try again[/]:");
                        guess = Console.ReadLine();
                    }
                }
                //show score
                foreach (Player player in game.Players)
                {
                    AnsiConsole.MarkupLine($"[yellow]{player.Name}[/]: [green]{player.Score}[/]");
                }
                game.Turn++;

                
            }

            //score table and screen
            AnsiConsole.Write(
                new FigletText("FINAL SCORES")
                .Centered().
                Color(Color.Yellow));

            var finalScoreTable = new Table()
                .Border(TableBorder.HeavyEdge)          
                .BorderColor(Color.White)
                .Expand()                                
                .Centered()                              
                .AddColumn(new TableColumn("[green]PLAYER[/]").Centered())
                .AddColumn(new TableColumn("[green]SCORE[/]").Centered());

            foreach (var player in game.Players)
            {
                finalScoreTable.AddRow($"[white]{player.Name}[/]", $"[white]{player.Score}[/]");
            }

            AnsiConsole.Write(finalScoreTable);


            AnsiConsole.Write(new Rule("").RuleStyle("bold yellow").Centered());

            int totalQuestions = game.Questions.Count;

            

           foreach (var player in game.Players)
            {
                int correct = player.Score;
                int total = game.Questions.Count;
                int incorrect = total - correct;

                int barWidth = 100;
                int correctWidth = (int)((double)correct / total * barWidth);
                int incorrectWidth = barWidth - correctWidth;

                string bar = $"[green]{new string('█', correctWidth)}[/][red]{new string('█', incorrectWidth)}[/]";

                var panel = new Panel(bar)
                    .Header($"[bold white]{player.Name}[/] — [white]{correct}[/]/[white]{total}[/] correct")
                    .Border(BoxBorder.Square)
                    .BorderColor(Color.White);

                AnsiConsole.Write(panel);
            }

            AnsiConsole.MarkupLine("[green]Thank you for playing![/]\n");

   
            //show winner
            int maxScore = game.Players.Max(p => p.Score);
            List<Player> winners = game.Players.Where(p => p.Score == maxScore).ToList();

            foreach (Player player in winners)
            {
                AnsiConsole.MarkupLine($"[bold yellow]{player.Name}[/] [green]won[/] with a score of [bold cyan]{player.Score}[/]");
            }



            return 0;

        }
    }

    internal sealed class CategoriesCommand : Command<CategoriesCommand.Settings>
    {
        public sealed class Settings : CommandSettings { }

        public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
        {

            foreach (var category in Enum.GetValues(typeof(Categories))){
                AnsiConsole.MarkupLine($"[bold yellow]{category}[/]");
            }
            return 0;
        }
    }

    internal sealed class DifficultiesCommand : Command<DifficultiesCommand.Settings>
    {
        public sealed class Settings : CommandSettings { }

        public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
        {

            foreach (var difficulty in Enum.GetValues(typeof(Difficulties)))
            {
                AnsiConsole.MarkupLine($"[bold yellow]{difficulty}[/]");
            }
            return 0;
        }
    }
}