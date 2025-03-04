using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahjong
{
    class Charleston
    {
        // player picks tiles
        // player moves tiles
        // depends on the charleston step
        private LinkedList<Player>? players;
        //private List<Tile> discardTiles;

        // TODO THIS SHOULD BE PRIVATE?
        //public Charleston()
        //{
        //    players = new LinkedList<Player>();
        //    walls = new LinkedList<Wall>();
        //    discardTiles = new List<Tile>();
        //}

        //public GamePlay(Player[]? players, Wall[]? walls, Rack[]? racks)
        //{
        //    if (players != null && racks != null && players.Length == 4 && racks.Length == 4)
        //    {
        //        this.players = new LinkedList<Player>();
        //        Array.ForEach(players, player => { this.players.AddLast(player); });
        //        racks.Zip(players, (rack, player) => player.Rack = rack);
        //    }

        //    discardTiles = new List<Tile>();

        //    if (walls != null)
        //    {
        //        this.walls = new LinkedList<Wall>();
        //        Array.ForEach(walls, wall => { this.walls.AddLast(wall); });
        //    }
        //}
    }
}
