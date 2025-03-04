#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Mahjong;

namespace Mahjong;

public enum Suits
{
    BLANK = 0, // optional
    JOKER,
    FLOWER,
    WIND,
    DRAGON,
    DOT,
    BAM,
    CRACK
}

public enum Rank
{
    ONE = 1, // (4*3)
    TWO, // (4*3)
    THREE, // (4*3)
    FOUR, // (4*3)
    FIVE, // (4*3)
    SIX, // (4*3)
    SEVEN, // (4*3)
    EIGHT, // (4*3)
    NINE, // (4*3)
    JOKER, // (8)
    FLOWER, // (8)
    NORTH, // (4)
    SOUTH, // (4)
    EAST, // (4)
    WEST, // (4)
    GREEN, // Bam Dragon (4)
    RED, // Crack Dragon (4)
    WHITE, // Dot Dragon (4)
    BLANK // (4)
}
public class Tile : IComparable<Tile>
{
    public static readonly Tile[] ALLTILESNOBLANKS = new Tile[156];
    private static readonly List<Tile> shuffledTiles;
    static Tile() {
        ALLTILESNOBLANKS = [
             new Tile(Suits.JOKER, Rank.JOKER), new Tile(Suits.JOKER, Rank.JOKER),
             new Tile(Suits.JOKER, Rank.JOKER ), new Tile(Suits.JOKER, Rank.JOKER ),
             new Tile(Suits.JOKER, Rank.JOKER ), new Tile(Suits.JOKER, Rank.JOKER ),
             new Tile(Suits.JOKER, Rank.JOKER ), new Tile(Suits.JOKER, Rank.JOKER ),
             new Tile(Suits.FLOWER, Rank.FLOWER ), new Tile(Suits.FLOWER, Rank.FLOWER ),
             new Tile(Suits.FLOWER, Rank.FLOWER ), new Tile(Suits.FLOWER, Rank.FLOWER ),
             new Tile(Suits.FLOWER, Rank.FLOWER ), new Tile(Suits.FLOWER, Rank.FLOWER ),
             new Tile(Suits.FLOWER, Rank.FLOWER ), new Tile(Suits.FLOWER, Rank.FLOWER ),
             new Tile(Suits.WIND, Rank.NORTH ), new Tile(Suits.WIND, Rank.NORTH ),
             new Tile(Suits.WIND, Rank.NORTH ), new Tile(Suits.WIND, Rank.NORTH ),
             new Tile(Suits.WIND, Rank.SOUTH ), new Tile(Suits.WIND, Rank.SOUTH ),
             new Tile(Suits.WIND, Rank.SOUTH ), new Tile(Suits.WIND, Rank.SOUTH ),
             new Tile(Suits.WIND, Rank.EAST ), new Tile(Suits.WIND, Rank.EAST ),
             new Tile(Suits.WIND, Rank.EAST ), new Tile(Suits.WIND, Rank.EAST ),
             new Tile(Suits.WIND, Rank.WEST ), new Tile(Suits.WIND, Rank.WEST ),
             new Tile(Suits.WIND, Rank.WEST ), new Tile(Suits.WIND, Rank.WEST ),
             new Tile(Suits.DRAGON, Rank.GREEN ), new Tile(Suits.DRAGON, Rank.GREEN ),
             new Tile(Suits.DRAGON, Rank.GREEN ), new Tile(Suits.DRAGON, Rank.GREEN ),
             new Tile(Suits.DRAGON, Rank.RED ), new Tile(Suits.DRAGON, Rank.RED ),
             new Tile(Suits.DRAGON, Rank.RED ), new Tile(Suits.DRAGON, Rank.RED ),
             new Tile(Suits.DRAGON, Rank.WHITE ), new Tile(Suits.DRAGON, Rank.WHITE ),
             new Tile(Suits.DRAGON, Rank.WHITE ), new Tile(Suits.DRAGON, Rank.WHITE ),
             new Tile(Suits.DOT, Rank.ONE ), new Tile(Suits.DOT, Rank.ONE ),
             new Tile(Suits.DOT, Rank.ONE ), new Tile(Suits.DOT, Rank.ONE ),
             new Tile(Suits.BAM, Rank.ONE ), new Tile(Suits.BAM, Rank.ONE ),
             new Tile(Suits.BAM, Rank.ONE ), new Tile(Suits.BAM, Rank.ONE ),
             new Tile(Suits.CRACK, Rank.ONE ), new Tile(Suits.CRACK, Rank.ONE ),
             new Tile(Suits.CRACK, Rank.ONE ), new Tile(Suits.CRACK, Rank.ONE ),
             new Tile(Suits.DOT, Rank.TWO ), new Tile(Suits.DOT, Rank.TWO ),
             new Tile(Suits.DOT, Rank.TWO ), new Tile(Suits.DOT, Rank.TWO ),
             new Tile(Suits.BAM, Rank.TWO ), new Tile(Suits.BAM, Rank.TWO ),
             new Tile(Suits.BAM, Rank.TWO ), new Tile(Suits.BAM, Rank.TWO ),
             new Tile(Suits.CRACK, Rank.TWO ), new Tile(Suits.CRACK, Rank.TWO ),
             new Tile(Suits.CRACK, Rank.TWO ), new Tile(Suits.CRACK, Rank.TWO ),
             new Tile(Suits.DOT, Rank.THREE ), new Tile(Suits.DOT, Rank.THREE ),
             new Tile(Suits.DOT, Rank.THREE ), new Tile(Suits.DOT, Rank.THREE ),
             new Tile(Suits.BAM, Rank.THREE ), new Tile(Suits.BAM, Rank.THREE ),
             new Tile(Suits.BAM, Rank.THREE ), new Tile(Suits.BAM, Rank.THREE ),
             new Tile(Suits.CRACK, Rank.THREE ), new Tile(Suits.CRACK, Rank.THREE ),
             new Tile(Suits.CRACK, Rank.THREE ), new Tile(Suits.CRACK, Rank.THREE ),
             new Tile(Suits.DOT, Rank.FOUR ), new Tile(Suits.DOT, Rank.FOUR ),
             new Tile(Suits.DOT, Rank.FOUR ), new Tile(Suits.DOT, Rank.FOUR ),
             new Tile(Suits.BAM, Rank.FOUR ), new Tile(Suits.BAM, Rank.FOUR ),
             new Tile(Suits.BAM, Rank.FOUR ), new Tile(Suits.BAM, Rank.FOUR ),
             new Tile(Suits.CRACK, Rank.FOUR ), new Tile(Suits.CRACK, Rank.FOUR ),
             new Tile(Suits.CRACK, Rank.FOUR ), new Tile(Suits.CRACK, Rank.FOUR ),
             new Tile(Suits.DOT, Rank.FIVE ), new Tile(Suits.DOT, Rank.FIVE ),
             new Tile(Suits.DOT, Rank.FIVE ), new Tile(Suits.DOT, Rank.FIVE ),
             new Tile(Suits.BAM, Rank.FIVE ), new Tile(Suits.BAM, Rank.FIVE ),
             new Tile(Suits.BAM, Rank.FIVE ), new Tile(Suits.BAM, Rank.FIVE ),
             new Tile(Suits.CRACK, Rank.FIVE ), new Tile(Suits.CRACK, Rank.FIVE ),
             new Tile(Suits.CRACK, Rank.FIVE ), new Tile(Suits.CRACK, Rank.FIVE ),
             new Tile(Suits.DOT, Rank.SIX ), new Tile(Suits.DOT, Rank.SIX ),
             new Tile(Suits.DOT, Rank.SIX ), new Tile(Suits.DOT, Rank.SIX ),
             new Tile(Suits.BAM, Rank.SIX ), new Tile(Suits.BAM, Rank.SIX ),
             new Tile(Suits.BAM, Rank.SIX ), new Tile(Suits.BAM, Rank.SIX ),
             new Tile(Suits.CRACK, Rank.SIX ), new Tile(Suits.CRACK, Rank.SIX ),
             new Tile(Suits.CRACK, Rank.SIX ), new Tile(Suits.CRACK, Rank.SIX ),
             new Tile(Suits.DOT, Rank.SEVEN ), new Tile(Suits.DOT, Rank.SEVEN ),
             new Tile(Suits.DOT, Rank.SEVEN ), new Tile(Suits.DOT, Rank.SEVEN ),
             new Tile(Suits.BAM, Rank.SEVEN ), new Tile(Suits.BAM, Rank.SEVEN ),
             new Tile(Suits.BAM, Rank.SEVEN ), new Tile(Suits.BAM, Rank.SEVEN ),
             new Tile(Suits.CRACK, Rank.SEVEN ), new Tile(Suits.CRACK, Rank.SEVEN ),
             new Tile(Suits.CRACK, Rank.SEVEN ), new Tile(Suits.CRACK, Rank.SEVEN ),
             new Tile(Suits.DOT, Rank.EIGHT ), new Tile(Suits.DOT, Rank.EIGHT ),
             new Tile(Suits.DOT, Rank.EIGHT ), new Tile(Suits.DOT, Rank.EIGHT ),
             new Tile(Suits.BAM, Rank.EIGHT ), new Tile(Suits.BAM, Rank.EIGHT ),
             new Tile(Suits.BAM, Rank.EIGHT ), new Tile(Suits.BAM, Rank.EIGHT ),
             new Tile(Suits.CRACK, Rank.EIGHT ), new Tile(Suits.CRACK, Rank.EIGHT ),
             new Tile(Suits.CRACK, Rank.EIGHT ), new Tile(Suits.CRACK, Rank.EIGHT ),
             new Tile(Suits.DOT, Rank.NINE ), new Tile(Suits.DOT, Rank.NINE ),
             new Tile(Suits.DOT, Rank.NINE ), new Tile(Suits.DOT, Rank.NINE ),
             new Tile(Suits.BAM, Rank.NINE ), new Tile(Suits.BAM, Rank.NINE ),
             new Tile(Suits.BAM, Rank.NINE ), new Tile(Suits.BAM, Rank.NINE ),
             new Tile(Suits.CRACK, Rank.NINE ), new Tile(Suits.CRACK, Rank.NINE ),
             new Tile(Suits.CRACK, Rank.NINE ), new Tile(Suits.CRACK, Rank.NINE ),
        ];

        shuffledTiles = new List<Tile>();
        ShuffleAllTiles();
    }
    public Tile(Suits suit, Rank rank)
    {
        this.Suit = suit;
        this.Rank = rank;
        // do I want to error corret my suit & rank? TODO
    }

