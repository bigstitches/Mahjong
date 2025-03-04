using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahjong
{
    internal class Game
    {
        // every game has four players
        // turns go in circle
        // if turn is missy skip unless it's time to pull out wall
        // every player has 14 or 13 tiles
        // the first player has 14 tiles and discards first
        // the second player has 13 tiles and picks up the discard or gets from the wall
        //public static void Main()
        //{
        //    Player one = new Player() { Name = "Player One", Order = 1 };
        //    Player two = new Player() { Name = "Player Two", Order = 2 };
        //    Player three = new Player() { Name = "Player Three", Order = 3 };
        //    Player four = new Player() { Name = "Plaer Four", Order = 4 };

        //    Wall wallOne = new Wall();
        //    Wall wallTwo = new Wall();
        //    Wall wallThree = new Wall();
        //    Wall wallFour = new Wall();

        //    Rack rackOne = new Rack();
        //    Rack rackTwo = new Rack();
        //    Rack rackThree = new Rack();
        //    Rack rackFour = new Rack();

        //    Player[] players = {one, two, three, four};
        //    List<Tile> gameTiles = new List<Tile>(Tile.ShuffledTiles);

        //    for (int i = 0, j = 0; i < 13; i++, j+=4)
        //    {
        //        rackOne.AddTile(gameTiles[j]);
        //        rackTwo.AddTile(gameTiles[j+1]);
        //        rackThree.AddTile(gameTiles[j+2]);
        //        rackFour.AddTile(gameTiles[j+3]);
        //        // remove those game tiles
        //    }
        //    rackOne.AddTile(gameTiles[0]);

        //    while (gameTiles.Count > 0)
        //    {
        //        // put the rest on a rack
        //    }

        //    bool mahjong = false;
        //    bool EmptyWall = wallOne.IsEmpty() && wallTwo.IsEmpty() && wallThree.IsEmpty() && wallFour.IsEmpty();
        //}
    }
}
