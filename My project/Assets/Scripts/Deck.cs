using System;
using System.Collections;
using System.Collections.Generic;


public class Deck
{
    private List<Card> list;
    private Card currentCard;

    public void Initialize()
    {
        list = new List<Card>();
    }

    public void PutOnBottom(Card next)
    {
        list.Add(next);
    }

    public Card LookTopCard()
    { return list[0];  }

    public Card DrawTopCard()
    {
        currentCard = list[0];
        list.RemoveAt(0);
        return currentCard;
    }

    private static Random rng = new Random();
    public void Shuffle()
    {
        int n = list.Count, k;
        while (n > 1)
        {
            n--;
            k = rng.Next(n + 1);
            currentCard = list[k];
            list[k] = list[n];
            list[n] = currentCard;
        }
    }


}
