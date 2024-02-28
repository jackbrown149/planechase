using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //private List<RawCard> cards; // this, extra card type that contains card plus bool for inclusion
                                   // or card carries a mostly useless bool "included" 

    // Start is called before the first frame update
    void Awake()
    {
        //FetchAllCards();

        StartCoroutine(WaitThenPopulate(0.5f));
    }

    private IEnumerator WaitThenPopulate(float delay)
    {
        //yield return new WaitForSeconds(delay);
        yield return null; // a one frame delay is sufficient to know that every start has run

        //grab singleton Deck.Instance, start running commands (over a few frames maybe)
        //for each (rawcard)
        //  if .legal
        //      new card(rawcard.blah, .blah, .blah)
        //      Deck.Instance.AddCard(newCard)
    }
}
