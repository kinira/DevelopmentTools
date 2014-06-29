using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker
{
    public class PokerHandsChecker : IPokerHandsChecker
    {
        const int handSize = 5;
        public bool IsValidHand(IHand hand)
        {           
            if (hand.Cards.Count == handSize)
            {
                for (int i = 0; i < handSize; i++)
                {
                    for (int j = i + 1; j < handSize; j++)
                    {
                        if (hand.Cards[i].Face == hand.Cards[j].Face && hand.Cards[i].Suit == hand.Cards[j].Suit)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            return false;
        }

        public bool IsStraightFlush(IHand hand)
        {
            if (IsValidHand(hand))
            {
                int countFacesMax = CalculateFacesMaxCountInHand(hand);
                int countOfFacesTotal = CalculateCountDifferentCardsFaces(hand);
                int coutOfSuits = CalculateCountDifferentCardsSuits(hand);
                if (countFacesMax == 1 && countOfFacesTotal == 5 && coutOfSuits == 1 && IsConsecutive(hand))
                {
                    return true;
                }                
            }
            return false;
        }

        public bool IsFourOfAKind(IHand hand)
        {
            if (IsValidHand(hand))
            {
                int countFacesMax = CalculateFacesMaxCountInHand(hand);
                if (countFacesMax == 4)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsFullHouse(IHand hand)
        {
            if (IsValidHand(hand))
            {
                int countFacesMax = CalculateFacesMaxCountInHand(hand);
                int countOfFacesTotal = CalculateCountDifferentCardsFaces(hand);
                if (countFacesMax == 3 && countOfFacesTotal == 2)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsFlush(IHand hand)
        {
            throw new NotImplementedException();
        }

        public bool IsStraight(IHand hand)
        {
            if (IsValidHand(hand))
            {
                int countFacesMax = CalculateFacesMaxCountInHand(hand);
                int countOfFacesTotal = CalculateCountDifferentCardsFaces(hand);
                int coutOfSuits = CalculateCountDifferentCardsSuits(hand);
                if (countFacesMax == 1 && countOfFacesTotal == 5 && coutOfSuits != 1 && IsConsecutive(hand))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsThreeOfAKind(IHand hand)
        {
            if (IsValidHand(hand) && !IsFullHouse(hand))
            {
                int countFacesMax = CalculateFacesMaxCountInHand(hand);
                if (countFacesMax == 3)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsTwoPair(IHand hand)
        {
            if (IsValidHand(hand))
            {
                int countFacesMax = CalculateFacesMaxCountInHand(hand);
                int countOfFacesTotal = CalculateCountDifferentCardsFaces(hand);
                if (countFacesMax == 2 && countOfFacesTotal == 3)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsOnePair(IHand hand)
        {
            if (IsValidHand(hand) && !IsFullHouse(hand) && !IsTwoPair(hand))
            {
                int countFacesMax = CalculateFacesMaxCountInHand(hand);
                int countOfFacesTotal = CalculateCountDifferentCardsFaces(hand);
                if (countFacesMax == 2 && countOfFacesTotal == 4)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsHighCard(IHand hand)
        {
            if (IsValidHand(hand))
            {
                int countFacesMax = CalculateFacesMaxCountInHand(hand);
                int countOfFacesTotal = CalculateCountDifferentCardsFaces(hand);
                if (countFacesMax == 1 && countOfFacesTotal == 5 && !IsConsecutive(hand))
                {
                    return true;
                }
            }
            return false;
        }

        public int CompareHands(IHand firstHand, IHand secondHand)
        {
            throw new NotImplementedException();
        }

        private static int CalculateFacesMaxCountInHand(IHand hand)
        {
            Dictionary<string, int> cardFaces = CalculateCountOfAllCardDifferentFaces(hand);

            int maxCardOftype = cardFaces.Values.Max();

            return maxCardOftype;
        }

        private static int CalculateCountDifferentCardsFaces(IHand hand)
        {
            Dictionary<string, int> cardFaces = CalculateCountOfAllCardDifferentFaces(hand);

            int cardFacesCountInHand = cardFaces.Count();

            return cardFacesCountInHand;
        }

        private static Dictionary<string, int> CalculateCountOfAllCardDifferentFaces(IHand hand)
        {
            Dictionary<string, int> cardFaces = new Dictionary<string, int>();
            for (int i = 0; i < hand.Cards.Count; i++)
            {
                ICard currentCard = hand.Cards[i];
                string currentCardFace = currentCard.Face.ToString();
                if (cardFaces.ContainsKey(currentCardFace))
                {
                    cardFaces[currentCardFace]++;
                }
                else
                {
                    cardFaces.Add(currentCardFace, 1);
                }
            }

            return cardFaces;
        }

        private static int CalculateSuitMaxCountInHand(IHand hand)
        {
            Dictionary<string, int> cardSuits = CalculateCountAllDifferentSuitsInHand(hand);

            int maxCountOfSuit = cardSuits.Values.Max();

            return maxCountOfSuit;
        }

        private static int CalculateCountDifferentCardsSuits(IHand hand)
        {
            Dictionary<string, int> cardSuits = CalculateCountAllDifferentSuitsInHand(hand);

            int cardSuitsCountInHand = cardSuits.Count();

            return cardSuitsCountInHand;
        }

        private static Dictionary<string, int> CalculateCountAllDifferentSuitsInHand(IHand hand)
        {
            Dictionary<string, int> cardSuits = new Dictionary<string, int>();
            for (int i = 0; i < hand.Cards.Count; i++)
            {
                ICard currentCard = hand.Cards[i];
                string currentCardSuit = currentCard.Suit.ToString();
                if (cardSuits.ContainsKey(currentCardSuit))
                {
                    cardSuits[currentCardSuit]++;
                }
                else
                {
                    cardSuits.Add(currentCardSuit, 1);
                }
            }

            return cardSuits;
        }

        private static bool IsConsecutive(IHand hand)
        {
            Dictionary<string, int> cardFaces = CalculateCountOfAllCardDifferentFaces(hand);

            int countconsequative = 1;
            for (int i = 1; i < hand.Cards.Count; i++)
            {
                ICard currentCard = hand.Cards[i];
                string currentCardFace = currentCard.Face.ToString();

                string previous = CalculatePreviousCard(currentCardFace);
                if (cardFaces.ContainsKey(previous))
                {
                    countconsequative++;
                }
            }

            if (countconsequative == 4)
            {
                return true;
            }

            return false;
        }

        private static string CalculatePreviousCard(string previousCardFace)
        {
            switch (previousCardFace)
            {
                case "Ace": return "King";
                case "King": return "Queen";
                case "Queen": return "Jack";
                case "Jack": return "Ten";
                case "Ten": return "Nine";
                case "Nine": return "Eight";
                case "Eight": return "Seven";
                case "Seven": return "Six";
                case "Six": return "Five";
                case "Five": return "Four";
                case "Four": return "Three";
                case "Three": return "Two";
                case "Two": return "Ace";
            }

            throw new ArgumentOutOfRangeException("Invalid Face of the card!");
        }
    }
}
