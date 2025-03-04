using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahjong
{
    public class Sequences
    {
        private Tile?[] hand;
        public Sequences(Tile?[] hand)
        {
            if (hand.Length == 14)
            {
                this.hand = new Tile[14];
                Array.Copy(hand, this.hand, 14);
            } else
            {
                this.hand = Enumerable.Repeat<Tile>(new Tile(Suits.BLANK, Rank.BLANK), 14).ToArray();
            }
        }
        internal Sequences() : this( Enumerable.Repeat<Tile>(new Tile(Suits.BLANK, Rank.BLANK), 14).ToArray() ) { }

        public static bool IsWinningHand(List<Tile> tiles)
        {
            if (tiles.Count != 14)
                return false;

            // create a dictionary to count the jokers
            // Dictionary<Tile, Int> tileCounts created
            var tileCounts = tiles.GroupBy(tile => new { tile.Suit, tile.Rank }).ToDictionary(t => t.Key, t => t.Count());
            int jokerCount = 0;
            if(tileCounts.Keys.Any(t => t.Rank == Rank.JOKER))
            {
                var kvp = tileCounts.FirstOrDefault(t => t.Key.Rank == Rank.JOKER);
                jokerCount = kvp.Value;
                tileCounts.Remove(kvp.Key); // Remove jokers from normal tile evaluation
                tiles.RemoveAll(t => t.Rank == Rank.JOKER); // Remove jokers from paramter tiles; only use jokerCount
            }

            

            foreach (var pair in tileCounts.Where(t => t.Value >= 2)) // Find a valid pair
            {
                // make copy of tiles list so remainingTiles can be modified on each iteration
                // but original list (minus jokers) stays intact
                var remainingTiles = new List<Tile>(tiles);
                int count = 0;
                for(int i = 0; i < remainingTiles.Count; i++)
                {
                    if (remainingTiles[i].Suit == pair.Key.Suit && remainingTiles[i].Rank == pair.Key.Rank)
                    {
                        remainingTiles.RemoveAt(i);
                        count++;
                        i--;
                    }
                    if (count == 2) { break; }
                }

                if (CanFormValidSets(remainingTiles, jokerCount))
                    return true;
            }

            return false;
        }

        private static bool CanFormValidSets(List<Tile> tiles, int jokerCount)
        {
            // if I make a new Tile OBJECT, it will only ever have 1 instance of that object....
            //var tileCounts = tiles.GroupBy(tile => new { tile.Suit, tile.Rank }).ToDictionary(t => t.Key, t => t.Count());
            // this is an anonymous type
            // var tileCounts = tiles.GroupBy(tile => new { tile.Suit, tile.Rank }).ToDictionary(t => t.Key, t => t.Count());
            var tileCounts = tiles.GroupBy(tile => new ValueTuple<Suits, Rank>(tile.Suit, tile.Rank)).ToDictionary(t => t.Key, t => t.Count());
            Dictionary<ValueTuple<Suits, Rank>, int> copyDictionary = tileCounts.ToDictionary(entry => entry.Key, entry => entry.Value);

            return CheckForRuns(tileCounts, 4) ||
                   CheckThreeOfAKindAndRuns(copyDictionary, 4, jokerCount);
        }

        public static bool CheckThreeOfAKindAndRuns(Dictionary<ValueTuple<Suits, Rank>, int> tileCounts, int threeOfAKindCount, int jokerCount)
        {
            var tileList = tileCounts.Keys.ToList();

            if (threeOfAKindCount == 0) { return true; }

            _ = tileList.OrderBy(t => t.Item1).ThenBy(t => t.Item2);

            foreach (var tile in tileList)
            {

                if (tileCounts.Keys.Any(t => t.Item1 == tile.Item1 && t.Item2 == tile.Item2))
                {
                    var kvp = tileCounts.FirstOrDefault(t => t.Key.Item1 == tile.Item1 && t.Key.Item2 == tile.Item2);

                    if (kvp.Value + jokerCount >= 3)
                    {
                        if (jokerCount >= 2)
                        {
                            //  if (recursive with less jokers and one less tile) return true;
                            Dictionary<ValueTuple<Suits, Rank>, int> copyDictionary1 = tileCounts.ToDictionary(entry => entry.Key, entry => entry.Value);
                            copyDictionary1[kvp.Key] -= 1;
                            if (copyDictionary1[kvp.Key] == 0) { copyDictionary1.Remove(kvp.Key); }
                            if (CheckThreeOfAKindAndRuns(copyDictionary1, threeOfAKindCount - 1, jokerCount - 2))
                                return true;
                        }

                        if (jokerCount >= 1)
                        {
                            // if (recursive with one less jokers and two less tile) return true;
                            Dictionary<ValueTuple<Suits, Rank>, int> copyDictionary2 = tileCounts.ToDictionary(entry => entry.Key, entry => entry.Value);
                            copyDictionary2[kvp.Key] -= 2;
                            if (copyDictionary2[kvp.Key] == 0) { copyDictionary2.Remove(kvp.Key); }
                            if (CheckThreeOfAKindAndRuns(copyDictionary2, threeOfAKindCount - 1, jokerCount - 1))
                                return true;
                        }

                        if (kvp.Value >= 3)
                        {
                            // if (return recursive with three less tile) return true;
                            Dictionary<ValueTuple<Suits, Rank>, int> copyDictionary3 = tileCounts.ToDictionary(entry => entry.Key, entry => entry.Value);
                            copyDictionary3[kvp.Key] -= 3;
                            if (CheckThreeOfAKindAndRuns(copyDictionary3, threeOfAKindCount - 1, jokerCount))
                                return true;
                        }
                    }
                }
            }

            //// TODO - if there are 3, 6 or 9 remaining tiles that were NOT of the same kind
            if (threeOfAKindCount != 4 && CheckForRuns(tileCounts, threeOfAKindCount)) { return true; }

            return false;
        }

        public static bool CheckForRuns(Dictionary<ValueTuple<Suits, Rank>, int> tileCounts, int numRuns)
        {
            return CheckForRuns(tileCounts, numRuns, out _);
        }
        private static bool CheckForRuns(Dictionary<ValueTuple<Suits, Rank>, int> tileCounts, int numRuns, out int foundRuns)
        {
            var tileList = tileCounts.Keys.ToList();

            _ = tileList.OrderBy(t => t.Item1).ThenBy(t => t.Item2);

            foundRuns = 0;

            for (int i = 0; i < tileList.Count - 2; i++)
            {
                // while the tile is still there
                while (tileCounts.Keys.Any(t => t.Item1 == tileList[i].Item1 && t.Item2 == tileList[i].Item2))
                {
                    var kvp = tileCounts.FirstOrDefault(t => t.Key.Item1 == tileList[i].Item1 && t.Key.Item2 == tileList[i].Item2);

                    // get it's next two tiles and remove all three from dictionary; if 
                    // there is no match return false, else decrease number of runs
                    Suits matchingSuit = kvp.Key.Item1;
                    Rank nTile = kvp.Key.Item2 + 1;
                    Rank nNTile = kvp.Key.Item2 + 2;
                    //Rank nTile = Tile.GetNextEnumValue<Rank>(kvp.Key.Item2);
                    //Rank nNTile = Tile.GetNextEnumValue<Rank>(nTile);

                    if (tileCounts.Keys.Any(t => t.Item1 == matchingSuit && t.Item2 == nTile) &&
                        tileCounts.Keys.Any(t => t.Item1 == matchingSuit && t.Item2 == nNTile))
                    {
                        // remove all three
                        // if the dictionary is == 0; remove the item from the dictionary
                        var kvpNext = tileCounts.FirstOrDefault(t => t.Key.Item1 == matchingSuit && t.Key.Item2 == nTile);
                        var kvpNextNext = tileCounts.FirstOrDefault(t => t.Key.Item1 == matchingSuit && t.Key.Item2 == nNTile);
                        //if (tileCounts[kvp.Key] > 1) { tileCounts[kvp.Key]--; } else { tileCounts.Remove(kvp.Key); }
                        if (tileCounts[kvpNext.Key] > 1) { tileCounts[kvpNext.Key]--; } else { tileCounts.Remove(kvpNext.Key); }
                        if (tileCounts[kvpNextNext.Key] > 1) { tileCounts[kvpNextNext.Key]--; } else { tileCounts.Remove(kvpNextNext.Key); }
                        // decrease numRuns
                        foundRuns++;
                    }

                    if (tileCounts[kvp.Key] > 1) { tileCounts[kvp.Key]--; } else { tileCounts.Remove(kvp.Key); }
                }
            }

            return foundRuns >= numRuns;
        }



        // sequences of sets and/or runs, plus a matched pair of any two tiles, 'Eyes'. 
        // https://www.themahjongproject.com/how-to-play/basics/#structure
        // First set test out every element as an 'Eye' and find it's matching pair
        public static bool GoodMahjong(Tile?[] hand)
        {
            if (hand.Length > 14) { return false; }
            foreach (Tile ?tile in hand)
            {
                if (tile == null) { return false; }
            }
            
            Sequences sequence = new Sequences(hand);
            return sequence.GoodMahjong();
        }
        public bool GoodMahjong()
        {
            foreach (Tile? tile in this.hand)
            {
                if (tile == null) { return false; }
            }

            int jokerCount = 0;
            int localCount = 0;
            localCount = hand.Count(t => t?.Rank == Rank.JOKER);
            if (localCount > 0) { jokerCount = localCount; }

            // find the 'Eyes', let every element be a potential Eyes pair
            for (int i = 0; i < this.hand.Length; i++)
            {
                if (this.hand is null) { return false; }
                List<Tile> remainingTiles = new List<Tile>(hand as Tile[]);
                Tile pairTile = remainingTiles[i];
                remainingTiles.RemoveAt(i);

                int index = remainingTiles.FindIndex(i => (i.Suit == pairTile.Suit && i.Rank == pairTile.Rank));

                if (index != -1)
                {
                    remainingTiles.RemoveAt(index);

                    // check CanFormSets with remaining 12 tiles
                    //if (CanFormSets(new List<Tile>(remainingTiles)))
                    //    return true;
                    if (CanFormSetsJokers(new List<Tile>(remainingTiles), jokerCount))
                        return true;
                }
            }

            return false;
        }

        private bool CanFormSetsJokers(List<Tile> tiles, int jokerCount)
        {
            if (jokerCount == 0) { return CanFormSets(tiles); }

            if (tiles.Count == 0)
                return true;

            tiles.Sort();

            Tile firstTile = tiles[0];

            int sequenceCount = tiles.Count(t => t.Rank == firstTile.Rank);

            // Check for triplet
            if (sequenceCount + jokerCount >= 3 && sequenceCount > 0)
            {
                List<Tile> remainingTiles = new List<Tile>(tiles);
                remainingTiles.RemoveRange(0, 3);

                // how many jokers were used
                int usedJokers = 3 - sequenceCount < 0 ? 0 : 3 - sequenceCount;
                Tile.RemoveFromList(remainingTiles, Rank.JOKER, Suits.JOKER, usedJokers);

                if (CanFormSetsJokers(new List<Tile>(remainingTiles), jokerCount - usedJokers))
                    return true;
            }

            // Check for quartet
            if (sequenceCount + jokerCount >= 4 && sequenceCount > 0)
            {
                List<Tile> remainingTiles = new List<Tile>(tiles);
                remainingTiles.RemoveRange(0, 4);

                // how many jokers were used
                int usedJokers = 4 - sequenceCount < 0 ? 0 : 4 - sequenceCount;
                Tile.RemoveFromList(remainingTiles, Rank.JOKER, Suits.JOKER, usedJokers);

                if (CanFormSetsJokers(new List<Tile>(remainingTiles), jokerCount - usedJokers))
                    return true;
            }

            // Check for sequence
            if (tiles.Count >= 4)
            {
                Tile[] fourCopy = new Tile[4];
                Array.Copy(tiles.ToArray(), fourCopy, 4);

                if (Run(fourCopy, true))
                {
                    List<Tile> remainingTiles = new List<Tile>(tiles);
                    remainingTiles.RemoveRange(0, 4);
                    if (CanFormSetsJokers(new List<Tile>(remainingTiles), jokerCount))
                        return true;
                }
            }
            else if (tiles.Count >= 3)
            {
                Tile[] threeCopy = new Tile[3];
                Array.Copy(tiles.ToArray(), threeCopy, 3);

                if (Run(threeCopy, true))
                {
                    List<Tile> remainingTiles = new List<Tile>(tiles);
                    remainingTiles.RemoveRange(0, 3);
                    if (CanFormSetsJokers(new List<Tile>(remainingTiles), jokerCount))
                        return true;
                }
            }

            return false;
        }

        // No jokers in quartet or triplet
        private bool CanFormSets(List<Tile> tiles)
        {
            if (tiles.Count == 0)
                return true;

            tiles.Sort();

            Tile firstTile = tiles[0];

            // Check for triplet
            if (tiles.Count(t => t.Rank == firstTile.Rank) == 3)
            {
                List<Tile> remainingTiles = new List<Tile>(tiles);
                remainingTiles.RemoveRange(0, 3);
                if (CanFormSets(new List<Tile>(remainingTiles)))
                    return true;
            }

            // Check for quartet
            if (tiles.Count(t => t.Rank == firstTile.Rank) == 4)
            {
                List<Tile> remainingTiles = new List<Tile>(tiles);
                remainingTiles.RemoveRange(0, 4);
                if (CanFormSets(new List<Tile>(remainingTiles)))
                    return true;
            }

            // Check for sequence
            Tile[] fourCopy = new Tile[4];
            Array.Copy(tiles.ToArray(), fourCopy, 4);
            Tile[] threeCopy = new Tile[3];
            Array.Copy(tiles.ToArray(), threeCopy, 3);

            if (Run(fourCopy, true))
            {
                List<Tile> remainingTiles = new List<Tile>(tiles);
                remainingTiles.RemoveRange(0, 4);
                if (CanFormSets(new List<Tile>(remainingTiles)))
                    return true;
            }
            if (Run(threeCopy, true))
            {
                List<Tile> remainingTiles = new List<Tile>(tiles);
                remainingTiles.RemoveRange(0, 3);
                if (CanFormSets(new List<Tile>(remainingTiles)))
                    return true;
            }

            return false;
        }

        public static int TotalScore(Dictionary<ValueTuple<Suits, Rank>, int> tileCounts, int jokerCount, int currentCount = 0)
        {
            currentCount = Math.Max(KongsCount(tileCounts, jokerCount), PongsWithRuns(tileCounts, jokerCount));

            return currentCount;
        }

        // NOT QUITE RIGHT TODO
        public static int KongsCount(Dictionary<ValueTuple<Suits, Rank>, int> tileCounts, 
            int jokerCount, int currentCount = 0)
        {
            // foreach set of Four, remove, score
            foreach (KeyValuePair<ValueTuple<Suits, Rank>, int> kvp in tileCounts)
            {
                if (tileCounts[kvp.Key] > 3 || tileCounts[kvp.Key] + jokerCount > 3)
                {
                    if (tileCounts[kvp.Key] < 3) { jokerCount = 3 - tileCounts[kvp.Key]; }
                    tileCounts[kvp.Key] -= 4;

                    if (tileCounts[kvp.Key] <= 0) { tileCounts.Remove(kvp.Key); }
                    
                    currentCount += 1 + KongsCount(tileCounts, jokerCount, currentCount);
                }
  
            }
            return currentCount;
        }

        public static int PongsWithRuns(Dictionary<ValueTuple<Suits, Rank>, int> tileCounts,
            int jokerCount, int currentCount = 0)
        {
            // foreach set of Four and three, remove, check score
            foreach (KeyValuePair<ValueTuple<Suits, Rank>, int> kvp in tileCounts)
            {
                if (kvp.Value > 2 || kvp.Value + jokerCount > 2)
                {
                    tileCounts[kvp.Key] -= 3;

                    if (kvp.Value == 0) { tileCounts.Remove(kvp.Key); }

                    currentCount += 1 + PongsWithRuns(tileCounts, jokerCount, currentCount);
                }
            }

            currentCount += RunsCount(tileCounts);

            return currentCount;
        }

        private static int RunsCount(Dictionary<(Suits, Rank), int> tileCounts)
        {
            List<Tile> tileList = new List<Tile>();
            int count = 0;

            int differentSuits = 0;
            Suits suitTracker = Suits.BLANK;

            foreach (KeyValuePair<ValueTuple<Suits, Rank>, int> kvp in tileCounts)
            {
                Tile t = new Tile(kvp.Key.Item1, kvp.Key.Item2);
                if (t.Suit != suitTracker)
                {
                    suitTracker = t.Suit;
                    differentSuits += 1;
                }
                tileList.Add(t);
            }

            tileList.Sort();

            for (int i = 0; i < tileList.Count - 2; i++)
            {
                // Suits must match: BAM == BAM, DOT == DOT, CRACK == CRACK
                if (tileList[i].Suit == tileList[i+1].Suit && tileList[i].Suit == tileList[i+2].Suit) {
                    if (tileList[i].Suit == Suits.BAM || tileList[i].Suit == Suits.DOT || 
                        tileList[i].Suit == Suits.CRACK) {
                        // Run must be in consecutive order
                        if ((int)tileList[i+1].Rank + 1 == (int)tileList[i].Rank &&
                            (int)tileList[i+2].Rank + 1 == (int)tileList[i+1].Rank) { count++; }
                    }
                }
            }

            return count;
        }

        public static int Pair(Dictionary<ValueTuple<Suits, Rank>, int> tileCounts, int jokerCount, int currentCount = 0)
        {
            foreach(KeyValuePair<ValueTuple<Suits, Rank>, int> kvp in tileCounts)
            {
                if (kvp.Value > 1)
                {
                    tileCounts[kvp.Key] -= 2;
                    currentCount += 1;
                }
            }

            return currentCount;
        } // end Pair

        public static int Kong(Dictionary<ValueTuple<Suits, Rank>, int> tileCounts, int jokerCount, int currentCount = 0)
        {
            foreach (KeyValuePair<ValueTuple<Suits, Rank>, int> kvp in tileCounts)
            {
                if (kvp.Value > 3)
                {
                    tileCounts[kvp.Key] -= 3;
                    currentCount += 1;
                }
            }

            return currentCount;
        } // end Pair

        public static int Pong(Dictionary<ValueTuple<Suits, Rank>, int> tileCounts, int jokerCount, int currentCount = 0)
        {
            foreach (KeyValuePair<ValueTuple<Suits, Rank>, int> kvp in tileCounts)
            {
                if (kvp.Value > 4)
                {
                    tileCounts[kvp.Key] -= 4;
                    currentCount += 1;
                }
            }

            return currentCount;
        } // end Pair

        public static bool ThreeOrFourOfAKind(Tile[] set)
        {
            if (set == null) return false;
            if (set.Length < 3) return false;

            for (int i = 0; i < set.Length - 1; i++)
            {
                {
                    if (set[i].Suit != set[i + 1].Suit && set[i].Rank != set[i + 1].Rank) { return false; }
                }
            }

            return true;
        } // end ThreeOrFourOfAKind()

        public static bool ValueSet(Tile[] set)
        {
            if (set == null) { return false; }

            foreach (Tile tile in set)
            {
                if (tile == null) { return false; }
                if (tile.Suit != Suits.DRAGON && tile.Suit != Suits.WIND) { return false; }
            }

            return true;
        } // end ValueSet()

        public static bool Run(Tile[] set) { return Run(set, false); }
        public static bool Run(Tile[] set, bool skipSort)
        {
            if (set == null) return false;
            if (set.Length < 3) return false;

            Tile[] copy = new Tile[set.Length];
            Array.Copy(set, copy, set.Length);

            if (!skipSort) { Array.Sort(copy); }

            Suits strictSuit = copy[0].Suit;

            for (int i = 0, j = 1; j < copy.Length; i++, j++)
            {
                // Suits must match: BAM == BAM, DOT == DOT, CRACK == CRACK
                if (copy[j].Suit != strictSuit) { return false; }
                // Runs can only be BAM, DOT or CRACK
                if (copy[i].Suit != Suits.BAM && copy[i].Suit != Suits.DOT && copy[i].Suit != Suits.CRACK) { return false; }
                // Run must be in consecutive order
                if ((int)copy[i].Rank + 1 != (int)copy[j].Rank) { return false; }
            }

            // check the last element since for loop stopped short
            if (copy[copy.Length - 1].Suit != Suits.BAM && copy[copy.Length - 1].Suit != Suits.DOT && 
                copy[copy.Length - 1].Suit != Suits.CRACK) { return false; }

            return true;
        } // end run

    }
}