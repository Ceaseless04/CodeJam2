using Spectre.Console;
using Spectre.Console.Cli;
using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;



class Program
{
    static async Task Main(string[] args)
    {
        var app = new CommandApp<FileSizeCommand>();
        app.Run(args);
    }

    internal sealed class FileSizeCommand : Command<FileSizeCommand.Settings>
    {
        public sealed class Settings : CommandSettings
        {

            [CommandOption("-p|--players")]
            [DefaultValue(1)]
            public int NumPlayers { get; init; }
            [CommandOption("-n|--questions")]
            [DefaultValue(1)]
            public int NumQuestions { get; init; }

            [CommandOption("-d|--difficulty")]
            [DefaultValue("Easy")]
            public string? Difficulty { get; init; }

            [CommandOption("-c|--category")]
            [DefaultValue("All")]
            public string? Category { get; init; }

            
        }

        public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
        {
            TriviaGame game = new TriviaGame(settings.NumQuestions, settings.NumPlayers, settings.Difficulty, settings.Category);


            return 0;
        }
    }
}