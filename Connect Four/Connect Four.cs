using System.ComponentModel.DataAnnotations.Schema;

GameBoard gameboard = new();
while (true)
{
    Console.WriteLine("Please select a difficulty: \n1. Easy\n2. Medium\n3. Hard\n4. Pro");
    var input = Console.ReadLine();
    bool valid = int.TryParse(input, out int selection);

    if (valid)
    {
        if (selection == 1)
            gameboard.Difficulty = Difficulty.Easy;
        if (selection == 2)
            gameboard.Difficulty = Difficulty.Medium;
        if (selection == 3)
            gameboard.Difficulty = Difficulty.Hard;
        if (selection == 4)
            gameboard.Difficulty = Difficulty.Pro;
        break;
    }
    else
        Console.WriteLine("There is something wrong with your selection.");
}

GameLoop();

void GameLoop()
{
    while (true)
    {

        gameboard.GetGameBoard();
        //gameboard.UpdateToken(Player.Player2, 1);
        //gameboard.UpdateToken(Player.Player1, 1);
        //gameboard.UpdateToken(Player.Player1, 1);
        //gameboard.UpdateToken(Player.Player1, 1);
        //gameboard.UpdateToken(Player.Player1, 1);
        gameboard.TurnPrompt();
        gameboard.CheckForVictory(gameboard.currentPlayer);
        if (gameboard.currentPlayer == Player.Player1)
            gameboard.currentPlayer = Player.Player2;
        else
            gameboard.currentPlayer = Player.Player1;
        gameboard.Turn++;

    }
}

//For debugging~~~
//foreach (Token token in gameboard.Tokens)
//{
    //Console.WriteLine($" Player: {token.Player} Symbol: {token.Symbol} Color: {token.Color} Column: {token.Column} Row: {token.Row}");
//}



/// Below are the classes and enums /

public class GameBoard
{
    public List<Token> Tokens { get; set; }
    public bool PlayerOneFirst { get; set; }
    public Player currentPlayer { get; set; }
    public int Turn { get; set; }
    public Difficulty Difficulty { get; set; }


    public GameBoard()
    {
        Tokens = new List<Token>();
        for (int column = 1; column <= 7; column++)
        {
            for (int row = 1; row <= 6; row++)
            {
                Tokens.Add(new Token(row, column));
            }
        }
        currentPlayer = Player.Player1;
    }

    public void TurnPrompt()
    {
        if (currentPlayer == Player.Player1)
        {
            Console.Write("It is your turn! Type the number of the column you want to drop your coin in.\n ");
            while (true)
            {
                Console.Write("> ");
                var input = Console.ReadLine();
                bool valid = int.TryParse(input, out int column);
                if (column < 1 && column > 7)
                {
                    valid = false;
                    Console.WriteLine("That column does not exist.  Try again.");
                    Console.ReadLine();
                    continue;
                }
                    
                if (valid)
                {
                    valid = VerticalValidation(column);
                }
                if (valid)
                {
                    UpdateToken(Player.Player1, column);
                    Console.WriteLine($"You dropped your token down column {column}.");
                    break;
                }
            }

        }

        if (currentPlayer == Player.Player2)
        {
            Console.WriteLine("It is the computer's turn...");
            while (true)
            {
                int column = ComputerAI();
                Random random = new();
                //int column = random.Next(1, 8);
                bool valid = VerticalValidation(column);
                if (valid)
                {
                    UpdateToken(Player.Player2, column);
                    Thread.Sleep(random.Next(1000, 4001));
                    Console.WriteLine($"The computer put it's token down column {column}.");
                    Console.ReadLine();
                    break;
                }
            }
        }
    }

    public bool VerticalValidation(int column)
    {
        if (Tokens.Exists(x => x.Column == column && x.Row == 6 && x.Player == Player.None))
            return true;
        else
        {
            if (currentPlayer == Player.Player1)
                Console.WriteLine("That row is full.");
            return false;
        }
    }

    public void ResetGameBoard()
    {
        Tokens = new List<Token>();
        for (int column = 1; column <= 7; column++)
        {
            for (int row = 1; row <= 6; row++)
            {
                Tokens.Add(new Token(row, column));
            }
        }
        Turn = 0;
    }

