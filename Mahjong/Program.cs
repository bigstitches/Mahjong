using Mahjong;

public class Program
{
    public static void Main(string[] args)
    {
        //Tile dotOne = new Tile(Suits.DOT, Rank.ONE);
        //Tile dotTwo = new Tile(Suits.DOT, Rank.TWO);
        //Tile bamOne = new Tile(Suits.BAM, Rank.ONE);
        //Tile bamTwo = new Tile(Suits.BAM, Rank.TWO);
        //Tile bamThree = new Tile(Suits.BAM, Rank.THREE);
        //Tile bamFive = new Tile(Suits.BAM, Rank.FIVE);
        //Tile bamFour = new Tile(Suits.BAM, Rank.FOUR);
        //Tile crackFour = new Tile(Suits.CRACK, Rank.FOUR);
        //Tile joker = new Tile(Suits.JOKER, Rank.JOKER);
        //Tile flower = new Tile(Suits.FLOWER, Rank.FLOWER);
        //Tile north = new Tile(Suits.WIND, Rank.NORTH);
        //Tile south = new Tile(Suits.WIND, Rank.SOUTH);
        //Tile west = new Tile(Suits.WIND, Rank.WEST);

        //List<Tile> winningHand = new List<Tile> { flower, flower, dotOne, dotOne, dotOne, joker, bamTwo, bamTwo, bamThree, bamThree, bamThree, crackFour, crackFour, crackFour };

        //bool isWinning = Sequences.IsWinningHand(winningHand);

        //List<Tile> winningHand1 = new List<Tile> { dotOne, dotOne, dotOne, joker, bamTwo, bamTwo, bamThree, bamThree, bamThree, crackFour, crackFour, crackFour, flower, flower};

        //bool isWinning1= Sequences.IsWinningHand(winningHand1);

        //List<Tile> winningHand2 = new List<Tile> { dotOne, dotOne, dotOne, joker, bamTwo, bamTwo, bamThree, flower, flower, bamThree, bamThree, crackFour, crackFour, crackFour };

        //bool isWinning2 = Sequences.IsWinningHand(winningHand2);

        //List<Tile> winningHand3 = new List<Tile> { flower, dotOne, dotOne, flower, dotOne, joker, bamTwo, bamTwo, bamThree, bamThree, bamThree, crackFour, crackFour, crackFour };

        //bool isWinning3 = Sequences.IsWinningHand(winningHand3);

        //List<Tile> winningHand4 = new List<Tile> { flower, flower, dotOne, dotOne, joker, bamTwo, bamTwo, bamThree, bamThree, bamThree, crackFour, crackFour, crackFour, dotOne };

        //bool isWinning4 = Sequences.IsWinningHand(winningHand4);

        //List<Tile> winningHand5 = new List<Tile> { flower, flower, dotOne, dotOne, dotOne, joker, bamTwo, bamTwo, bamThree, bamThree, bamThree, crackFour, crackFour, joker };

        //bool isWinning5 = Sequences.IsWinningHand(winningHand5);

        //Console.WriteLine(isWinning);
        //Console.WriteLine(isWinning1);
        //Console.WriteLine(isWinning2);
        //Console.WriteLine(isWinning3);
        //Console.WriteLine(isWinning4);
        //Console.WriteLine(isWinning5);

        GameConsolePlay firstGame = new GameConsolePlay();
        firstGame.Play();
    }
}