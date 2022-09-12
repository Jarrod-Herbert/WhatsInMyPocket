using System;
using System.Collections.Generic;
using System.Diagnostics;
// using System.Linq;
using System.Runtime.CompilerServices;

namespace PokerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var deck = new Deck();
            //var hand = deck.GetHand();
            //var hand = deck.GetHandWithPair();
            //var hand = deck.GetHandWithTwoPair();
            //var hand = deck.GetHandWithThreeOfAKind();
            //var hand = deck.GetHandWithStraight();
            //var hand = deck.GetHandWithFlush();
            //var hand = deck.GetHandWithFullHouse();
            //var hand = deck.GetHandWithFourOfAKind();
            //var hand = deck.GetHandWithStraightFlush();
            //var hand = deck.GetHandWithRoyalFlush();
            var hand = deck.GetHandWithAceTwoStraight();

            print("Your hand is...");
            foreach (var card in hand)
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

            if (HandIsStraightFlush(ordered))
                return HandType.STRAIGHT_FLUSH;

            if (HandIsFourOfAKind(ordered))
                return HandType.FOUR_OF_A_KIND;
            
            if (HandIsFullHouse(ordered))
                return HandType.FULL_HOUSE;

            if (HandIsFlush(ordered))
                return HandType.FLUSH;

            if (HandIsStraight(ordered))
                return HandType.STRAIGHT;

            if (HandIsThreeOfAKind(ordered))
                return HandType.THREE_OF_A_KIND;

            if (HandIsTwoPair(ordered))
                return HandType.TWO_PAIR;

            if (HandIsPair(ordered))
                return HandType.PAIR;
            
            print($"High Card: {ordered[^1]}");
            return HandType.HIGH_CARD;
        }

        private static int HowManyPairs(List<Card> hand)
        {
            var results = CardCountByValue(hand);
            int i = 0;

            foreach (var result in results)
            {
                if ((int) result.Count() == 2)
                    i++;
            }

            return i;
        }

        private static IEnumerable<IGrouping<Value, Card>> CardCountByValue(List<Card> hand)
        {
            return hand.GroupBy(x => x.Value);
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
            print($"are cards sequential: {HandIsStraight(orderedList)}");
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
        
        private static bool HandIsPair(List<Card> hand)
        {
            return HowManyPairs(hand) == 1;
        }
        private static bool HandIsTwoPair(List<Card> hand)
        {
            return HowManyPairs(hand) == 2;
        }
        
        private static bool HandIsFullHouse(List<Card> hand)
        {
            if (HandIsThreeOfAKind(hand) && HandIsPair(hand))
                return true;

            return false;
        }

        private static bool HandIsThreeOfAKind(List<Card> hand)
        {
            var results = CardCountByValue(hand);

            foreach (var result in results)
            {
                if ((int) result.Count() == 3)
                    return true;
            }

            return false;
        }

        private static bool HandIsFourOfAKind(List<Card> hand)
        {
            var results = CardCountByValue(hand);

            foreach (var result in results)
            {
                if ((int) result.Count() == 4)
                    return true;
            }

            return false;
        }
        
        private static bool HandIsStraight(List<Card> hand)
        {
            var newHand = WhereShouldTheAceGo(hand);
            
            var startId = (int) newHand[0].Value;

            for (int i = 0; i < 5; i++)
            {   
                if (startId + i != (int) newHand[i].Value)
                    return false;
            }
            
            return true;
        }

        private static List<Card> WhereShouldTheAceGo(List<Card> hand)
        {
            // TODO: Ace handling. IF orderedList[0].Value is TWO, and orderedList[4].Value is ACE, move ACE to index 0.
            bool lowestCardTwo = hand[0].Value == Value.TWO;
            bool highestCardAce = hand[^1].Value == Value.ACE;
            bool hasKing = false;
            bool hasThree = false;

            if (highestCardAce && lowestCardTwo)
            {
                var outputList = new List<Card>();
                // is there a king or a three?
                foreach (var card in hand)
                {
                    if (card.Value == Value.KING) {}
                        hasKing = true;
                    if (card.Value == Value.THREE)
                        hasThree = true;
                }
                
                if (!hasKing && hasThree)
                {
                    print("Does not have a king, has a three. Move ace to index 0!");
                }
            }

            return hand;
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
        
        private static bool HandIsStraightFlush(List<Card> hand)
        {
            if (!HandIsFlush(hand) && !HandIsStraight(hand))
                return false;
            
            return true;
        }
        
        private static bool HandIsRoyalFlush(List<Card> hand)
        {
            if (!HandIsFlush(hand))
                return false;

            if (!HandIsStraight(hand))
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
    HIGH_CARD, PAIR, TWO_PAIR, THREE_OF_A_KIND, STRAIGHT, FLUSH, FULL_HOUSE, FOUR_OF_A_KIND, STRAIGHT_FLUSH, ROYAL_FLUSH
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