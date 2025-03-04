using Mahjong;

namespace TestMahjong;

[TestClass]
public class TestComputerPlayer
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
    public void TestComputerDiscard()
    {
#pragma warning disable CS8601 // Possible null reference assignment.
        Tile[] goodHand = { joker, bamOne, bamTwo, bamThree, bamFour, flower, flower, north, flower, flower, south, south, south, south };
#pragma warning restore CS8601 // Possible null reference assignment.

        Rack computerRack = new Rack(goodHand);

        ComputerPlayer computerPlayer = new ComputerPlayer()
        {
            Name = "Robot",
            Rack = computerRack
        };

#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        Tile? t = computerPlayer.ChooseDiscard(computerPlayer.Rack.Hand);
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.

        Assert.AreEqual(t.Suit, Suits.WIND);
        Assert.AreEqual(t.Rank, Rank.NORTH);
    }

    public void TestComputerPickUp()
    {
#pragma warning disable CS8601 // Possible null reference assignment.
        Tile[] goodHandThree = { north, bamOne, bamTwo, bamThree, flower, flower, flower, south, south, south, west, west, west };
#pragma warning restore CS8601 // Possible null reference assignment.

        Rack computerRack = new Rack(goodHandThree);

        ComputerPlayer computerPlayer = new ComputerPlayer()
        {
            Name = "Robot",
            Rack = computerRack
        };

        Tile? wallTile = north;
        Tile? discardTile = west;

        bool mahjongable = false;
        bool choosewall = false;


#pragma warning disable CS8604 // Possible null reference argument.
        Tile? t = computerPlayer.TilePickUp(wallTile, discardTile, out mahjongable, out choosewall);
#pragma warning restore CS8604 // Possible null reference argument.

        Assert.IsTrue(mahjongable);
        Assert.IsTrue(choosewall);
        Assert.IsNotNull(t);
        Assert.AreEqual(t.Rank, Rank.NORTH);
    }
}
