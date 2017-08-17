using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class War
{
    class WarMagic
    {
        public class Card
        {
            public string Number { get; set; }
            public char Suit { get; set; }
            public int Value { get; set; }

            public Card(string card)
            {
                Number = card.Substring(0, card.Length - 1);
                Suit = card[card.Length - 1];
                int value;
                if (!int.TryParse(Number, out value))
                {
                    switch (Number)
                    {
                        case "J":
                            Value = 11;
                            break;
                        case "Q":
                            Value = 12;
                            break;
                        case "K":
                            Value = 13;
                            break;
                        case "A":
                            Value = 14;
                            break;
                    }
                }
                else
                {
                    Value = value;
                }
            }
            public override string ToString()
            {
                return string.Format("{0}{1} ({2})", Number, Suit, Value);
            }
        }

        public class Player
        {
            public int PlayerNumber { get; private set; }
            public List<Card> HandCards { get; private set; }
            public List<Card> BattleCards { get; private set; }
            public Player(int number)
            {
                HandCards = new List<Card>();
                PlayerNumber = number;
                BattleCards = new List<Card>();
            }

            public bool IsReadyToWar()
            {
                return (HandCards.Count >= 4);
            }

            public void AddCards(IEnumerable<Card> cards, bool log = false)
            {
                if (log) Log("Player {0} won: {1}", PlayerNumber, string.Join(", ", cards));
                HandCards.AddRange(cards);
            }
            public void AddCard(Card card)
            {
                AddCards(new Card[] { card });
            }
            public void AddCard(string card)
            {
                AddCard(new Card(card));
            }

            public Card SendCardToBattle()
            {
                Card card = null;
                if (HandCards.Count > 0)
                {
                    card = HandCards.FirstOrDefault();
                    BattleCards.Add(card);
                    HandCards.RemoveAt(0);
                }
                return card;
            }
            public void SendCardsToWar()
            {
                for (int i = 0; i < 3; i++)
                {
                    SendCardToBattle();
                }
            }

            public void CardNumber()
            {
                Log("Player {0} has {1} card(s)", PlayerNumber, HandCards.Count);
            }
            public void ShowCards()
            {
                Log("Player {0} Cards: {1}", PlayerNumber, string.Join(", ", HandCards));
            }

            public void ClearBattleCards()
            {
                BattleCards.Clear();
            }
        }

        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }

        public WarMagic()
        {
            Player1 = new Player(1);
            Player2 = new Player(2);
        }

        public string SimulateWar()
        {
            int numberOfRounds = 0;
            string result = null;
            while (true)
            {
                numberOfRounds++;
                result = Battle();
                if (result != null) { break; }
            }
            if (result.Equals("PAT"))
            {
                return result;
            }
            return string.Format("{0} {1}", result, numberOfRounds);
        }

        private string GetWinner()
        {
            if (Player1.HandCards.Count == 0)
            {
                return "2";
            }
            if (Player2.HandCards.Count == 0)
            {
                return "1";
            }
            return null;
        }

        private string Battle()
        {
            // Get Cards
            var p1Card = Player1.SendCardToBattle();
            var p2Card = Player2.SendCardToBattle();

            // Logs the cards and the expected result
            // Log("{0} VS {1} = {2}", p1Card, p2Card, (p1Card.Value == p2Card.Value) ? "WAR" : "BATTLE");

            if (p1Card.Value > p2Card.Value)
            {
                // P1 Wins Battle
                Player1.AddCards(Player1.BattleCards);
                Player1.AddCards(Player2.BattleCards);
            }
            else if (p1Card.Value < p2Card.Value)
            {
                // P2 Wins Battle
                Player2.AddCards(Player1.BattleCards);
                Player2.AddCards(Player2.BattleCards);
            }
            else
            {
                // War
                if (!Player1.IsReadyToWar() || !Player2.IsReadyToWar())
                {
                    return "PAT";
                }

                // Both players send cards to war
                Player1.SendCardsToWar();
                Player2.SendCardsToWar();

                // Battle the WAR
                Battle();
            }

            // Clear battle cards
            Player1.ClearBattleCards();
            Player2.ClearBattleCards();

            // Check we have a winner.
            return GetWinner();
        }

    }

    static void Main(string[] args)
    {
        WarMagic war = new WarMagic();

        int n = int.Parse(Console.ReadLine()); // the number of cards for player 1
        for (int i = 0; i < n; i++)
        {
            string cardp1 = Console.ReadLine(); // the n cards of player 1
            war.Player1.AddCard(cardp1);
        }
        int m = int.Parse(Console.ReadLine()); // the number of cards for player 2
        for (int i = 0; i < m; i++)
        {
            string cardp2 = Console.ReadLine(); // the m cards of player 2
            war.Player2.AddCard(cardp2);
        }

        var warResult = war.SimulateWar();
        Console.WriteLine(warResult);
    }

    static void Log(string message, params object[] pars)
    {
        Console.Error.WriteLine(string.Format(message, pars));
    }
}