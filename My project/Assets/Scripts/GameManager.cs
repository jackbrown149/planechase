using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CsvHelper;
using UnityEngine.SceneManagement;

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
            print("GameManager established");
        }
        else
        {
            print("Duplicate GameManager culled");
            Destroy(this);
        }

        //FetchAllCards();

        StartCoroutine(WaitThenPopulate(0.5f));
    }

    private IEnumerator WaitThenPopulate(float delay)
    {
        //yield return new WaitForSeconds(delay);
        yield return null; // a one frame delay is sufficient to know that every start has run

        //fetch all cards from the deck list and add them to our Deck instance
        using (var reader = new System.IO.StreamReader("Assets/Scripts/DefaultDeck.csv"))
        using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
        {
            Card newCard;
            string newTitle;
            string newSubtitle;
            byte newImageID;
            string newPlaneText;
            string newChaosText;

            csv.Read();
            csv.ReadHeader();
            while (csv.Read())
            {
                newTitle = csv.GetField<string>("title");
                newSubtitle = csv.GetField<string>("subtitle");
                newImageID = csv.GetField<byte>("imageID");
                newPlaneText = csv.GetField<string>("planeText");
                newChaosText = csv.GetField<string>("chaosText");
                
                newCard = new Card(newTitle, newSubtitle, newImageID, newPlaneText, newChaosText);
                Deck.Instance.PutOnBottom(newCard);
            }
        }

        Deck.Instance.Shuffle();
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

    public void GoToEditor()
    {
        SceneManager.LoadScene(1);
    }

}
