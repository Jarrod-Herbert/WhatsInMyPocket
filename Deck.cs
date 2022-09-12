namespace PokerDemo;

public class Deck
{
    public List<Card> Cards = new List<Card>(52);
    public Deck()
    {
        foreach(var suit in Util.SuitList())
        {
            foreach(var value in Util.ValueList())
            {
                Cards.Add(new Card(suit, value));
            }
        }
 
        // Give it a healthy shuffle 3x.
        Cards.Shuffle();
        Cards.Shuffle();
        Cards.Shuffle();
    }
 
    public List<Card> GetHand()
    {
        var hand = new List<Card>(5);
        for(var i = 0; i < 5; i++)
        {
            hand.Add(Cards[i]);
        }
        return hand;
    }
    
        public List<Card> GetHandWithPair()
    {
        var hand = new List<Card>(5);
        hand.Add(new Card(Suit.CLUBS, Value.FIVE));
        hand.Add(new Card(Suit.HEARTS, Value.FIVE));
        hand.Add(new Card(Suit.DIAMONDS, Value.SIX));
        hand.Add(new Card(Suit.SPADES, Value.SEVEN));
        hand.Add(new Card(Suit.SPADES, Value.JACK));
        return hand;
    }
 
    public List<Card> GetHandWithTwoPair()
    {
        var hand = new List<Card>(5);
        hand.Add(new Card(Suit.CLUBS, Value.FIVE));
        hand.Add(new Card(Suit.HEARTS, Value.FIVE));
        hand.Add(new Card(Suit.DIAMONDS, Value.SIX));
        hand.Add(new Card(Suit.SPADES, Value.SIX));
        hand.Add(new Card(Suit.SPADES, Value.JACK));
        return hand;
    }
 
    public List<Card> GetHandWithThreeOfAKind()
    {
        var hand = new List<Card>(5);
        hand.Add(new Card(Suit.CLUBS, Value.FIVE));
        hand.Add(new Card(Suit.HEARTS, Value.FIVE));
        hand.Add(new Card(Suit.DIAMONDS, Value.FIVE));
        hand.Add(new Card(Suit.SPADES, Value.SIX));
        hand.Add(new Card(Suit.SPADES, Value.JACK));
        return hand;
    }
 
    public List<Card> GetHandWithStraight()
    {
        var hand = new List<Card>(5);
        hand.Add(new Card(Suit.CLUBS, Value.FIVE));
        hand.Add(new Card(Suit.HEARTS, Value.SIX));
        hand.Add(new Card(Suit.DIAMONDS, Value.SEVEN));
        hand.Add(new Card(Suit.SPADES, Value.EIGHT));
        hand.Add(new Card(Suit.SPADES, Value.NINE));
        return hand;
    }
 
    public List<Card> GetHandWithFlush()
    {
        var hand = new List<Card>(5);
        hand.Add(new Card(Suit.HEARTS, Value.TWO));
        hand.Add(new Card(Suit.HEARTS, Value.SIX));
        hand.Add(new Card(Suit.HEARTS, Value.SEVEN));
        hand.Add(new Card(Suit.HEARTS, Value.JACK));
        hand.Add(new Card(Suit.HEARTS, Value.KING));
        return hand;
    }
 
    public List<Card> GetHandWithFullHouse()
    {
        var hand = new List<Card>(5);
        hand.Add(new Card(Suit.HEARTS, Value.TWO));
        hand.Add(new Card(Suit.CLUBS, Value.TWO));
        hand.Add(new Card(Suit.CLUBS, Value.JACK));
        hand.Add(new Card(Suit.DIAMONDS, Value.JACK));
        hand.Add(new Card(Suit.SPADES, Value.JACK));
        return hand;
    }
 
    public List<Card> GetHandWithFourOfAKind()
    {
        var hand = new List<Card>(5);
        hand.Add(new Card(Suit.HEARTS, Value.TWO));
        hand.Add(new Card(Suit.HEARTS, Value.JACK));
        hand.Add(new Card(Suit.CLUBS, Value.JACK));
        hand.Add(new Card(Suit.DIAMONDS, Value.JACK));
        hand.Add(new Card(Suit.SPADES, Value.JACK));
        return hand;
    }
 
    public List<Card> GetHandWithStraightFlush()
    {
        var hand = new List<Card>(5);
        hand.Add(new Card(Suit.CLUBS, Value.THREE));
        hand.Add(new Card(Suit.CLUBS, Value.FOUR));
        hand.Add(new Card(Suit.CLUBS, Value.FIVE));
        hand.Add(new Card(Suit.CLUBS, Value.SIX));
        hand.Add(new Card(Suit.CLUBS, Value.SEVEN));
        return hand;
    }
 
    public List<Card> GetHandWithRoyalFlush()
    {
        var hand = new List<Card>(5);
        hand.Add(new Card(Suit.DIAMONDS, Value.TEN));
        hand.Add(new Card(Suit.DIAMONDS, Value.JACK));
        hand.Add(new Card(Suit.DIAMONDS, Value.QUEEN));
        hand.Add(new Card(Suit.DIAMONDS, Value.KING));
        hand.Add(new Card(Suit.DIAMONDS, Value.ACE));
        return hand;
    }
}