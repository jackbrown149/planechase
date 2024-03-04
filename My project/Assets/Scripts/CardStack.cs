using Mirror;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CardStack : NetworkBehaviour
{

    [SerializeField]
    private GameObject cardStack;

    [SerializeField]
    [SyncVar]
    private List<int> cards = new List<int>();

    //[Server]
    //private void Start()
    //{
    //    for (int i = 0; i < cardStack.transform.childCount; i++)
    //    {
    //        AddCard(cardStack.transform.GetChild(0).GetComponent<Card>());
    //    }
    //}

    [Server]
    public void AddCard(Card card)
    {
        cards.Add(card.cardId);

        Destroy(card.gameObject);

        //if (card.GetComponent<Interactable>().isHeld()) return;
        //card.transform.parent = cardStack.transform;
        //card.transform.localPosition = Vector3.zero;
        //card.gameObject.SetActive(false);
        //RpcAddCard(card.gameObject);

        RpcAddCard(card.cardId);
    }

    [Server]
    public void AddCard(int id)
    {
        cards.Add(id);

        RpcAddCard(id);
    }

    //[ClientRpc]
    //public void RpcAddCard(GameObject card)
    //{
    //    card.transform.parent = cardStack.transform;
    //    card.transform.localPosition = Vector3.zero;
    //    card.gameObject.SetActive(false);
    //}

    [ClientRpc]
    public void RpcAddCard(int cardId)
    {
        if (!isServer)
        {
            cards.Add(cardId);
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdRemoveTop()
    {
        //Card card = null;
        //if (cardStack.transform.childCount != 0)
        //{
        //    card = cardStack.transform.GetChild(0).GetComponent<Card>();
        //    card.transform.parent = null;
        //    card.gameObject.SetActive(true);
        //}

        if (cards.Count == 0) return;
        CardManager.SpawnFromID(cards[0], cardStack.transform.position, cardStack.transform.rotation);
        cards.RemoveAt(0);
        RpcLol();
    }

    //[ClientRpc]
    //public void RpcRemoveTop()
    //{
    //    Card card = null;
    //    if (cardStack.transform.childCount != 0)
    //    {
    //        card = cardStack.transform.GetChild(0).GetComponent<Card>();
    //        card.transform.parent = null;
    //        card.gameObject.SetActive(true);
    //    }
    //}

    public int GetTop()
    {
        //if (cardStack.transform.childCount == 0) return null;
        //return cardStack.transform.GetChild(0).GetComponent<Card>();

        if (cards.Count == 0) return -1;
        int top = cards[0];
        CmdLol();
        return top;
    }

    [Command(requiresAuthority = false)]
    public void CmdLol()
    {
        cards.RemoveAt(0);
        RpcLol();
    }

    [ClientRpc]
    public void RpcLol()
    {
        if (isServer) return;
        cards.RemoveAt(0);
    }

    //[ClientRpc]
    //public void RpcRemoveTop()
    //{
    //    Card card = null;
    //    if (cardStack.transform.childCount != 0)
    //    {
    //        card = cardStack.transform.GetChild(0).GetComponent<Card>();
    //        card.transform.parent = null;
    //        card.gameObject.SetActive(true);
    //    }
    //}

    [ClientRpc]
    public void RpcShuffle(Int2[] tuple)
    {
        if (isServer) return;
        foreach (var a in tuple)
        {
            int l = cards[a.x];
            cards.RemoveAt(a.x);
            cards.Insert(a.y, l);
        }
    }

    [ClientRpc]
    public void RpcShuffles(int[] tuple)
    {
        if (isServer) return;
        cards = new List<int>(tuple);
    }

    [Command(requiresAuthority = false)]
    public void CmdShuffleCards()
    {
        Int2[] tupleArray = new Int2[cards.Count - 1];
        for (int i = 0; i < cards.Count - 1; i++)
        {
            tupleArray[i] = new Int2 { x = i, y = UnityEngine.Random.Range(0, cards.Count) };
            int l = cards[tupleArray[i].x];
            cards.RemoveAt(tupleArray[i].x);
            cards.Insert(tupleArray[i].y, l);
        }

        RpcShuffles(cards.ToArray());
    }

    [Server]
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Card"))
        {
            Card card = collision.gameObject.GetComponent<Card>();
            if (card.stackable)
            {
                AddCard(card);
            }
        }
    }

}

[Serializable]
public struct Int2
{
    public int x;
    public int y;
}
