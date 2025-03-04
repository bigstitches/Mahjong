using Mahjong;

namespace TestMahjong;

[TestClass]
public class TestInitialize
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

    public static Tile? DOTONE => dotOne;
    public static Tile? DOTTWO => dotTwo;
    public static Tile? DOTTHREE => dotThree;
    public static Tile? BAMONE => bamOne;
    public static Tile? BAMTWO => bamTwo;
    public static Tile? BAMTHREE => bamThree;
    public static Tile? BAMFOUR => bamFour;
    public static Tile? CRACKONE => crackOne;
    public static Tile? CRACKTWO => crackTwo;
    public static Tile? CRACKTHREE => crackThree;
    public static Tile? CRACKFOUR => crackFour;
    public static Tile? JOKER => joker;
    public static Tile? FLOWER => flower;
    public static Tile? NORTH => north;
    public static Tile? SOUTH => south;
    public static Tile? WEST => west;


    [TestMethod]
    public void TestMethod1()
    {
    }
}
