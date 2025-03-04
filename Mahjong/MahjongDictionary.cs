namespace Mahjong;

public class MahjongDictionary : Dictionary<(Suits suit, Rank rank), int>
{
    public enum TileMembership
    {
        SOLO = 0,
        RUN = 1,
        KONG = 3, 
        EYE = 4,
        PONG = 5
    };

    public MahjongDictionary() : base() { }

    // if a dictionary is created with regular Dictionary class, convert
    // Constructor that takes a regular dictionary
    public MahjongDictionary(Dictionary<(Suits suit, Rank rank), int> dictionary)
    {
        foreach (var kvp in dictionary)
        {
            this.Add(kvp.Key, kvp.Value);
        }
    }

    public MahjongDictionary(Tile?[] tiles)
    {
        foreach (Tile? t in tiles)
        {
            if (t is not null)
            {
                this.AddTile(t.Suit, t.Rank);
            }
        }
    }


    public void AddTile(Suits suit, Rank rank)
    {
        var key = (suit, rank);
        if (this.ContainsKey(key))
        {
            this[key]++;
        }
        else
        {
            this[key] = 1;
        }
    }

    public void AddTile(ValueTuple<Suits, Rank> vT)
    {
        AddTile(vT.Item1, vT.Item2);
    }


    public TileMembership CheckTileType((Suits suit, Rank rank) key)
    {
        int quantity = this[key];
        var suitGroup = this.Keys.Where(k => k.suit == key.suit).ToList();

        bool isRun = false;
        bool isKong = quantity > 3;
        bool isPong = quantity > 2 && quantity < 4;
        bool isEye = quantity > 1 && quantity < 3;

        for (int i = 0; i < suitGroup.Count - 2; i++)
        {
            if (suitGroup[i].rank + 1 == suitGroup[i + 1].rank &&
                suitGroup[i + 1].rank + 1 == suitGroup[i + 2].rank &&
                (key.suit == Suits.BAM || key.suit == Suits.DOT || key.suit == Suits.CRACK))
            {
                isRun = true;
                break;
            }
        }

        if (isKong)
        {
            return TileMembership.KONG;
        }
        else if (isPong)
        {
            return TileMembership.PONG;
        }
        else if (isEye)
        {
            return TileMembership.EYE;
        }
        else if (isRun)
        {
            return TileMembership.RUN;
        }
        else
        {
            return TileMembership.SOLO;
        }

    }
}