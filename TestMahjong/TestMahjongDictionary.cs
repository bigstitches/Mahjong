using Mahjong;

namespace TestMahjong;

[TestClass]
public class TestMahjongDictionary
{

    [TestMethod]
    public void TestMethod1()
    {
        MahjongDictionary dictionary = new MahjongDictionary();
        dictionary.AddTile(Suits.FLOWER, Rank.FLOWER);
        dictionary.AddTile(Suits.FLOWER, Rank.FLOWER);
        dictionary.AddTile(Suits.FLOWER, Rank.FLOWER);
        dictionary.AddTile(Suits.FLOWER, Rank.FLOWER);
        dictionary.AddTile(Suits.DRAGON, Rank.GREEN);
        dictionary.AddTile(Suits.CRACK, Rank.ONE);
        dictionary.AddTile(Suits.CRACK, Rank.TWO);
        dictionary.AddTile(Suits.CRACK, Rank.THREE);

        MahjongDictionary.TileMembership dragonMembership = dictionary.CheckTileType((Suits.DRAGON, Rank.GREEN));
        Assert.AreEqual(dragonMembership, MahjongDictionary.TileMembership.SOLO);

        MahjongDictionary.TileMembership flowerMembership = dictionary.CheckTileType((Suits.FLOWER, Rank.FLOWER));
        Assert.AreEqual(flowerMembership, MahjongDictionary.TileMembership.KONG);
        Assert.AreNotEqual<MahjongDictionary.TileMembership>(flowerMembership, MahjongDictionary.TileMembership.EYE);

        MahjongDictionary.TileMembership crackOneMembership = dictionary.CheckTileType((Suits.CRACK, Rank.ONE));
        Assert.AreEqual(crackOneMembership, MahjongDictionary.TileMembership.RUN);
        Assert.AreNotEqual<MahjongDictionary.TileMembership>(crackOneMembership, MahjongDictionary.TileMembership.KONG);

        MahjongDictionary.TileMembership crackTwoMembership = dictionary.CheckTileType((Suits.CRACK, Rank.TWO));
        Assert.AreEqual(crackTwoMembership, MahjongDictionary.TileMembership.RUN);
        Assert.AreNotEqual<MahjongDictionary.TileMembership>(crackTwoMembership, MahjongDictionary.TileMembership.PONG);

        MahjongDictionary.TileMembership crackThreeMembership = dictionary.CheckTileType((Suits.CRACK, Rank.THREE));
        Assert.AreEqual(crackThreeMembership, MahjongDictionary.TileMembership.RUN);
        Assert.AreNotEqual<MahjongDictionary.TileMembership>(crackThreeMembership, MahjongDictionary.TileMembership.SOLO);
    }
}
