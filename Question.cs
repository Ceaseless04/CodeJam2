public enum Categories
{
    General_Knowledge = 9,
    Entertainment_Books = 10,
    Entertainment_Film = 11,
    Entertainment_Music = 12,
    Entertainment_Musicals_Theatres = 13,
    Entertainment_Television = 14,
    Entertainment_Video_Games = 15,
    Entertainment_Board_Games = 16,
    Science_Nature = 17,
    Science_Computers = 18,
    Science_Mathematics = 19,
    Mythology = 20,
    Sports = 21,
    Geography = 22,
    History = 23,
    Politics = 24,
    Art = 25,
    Celebrities = 26,
    Animals = 27,
    Vehicles = 28,
    Entertainment_Comics = 29,
    Science_Gadgets = 30,
    Entertainment_Japanese_Anime_Manga = 31,
    Entertainment_Cartoon_Animations = 32
}

public enum Difficulties
{
    Easy = 0,
    Medium = 1,
    Hard = 2
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