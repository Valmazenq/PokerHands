﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PokerHands
{
    public class Poker
    {
        public Result Compare(string bobsHand, string johnsHand)
        {
            var bobsCards = bobsHand.Split(" ");
            var johnsCards = johnsHand.Split(" ");

            var values = new char[] { 'A', 'K', 'Q', 'J', 'T' , '9', '8', '7', '6', '5', '4', '3', '2' };

            foreach (var value in values)
            {
                if (WhoWinsPair(bobsCards, johnsCards, value) != null) 
                    return WhoWinsPair(bobsCards, johnsCards, value);
            }

            foreach (var value in values)
            {
                if (WhoWinsHighCard(bobsCards, johnsCards, value) != null)
                    return WhoWinsHighCard(bobsCards, johnsCards, value);
            }

            return new Result { Winner = "Tie", WinningCard = null, WinningCombination = null };

        }

        private Result WhoWinsPair(string[] bobsCards, string[] johnsCards, char value)
        {
            var bobHasPair = bobsCards.Count(x => x.Contains(value)) == 2;
            var johnHasPair = johnsCards.Count(x => x.Contains(value)) == 2;
            var combination = "Pair";

            return WhoWins(value, bobHasPair, johnHasPair, combination);

        }

        private Result WhoWinsHighCard(string[] bobsCards, string[] johnsCards, char value)
        {
            var bobHasThisCard = bobsCards.Any(x => x.Contains(value));
            var johnHasThisCard = johnsCards.Any(x => x.Contains(value));
            var combination = "High Card";

            return WhoWins(value, bobHasThisCard, johnHasThisCard, combination);
        }

        private Result WhoWins(char value, bool bobWins, bool johnWins, string combination)
        {
            if (bobWins && johnWins)
                return null;

            if (bobWins)
                return new Result() {Winner = "Bob", WinningCard = value.ToString(), WinningCombination = combination};

            if (johnWins)
                return new Result() {Winner = "John", WinningCard = value.ToString(), WinningCombination = combination};

            return null;
        }
    }

    public class Result
    {
        public string Winner { get; set; }
        public string WinningCard { get; set; }
        public string WinningCombination { get; set; }
    }

    public abstract class Combination
    {
        public abstract Result WhoWins(string player1Cards, string player2Cards, char cardValue);
        protected Result WhoWinsHelper(char value, bool bobWins, bool johnWins, string combination)
        {
            if (bobWins && johnWins)
                return null;

            if (bobWins)
                return new Result() { Winner = "Bob", WinningCard = value.ToString(), WinningCombination = combination };

            if (johnWins)
                return new Result() { Winner = "John", WinningCard = value.ToString(), WinningCombination = combination };

            return null;

        }
    }

    public class HighCard : Combination
    {
        public override Result WhoWins(string player1Cards, string player2Cards, char cardValue)
        {
            /*
            var bobHasThisCard = player1Cards.Any(x => x.Contains(cardValue));
            var johnHasThisCard = player2Cards.Any(x => x.Contains(cardValue));
            */
            var combination = "High Card";

            /*
            return WhoWinsHelper(cardValue, bobHasThisCard, johnHasThisCard, combination);
            */
            return null;
        }
    }
}
