using Mahjong;

namespace TestMahjong;

[TestClass]
public class TestSequence
{
    private static Tile? dotOne;
    private static Tile? dotTwo;
    private static Tile? dotThree;
    private static Tile? bamOne;
    private static Tile? bamTwo;
    private static Tile? bamThree;
    private static Tile? bamFour;
    private static Tile? north;
    private static Tile? south;
    private static Tile? crackOne;
    private static Tile? crackTwo;
    private static Tile? crackThree;
    private static Tile? crackFour;
    private static Tile? joker;
    private static Tile? flower;
    private static Tile? west;

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        dotOne = new Tile(Suits.DOT, Rank.ONE);
        dotTwo = new Tile(Suits.DOT, Rank.TWO);
        dotThree = new Tile(Suits.DOT, Rank.THREE);
        bamOne = new Tile(Suits.BAM, Rank.ONE);
        bamTwo = new Tile(Suits.BAM, Rank.TWO);
        bamThree = new Tile(Suits.BAM, Rank.THREE);
        bamFour = new Tile(Suits.BAM, Rank.FOUR);
        crackOne = new Tile(Suits.CRACK, Rank.ONE);
        crackTwo = new Tile(Suits.CRACK, Rank.TWO);
        crackThree = new Tile(Suits.CRACK, Rank.THREE);
        crackFour = new Tile(Suits.CRACK, Rank.FOUR);
        joker = new Tile(Suits.JOKER, Rank.JOKER);
        flower = new Tile(Suits.FLOWER, Rank.FLOWER);
        north = new Tile(Suits.WIND, Rank.NORTH);
        south = new Tile(Suits.WIND, Rank.SOUTH);
        west = new Tile(Suits.WIND, Rank.WEST);
    }

    [TestMethod]
    public void TestRun()
    {
#pragma warning disable CS8601 // Possible null reference assignment.
        Tile[] runOfThree = { bamThree, bamTwo, bamOne };
        Tile[] runOfFour = { bamTwo, bamOne, bamThree, bamFour };
        Tile[] badRunSuit = { dotOne, bamThree, bamTwo };
        Tile[] badRunRank = { bamOne, bamTwo, bamFour };
#pragma warning restore CS8601 // Possible null reference assignment.

        Assert.IsFalse(Sequences.Run(badRunRank));
        Assert.IsFalse(Sequences.Run(badRunSuit));
        Assert.IsTrue(Sequences.Run(runOfThree));
        Assert.IsTrue(Sequences.Run(runOfFour));
    }

    [TestMethod]

    public void TestNoJokers()
    {
#pragma warning disable CS8601 // Possible null reference assignment.
        Tile[] goodHand = { north, north, bamOne, bamTwo, bamThree, bamFour, flower, flower, flower, flower, south, south, south, south };
        Tile[] goodHandThree = { north, north, bamOne, bamTwo, bamThree, flower, flower, flower, south, south, south, west, west, west };
        Tile[] unsortedGoodHand = { bamOne, bamTwo, bamThree, flower, flower, north, flower, flower, south, south, south, bamFour, north, south };
        Tile[] badHand = { north, north, bamOne, bamTwo, bamThree, bamFour, flower, flower, flower, crackFour, south, south, south, south };
#pragma warning restore CS8601 // Possible null reference assignment.

        Sequences sequence = new Sequences(goodHand);
        Sequences sequenceThree = new Sequences(goodHandThree);
        Sequences unsortedSequence = new Sequences(unsortedGoodHand);
        Sequences badHandSequence = new Sequences(badHand);

        Assert.IsTrue(sequence.GoodMahjong());
        Assert.IsTrue(sequenceThree.GoodMahjong());
        Assert.IsTrue(unsortedSequence.GoodMahjong());
        Assert.IsFalse(badHandSequence.GoodMahjong());
    }

    [TestMethod]

    public void TestWithJokers()
    {
#pragma warning disable CS8601 // Possible null reference assignment.
        Tile[] goodHand = { flower, flower, dotOne, dotOne, dotOne, joker, bamTwo, bamTwo, bamThree, bamThree, bamThree, crackFour, crackFour, crackFour };
        Tile[] goodHandThree = { flower, flower, dotOne, dotOne, joker, bamTwo, bamTwo, bamThree, bamThree, bamThree, crackFour, crackFour, crackFour, dotOne };
        Tile[] unsortedGoodHand = { flower, flower, dotOne, dotOne, dotOne, joker, bamTwo, bamTwo, bamThree, bamThree, bamThree, crackFour, crackFour, joker };
        Tile[] badHand = { north, north, bamOne, bamTwo, bamThree, bamFour, flower, flower, flower, crackFour, south, south, south, south };
#pragma warning restore CS8601 // Possible null reference assignment.

        Assert.IsTrue(Sequences.IsWinningHand(goodHand.ToList<Tile>()));
        Assert.IsTrue(Sequences.IsWinningHand(goodHandThree.ToList<Tile>()));
        Assert.IsTrue(Sequences.IsWinningHand(unsortedGoodHand.ToList<Tile>()));
        Assert.IsFalse(Sequences.IsWinningHand(badHand.ToList<Tile>()));
    }

    [TestMethod]

    public void TestRunsWithJokers()
    {
#pragma warning disable CS8601 // Possible null reference assignment.
        Tile[] goodHand = { flower, flower, dotOne, dotTwo, dotThree, dotOne, dotTwo, dotThree, bamThree, bamThree, bamThree, crackFour, crackFour, crackFour };
        Tile[] goodHandThree = { flower, flower, dotOne, dotTwo, dotOne, dotTwo, dotThree, bamThree, bamThree, bamThree, crackFour, crackFour, crackFour, dotThree };
        Tile[] unsortedGoodHand = { flower, flower, dotOne, dotTwo, dotThree, bamOne, bamTwo, bamThree, crackOne, crackTwo, crackThree, bamTwo, bamThree, bamFour };
        Tile[] badHand = { north, north, bamOne, bamTwo, bamThree, bamFour, flower, flower, flower, crackFour, south, south, south, south };
#pragma warning restore CS8601 // Possible null reference assignment.

        Assert.IsTrue(Sequences.IsWinningHand(goodHand.ToList<Tile>()));
        Assert.IsTrue(Sequences.IsWinningHand(goodHandThree.ToList<Tile>()));
        Assert.IsTrue(Sequences.IsWinningHand(unsortedGoodHand.ToList<Tile>()));
        Assert.IsFalse(Sequences.IsWinningHand(badHand.ToList<Tile>()));
    }
}
