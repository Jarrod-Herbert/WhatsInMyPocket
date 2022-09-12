using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

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
            List<Card> ordered = OrderHandByValue(hand);
            
            if (HandIsRoyalFlush(ordered))
                return HandType.ROYAL_FLUSH;
            
            // if (HandIsStraightFlush(hand))
                // return HandType.STRAIGHT_FLUSH;
            
            if (HandIsFlush(ordered))
                return HandType.FLUSH;
            
            return HandType.NOTHING;
        }

        private static List<Card> OrderHandByValue(List<Card> hand)
        {   

            List<Card> orderedList = new List<Card>();
            List<Card> tempList = new List<Card>();

            foreach (var card in hand)
            {
                tempList.Add(card);
            }

            for (int i = 0; i < 5; i++)
            {
                Card lowestCard = new Card(Suit.SPADES, Value.ACE);

                foreach (var card in tempList)
                {
                    if ((int) card.Value <= (int) lowestCard.Value)
                        lowestCard = card;
                }
                
                orderedList.Add(lowestCard);
                tempList.Remove(lowestCard);
            }

            print($"cards ordered by value: {orderedList[0]}, {orderedList[1]}, {orderedList[2]}, {orderedList[3]}, {orderedList[4]}");
            print($"are cards in order: {HandIsOrderedByValue(orderedList)}");
            print($"are cards sequential: {HandIsSequential(orderedList)}");
            return orderedList;
        }

        private static bool HandIsOrderedByValue(List<Card> hand)
        {
            for (int i = 0; i < 3; i++)
            {
                if ((int) hand[i].Value > (int) hand[i + 1].Value)
                    return false;
            }

            return true;
        }

        private static bool HandIsFlush(List<Card> hand)
        {
            Suit firstSuit = hand[0].Suit;

            foreach (var card in hand)
            {
                if (card.Suit != firstSuit)
                    return false;
            }
            
            return true;
        }

        private static bool HandIsSequential(List<Card> hand)
        {
            // TODO: Ace handling. IF orderedList[0].Value is TWO, and orderedList[4].Value is ACE, move ACE to index 0.
            
            var startId = (int) hand[0].Value;

            for (int i = 0; i < 5; i++)
            {   
                if (startId + i != (int) hand[i].Value)
                    return false;
            }
            
            return true;
        }

        private static bool HandIsRoyalFlush(List<Card> hand)
        {
            if (!HandIsFlush(hand))
                return false;

            if (!HandIsSequential(hand))
            {
                print($"Hand not sequential");
                return false;
            }

            if (hand[0].Value != Value.TEN)
                return false;
            
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