using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Spectre.Console;

namespace WordScramble
{
    public class Game
    {
        /// <summary>
        /// The provider responsible for generating the words for the game.
        /// </summary>
        private readonly WordProvider wordProvider;

        /// <summary>
        /// A list to store the last 5 game results for the game stats board.
        /// </summary>
        private readonly GameResult[] gameStats;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// Sets up the word provider, evaluator, and initializes the 
        /// game stats board.
        /// </summary>
        public Game()
        {
            wordProvider = new WordProvider();
            gameStats = new GameResult[5];
            for (int i = 0; i < gameStats.Length; i++)
            {
                gameStats[i] = null;
            }
        }

        /// <summary>
        /// Displays the main menu of the game and prompts the user to choose 
        /// an option. The available choices are to start the game, view the 
        /// game stats board, or quit the game.
        /// </summary>
        /// <remarks>
        /// This method uses the <see cref="Spectre.Console"/> library to 
        /// display the main menu and handle user input for choosing different 
        /// options. The game will continue prompting the user until they choose
        /// to quit.
        /// </remarks>
        public void ShowMenu()
        {
            while (true)
            {
                AnsiConsole.Clear();
                string choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold yellow]Word Scramble[/]")
                        .AddChoices("Start Game", "View Game Stats","BreakDown Chart", "Quit"));

                switch (choice)
                {
                    case "Start Game":
                        StartGame();
                        break;
                    case "View Game Stats":
                        ShowGameStats();
                        break;
                    case "BreakDown Chart":
                        ShowBreakDownChart();
                        break;
                    case "Quit":
                        return;
                }
            }
        }

        /// <summary>
        /// Starts a new game round where the player must unscramble a word.
        /// The game measures the time taken by the player to guess the word.
        /// </summary>
        /// <remarks>
        /// In this method, a random word is generated using the 
        /// <see cref="WordProvider"/> and scrambled. The player is asked to 
        /// guess the unscrambled word. The game checks if the player's guess 
        /// is correct. If correct, the result is stored in the game stats 
        /// board; otherwise, the correct word is displayed. The game stats 
        /// board only stores the last 5 results.
        /// </remarks>
        private void StartGame()
        {
            /// <summary>
            /// The randomly chosen word for the current round.
            /// </summary>
            string word = wordProvider.GetRandomWord();

            /// <summary>
            /// The scrambled version of the word.
            /// </summary>
            string scrambledWord = wordProvider.GetScrambledWord(word);

            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[bold green]Unscramble the word:[/]");
            AnsiConsole.MarkupLine($"[italic yellow]{scrambledWord}[/]");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            /// <summary>
            /// The player's guess for the unscrambled word.
            /// </summary>
            string userInput = AnsiConsole.Ask<string>(
                "\n[bold cyan]Your Guess (type the word):[/] ");

            stopwatch.Stop();
            double timeTaken = stopwatch.Elapsed.TotalSeconds;

            /// <summary>
            /// Checks if the player's guess is correct.
            /// </summary>
            bool isCorrect = string.Equals(userInput, word, StringComparison.OrdinalIgnoreCase);

            if (isCorrect)
            {
                AnsiConsole.MarkupLine("\n[bold green]You Won![/]");
                AnsiConsole.MarkupLine(
                    $"[bold]Time Taken:[/] {timeTaken:F2} Seconds");

                
                for (int i = gameStats.Length - 1; i > 0; i--)
                {
                    gameStats[i] = gameStats[i - 1];
                }
                gameStats[0] = new GameResult(word, timeTaken);

            }
            else
            {
                AnsiConsole.MarkupLine("\n[bold red]You Lost![/]");
                AnsiConsole.MarkupLine($"[bold]Correct Word:[/] {word}");
                AnsiConsole.MarkupLine(
                    $"[bold]Time Taken:[/] {timeTaken:F2} Seconds");
            }

            AnsiConsole.Markup(
                "\n[bold green]Press Enter to Return to the Menu...[/]");
            Console.ReadLine();
        }

        /// <summary>
        /// Displays the game stats board showing the last 5 results with the 
        /// word and time taken.
        /// </summary>
        /// <remarks>
        /// This method uses <see cref="Spectre.Console"/> to format and display
        /// a table with the game stats board results. Each row displays the 
        /// rank, word, and time taken for each entry.
        /// </remarks>
        private void ShowGameStats()
        {
            AnsiConsole.Clear();
            Table table = new Table();
            table.AddColumn("#");
            table.AddColumn("Word");
            table.AddColumn("Time Taken (s)");

            for (int i = 0; i < gameStats.Length; i++)
            {
                if (gameStats[i] == null)
                {
                    continue;
                }
                string rank = (i + 1).ToString();
                string word = gameStats[i].Word;
                string timeTaken = gameStats[i].TimeTaken.ToString("F2");
                table.AddRow(rank, word, timeTaken);
    
            }

            AnsiConsole.Write(table);
            AnsiConsole.Markup(
                "\n[bold green]Press Enter to Return to Menu...[/]");
            Console.ReadLine();
        }
        
        private void ShowBreakDownChart()
        {
            // Create a new chart

            BreakdownChart brk = new BreakdownChart()
                .Width(60);
            
            
            // Add the data to the chart

            int countTotal = gameStats.Count(g => g != null);
            for (int i = 0; i < gameStats.Length; i++)
            {
                if (gameStats[i] == null)
                {
                    continue;
                }
                string word = gameStats[i].Word;
                int count = gameStats.Count(g => g != null && g.Word == word);
                double percentage = Math.Round((double)count / countTotal * 100, 2);
                if (brk.Data.Any(item => item.Label == word))
                {
                    continue;
                }
                brk.AddItem(word, percentage, Color.Green);
            }


            AnsiConsole.Clear();
            AnsiConsole.Write(brk
            .ShowPercentage());

            AnsiConsole.Markup(
                "\n[bold green]Press Enter to Return to Menu...[/]");
            Console.ReadLine();
        
        
        }
    }
}