    public Tile() : this(Suits.BLANK, Rank.BLANK){}
    public static List<Tile> ShuffledTiles { get { return shuffledTiles; } }
    public Suits Suit { get; }
    public Rank Rank { get; }
    public bool FaceUp { get; set; }

    public static T GetNextEnumValue<T>(T enumValue) where T : Enum
    {
        T[] values = (T[])Enum.GetValues(enumValue.GetType());
        int index = Array.IndexOf(values, enumValue) + 1;
        return (index == values.Length) ? values[0] : values[index];
    }

    private static void ShuffleAllTiles()
    {
        HashSet<int> allTilesIndex = new HashSet<int>();
        Random random = new Random();
        int min = 0;
        int max = ALLTILESNOBLANKS.Length;

        while (allTilesIndex.Count < max)
        {
            int r = random.Next(min, max);
            if(allTilesIndex.Add(r))
            {
                shuffledTiles.Add(ALLTILESNOBLANKS[r]);
            }
        }
    }

    public int CompareTo(Tile? other)
    {
        if (other == null) return 1;

        if (this == other) return 0;

        if (this.Suit.CompareTo(other.Suit) == 0)
        {
            return this.Rank.CompareTo(other.Rank);
        }

        switch (this.Suit)
        {
            case Suits.BAM: // Rank 1 - 9
                if (((int)other.Suit) <  4) { return -1; } // other suit is flower/dragon/wind/joker
                if (this.Rank == other.Rank) { return 0; }
                return this.Rank > other.Rank ? 1 : -1;
            case Suits.CRACK: // Rank 1 - 9
                if (((int)other.Suit) < 4) { return -1; } // other suit is flower/dragon/wind/joker
                if (this.Rank == other.Rank) { return 0; }
                return this.Rank > other.Rank ? 1 : -1;
            case Suits.DOT: // Rank 1 - 9
                if (((int)other.Suit) < 4) { return -1; } // other suit is flower/dragon/wind/joker
                if (this.Rank == other.Rank) { return 0; }
                return (int)this.Rank > (int)other.Rank ? 1 : -1;
            case Suits.FLOWER:
                return (int)other.Suit > 3 ? 1 : 0; // FLOWER equal to Dragon, Joker, Wind and blank, greater than others
            case Suits.DRAGON:
                return (int)other.Suit > 3 ? 1 : 0; // DRAGON equal to Dragon, Joker, Wind and blank, greater than others
            case Suits.JOKER:
                return (int)other.Suit > 3 ? 1 : 0; // JOKER equal to Dragon, Joker, Wind and blank, greater than others
            case Suits.WIND:
                return (int)other.Suit > 3 ? 1 : 0; // WIND equal to Dragon, Joker, Wind and blank, greater than others
            case Suits.BLANK:
                return (int)other.Suit > 3 ? 1 : 0; // BLANK equal to Dragon, Joker, Wind and blank, greater than others
            default:
                return 0;
        }
    }

