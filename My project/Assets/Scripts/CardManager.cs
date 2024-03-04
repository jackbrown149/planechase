using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : NetworkBehaviour
{
    private static CardManager _main;

    private static List<CardInfo> cards = new List<CardInfo>();

    [SyncVar]
    private List<CardInfo> sessionCards = new List<CardInfo>();

    public GameObject cardPrefab;

    private CardStack stack;

    private void Start()
    {
        
        _main = this;
        CardSaver.Init();
        stack = GetComponent<CardStack>();
        if (isServer)
        {
            sessionCards = new List<CardInfo>(cards);

            for (int i = 0; i < sessionCards.Count; i++)
            {
                for (int l = 0; l < sessionCards[i].amount; l++)
                {
                    stack.AddCard(i);
                }
            }
        }
    }

    [Command(requiresAuthority = false)]
    private void CmdSpawn(int id, Vector3 position, Quaternion rotation)
    {
        Card c = Instantiate(cardPrefab, position, rotation).GetComponent<Card>();
        NetworkServer.Spawn(c.gameObject);

        c.UpdateCard(id);
    }

    public static void SpawnFromID(int id, Vector3 position, Quaternion rotation)
    {
        _main.CmdSpawn(id, position, rotation);
    }

    public static void LoadCards(List<CardInfo> cards)
    {
        CardManager.cards = cards;
    }

    public static CardInfo[] GetCards()
    {
        return cards.ToArray();
    }

    public static void SetCard(int cardId, CardInfo info)
    {
        cards[cardId] = info;
    }

    public static CardInfo GetInfo(int cardId)
    {
        return cards[cardId];
    }

    public static bool ValidId(int cardId)
    {
        return cardId < cards.Count || cardId >= 0;
    }

    public static int NewCard(CardInfo info)
    {
        cards.Add(info);
        return cards.Count - 1;
    }

    public static void Delete(int id)
    {
        cards.RemoveAt(id);
    }

}

[Serializable]
public struct CardInfo
{
    public string type;
    public string heroName, heroDescription;
    public int heroHealth, heroAttack, heroShield;

    public string dungeonName, dungeonDescription;
    public int dungeonHealth, dungeonAttack, dungeonShield;

    public string spellName;
    public string spellDescription;

    public int amount;
}