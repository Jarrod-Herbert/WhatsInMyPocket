namespace PokerDemo;

public class Card
{
    public Suit Suit { get; private set; }
    public Value Value { get; private set; }
    public Card(Suit suit, Value value)
    {
        this.Suit = suit;
        this.Value = value;
    }
 
    public override string ToString()
    {
        return $"{Value} of {Suit}";
    }
}