    public override string ToString()
    {
        return $"{Enum.GetName(typeof(Suits), this.Suit)}, {Enum.GetName(typeof(Rank), this.Rank)}";
    }

    public static bool RemoveFromList(List<Tile> list, Rank r, Suits s, int quantity)
    {
        if (quantity == 0) { return true; }

        int removed = 0;
        int copy = quantity;

        while (copy > 0)
        {
            foreach (Tile tile in list)
            {
                if (tile == null) { continue; }
                if ((tile.Suit == s) && (tile.Rank == r))
                {
                    list.Remove(tile);
                    removed++;
                    break;
                }
            }

            copy--;
        }
        return removed == quantity;
    }

} // end Tile Class



public class Wall : IDisposable
{
    private List<Tile> wallTiles;

    public Wall() { wallTiles = new List<Tile>(); }

    public List<Tile> WallTiles { get => wallTiles; set => wallTiles = value; }

    public bool IsEmpty() { return wallTiles.Count == 0; }
    
    public bool Draw(out Tile? tile)
    {
        if (IsEmpty()) { tile = null; return false; }

        tile = WallTiles[0];
        wallTiles.RemoveAt(wallTiles.Count - 1);
        return true;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}

public class Rack(Tile?[] hand)
{
    private readonly Tile?[] hand = hand;

