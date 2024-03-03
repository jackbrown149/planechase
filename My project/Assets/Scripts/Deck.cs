using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Deck : MonoBehaviour
{
    private List<Card> list;
    private Card currentCard;

    public static Deck Instance;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            print("Deck created");
        }
        else
        {
            print("Duplicate deck created and culled");
            Destroy(this);
        }
    }

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


    //fisher yates shuffle off the internet, looks like O(n^2) | https://stackoverflow.com/questions/273313/randomize-a-listt 

    /*
    someone else said:

    "As people have pointed out in the comments, GUIDs are not
    guaranteed to be random, so we should be using a real random
    number generator instead:"

    private static Random rng = new Random();
    ...
    var shuffledcards = cards.OrderBy(_ => rng.Next()).ToList();

    */

    private static System.Random rng = new System.Random();
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
