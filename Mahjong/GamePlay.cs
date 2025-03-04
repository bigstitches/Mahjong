using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahjong
{
    public delegate bool PlayerChoice(params Tile[]? tile);
    public class GamePlay
    {
        private LinkedList<Player> ?players;
        private LinkedList<Wall> ?walls;
        private List<Tile> discardTiles;

        public GamePlay()
        {
            players = new LinkedList<Player>();
            walls = new LinkedList<Wall>();
            discardTiles = new List<Tile>();
        }

        public GamePlay(Player[]? players, Wall[]? walls, Rack[]? racks)
        {
            if (players != null && racks != null && players.Length == 4 && racks.Length == 4)
            {
                this.players = new LinkedList<Player>();
                Array.ForEach(players, player => { this.players.AddLast(player); });
                racks.Zip(players, (rack, player) => player.Rack = rack);
            }

            discardTiles = new List<Tile>();

            if (walls != null)
            {
                this.walls = new LinkedList<Wall>();
                Array.ForEach(walls, wall => { this.walls.AddLast(wall); });
            }
        }

        public GamePlay(Wall[]? walls, params Player[]? players)
        {
            if (players != null && players.Length == 4)
            {
                this.players = new LinkedList<Player>();
                Array.ForEach(players, player => { this.players.AddLast(player); });
            }

            discardTiles = new List<Tile>();

            if (walls != null)
            {
                this.walls = new LinkedList<Wall>();
                Array.ForEach(walls, wall => { this.walls.AddLast(wall); });
            }
        }

        public LinkedList<Player> ?Players => players;
        public LinkedList<Wall> ?Walls => walls;
        public List<Tile> ?DiscardTiles => discardTiles;

        bool EmptyWall { get; set; }
        bool GameWon { get; set; }

        public virtual void Play() {}

        public bool RemovePlayer(Player playerToRemove)
        {
            if (players == null) { return false; }
            if (players.Remove(playerToRemove))
            {
                if (players.Count == 1)
                {
                    Winner(players.First());
                }
                return true;
            }
            return false;
        }
        public bool PickFromWall()
        {
            if (walls == null) { return false; }
            if (walls.Count == 0) { EmptyWall = true; return false; }
            if (players == null) { return false; }
            
            Rack ?rack = players.First().Rack;

            if (rack == null) { return false; }
            
            if (walls.First().WallTiles.Count > 0)
            {
                Tile tile = walls.First().WallTiles.Last();
                rack.AddTile(tile);
                walls.First().WallTiles.Remove(tile);
                return true;
            } else
            {
                walls.RemoveFirst();
                PickFromWall();
            }

            return false;
        }

        public bool Discard(Tile tile)
        {
            if (players == null) { return false; }

            Rack ?rack = players.First().Rack;

            if (rack == null) { return false; }

            if (rack.RemoveTile(tile))
            {
                discardTiles.Add(tile);
                return true;
            } 
        
            return false;
        }

        public virtual bool ChooseOwnTilesToPass(int numTilesToChoose, Player player, out Tile?[]? tiles)
        {
            tiles = default;
            return false;
        }

        public bool Discard(params Tile[] tiles)
        {
            Array.ForEach(tiles, tile => { Discard(tile); });
            return true;
        }

        public bool PickDiscard()
        {
            // TODO
            bool identifyQuartTrip = true;

            if (identifyQuartTrip)
            {
                // add the last discarded Tile to the player's rack
                Players?.First().Rack?.AddTile(discardTiles.Last());
                discardTiles.RemoveAt(discardTiles.Count - 1);
                return true;
            }

            return false;
        }

        private bool PickDiscard(Rack? rack)
        {
            if ( rack == null) { return false; }

            // TODO
            bool identifyQuartTrip = true;

            if (identifyQuartTrip)
            {
                // add the last discarded Tile to the player's rack
                _ = rack.AddTile(discardTiles.Last());
                discardTiles.RemoveAt(discardTiles.Count - 1);
                return true;
            }

            return false;
        }

        public bool PickDiscard(Tile tile)
        {
            discardTiles.Add(tile);
            if (players == null) { return false; }
            return PickDiscard(players.First().Rack);
        }

        public bool CallMahJong()
        {
            if (players is not null) { return false; }

            Rack? rack = players?.First().Rack;

            if (rack == null) { return false; }
            if (rack.Hand is not null) { return CallMahJong(rack.Hand); }

            return false;
        }

        public bool CallMahJong(Tile?[] tiles)
        {
            if (tiles == null) {  return false; }
            if (players == null) { return false; }

            List<Tile> list = [];
            foreach (Tile ?tile in tiles)
            {
                if (tile is null) { return false; }
                list.Add(tile);
            }
            
            if (Sequences.IsWinningHand(list))
            {
                Winner(players.First());
                return true;
            }
            return false;
        }

        private void Winner(Player player)
        {
            GameWon = true;
            // TODO count score of player, bad score for all the other players
            //throw new NotImplementedException();
        }


        protected virtual void SetUpGame()
        {
            Player playerOne = new Player() { Name = "Human" };
            Player playerTwo = new ComputerPlayer() { Name = "Computer2" };
            Player playerThree = new ComputerPlayer() { Name = "Computer3" };
            Player playerFour = new ComputerPlayer() { Name = "Computer4" };

            List<Tile> gamesShuffledTiles = new List<Tile>(Tile.ShuffledTiles);

            Tile[] rack = new Tile[14];
            Tile[] rack1 = new Tile[14];
            Tile[] rack2 = new Tile[14];
            Tile[] rack3 = new Tile[14];

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

        } // end set-up game

    }
}