    public Rack() : this(Enumerable.Repeat<Tile>(new Tile(Suits.BLANK, Rank.BLANK), 14).ToArray()) { }

    public Tile?[] Hand => hand;

    public Tile? GetTile(Suits suit, Rank rank, out bool successful)
    {
        successful = true;
        foreach(Tile? t in this.hand)
        {
            if (t is not null && t.Suit == suit && t.Rank == rank)
            {
                return t;
            }
        }
        successful = false;
        return default;
    }

    public bool GetEmptyTileAndSet(Tile tile)
    {
        for (int i = 0; i < hand.Length; i++)
        {
            if (hand[i] == null) { hand[i] = tile; return true; }
        }

        return false;
    }
    public bool SortHand()
    {
        List<Tile> tilesToSort = new List<Tile>();
        foreach (Tile? tile in hand)
        {
            if (tile == null) { continue; }
            if (!tile.FaceUp)
            {
                tilesToSort.Add(tile);
            }
        }
        tilesToSort.Sort();
        return true;
    }

    public bool AddTile(Tile tile)
    {
        for(int i = 0; i < hand.Length; i++)
        {
            if (hand[i] == null) { hand[i] = tile; return true; }
        }

        return false;
    }

    public bool RemoveTile(Tile tile)
    {
        if (tile == null) { return false; }

        for (int i = 0; i < hand.Length; i++)
        {
            if (hand[i] is not null)
            {
                if (hand[i] == tile || (hand[i]?.Suit == tile.Suit && hand[i]?.Rank == tile.Rank)) 
                { 
                    hand[i] = null; return true; 
                }
            }
        }

        return false;
    }

    public Dictionary<ValueTuple<Suits, Rank>, int>? GetDictionary()
    {
        if (this.hand is null) { return default; }

        Dictionary<ValueTuple<Suits, Rank>, int> tileDictionary = new();

        foreach (Tile? t in this.hand)
        {
            if (t is not null)
            {
                if (!tileDictionary.TryAdd((t.Suit, t.Rank), 1))
                {
                    tileDictionary[(t.Suit, t.Rank)]++;
                }
            }
        }

        return tileDictionary;
    }

    public static Dictionary<ValueTuple<Suits, Rank>, int>? GetDictionary(Tile[] tiles)
    {
        if (tiles is null) { return default; }

        Dictionary<ValueTuple<Suits, Rank>, int> tileDictionary = new();

        foreach (Tile? t in tiles)
        {
            if (t is not null)
            {
                if (!tileDictionary.TryAdd((t.Suit, t.Rank), 1))
                {
                    tileDictionary[(t.Suit, t.Rank)]++;
                }
            }
        }

        return tileDictionary;
    }

    public override string ToString()
    {
        StringBuilder str = new StringBuilder();
        str.Append("\n");
        int index = 0;

        foreach(Tile ?tile in hand)
        {
            str.Append($"{index}: ");
            if (tile == null) { str.Append(" empty \n"); }
            else
            {
                str.Append(" " + tile.ToString()+" \n");
            }
            index++;
        }

        return str.ToString();
    }

    

}
