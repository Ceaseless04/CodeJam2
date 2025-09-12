public enum Categories
{
    All = 8,
    Knowledge = 9,
    Books = 10,
    Film = 11,
    Music = 12,
    Musicals_Theatres = 13,
    Television = 14,
    Video_Games = 15,
    Board_Games = 16,
    Nature = 17,
    Computers = 18,
    Mathematics = 19,
    Mythology = 20,
    Sports = 21,
    Geography = 22,
    History = 23,
    Politics = 24,
    Art = 25,
    Celebrities = 26,
    Animals = 27,
    Vehicles = 28,
    Comics = 29,
    Gadgets = 30,
    Anime = 31,
    Cartoons = 32
}

public enum Difficulties
{
    all = 0,
    easy = 1,
    medium = 2,
    hard = 3
}


public class Question
{
    public string Text { get; set; }
    public string CorrectAnswer { get; set; }
    public string[] Options { get; set; }

    Categories Category { get; set; }

    Difficulties Difficulty { get; set; }

   public Question(string text, string correctAnswer, string[] options, Categories category, Difficulties difficulty){
        Text = text;
        CorrectAnswer = correctAnswer;
        Options = options;
        Category = category;
        Difficulty = difficulty;
    }

}