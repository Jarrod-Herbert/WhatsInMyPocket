using System;
using System.Collections.Generic;
using System.Linq;
 
namespace PokerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var deck = new Deck();
            // var hand = deck.GetHand();
            //var hand = deck.GetHandWithPair();
            //var hand = deck.GetHandWithTwoPair();
            //var hand = deck.GetHandWithThreeOfAKind();
            //var hand = deck.GetHandWithStraight();
            //var hand = deck.GetHandWithFlush();
            //var hand = deck.GetHandWithFullHouse();
            //var hand = deck.GetHandWithFourOfAKind();
            //var hand = deck.GetHandWithStraightFlush();
            var hand = deck.GetHandWithRoyalFlush();
            
            print("Your hand is...");
            foreach(var card in hand)
            {
                print($" - {card}");
            }
            printLine();
            print($"Your hand was: {EvaluateHand(hand)}");
            printLine();
        }
 
        static HandType EvaluateHand(List<Card> hand)
        {
            if (HandIsRoyalFlush(hand))
                return HandType.ROYAL_FLUSH;

            if (HandIsStraightFlush(hand))
                return HandType.STRAIGHT_FLUSH;
            
            return HandType.NOTHING;
        }

        private static bool HandIsStraightFlush(List<Card> hand)
        {
            throw new NotImplementedException();
        }

        private static bool HandIsRoyalFlush(List<Card> hand)
        {
            List<Value> requiredCardValues = new List<Value>();
            
            requiredCardValues.Add(Value.ACE);
            requiredCardValues.Add(Value.KING);
            requiredCardValues.Add(Value.QUEEN);
            requiredCardValues.Add(Value.JACK);
            requiredCardValues.Add(Value.TEN);

            Suit firstSuit = hand[0].Suit;

            foreach (var card in hand)
            {
                if (card.Suit != firstSuit)
                    return false;
                if (!requiredCardValues.Contains(card.Value))
                    return false;
            }
            
            return true;
        }

        static void print(string msg)
        {
            Console.WriteLine(msg);
        }
 
        static void print(object obj)
        {
            Console.WriteLine(obj.ToString());
        }
 
        static void printLine()
        {
            print("---------------------");
        }
    }
}

public enum HandType
{
    NOTHING, PAIR, TWO_PAIR, THREE_OF_A_KIND, STRAIGHT, FLUSH, FULL_HOUSE, FOUR_OF_A_KIND, STRAIGHT_FLUSH, ROYAL_FLUSH
}
 
public enum Suit
{
    HEARTS, SPADES, CLUBS, DIAMONDS
}
 
 
public enum Value
{
    TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, NINE, TEN, JACK, QUEEN, KING, ACE
}
 
public static class Util
{
    public static List<Suit> SuitList()
    {
        return Enum.GetValues(typeof(Suit))
                            .Cast<Suit>()
                            .ToList();
    }
 
    public static List<Value> ValueList()
    {
        return Enum.GetValues(typeof(Value))
                            .Cast<Value>()
                            .ToList();
    }
 
    private static Random rng = new Random();
 
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}