using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahjong
{
    internal class GameConsolePlay : GamePlay
    {
        GamePlay ?currentGame;
        //PlayerChoice ?choice;
        public override void Play()
        {
            SetUpGame();
            if (currentGame is null) { return; }

            bool WinnerMahjong = false;
            bool OnlyOnePlayer = false;

            Console.WriteLine($"Player {currentGame?.Players?.First().Name}, here is your rack: ");
            Console.WriteLine(currentGame?.Players?.First().Rack?.ToString());

            Console.WriteLine($"Player {currentGame?.Players?.First().Name}, Choose a Tile To Discard (by index)");
            string? discard = Console.ReadLine();
            int discardIndex = 0;
            bool goodInput = int.TryParse(discard, out discardIndex);
            if (!goodInput || (discardIndex < 0 && discardIndex >= 14)) { return; }

            Tile ?discardTile = currentGame?.Players?.First().Rack?.Hand[discardIndex];
            if (discardTile is null) { return; }

            currentGame?.Discard(discardTile);

            Console.WriteLine($"You chose to discard {discardTile.ToString()}");

            Console.ReadKey();

            Player? moveToLastPlayer = currentGame?.Players?.First();
            if (moveToLastPlayer != null)
            {
                currentGame?.Players?.RemoveFirst();
                currentGame?.Players?.AddLast(moveToLastPlayer);
            }
            else
            {
                currentGame?.Players?.RemoveFirst();
            }

            // TODO maybe change to dowhile loop so when all call mahjong there's a winner
            while (!WinnerMahjong && !OnlyOnePlayer)
            {
                Console.WriteLine($"At the top of the discard pile: {currentGame?.DiscardTiles?.Last().ToString()}\n");
                Console.WriteLine($"Player {currentGame?.Players?.First().Name}, here is your rack: ");
                Console.WriteLine(currentGame?.Players?.First().Rack?.ToString());

                if (currentGame?.Players?.First() is ComputerPlayer)
                {
                    bool mahjongable = false;
                    bool choosewall = false;
                    discardTile = currentGame?.Players?.First().TilePickUp(currentGame?.Walls.First().WallTiles[0], discardTile, out mahjongable, out choosewall);
                    Console.WriteLine($"Computer chose wall? {choosewall}");
                    Console.WriteLine($"Computer has Mahjong? {mahjongable}");
                    if (discardTile is null) { return; }
                    goto COMPUTERSKIPPED;
                }


                Console.WriteLine($"Player {currentGame?.Players?.First().Name}, Choose 1: to take from Wall, 2: to take from discard");
                string? choice = Console.ReadLine();
                
                
                int choiceIndex = 0;
                bool input = int.TryParse(choice, out choiceIndex);
                if (!input || (choiceIndex < 1 && choiceIndex >= 3)) { return; }

                if (choiceIndex == 1) {
                    currentGame?.PickFromWall();
                } else
                {
                    currentGame?.PickDiscard();
                }

                Console.WriteLine($"Player {currentGame?.Players?.First().Name}, here is your rack: ");
                Console.WriteLine(currentGame?.Players?.First().Rack?.ToString());

                Console.ReadKey();

                

                Console.WriteLine($"Player {currentGame?.Players?.First().Name}, Do you wish to call Mahjong?  Press 1 if you wish to call Mahjong or enter to skip.");
                string? mahJongQuestion = Console.ReadLine();
                int mahJongQuestionIndex = 0;
                _ = int.TryParse(mahJongQuestion, out mahJongQuestionIndex);
                if (mahJongQuestionIndex == 1) 
                {
                    WinnerMahjong = (bool)currentGame?.CallMahJong();
                    if (!((bool)WinnerMahjong)) {
                        Console.WriteLine($"Game over {currentGame?.Players?.First().Name}, that was not Mahjong");
                        currentGame?.Players?.RemoveFirst();
                        continue;
                        // TODO - no discard pile to choose from
                    }
                }

                if (currentGame?.Players?.Count == 1) { OnlyOnePlayer = true; }

                Console.WriteLine($"Player {currentGame?.Players?.First().Name}, Choose a Tile To Discard (by index)");
                discard = Console.ReadLine();
                discardIndex = 0;
                goodInput = int.TryParse(discard, out discardIndex);
                if (!goodInput || (discardIndex < 0 && discardIndex >= 14)) { return; }

                discardTile = currentGame?.Players?.First().Rack?.Hand[discardIndex];

            
                
            COMPUTERSKIPPED:

                if (discardTile is null) { return; }

                currentGame?.Discard(discardTile);
                Console.WriteLine($"You chose to discard {discardTile.ToString()}");

                Console.ReadKey();

                //SKIPDISCARD:

                Player? nextPlayer = currentGame?.Players?.First();
                if (nextPlayer != null)
                {
                    currentGame?.Players?.RemoveFirst();
                    currentGame?.Players?.AddLast(nextPlayer);
                }
                else
                {
                    currentGame?.Players?.RemoveFirst();
                }
            }
        }

        protected override void SetUpGame()
        {
            Console.WriteLine("First Player Name: ");
            string? One = Console.ReadLine();
            if (One == null) { One = "One"; };

            Console.WriteLine("Second Player Name: ");
            string? Two = Console.ReadLine();
            if (Two == null) { Two = "Two"; };

            Console.WriteLine("Third Player Name: ");
            string? Three = Console.ReadLine();
            if (Three == null) { Three = "Three"; };

            Console.WriteLine("Fourth Player Name: ");
            string? Four = Console.ReadLine();
            if (Four == null) { Four = "Four"; };

            Player playerOne = new Player() { Name = One };
            Player playerTwo = new ComputerPlayer() { Name = Two };
            Player playerThree = new ComputerPlayer() { Name = Three };
            //Player playerFour = new Player() { Name = Four };
            Player playerFour = new ComputerPlayer() { Name = Four };

            List<Tile> gamesShuffledTiles = new List<Tile>(Tile.ShuffledTiles);

            Tile[] rack = new Tile[14];
            Tile[] rack1 = new Tile[14];
            Tile[] rack2 = new Tile[14];
            Tile[] rack3 = new Tile[14];

            Tile tileHolder = gamesShuffledTiles[0];

            for (int i = 0; i < 14; i++)
            {
                rack[i] = gamesShuffledTiles[0];
                gamesShuffledTiles.RemoveAt(0);
            }

            playerOne.Rack = new Rack(rack.ToArray());

            for (int i = 0; i < 13; i++)
            {
                rack1[i] = gamesShuffledTiles[0];
                gamesShuffledTiles.RemoveAt(0);
            }
            playerTwo.Rack = new Rack(rack1.ToArray());

            for (int i = 0; i < 13; i++)
            {
                rack2[i] = gamesShuffledTiles[0];
                gamesShuffledTiles.RemoveAt(0);
            }
            playerThree.Rack = new Rack(rack2.ToArray());

            for (int i = 0; i < 13; i++)
            {
                rack3[i] = gamesShuffledTiles[0];
                gamesShuffledTiles.RemoveAt(0);
            }
            playerFour.Rack = new Rack(rack3.ToArray());

            int quarters = gamesShuffledTiles.Count / 4;
            int lastQuarter = gamesShuffledTiles.Count % 4 == 0 ? quarters : quarters - 1;

            Wall wallOne = new Wall();
            wallOne.WallTiles = gamesShuffledTiles.GetRange(0, quarters);
            gamesShuffledTiles.RemoveRange(0, quarters);

            Wall wallTwo = new Wall();
            wallTwo.WallTiles = gamesShuffledTiles.GetRange(0, quarters);
            gamesShuffledTiles.RemoveRange(0, quarters);

            Wall wallThree = new Wall();
            wallThree.WallTiles = gamesShuffledTiles.GetRange(0, quarters);
            gamesShuffledTiles.RemoveRange(0, quarters);

            Wall wallFour = new Wall();
            wallFour.WallTiles = gamesShuffledTiles.GetRange(0, lastQuarter);
            gamesShuffledTiles.RemoveRange(0, lastQuarter);

            foreach (Tile t in gamesShuffledTiles)
            {
                wallFour.WallTiles.Add(t);
            }

            currentGame = new GamePlay([wallOne, wallTwo, wallThree, wallFour],
                                                playerOne, playerTwo, playerThree, playerFour);

        } // end set-up game

        public override bool ChooseOwnTilesToPass(int numTilesToChoose, Player player, out Tile?[]? tiles)
        {
            tiles = default;
            return false;

        }
    }
}
