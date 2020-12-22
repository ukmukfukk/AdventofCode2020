using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.Day22Help;
using Microsoft.Extensions.Logging;

namespace AdventOfCode2020
{
    public class Day22 : SimpleDay
    {
        public override IList<string> InputFiles => new List<string> { "input22test.txt", "input22test2.txt", "input22.txt" };

        public override string Name => "Day 22";

        public List<int> Player1 { get; private set; }

        public List<int> Player2 { get; private set; }

        public List<int> Games { get; private set; }

        public override void SolvePuzzle1(IList<IList<string>> inputs)
        {
            for (int i = 0; i < inputs.Count; i++)
            {
                if (i != 1)
                {
                    SolvePuzzle1(inputs[i]);
                }
            }
        }

        protected override void SolvePuzzle1(IList<string> input)
        {
            Helper.Logger = new ConsoleLogger(LogLevel.Information);

            ReadInput(input);
            PlayGame();

            var winner = Player1.Count > 0 ? Player1 : Player2;
            long result = EvaluateWinner(winner);

            Helper.Logger.Log(Name, $"result: {result}");
            Console.ReadLine();
        }

        protected override void SolvePuzzle2(IList<string> input)
        {
            Helper.Logger = new ConsoleLogger(LogLevel.Information);
            ReadInput(input);

            Games = new List<int> { 0 };
            Log($"Start {DateTime.Now}", LogLevel.Critical);
            int winnerid = PlayRecursiveGame(Player1, Player2);
            Log($"End {DateTime.Now}", LogLevel.Critical);

            var winner = winnerid == 1 ? Player1 : Player2;
            long result = EvaluateWinner(winner);

            Log($"result: {result}", LogLevel.None);
            Console.ReadLine();
        }

        private long EvaluateWinner(List<int> winner)
        {
            long result = 0;
            for (int i = 0; i < winner.Count; i++)
            {
                result += (i + 1) * winner[winner.Count - i - 1];
            }

            return result;
        }

        private int PlayGame()
        {
            int r = 0;
            int p1;
            int p2;

            while (Player1.Count > 0 && Player2.Count > 0)
            {
                p1 = Player1[0];
                p2 = Player2[0];
                Player1 = Player1.Skip(1).ToList();
                Player2 = Player2.Skip(1).ToList();
                if (p1 > p2)
                {
                    Player1.Add(p1);
                    Player1.Add(p2);
                }
                else
                {
                    Player2.Add(p2);
                    Player2.Add(p1);
                }

                r++;
            }

            return r;
        }

        private void ReadInput(IList<string> input)
        {
            Player1 = new List<int>();
            int i = 1;
            while (!string.IsNullOrEmpty(input[i]))
            {
                Player1.Add(int.Parse(input[i]));
                i++;
            }

            i += 2;
            Player2 = new List<int>();
            while (i < input.Count)
            {
                Player2.Add(int.Parse(input[i]));
                i++;
            }
        }

        private int PlayRecursiveGame(List<int> player1, List<int> player2)
        {
            int gameId = Games.Max() + 1;
            Games.Add(gameId);
            LogNoName($"=== Game {gameId} ===", LogLevel.Information);

            int r = 1;
            int p1;
            int p2;
            int winnerid;
            var history = new DeckHistory();

            while (player1.Count > 0 && player2.Count > 0)
            {
                LogNoName(string.Empty, LogLevel.Information);
                LogNoName($"-- Round {r} (Game {gameId}) --", LogLevel.Warning);
                LogNoName($"Player 1's deck: {string.Join(", ", player1)}", LogLevel.Information);
                LogNoName($"Player 2's deck: {string.Join(", ", player2)}", LogLevel.Information);

                if (history.HadAlready(player1, player2))
                {
                    LogNoName($"Player 1 wins round {r} of game {gameId}!", LogLevel.Information);
                    LogNoName($"The winner of game {gameId} is player 1!", LogLevel.Information);
                    if (gameId == 1)
                    {
                        LogNoName(string.Empty, LogLevel.Information);
                        LogNoName(string.Empty, LogLevel.Information);
                        LogNoName("== Post-game results ==", LogLevel.Information);
                        LogNoName($"Player 1's deck: {string.Join(", ", player1)}", LogLevel.Information);
                        LogNoName($"Player 2's deck: {string.Join(", ", player2)}", LogLevel.Information);
                        Player1 = player1;
                        Player2 = player2;
                    }

                    return 1;
                }

                history.Add(new List<int>(player1), new List<int>(player2));
                p1 = player1[0];
                p2 = player2[0];
                LogNoName($"Player 1 plays: {p1}", LogLevel.Information);
                LogNoName($"Player 2 plays: {p2}", LogLevel.Information);
                player1 = player1.Skip(1).ToList();
                player2 = player2.Skip(1).ToList();

                if (p1 <= player1.Count && p2 <= player2.Count)
                {
                    LogNoName("Playing a sub-game to determine the winner...", LogLevel.Information);
                    LogNoName(string.Empty, LogLevel.Information);
                    winnerid = PlayRecursiveGame(new List<int>(player1.Take(p1)), new List<int>(player2.Take(p2)));
                    LogNoName(string.Empty, LogLevel.Information);
                    LogNoName($"...anyway, back to game {gameId}.", LogLevel.Information);
                }
                else
                {
                    winnerid = p1 > p2 ? 1 : 2;
                }

                LogNoName($"Player {winnerid} wins round {r} of game {gameId}!", LogLevel.Information);
                if (winnerid == 1)
                {
                    player1.Add(p1);
                    player1.Add(p2);
                }
                else
                {
                    player2.Add(p2);
                    player2.Add(p1);
                }

                r++;
            }

            winnerid = player1.Count > 0 ? 1 : 2;
            LogNoName($"The winner of game {gameId} is player {winnerid}!", LogLevel.Information);
            if (gameId == 1)
            {
                LogNoName();
                LogNoName();
                LogNoName("== Post-game results ==");
                LogNoName($"Player 1's deck: {string.Join(", ", player1)}");
                LogNoName($"Player 2's deck: {string.Join(", ", player2)}");
                Player1 = player1;
                Player2 = player2;
            }

            return winnerid;
        }
    }
}