    public void GetGameBoard()
    {
        Console.Clear();
        Console.WriteLine(",---------------------------------------,");
        Console.WriteLine("|              Connect Four             |");
        Console.WriteLine("'---------------------------------------'");
        Console.WriteLine();

        Console.WriteLine($"        1   2   3   4   5   6   7");
        Console.WriteLine();
        Console.WriteLine($"     |+---+---+---+---+---+---+---+|");
        Console.Write($"     || "); GetToken(6, 1); Console.Write(" | "); GetToken(6, 2); Console.Write(" | "); GetToken(6, 3); Console.Write(" | "); GetToken(6, 4); Console.Write(" | "); GetToken(6, 5); Console.Write(" | "); GetToken(6, 6); Console.Write(" | "); GetToken(6, 7); Console.Write(" ||\n");
        Console.WriteLine($"     |+---+---+---+---+---+---+---+|");
        Console.Write($"     || "); GetToken(5, 1); Console.Write(" | "); GetToken(5, 2); Console.Write(" | "); GetToken(5, 3); Console.Write(" | "); GetToken(5, 4); Console.Write(" | "); GetToken(5, 5); Console.Write(" | "); GetToken(5, 6); Console.Write(" | "); GetToken(5, 7); Console.Write(" ||\n");
        Console.WriteLine($"     |+---+---+---+---+---+---+---+|");
        Console.Write($"     || "); GetToken(4, 1); Console.Write(" | "); GetToken(4, 2); Console.Write(" | "); GetToken(4, 3); Console.Write(" | "); GetToken(4, 4); Console.Write(" | "); GetToken(4, 5); Console.Write(" | "); GetToken(4, 6); Console.Write(" | "); GetToken(4, 7); Console.Write(" ||\n");
        Console.WriteLine($"     |+---+---+---+---+---+---+---+|");
        Console.Write($"     || "); GetToken(3, 1); Console.Write(" | "); GetToken(3, 2); Console.Write(" | "); GetToken(3, 3); Console.Write(" | "); GetToken(3, 4); Console.Write(" | "); GetToken(3, 5); Console.Write(" | "); GetToken(3, 6); Console.Write(" | "); GetToken(3, 7); Console.Write(" ||\n");
        Console.WriteLine($"     |+---+---+---+---+---+---+---+|");
        Console.Write($"     || "); GetToken(2, 1); Console.Write(" | "); GetToken(2, 2); Console.Write(" | "); GetToken(2, 3); Console.Write(" | "); GetToken(2, 4); Console.Write(" | "); GetToken(2, 5); Console.Write(" | "); GetToken(2, 6); Console.Write(" | "); GetToken(2, 7); Console.Write(" ||\n");
        Console.WriteLine($"     |+---+---+---+---+---+---+---+|");
        Console.Write($"     || "); GetToken(1, 1); Console.Write(" | "); GetToken(1, 2); Console.Write(" | "); GetToken(1, 3); Console.Write(" | "); GetToken(1, 4); Console.Write(" | "); GetToken(1, 5); Console.Write(" | "); GetToken(1, 6); Console.Write(" | "); GetToken(1, 7); Console.Write(" ||\n");
        Console.WriteLine($"     |+---+---+---+---+---+---+---+|");
        Console.WriteLine($"    /||\\                         /||\\");
        Console.WriteLine(); 
        Console.WriteLine("-----------------------------------------");
        Console.Write($"Player1 Color: "); Console.ForegroundColor = ConsoleColor.Red; Console.Write("Red"); Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"  Player2 Color: "); Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("Yellow"); Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"Current Round: {Turn, 2}    Difficulty: {Difficulty}");
        Console.WriteLine("-----------------------------------------");
    }

    public void UpdateToken(Player player, int column)
    {
        int tokenDrop = 0;
        for (var row = 1; row <= 6; row++)
        {
            for (int i = 0; i < Tokens.Count; i++)
            {
                if (Tokens[i].Row == row && Tokens[i].Column == column && Tokens[i].Player == Player.None && tokenDrop == 0)
                {
                    Tokens.Remove(Tokens[i]);
                    Tokens.Add(new Token(player, '@', row, column));
                    tokenDrop++;
                }
            }
        }
    }

    public void GetToken(int row, int column)
    {
        for (int i = 0; i < Tokens.Count; i++)
        {
            if (Tokens[i].Row == row && Tokens[i].Column == column)
            {
                GetColor(Tokens[i].Color);
                Console.Write($"{Tokens[i].Symbol}");
                GetColor(TokenColor.None);
            }
        }
    }

    public void GetColor(TokenColor color)
    {
        if (color == TokenColor.Blue)
            Console.ForegroundColor = ConsoleColor.Blue;

        if (color == TokenColor.Green)
            Console.ForegroundColor = ConsoleColor.Green;

        if (color == TokenColor.Red)
            Console.ForegroundColor = ConsoleColor.Red;

        if (color == TokenColor.Yellow)
            Console.ForegroundColor = ConsoleColor.Yellow;

        if (color == TokenColor.Cyan)
            Console.ForegroundColor = ConsoleColor.Cyan;

        if (color == TokenColor.Purple)
            Console.ForegroundColor = ConsoleColor.Magenta;

        if (color == TokenColor.None)
            Console.ForegroundColor = ConsoleColor.White;
    }

    public void CheckForVictory(Player player)
    {
        int i, ii, iii, iv;
        
        for (i = 0; i < Tokens.Count; i++)
        {
            //Horizontal Check
            if (Tokens[i].Column >= 4 && Tokens[i].Player == player)
            {
                //Check right
                ii = Tokens.FindIndex(ii => ii.Column == Tokens[i].Column - 1 && ii.Row == Tokens[i].Row);
                iii = Tokens.FindIndex(iii => iii.Column == Tokens[i].Column - 2 && iii.Row == Tokens[i].Row);
                iv = Tokens.FindIndex(iv => iv.Column == Tokens[i].Column - 3 && iv.Row == Tokens[i].Row);
                if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == player && Tokens[iv].Player == player)
                {
                    Tokens[i].Color = TokenColor.Green;
                    Tokens[ii].Color = TokenColor.Green;
                    Tokens[iii].Color = TokenColor.Green;
                    Tokens[iv].Color = TokenColor.Green;
                    GetGameBoard();
                    Console.WriteLine($"4 in a row! Horizontal win! {player} wins!");
                    Console.ReadKey();
                    ResetGameBoard();
                }
            }
            if ((Tokens[i].Column <= 4 && Tokens[i].Player == player))
            {
                //check left
                ii = Tokens.FindIndex(ii => ii.Column == Tokens[i].Column + 1 && ii.Row == Tokens[i].Row);
                iii = Tokens.FindIndex(iii => iii.Column == Tokens[i].Column + 2 && iii.Row == Tokens[i].Row);
                iv = Tokens.FindIndex(iv => iv.Column == Tokens[i].Column + 3 && iv.Row == Tokens[i].Row);
                if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == player && Tokens[iv].Player == player)
                {
                    Tokens[i].Color = TokenColor.Green;
                    Tokens[ii].Color = TokenColor.Green;
                    Tokens[iii].Color = TokenColor.Green;
                    Tokens[iv].Color = TokenColor.Green;
                    GetGameBoard();
                    Console.WriteLine($"4 in a row! Horizontal Win! {player} wins!");
                    Console.ReadKey();
                    ResetGameBoard();
                }
            }
            //Vertical Check
            if (Tokens[i].Row <= 3 && Tokens[i].Player == player)
            {
                //Check up
                ii = Tokens.FindIndex(ii => ii.Row == Tokens[i].Row + 1 && ii.Column == Tokens[i].Column);
                iii = Tokens.FindIndex(iii => iii.Row == Tokens[i].Row + 2 && iii.Column == Tokens[i].Column);
                iv = Tokens.FindIndex(iv => iv.Row == Tokens[i].Row + 3 && iv.Column == Tokens[i].Column);
                if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == player && Tokens[iv].Player == player)
                {
                    Tokens[i].Color = TokenColor.Green;
                    Tokens[ii].Color = TokenColor.Green;
                    Tokens[iii].Color = TokenColor.Green;
                    Tokens[iv].Color = TokenColor.Green;
                    GetGameBoard();
                    Console.WriteLine($"4 in a row! Vertical Stack! {player} wins!");
                    Console.ReadKey();
                    ResetGameBoard();
                }
            }
            //Diagonal Down
            if (Tokens[i].Row >= 4 && Tokens[i].Column <= 4 && Tokens[i].Player == player)
            {
                ii = Tokens.FindIndex(ii => ii.Row == Tokens[i].Row - 1 && ii.Column == Tokens[i].Column + 1);
                iii = Tokens.FindIndex(iii => iii.Row == Tokens[i].Row - 2 && iii.Column == Tokens[i].Column + 2);
                iv = Tokens.FindIndex(iv => iv.Row == Tokens[i].Row - 3 && iv.Column == Tokens[i].Column + 3);
                if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == player && Tokens[iv].Player == player)
                {
                    Tokens[i].Color = TokenColor.Green;
                    Tokens[ii].Color = TokenColor.Green;
                    Tokens[iii].Color = TokenColor.Green;
                    Tokens[iv].Color = TokenColor.Green;
                    GetGameBoard();
                    Console.WriteLine($"4 in a row! Diagonal Down! {player} wins!");
                    Console.ReadKey();
                    ResetGameBoard();
                }
            }
            //Diagonal Up
            if (Tokens[i].Row <= 3 && Tokens[i].Column <=4 && Tokens[i].Player == player)
            {
                ii = Tokens.FindIndex(ii => ii.Row == Tokens[i].Row + 1 && ii.Column == Tokens[i].Column + 1);
                iii = Tokens.FindIndex(iii => iii.Row == Tokens[i].Row + 2 && iii.Column == Tokens[i].Column + 2);
                iv = Tokens.FindIndex(iv => iv.Row == Tokens[i].Row + 3 && iv.Column == Tokens[i].Column + 3);
                if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == player && Tokens[iv].Player == player)
                {
                    Tokens[i].Color = TokenColor.Green;
                    Tokens[ii].Color = TokenColor.Green;
                    Tokens[iii].Color = TokenColor.Green;
                    Tokens[iv].Color = TokenColor.Green;
                    GetGameBoard();
                    Console.WriteLine($"4 in a row! Diagonal Up! {player} wins!");
                    Console.ReadKey();
                    ResetGameBoard();
                }
            }
        } 
    }

    public int ComputerAI()
    {
        while (true)
        {
            int i, ii, iii, iv, v;
            Random random = new();
            for (i = 0; i < Tokens.Count; i++)
            {
                //Winning Moves => Vertical Check Self
                var player = Player.Player2;
                if (Tokens[i].Row <= 3 && Tokens[i].Player == player)
                {
                    ii = Tokens.FindIndex(ii => ii.Row == Tokens[i].Row + 1 && ii.Column == Tokens[i].Column);
                    iii = Tokens.FindIndex(iii => iii.Row == Tokens[i].Row + 2 && iii.Column == Tokens[i].Column);
                    iv = Tokens.FindIndex(iv => iv.Row == Tokens[i].Row + 3 && iv.Column == Tokens[i].Column);
                    if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == player && Tokens[iv].Player == Player.None)
                    {
                        if (VerticalValidation(Tokens[i].Column))
                            return Tokens[i].Column;
                    }
                }
                //Winning Moves => Vertical Check Player1
                player = Player.Player1;
                if (Tokens[i].Row <= 3 && Tokens[i].Player == player)
                {
                    ii = Tokens.FindIndex(ii => ii.Row == Tokens[i].Row + 1 && ii.Column == Tokens[i].Column);
                    iii = Tokens.FindIndex(iii => iii.Row == Tokens[i].Row + 2 && iii.Column == Tokens[i].Column);
                    iv = Tokens.FindIndex(iv => iv.Row == Tokens[i].Row + 3 && iv.Column == Tokens[i].Column);
                    if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == player && Tokens[iv].Player == Player.None)
                    {
                        if (VerticalValidation(Tokens[i].Column))
                            return Tokens[i].Column;
                    }
                }
                //Winning Moves => Horizontal Check Self Three in a Row
                player = Player.Player2;
                if (Tokens[i].Column >= 4 && Tokens[i].Player == player)
                {
                    //Check right
                    ii = Tokens.FindIndex(ii => ii.Column == Tokens[i].Column - 1 && ii.Row == Tokens[i].Row);
                    iii = Tokens.FindIndex(iii => iii.Column == Tokens[i].Column - 2 && iii.Row == Tokens[i].Row);
                    iv = Tokens.FindIndex(iv => iv.Column == Tokens[i].Column - 3 && iv.Row == Tokens[i].Row);
                    if (Tokens.Exists(v => v.Column == Tokens[iv].Column && v.Row == Tokens[iv].Row - 1))
                    {
                        v = Tokens.FindIndex(v => v.Column == Tokens[iv].Column && v.Row == Tokens[iv].Row - 1);
                        if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == player && Tokens[iv].Player == Player.None && Tokens[v].Player != Player.None)
                        {
                            if (VerticalValidation(Tokens[iv].Column))
                                return Tokens[iv].Column;
                        }
                    }
                    if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == player && Tokens[iv].Player == Player.None)
                    {
                        if (VerticalValidation(Tokens[iv].Column))
                            return Tokens[iv].Column;
                    }
                }
                //Winning Moves => Horizontal Check Self Three in a Row
                if ((Tokens[i].Column <= 4 && Tokens[i].Player == player))
                {
                    //check left
                    ii = Tokens.FindIndex(ii => ii.Column == Tokens[i].Column + 1 && ii.Row == Tokens[i].Row);
                    iii = Tokens.FindIndex(iii => iii.Column == Tokens[i].Column + 2 && iii.Row == Tokens[i].Row);
                    iv = Tokens.FindIndex(iv => iv.Column == Tokens[i].Column + 3 && iv.Row == Tokens[i].Row);
                    if (Tokens.Exists(v => v.Column == Tokens[iv].Column && v.Row == Tokens[iv].Row - 1))
                    {
                        v = Tokens.FindIndex(v => v.Column == Tokens[iv].Column && v.Row == Tokens[iv].Row - 1);
                        if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == player && Tokens[iv].Player == Player.None && Tokens[v].Player != Player.None)
                        {
                            if (VerticalValidation(Tokens[iv].Column))
                                return Tokens[iv].Column;
                        }
                    }
                    if (!(Tokens.Exists(v => v.Column == Tokens[iv].Column && v.Row == Tokens[iv].Row - 1)))
                    {
                        if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == player && Tokens[iv].Player == Player.None)
                        {
                            if (VerticalValidation(Tokens[iv].Column))
                                return Tokens[iv].Column;
                        }

                    }
                }
                //WInning Moves => Horizontal Check Player1 Three in a Row
                player = Player.Player1;
                if (Tokens[i].Column >= 4 && Tokens[i].Player == player)
                {
                    //Check right
                    ii = Tokens.FindIndex(ii => ii.Column == Tokens[i].Column - 1 && ii.Row == Tokens[i].Row);
                    iii = Tokens.FindIndex(iii => iii.Column == Tokens[i].Column - 2 && iii.Row == Tokens[i].Row);
                    iv = Tokens.FindIndex(iv => iv.Column == Tokens[i].Column - 3 && iv.Row == Tokens[i].Row);
                    if (Tokens.Exists(v => v.Column == Tokens[iv].Column && v.Row == Tokens[iv].Row - 1))
                    {
                        v = Tokens.FindIndex(v => v.Column == Tokens[iv].Column && v.Row == Tokens[iv].Row - 1);
                        if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == player && Tokens[iv].Player == Player.None && Tokens[v].Player != Player.None)
                        {
                            if (VerticalValidation(Tokens[iv].Column))
                                return Tokens[iv].Column;
                        }
                    }
                    if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == player && Tokens[iv].Player == Player.None)
                    {
                        if (VerticalValidation(Tokens[iv].Column))
                            return Tokens[iv].Column;
                    }
                }
                //Winning Moves => Horizontal Check Self Three in a Row
                if ((Tokens[i].Column <= 4 && Tokens[i].Player == player))
                {
                    //check left
                    ii = Tokens.FindIndex(ii => ii.Column == Tokens[i].Column + 1 && ii.Row == Tokens[i].Row);
                    iii = Tokens.FindIndex(iii => iii.Column == Tokens[i].Column + 2 && iii.Row == Tokens[i].Row);
                    iv = Tokens.FindIndex(iv => iv.Column == Tokens[i].Column + 3 && iv.Row == Tokens[i].Row);
                    if (Tokens.Exists(v => v.Column == Tokens[iv].Column && v.Row == Tokens[iv].Row - 1))
                    {
                        v = Tokens.FindIndex(v => v.Column == Tokens[iv].Column && v.Row == Tokens[iv].Row - 1);
                        if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == player && Tokens[iv].Player == Player.None && Tokens[v].Player != Player.None)
                        {
                            if (VerticalValidation(Tokens[iv].Column))
                                return Tokens[iv].Column;
                        }
                    }
                    if (!(Tokens.Exists(v => v.Column == Tokens[iv].Column && v.Row == Tokens[iv].Row - 1)))
                    {
                        if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == player && Tokens[iv].Player == Player.None)
                        {
                            if (VerticalValidation(Tokens[iv].Column))
                                return Tokens[iv].Column;
                        }

                    }
                }
                //Winning Moves => Horizontal Check Self Middle Left Piece Missing
                player = Player.Player2;
                if (Tokens[i].Column >= 4 && Tokens[i].Player == player)
                {
                    //Check right
                    ii = Tokens.FindIndex(ii => ii.Column == Tokens[i].Column - 1 && ii.Row == Tokens[i].Row);
                    iii = Tokens.FindIndex(iii => iii.Column == Tokens[i].Column - 2 && iii.Row == Tokens[i].Row);
                    iv = Tokens.FindIndex(iv => iv.Column == Tokens[i].Column - 3 && iv.Row == Tokens[i].Row);
                    if (Tokens.Exists(v => v.Column == Tokens[ii].Column && v.Row == Tokens[ii].Row - 1))
                    {
                        v = Tokens.FindIndex(v => v.Column == Tokens[ii].Column && v.Row == Tokens[ii].Row - 1);
                        if (Tokens[i].Player == player && Tokens[ii].Player == Player.None && Tokens[iii].Player == player && Tokens[iv].Player == player && Tokens[v].Player != Player.None)
                        {
                            if (VerticalValidation(Tokens[ii].Column))
                                return Tokens[ii].Column;
                        }
                    }
                    if (Tokens[i].Player == player && Tokens[ii].Player == Player.None && Tokens[iii].Player == player && Tokens[iv].Player == player)
                    {
                        if (VerticalValidation(Tokens[ii].Column))
                            return Tokens[ii].Column;
                    }
                }
                //Winning Moves => Horizontal Check Self Middle Right Piece Missing
                if (Tokens[i].Column >= 4 && Tokens[i].Player == player)
                {
                    //Check right
                    ii = Tokens.FindIndex(ii => ii.Column == Tokens[i].Column - 1 && ii.Row == Tokens[i].Row);
                    iii = Tokens.FindIndex(iii => iii.Column == Tokens[i].Column - 2 && iii.Row == Tokens[i].Row);
                    iv = Tokens.FindIndex(iv => iv.Column == Tokens[i].Column - 3 && iv.Row == Tokens[i].Row);
                    if (Tokens.Exists(v => v.Column == Tokens[iii].Column && v.Row == Tokens[iii].Row - 1))
                    {
                        v = Tokens.FindIndex(v => v.Column == Tokens[iii].Column && v.Row == Tokens[iii].Row - 1);
                        if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == Player.None && Tokens[iv].Player == player && Tokens[v].Player != Player.None)
                        {
                            if (VerticalValidation(Tokens[iii].Column))
                                return Tokens[iii].Column;
                        }
                    }
                    if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == Player.None && Tokens[iv].Player == player)
                    {
                        if (VerticalValidation(Tokens[iii].Column))
                            return Tokens[iii].Column;
                    }
                }
                //Winning Moves => Horizontal Check Self Middle Right Piece Missing
                if ((Tokens[i].Column <= 4 && Tokens[i].Player == player))
                {
                    //check left
                    ii = Tokens.FindIndex(ii => ii.Column == Tokens[i].Column + 1 && ii.Row == Tokens[i].Row);
                    iii = Tokens.FindIndex(iii => iii.Column == Tokens[i].Column + 2 && iii.Row == Tokens[i].Row);
                    iv = Tokens.FindIndex(iv => iv.Column == Tokens[i].Column + 3 && iv.Row == Tokens[i].Row);
                    if (Tokens.Exists(v => v.Column == Tokens[ii].Column && v.Row == Tokens[ii].Row - 1))
                    {
                        v = Tokens.FindIndex(v => v.Column == Tokens[ii].Column && v.Row == Tokens[ii].Row - 1);
                        if (Tokens[i].Player == player && Tokens[ii].Player == Player.None && Tokens[iii].Player == player && Tokens[iv].Player == player && Tokens[v].Player != Player.None)
                        {
                            if (VerticalValidation(Tokens[ii].Column))
                                return Tokens[ii].Column;
                        }
                    }
                    if (!(Tokens.Exists(v => v.Column == Tokens[ii].Column && v.Row == Tokens[ii].Row - 1)))
                    {
                        if (Tokens[i].Player == player && Tokens[ii].Player == Player.None && Tokens[iii].Player == player && Tokens[iv].Player == player)
                        {
                            if (VerticalValidation(Tokens[ii].Column))
                                return Tokens[ii].Column;
                        }

                    }
                }

                if ((Tokens[i].Column <= 4 && Tokens[i].Player == player))
                {
                    //check left
                    ii = Tokens.FindIndex(ii => ii.Column == Tokens[i].Column + 1 && ii.Row == Tokens[i].Row);
                    iii = Tokens.FindIndex(iii => iii.Column == Tokens[i].Column + 2 && iii.Row == Tokens[i].Row);
                    iv = Tokens.FindIndex(iv => iv.Column == Tokens[i].Column + 3 && iv.Row == Tokens[i].Row);
                    if (Tokens.Exists(v => v.Column == Tokens[iii].Column && v.Row == Tokens[iii].Row - 1))
                    {
                        v = Tokens.FindIndex(v => v.Column == Tokens[iii].Column && v.Row == Tokens[iii].Row - 1);
                        if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == Player.None && Tokens[iv].Player == player && Tokens[v].Player != Player.None)
                        {
                            if (VerticalValidation(Tokens[iii].Column))
                                return Tokens[iii].Column;
                        }
                    }
                    if (!(Tokens.Exists(v => v.Column == Tokens[iii].Column && v.Row == Tokens[iii].Row - 1)))
                    {
                        if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == Player.None && Tokens[iv].Player == player)
                        {
                            if (VerticalValidation(Tokens[iii].Column))
                                return Tokens[iii].Column;
                        }

                    }
                }

                //Winning Moves => Horizontal Check Player1
                player = Player.Player1;
                if (Tokens[i].Column >= 4 && Tokens[i].Player == player)
                {
                    //Check right
                    ii = Tokens.FindIndex(ii => ii.Column == Tokens[i].Column - 1 && ii.Row == Tokens[i].Row);
                    iii = Tokens.FindIndex(iii => iii.Column == Tokens[i].Column - 2 && iii.Row == Tokens[i].Row);
                    iv = Tokens.FindIndex(iv => iv.Column == Tokens[i].Column - 3 && iv.Row == Tokens[i].Row);
                    if (Tokens.Exists(v => v.Column == Tokens[iv].Column && v.Row == Tokens[iv].Row - 1))
                    {
                        v = Tokens.FindIndex(v => v.Column == Tokens[iv].Column && v.Row == Tokens[iv].Row - 1);
                        if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == player && Tokens[iv].Player == Player.None && Tokens[v].Player != Player.None)
                        {
                            if (VerticalValidation(Tokens[iv].Column))
                                return Tokens[iv].Column;
                        }
                    }
                    if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == player && Tokens[iv].Player == Player.None)
                    {
                        if (VerticalValidation(Tokens[iv].Column))
                            return Tokens[iv].Column;
                    }
                }
                //Winning Moves => Horizontal Check Player1
                if ((Tokens[i].Column <= 4 && Tokens[i].Player == player))
                {
                    //check left
                    ii = Tokens.FindIndex(ii => ii.Column == Tokens[i].Column + 1 && ii.Row == Tokens[i].Row);
                    iii = Tokens.FindIndex(iii => iii.Column == Tokens[i].Column + 2 && iii.Row == Tokens[i].Row);
                    iv = Tokens.FindIndex(iv => iv.Column == Tokens[i].Column + 3 && iv.Row == Tokens[i].Row);
                    if (Tokens.Exists(v => v.Column == Tokens[iv].Column && v.Row == Tokens[iv].Row - 1))
                    {
                        v = Tokens.FindIndex(v => v.Column == Tokens[iv].Column && v.Row == Tokens[iv].Row - 1);
                        if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == player && Tokens[iv].Player == Player.None && Tokens[v].Player != Player.None)
                        {
                            if (VerticalValidation(Tokens[iv].Column))
                                return Tokens[iv].Column;
                        }
                    }
                    if (!(Tokens.Exists(v => v.Column == Tokens[iv].Column && v.Row == Tokens[iv].Row - 1)))
                    {
                        if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == player && Tokens[iv].Player == Player.None)
                        {
                            if (VerticalValidation(Tokens[iv].Column))
                                return Tokens[iv].Column;
                        }

                    }
                }
                //Winning Moves => Horizontal Check Self Middle Left Piece Missing
                if (Tokens[i].Column >= 4 && Tokens[i].Player == player)
                {
                    //Check right
                    ii = Tokens.FindIndex(ii => ii.Column == Tokens[i].Column - 1 && ii.Row == Tokens[i].Row);
                    iii = Tokens.FindIndex(iii => iii.Column == Tokens[i].Column - 2 && iii.Row == Tokens[i].Row);
                    iv = Tokens.FindIndex(iv => iv.Column == Tokens[i].Column - 3 && iv.Row == Tokens[i].Row);
                    if (Tokens.Exists(v => v.Column == Tokens[ii].Column && v.Row == Tokens[ii].Row - 1))
                    {
                        v = Tokens.FindIndex(v => v.Column == Tokens[ii].Column && v.Row == Tokens[ii].Row - 1);
                        if (Tokens[i].Player == player && Tokens[ii].Player == Player.None && Tokens[iii].Player == player && Tokens[iv].Player == player && Tokens[v].Player != Player.None)
                        {
                            if (VerticalValidation(Tokens[ii].Column))
                                return Tokens[ii].Column;
                        }
                    }
                    if (Tokens[i].Player == player && Tokens[ii].Player == Player.None && Tokens[iii].Player == player && Tokens[iv].Player == player)
                    {
                        if (VerticalValidation(Tokens[ii].Column))
                            return Tokens[ii].Column;
                    }
                }
                if (Tokens[i].Column >= 4 && Tokens[i].Player == player)
                {
                    //Check right
                    ii = Tokens.FindIndex(ii => ii.Column == Tokens[i].Column - 1 && ii.Row == Tokens[i].Row);
                    iii = Tokens.FindIndex(iii => iii.Column == Tokens[i].Column - 2 && iii.Row == Tokens[i].Row);
                    iv = Tokens.FindIndex(iv => iv.Column == Tokens[i].Column - 3 && iv.Row == Tokens[i].Row);
                    if (Tokens.Exists(v => v.Column == Tokens[iii].Column && v.Row == Tokens[iii].Row - 1))
                    {
                        v = Tokens.FindIndex(v => v.Column == Tokens[iii].Column && v.Row == Tokens[iii].Row - 1);
                        if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == Player.None && Tokens[iv].Player == player && Tokens[v].Player != Player.None)
                        {
                            if (VerticalValidation(Tokens[iii].Column))
                                return Tokens[iii].Column;
                        }
                    }
                    if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == Player.None && Tokens[iv].Player == player)
                    {
                        if (VerticalValidation(Tokens[iii].Column))
                            return Tokens[iii].Column;
                    }
                }
                //Winning Moves => Horizontal Check Self Middle Right Piece Missing
                if ((Tokens[i].Column <= 4 && Tokens[i].Player == player))
                {
                    //check left
                    ii = Tokens.FindIndex(ii => ii.Column == Tokens[i].Column + 1 && ii.Row == Tokens[i].Row);
                    iii = Tokens.FindIndex(iii => iii.Column == Tokens[i].Column + 2 && iii.Row == Tokens[i].Row);
                    iv = Tokens.FindIndex(iv => iv.Column == Tokens[i].Column + 3 && iv.Row == Tokens[i].Row);
                    if (Tokens.Exists(v => v.Column == Tokens[ii].Column && v.Row == Tokens[ii].Row - 1))
                    {
                        v = Tokens.FindIndex(v => v.Column == Tokens[ii].Column && v.Row == Tokens[ii].Row - 1);
                        if (Tokens[i].Player == player && Tokens[ii].Player == Player.None && Tokens[iii].Player == player && Tokens[iv].Player == player && Tokens[v].Player != Player.None)
                        {
                            if (VerticalValidation(Tokens[ii].Column))
                                return Tokens[iv].Column;
                        }
                    }
                    if (!(Tokens.Exists(v => v.Column == Tokens[ii].Column && v.Row == Tokens[ii].Row - 1)))
                    {
                        if (Tokens[i].Player == player && Tokens[ii].Player == Player.None && Tokens[iii].Player == player && Tokens[iv].Player == player)
                        {
                            if (VerticalValidation(Tokens[ii].Column))
                                return Tokens[ii].Column;
                        }

                    }
                }
                if ((Tokens[i].Column <= 4 && Tokens[i].Player == player))
                {
                    //check left
                    ii = Tokens.FindIndex(ii => ii.Column == Tokens[i].Column + 1 && ii.Row == Tokens[i].Row);
                    iii = Tokens.FindIndex(iii => iii.Column == Tokens[i].Column + 2 && iii.Row == Tokens[i].Row);
                    iv = Tokens.FindIndex(iv => iv.Column == Tokens[i].Column + 3 && iv.Row == Tokens[i].Row);
                    if (Tokens.Exists(v => v.Column == Tokens[iii].Column && v.Row == Tokens[iii].Row - 1))
                    {
                        v = Tokens.FindIndex(v => v.Column == Tokens[iii].Column && v.Row == Tokens[iii].Row - 1);
                        if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == Player.None && Tokens[iv].Player == player && Tokens[v].Player != Player.None)
                        {
                            if (VerticalValidation(Tokens[iii].Column))
                                return Tokens[iii].Column;
                        }
                    }
                    if (!(Tokens.Exists(v => v.Column == Tokens[iii].Column && v.Row == Tokens[iii].Row - 1)))
                    {
                        if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == Player.None && Tokens[iv].Player == player)
                        {
                            if (VerticalValidation(Tokens[iii].Column))
                                return Tokens[iii].Column;
                        }

                    }
                }
                //Winning Moves => Diagonal Self Checking
                //Diagonal Down Three in a Row
                player = Player.Player2;
                if (Tokens[i].Row >= 4 && Tokens[i].Column <= 4 && Tokens[i].Player == player)
                {
                    ii = Tokens.FindIndex(ii => ii.Row == Tokens[i].Row - 1 && ii.Column == Tokens[i].Column + 1);
                    iii = Tokens.FindIndex(iii => iii.Row == Tokens[i].Row - 2 && iii.Column == Tokens[i].Column + 2);
                    iv = Tokens.FindIndex(iv => iv.Row == Tokens[i].Row - 3 && iv.Column == Tokens[i].Column + 3);
                    if (Tokens.Exists(v => v.Row == Tokens[iv].Row - 1 && v.Row == Tokens[iv].Column))
                    {
                        v = Tokens.FindIndex(v => v.Row == Tokens[iv].Row - 1 && v.Row == Tokens[iv].Column);
                        if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == player && Tokens[iv].Player == Player.None && Tokens[v].Player != Player.None)
                        {
                            if (VerticalValidation(Tokens[iv].Column))
                                return Tokens[iv].Column;
                        }
                    }
                    if (!(Tokens.Exists(v => v.Row == Tokens[iv].Row - 1 && v.Row == Tokens[iv].Column)))
                    {
                        if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == player && Tokens[iv].Player == Player.None)
                        {
                            if (VerticalValidation(Tokens[iv].Column))
                                return Tokens[iv].Column;
                        }
                    }
                }
                //Diagonal Down Second Missing
                if (Tokens[i].Row >= 4 && Tokens[i].Column <= 4 && Tokens[i].Player == player)
                {
                    ii = Tokens.FindIndex(ii => ii.Row == Tokens[i].Row - 1 && ii.Column == Tokens[i].Column + 1);
                    iii = Tokens.FindIndex(iii => iii.Row == Tokens[i].Row - 2 && iii.Column == Tokens[i].Column + 2);
                    iv = Tokens.FindIndex(iv => iv.Row == Tokens[i].Row - 3 && iv.Column == Tokens[i].Column + 3);
                    if (Tokens.Exists(v => v.Row == Tokens[ii].Row - 1 && v.Row == Tokens[ii].Column))
                    {
                        v = Tokens.FindIndex(v => v.Row == Tokens[ii].Row - 1 && v.Row == Tokens[ii].Column);
                        if (Tokens[i].Player == player && Tokens[ii].Player == Player.None && Tokens[iii].Player == player && Tokens[iv].Player == player && Tokens[v].Player != Player.None)
                        {
                            if (VerticalValidation(Tokens[ii].Column))
                                return Tokens[ii].Column;
                        }
                    }
                    if (!(Tokens.Exists(v => v.Row == Tokens[ii].Row - 1 && v.Row == Tokens[ii].Column)))
                    {
                        if (Tokens[i].Player == player && Tokens[ii].Player == Player.None && Tokens[iii].Player == player && Tokens[iv].Player == player)
                        {
                            if (VerticalValidation(Tokens[ii].Column))
                                return Tokens[ii].Column;
                        }
                    }
                }
                //Diagonal Down Third Missing
                if (Tokens[i].Row >= 4 && Tokens[i].Column <= 4 && Tokens[i].Player == player)
                {
                    ii = Tokens.FindIndex(ii => ii.Row == Tokens[i].Row - 1 && ii.Column == Tokens[i].Column + 1);
                    iii = Tokens.FindIndex(iii => iii.Row == Tokens[i].Row - 2 && iii.Column == Tokens[i].Column + 2);
                    iv = Tokens.FindIndex(iv => iv.Row == Tokens[i].Row - 3 && iv.Column == Tokens[i].Column + 3);
                    if (Tokens.Exists(v => v.Row == Tokens[iii].Row - 1 && v.Row == Tokens[iii].Column))
                    {
                        v = Tokens.FindIndex(v => v.Row == Tokens[iii].Row - 1 && v.Row == Tokens[iii].Column);
                        if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == Player.None && Tokens[iv].Player == player && Tokens[v].Player != Player.None)
                        {
                            if (VerticalValidation(Tokens[iii].Column))
                                return Tokens[iii].Column;
                        }
                    }
                    if (!(Tokens.Exists(v => v.Row == Tokens[iii].Row - 1 && v.Row == Tokens[iii].Column)))
                    {
                        if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == Player.None && Tokens[iv].Player == player)
                        {
                            if (VerticalValidation(Tokens[iii].Column))
                                return Tokens[iii].Column;
                        }
                    }
                }
                //Diagonal Up Three in a Row
                if (Tokens[i].Row <= 3 && Tokens[i].Column <= 4 && Tokens[i].Player == player)
                {
                    ii = Tokens.FindIndex(ii => ii.Row == Tokens[i].Row + 1 && ii.Column == Tokens[i].Column + 1);
                    iii = Tokens.FindIndex(iii => iii.Row == Tokens[i].Row + 2 && iii.Column == Tokens[i].Column + 2);
                    iv = Tokens.FindIndex(iv => iv.Row == Tokens[i].Row + 3 && iv.Column == Tokens[i].Column + 3);
                    if (Tokens.Exists(v => v.Row == Tokens[iv].Row - 1 && v.Row == Tokens[iv].Column))
                    {
                        v = Tokens.FindIndex(v => v.Row == Tokens[iv].Row - 1 && v.Row == Tokens[iv].Column);
                        if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == player && Tokens[iv].Player == Player.None && Tokens[v].Player != Player.None)
                        {
                            if (VerticalValidation(Tokens[iv].Column))
                                return Tokens[iv].Column;
                        }
                    }
                    if (!(Tokens.Exists(v => v.Row == Tokens[iv].Row - 1 && v.Row == Tokens[iv].Column)))
                    {
                        if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == player && Tokens[iv].Player == Player.None)
                        {
                            if (VerticalValidation(Tokens[iv].Column))
                                return Tokens[iv].Column;
                        }
                    }
                }
                //Diagonal Up Second One Missing
                if (Tokens[i].Row <= 3 && Tokens[i].Column <= 4 && Tokens[i].Player == player)
                {
                    ii = Tokens.FindIndex(ii => ii.Row == Tokens[i].Row + 1 && ii.Column == Tokens[i].Column + 1);
                    iii = Tokens.FindIndex(iii => iii.Row == Tokens[i].Row + 2 && iii.Column == Tokens[i].Column + 2);
                    iv = Tokens.FindIndex(iv => iv.Row == Tokens[i].Row + 3 && iv.Column == Tokens[i].Column + 3);
                    if (Tokens.Exists(v => v.Row == Tokens[ii].Row - 1 && v.Row == Tokens[ii].Column))
                    {
                        v = Tokens.FindIndex(v => v.Row == Tokens[ii].Row - 1 && v.Row == Tokens[ii].Column);
                        if (Tokens[i].Player == player && Tokens[ii].Player == Player.None && Tokens[iii].Player == player && Tokens[iv].Player == player && Tokens[v].Player != Player.None)
                        {
                            if (VerticalValidation(Tokens[ii].Column))
                                return Tokens[ii].Column;
                        }
                    }
                    if (!(Tokens.Exists(v => v.Row == Tokens[ii].Row - 1 && v.Row == Tokens[ii].Column)))
                    {
                        if (Tokens[i].Player == player && Tokens[ii].Player == Player.None && Tokens[iii].Player == player && Tokens[iv].Player == player)
                        {
                            if (VerticalValidation(Tokens[ii].Column))
                                return Tokens[ii].Column;
                        }
                    }
                }
                //Diagonal Up Third One Missing
                if (Tokens[i].Row <= 3 && Tokens[i].Column <= 4 && Tokens[i].Player == player)
                {
                    ii = Tokens.FindIndex(ii => ii.Row == Tokens[i].Row + 1 && ii.Column == Tokens[i].Column + 1);
                    iii = Tokens.FindIndex(iii => iii.Row == Tokens[i].Row + 2 && iii.Column == Tokens[i].Column + 2);
                    iv = Tokens.FindIndex(iv => iv.Row == Tokens[i].Row + 3 && iv.Column == Tokens[i].Column + 3);
                    if (Tokens.Exists(v => v.Row == Tokens[iii].Row - 1 && v.Row == Tokens[iii].Column))
                    {
                        v = Tokens.FindIndex(v => v.Row == Tokens[iii].Row - 1 && v.Row == Tokens[iii].Column);
                        if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == Player.None && Tokens[iv].Player == player && Tokens[v].Player != Player.None)
                        {
                            if (VerticalValidation(Tokens[iii].Column))
                                return Tokens[iii].Column;
                        }
                    }
                    if (!(Tokens.Exists(v => v.Row == Tokens[iii].Row - 1 && v.Row == Tokens[iii].Column)))
                    {
                        if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == Player.None && Tokens[iv].Player == player)
                        {
                            if (VerticalValidation(Tokens[iii].Column))
                                return Tokens[iii].Column;
                        }
                    }
                }
                //Winning Moves => Diagonals Player1
                //Diagonal Down Three in a Row
                player = Player.Player1;
                if (Tokens[i].Row >= 4 && Tokens[i].Column <= 4 && Tokens[i].Player == player)
                {
                    ii = Tokens.FindIndex(ii => ii.Row == Tokens[i].Row - 1 && ii.Column == Tokens[i].Column + 1);
                    iii = Tokens.FindIndex(iii => iii.Row == Tokens[i].Row - 2 && iii.Column == Tokens[i].Column + 2);
                    iv = Tokens.FindIndex(iv => iv.Row == Tokens[i].Row - 3 && iv.Column == Tokens[i].Column + 3);
                    if (Tokens.Exists(v => v.Row == Tokens[iv].Row - 1 && v.Row == Tokens[iv].Column))
                    {
                        v = Tokens.FindIndex(v => v.Row == Tokens[iv].Row - 1 && v.Row == Tokens[iv].Column);
                        if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == player && Tokens[iv].Player == Player.None && Tokens[v].Player != Player.None)
                        {
                            if (VerticalValidation(Tokens[iv].Column))
                                return Tokens[iv].Column;
                        }
                    }
                    if (!(Tokens.Exists(v => v.Row == Tokens[iv].Row - 1 && v.Row == Tokens[iv].Column)))
                    {
                        if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == player && Tokens[iv].Player == Player.None)
                        {
                            if (VerticalValidation(Tokens[iv].Column))
                                return Tokens[iv].Column;
                        }
                    }
                }
                //Diagonal Down Second Missing
                if (Tokens[i].Row >= 4 && Tokens[i].Column <= 4 && Tokens[i].Player == player)
                {
                    ii = Tokens.FindIndex(ii => ii.Row == Tokens[i].Row - 1 && ii.Column == Tokens[i].Column + 1);
                    iii = Tokens.FindIndex(iii => iii.Row == Tokens[i].Row - 2 && iii.Column == Tokens[i].Column + 2);
                    iv = Tokens.FindIndex(iv => iv.Row == Tokens[i].Row - 3 && iv.Column == Tokens[i].Column + 3);
                    if (Tokens.Exists(v => v.Row == Tokens[ii].Row - 1 && v.Row == Tokens[ii].Column))
                    {
                        v = Tokens.FindIndex(v => v.Row == Tokens[ii].Row - 1 && v.Row == Tokens[ii].Column);
                        if (Tokens[i].Player == player && Tokens[ii].Player == Player.None && Tokens[iii].Player == player && Tokens[iv].Player == player && Tokens[v].Player != Player.None)
                        {
                            if (VerticalValidation(Tokens[ii].Column))
                                return Tokens[ii].Column;
                        }
                    }
                    if (!(Tokens.Exists(v => v.Row == Tokens[ii].Row - 1 && v.Row == Tokens[ii].Column)))
                    {
                        if (Tokens[i].Player == player && Tokens[ii].Player == Player.None && Tokens[iii].Player == player && Tokens[iv].Player == player)
                        {
                            if (VerticalValidation(Tokens[ii].Column))
                                return Tokens[ii].Column;
                        }
                    }
                }
                //Diagonal Down Third Missing
                if (Tokens[i].Row >= 4 && Tokens[i].Column <= 4 && Tokens[i].Player == player)
                {
                    ii = Tokens.FindIndex(ii => ii.Row == Tokens[i].Row - 1 && ii.Column == Tokens[i].Column + 1);
                    iii = Tokens.FindIndex(iii => iii.Row == Tokens[i].Row - 2 && iii.Column == Tokens[i].Column + 2);
                    iv = Tokens.FindIndex(iv => iv.Row == Tokens[i].Row - 3 && iv.Column == Tokens[i].Column + 3);
                    if (Tokens.Exists(v => v.Row == Tokens[iii].Row - 1 && v.Row == Tokens[iii].Column))
                    {
                        v = Tokens.FindIndex(v => v.Row == Tokens[iii].Row - 1 && v.Row == Tokens[iii].Column);
                        if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == Player.None && Tokens[iv].Player == player && Tokens[v].Player != Player.None)
                        {
                            if (VerticalValidation(Tokens[iii].Column))
                                return Tokens[iii].Column;
                        }
                    }
                    if (!(Tokens.Exists(v => v.Row == Tokens[iii].Row - 1 && v.Row == Tokens[iii].Column)))
                    {
                        if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == Player.None && Tokens[iv].Player == player)
                        {
                            if (VerticalValidation(Tokens[iii].Column))
                                return Tokens[iii].Column;
                        }
                    }
                }
                //Diagonal Up Three in a Row
                if (Tokens[i].Row <= 3 && Tokens[i].Column <= 4 && Tokens[i].Player == player)
                {
                    ii = Tokens.FindIndex(ii => ii.Row == Tokens[i].Row + 1 && ii.Column == Tokens[i].Column + 1);
                    iii = Tokens.FindIndex(iii => iii.Row == Tokens[i].Row + 2 && iii.Column == Tokens[i].Column + 2);
                    iv = Tokens.FindIndex(iv => iv.Row == Tokens[i].Row + 3 && iv.Column == Tokens[i].Column + 3);
                    if (Tokens.Exists(v => v.Row == Tokens[iv].Row - 1 && v.Row == Tokens[iv].Column))
                    {
                        v = Tokens.FindIndex(v => v.Row == Tokens[iv].Row - 1 && v.Row == Tokens[iv].Column);
                        if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == player && Tokens[iv].Player == Player.None && Tokens[v].Player != Player.None)
                        {
                            if (VerticalValidation(Tokens[iv].Column))
                                return Tokens[iv].Column;
                        }
                    }
                    if (!(Tokens.Exists(v => v.Row == Tokens[iv].Row - 1 && v.Row == Tokens[iv].Column)))
                    {
                        if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == player && Tokens[iv].Player == Player.None)
                        {
                            if (VerticalValidation(Tokens[iv].Column))
                                return Tokens[iv].Column;
                        }
                    }
                }
                //Diagonal Up Second One Missing
                if (Tokens[i].Row <= 3 && Tokens[i].Column <= 4 && Tokens[i].Player == player)
                {
                    ii = Tokens.FindIndex(ii => ii.Row == Tokens[i].Row + 1 && ii.Column == Tokens[i].Column + 1);
                    iii = Tokens.FindIndex(iii => iii.Row == Tokens[i].Row + 2 && iii.Column == Tokens[i].Column + 2);
                    iv = Tokens.FindIndex(iv => iv.Row == Tokens[i].Row + 3 && iv.Column == Tokens[i].Column + 3);
                    if (Tokens.Exists(v => v.Row == Tokens[ii].Row - 1 && v.Row == Tokens[ii].Column))
                    {
                        v = Tokens.FindIndex(v => v.Row == Tokens[ii].Row - 1 && v.Row == Tokens[ii].Column);
                        if (Tokens[i].Player == player && Tokens[ii].Player == Player.None && Tokens[iii].Player == player && Tokens[iv].Player == player && Tokens[v].Player != Player.None)
                        {
                            if (VerticalValidation(Tokens[ii].Column))
                                return Tokens[ii].Column;
                        }
                    }
                    if (!(Tokens.Exists(v => v.Row == Tokens[ii].Row - 1 && v.Row == Tokens[ii].Column)))
                    {
                        if (Tokens[i].Player == player && Tokens[ii].Player == Player.None && Tokens[iii].Player == player && Tokens[iv].Player == player)
                        {
                            if (VerticalValidation(Tokens[ii].Column))
                                return Tokens[ii].Column;
                        }
                    }
                }
                //Diagonal Up Third One Missing
                if (Tokens[i].Row <= 3 && Tokens[i].Column <= 4 && Tokens[i].Player == player)
                {
                    ii = Tokens.FindIndex(ii => ii.Row == Tokens[i].Row + 1 && ii.Column == Tokens[i].Column + 1);
                    iii = Tokens.FindIndex(iii => iii.Row == Tokens[i].Row + 2 && iii.Column == Tokens[i].Column + 2);
                    iv = Tokens.FindIndex(iv => iv.Row == Tokens[i].Row + 3 && iv.Column == Tokens[i].Column + 3);
                    if (Tokens.Exists(v => v.Row == Tokens[iii].Row - 1 && v.Row == Tokens[iii].Column))
                    {
                        v = Tokens.FindIndex(v => v.Row == Tokens[iii].Row - 1 && v.Row == Tokens[iii].Column);
                        if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == Player.None && Tokens[iv].Player == player && Tokens[v].Player != Player.None)
                        {
                            if (VerticalValidation(Tokens[iii].Column))
                                return Tokens[iii].Column;
                        }
                    }
                    if (!(Tokens.Exists(v => v.Row == Tokens[iii].Row - 1 && v.Row == Tokens[iii].Column)))
                    {
                        if (Tokens[i].Player == player && Tokens[ii].Player == player && Tokens[iii].Player == Player.None && Tokens[iv].Player == player)
                        {
                            if (VerticalValidation(Tokens[iii].Column))
                                return Tokens[iii].Column;
                        }
                    }
                }
            }
            int column = random.Next(1, 12);
            if (column > 7)
                column = 4;
            if (VerticalValidation(column))
                return column;
        }
    }
}


public class Token
{
    
    public Player Player { get; set; }
    public TokenColor Color { get; set; }
    public char Symbol { get; set; }
    public int Column { get; set; }
    public int Row { get; set; }

    public Token(int row, int column)
    {
        Player = Player.None;
        Color = TokenColor.None;
        Symbol = ' ';
        Column = column;
        Row = row;
    }

    public Token(Player player, char symbol, int row, int column)
    {
        Player = player;
        Color = GetPlayerColor(player);
        Symbol = symbol;
        Column = column;
        Row = row;
    }

    public TokenColor GetPlayerColor(Player player)
    {
        if (player == Player.Player1) return TokenColor.Red;
        if (player == Player.Player2) return TokenColor.Yellow;
        else return TokenColor.None;
    }

}



public enum TokenColor
{
    None,
    Red,
    Green,
    Blue,
    Yellow,
    Cyan,
    Purple
}

public enum Player
{
    None,
    Player1,
    Player2
}

public enum Difficulty
{
    Easy,
    Medium,
    Hard,
    Pro
}