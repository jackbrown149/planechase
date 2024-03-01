using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //private List<RawCard> cards; // this, extra card type that contains card plus bool for inclusion
                                   // or card carries a mostly useless bool "included" 

    public static GameManager Instance;
    // Start is called before the first frame update
    void Awake()
    {
        //Singleton
        if(Instance == null)
        {
            Instance = this;
            print("gained sentience...");
        }
        else
        {
            print("found out I was a clone and killed myself");
            Destroy(this);
        }

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


    //input handler - touch facilitation
    [Header("Touch Input Facilitation Data")]
    [SerializeField]
    private Vector2 pwPos;
    [SerializeField]
    private Vector2 settingsPos;
    [SerializeField]
    private float pwTolerance, settingsTolerance;

    public Vector2 GetPWPos()
    {
        return pwPos;
    }

    public void SetPWPos(Vector2 pos)
    {
        pwPos = pos;
    }

    public Vector2 GetSettingsPos()
    {
        return settingsPos;
    }
    public void SetSettingsPos(Vector2 pos)
    {
        settingsPos = pos;
    }

    public float GetPWTolerance()
    {
        return pwTolerance;
    }
    public void SetPWTolerance(float tol)
    {
        pwTolerance = tol;
    }

    public float GetSettingsTolerance()
    {
        return settingsTolerance;
    }
    public void SetSettingsTolerance(float tol)
    {
        settingsTolerance = tol;
    }

}
