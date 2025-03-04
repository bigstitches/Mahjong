using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahjong
{
    internal class WinningHands
    {
        // a winning hand is composed of four groupings of sets and/or runs
        // plus a pair
        // totallying fourteen tiles
        internal Tile[] Eyes; 
        
        internal WinningHands()
        {
            Eyes = new Tile[2];
        }
        
    }
}
