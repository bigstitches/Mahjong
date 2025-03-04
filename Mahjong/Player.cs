using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Mahjong
{
    public class Player
    {
        public string? Name { get; set; }
        public Rack? Rack { get; set; }

        public virtual Tile?[]? ComputeCharlestonDiscard(int n) { return []; }
        public virtual Tile? TilePickUp(Tile wallTile, Tile discardTile, out bool mahjongable, out bool choseWall) {
            mahjongable = false;
            choseWall = false;
            return default; 
        }
    }

    public class ComputerPlayer : Player
    {
        public override Tile?[]? ComputeCharlestonDiscard(int n)
        {
            if (this.Rack is null) { return default; }
            // check tiles in rack
            // put tiles in rack into dictionary
            Dictionary<ValueTuple<Suits, Rank>, int>? tileDictionary = Rack.GetDictionary();

            // establish rankings of desirable tiles
            List<ValueTuple<Suits, Rank>> considerRemoval = new();
            List<ValueTuple<Suits, Rank>> pairs = new();
            List<ValueTuple<Suits, Rank>> threeOrMore = new();

            // if ComputerCharlestonDiscard is called with the rack is not established, return null
            if (tileDictionary is null) { return default; }

            // keep the jokers during Charleston
            foreach(KeyValuePair<ValueTuple<Suits, Rank>, int> kvp in tileDictionary)
            {
                var key = kvp.Key;
                if ((Suits)key.Item1 == Suits.JOKER)
                {
                    tileDictionary.Remove(key);
                }

                else if (kvp.Value == 1)
                {
                    considerRemoval.Add(key);
                }
                else if (kvp.Value > 1 && kvp.Value < 3)
                {
                    pairs.Add(key);
                }
                else 
                {
                    threeOrMore.Add(key);
                }
            }

            // if there are less than three undesirable tiles, remove from threeormore
            // then remove from pair until you get three undesirable tiles
            while (considerRemoval.Count < n)
            {
                if (threeOrMore.Count > 0)
                {
                    considerRemoval.Add(threeOrMore[0]);
                    threeOrMore.RemoveAt(0);
                } else
                {
                    considerRemoval.Add(pairs[0]);
                    pairs.RemoveAt(0);
                }
            }

            // considerRemoval must have more than three tiles
            if (considerRemoval.Count < n) { return []; }

            Tile[] removeTilesFromHand = new Tile[n];
            for(int i = 0; i < n; i++)
            {
                bool successfull = true;
                removeTilesFromHand[i] = this.Rack.GetTile(considerRemoval[0].Item1, considerRemoval[0].Item2, out successfull);
                if (!successfull) { return []; }
                considerRemoval.RemoveAt(0);
            }
            return removeTilesFromHand; // array of objects/tiles within computer's hand
        }

        //TODO!!! computer gets to see the walltile and choose?
        // NOOOOO, the computer should see if the discardTile makes a 
        public override Tile? TilePickUp(Tile wallTile, Tile discardTile, out bool mahjongable, out bool choseWall)
        {
            mahjongable = false;
            choseWall = true;

            // assume you have all the tiles in your hand
            // loop through without the discardTile, if you have mahjong, keep the walltile
            // loop through without the wallTile, if you have mahjong, keep the discardtile
            // in either of those cases, mahjongable sets to true
            if (this.Rack is null) { return default; }
            // check tiles in rack
            // put tiles in rack into dictionary
            Dictionary<ValueTuple<Suits, Rank>, int>? tileDictionary = Rack.GetDictionary();
            var kvp = tileDictionary?.FirstOrDefault(t => t.Key.Item2 == Rank.JOKER);
            int jokerCount = 0;

            if (kvp is not null)
            {
                jokerCount = kvp.Value.Value;
                tileDictionary?.Remove(kvp.Value.Key);
            }

            Dictionary<ValueTuple<Suits, Rank>, int>? tileDictionaryWall = new();
            if (!tileDictionaryWall.TryAdd((wallTile.Suit, wallTile.Rank), 1))
            {
                tileDictionaryWall[(wallTile.Suit, wallTile.Rank)]++;
            }

            Dictionary<ValueTuple<Suits, Rank>, int>? tileDictionaryDiscard = new();
            if (!tileDictionaryDiscard.TryAdd((discardTile.Suit, discardTile.Rank), 1))
            {
                tileDictionaryDiscard[(discardTile.Suit, discardTile.Rank)]++;
            }

            if (discardTile.Suit != Suits.JOKER)
            {
                if (Sequences.CheckForRuns(tileDictionaryDiscard, 4) ||
                Sequences.CheckThreeOfAKindAndRuns(tileDictionaryDiscard, 4, jokerCount))
                {
                    mahjongable = true;
                    choseWall = false;
                    Rack.AddTile(discardTile);
                    return null; // no discard
                }
            }

            Rack.AddTile(wallTile);
            Tile?[] hand = Rack.Hand;
            if (hand is not null) { mahjongable = Sequences.IsWinningHand([.. hand]); }

            if (mahjongable) { return null; } // no discard

            return hand is not null ? ChooseDiscard([.. hand]) : default;
        }

        public Tile? ChooseDiscard(Tile?[] hand)
        {
            MahjongDictionary dictionary = new MahjongDictionary(hand);

            ValueTuple<Suits, Rank> worstTile = new ValueTuple<Suits, Rank>();
            // impossible to have four pongs with 14 tiles, set it as worst to be overwritten
            MahjongDictionary.TileMembership worstMembership = MahjongDictionary.TileMembership.PONG;

            foreach (var kvp in dictionary)
            {
                if (kvp.Key.rank != Rank.JOKER)
                {
                    MahjongDictionary.TileMembership membership = dictionary.CheckTileType((kvp.Key.suit, kvp.Key.rank));
                    if (worstMembership > membership)
                    {
                        worstMembership = membership;
                        worstTile = ((kvp.Key.suit, kvp.Key.rank));
                    }
                }
            }

            foreach(Tile? t in hand)
            {
                if (t is not null && t.Suit == worstTile.Item1 && t.Rank == worstTile.Item2)
                {
                    return t;
                }
            }

            return default;

            //foreach (Tile? t in hand)
            //{
            //    if (t is not null && t.Rank != Rank.JOKER)
            //    {
            //        MahjongDictionary.TileMembership membership = dictionary.CheckTileType((t.Suit, t.Rank));
            //        if (worstMembership > membership) {
            //            worstMembership = membership;
            //            worstTile = t;
            //        }
            //    }
            //}

            //// test for Mahjong before this method, in TilePickUp
            //List<Tile> tileList = new List<Tile>();
            //int jokerCount = 0;

            //foreach(Tile t in hand)
            //{
            //    if (t != null)
            //    {
            //        if (t.Suit == Suits.JOKER)
            //        {
            //            jokerCount++;
            //        }
            //        else
            //        {
            //            tileList.Add(t);
            //        }
            //    }
            //}

            //Tile worstRatedTile = hand[0];
            //int worstScore = Int32.MaxValue;
            //int score = 0;
            //// memoize results
            //Dictionary<ValueTuple<Suits, Rank>, int> memo = new Dictionary<(Suits, Rank), int>();

            //// CORRECTED TODO System.InvalidOperationException: Collection was modified; enumeration operation may not execute.
            //// Created copy of list inside loop and removed the current tile

            //foreach (Tile t in tileList) // Evaluate whether removing this tile will offer Mahjong
            //{
            //    if (memo.ContainsKey((t.Suit, t.Rank))) { continue; }

            //    var remainingTiles = new List<Tile>(tileList);

            //    for (int i = 0; i < remainingTiles.Count; i++)
            //    {
            //        if (remainingTiles[i].Suit == t.Suit && remainingTiles[i].Rank == t.Rank)
            //        {
            //            remainingTiles.RemoveAt(i);
            //            i--;
            //            break;
            //        }
            //    }

            //    var tileCounts = remainingTiles.GroupBy(tile => new ValueTuple<Suits, Rank>(tile.Suit, tile.Rank)).ToDictionary(t => t.Key, t => t.Count());
            //    Dictionary<ValueTuple<Suits, Rank>, int> dictionary = tileCounts.ToDictionary(entry => entry.Key, entry => entry.Value);

            //    score = Sequences.TotalScore(dictionary, jokerCount);

            //    memo[(t.Suit, t.Rank)] = score;

            //    if (score < worstScore) { worstRatedTile = t; worstScore = score; }

            //}

            //// if memo has the same score
            

            //return worstRatedTile;
        }
    }
}
