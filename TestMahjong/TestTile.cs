using Mahjong;

namespace TestMahjong
{
    [TestClass]
    public sealed class TestTile
    {
        [TestMethod]
        public void TestRandomTileGeneration()
        {
            bool suitMatchRank = true;

            Assert.IsTrue(Tile.ShuffledTiles != null);

            foreach (Tile randomTile in Tile.ShuffledTiles)
            {
                if (randomTile.Suit == Suits.DRAGON)
                {
                    suitMatchRank = (randomTile.Rank == Rank.WHITE ||
                                     randomTile.Rank == Rank.RED ||
                                     randomTile.Rank == Rank.GREEN);
                }
                else if (randomTile.Suit == Suits.WIND)
                {
                    suitMatchRank = (randomTile.Rank == Rank.NORTH ||
                                     randomTile.Rank == Rank.SOUTH ||
                                     randomTile.Rank == Rank.EAST ||
                                     randomTile.Rank == Rank.WEST);
                }
                else if (randomTile.Suit == Suits.FLOWER)
                {
                    suitMatchRank = (randomTile.Rank == Rank.FLOWER);
                }
                else if (randomTile.Suit == Suits.JOKER)
                {
                    suitMatchRank = (randomTile.Rank == Rank.JOKER);
                }
                else // Dot, Bam or Crack
                {
                    suitMatchRank = (randomTile.Rank == Rank.ONE ||
                                     randomTile.Rank == Rank.TWO ||
                                     randomTile.Rank == Rank.THREE ||
                                     randomTile.Rank == Rank.FOUR ||
                                     randomTile.Rank == Rank.FIVE ||
                                     randomTile.Rank == Rank.SIX ||
                                     randomTile.Rank == Rank.SEVEN ||
                                     randomTile.Rank == Rank.EIGHT ||
                                     randomTile.Rank == Rank.NINE);
                }

                if (!suitMatchRank) { break; }
            }

            Assert.IsTrue(suitMatchRank);
        } // end TestTileGeneration

        [TestMethod]

        public void TestCompareTo()
        {
            Tile dotOne = new Tile(Suits.DOT, Rank.ONE);
            Tile dotTwo = new Tile(Suits.DOT, Rank.TWO);
            Tile bamFour = new Tile(Suits.BAM, Rank.FOUR);
            Tile crackFour = new Tile(Suits.CRACK, Rank.FOUR);
            Tile joker = new Tile(Suits.JOKER, Rank.JOKER);

            int compareVal = dotTwo.CompareTo(bamFour);

            Assert.IsTrue(dotOne.CompareTo(dotTwo) == -1);
            Assert.IsTrue(dotTwo.CompareTo(bamFour) == -1);
            Assert.IsTrue(bamFour.CompareTo(crackFour) == 0);
            Assert.IsTrue(crackFour.CompareTo(dotTwo) == 1);
            Assert.IsTrue(joker.CompareTo(joker) == 0);
        }
    }
}
