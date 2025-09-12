using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Player
{
    public string Name { get; set; }
    public int Score { get; set; }

    private static int _numberOfPlayers = 0;

    public Player(string name)
    {
        Name = name;
        Score = 0;
        _numberOfPlayers++;
    }
    public void RecordGuess(bool isCorrect)
    {
        if (isCorrect) Score++;
    }
}