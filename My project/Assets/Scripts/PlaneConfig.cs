using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneConfig : MonoBehaviour
{
    [SerializeField]
    public Image cardImage, pwSymbol, chaosSymbol;
    [SerializeField]
    public TMPro.TMP_Text title, desc, chaos;

    [Header("Deck and Cards")]
    [SerializeField]
    private Card currentPlane;
    private Card lastPlane;

    // Start is called before the first frame update
    void Start()
    {
        print("Planeswalk Symbol position reads as "+pwSymbol.rectTransform.position);

        GameManager.Instance.SetPWPos(
            new Vector2(pwSymbol.rectTransform.position.x, pwSymbol.rectTransform.position.y));
        GameManager.Instance.SetSettingsPos(
            new Vector2(-999999,-999999)); //for now, but if we add a settings button we'll need this
        GameManager.Instance.SetPWTolerance(80);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Planeswalk()
    {
        //
        //currentPlane = deck.DrawTopCard()
    }


    float time;
    private IEnumerator ChaosEnsues()
    {
        time = 0;
        while (time<5f)
        {
            chaosSymbol.gameObject.transform.localScale = new Vector3(1,1,1) *(1 + (time / 5f * 0.1f));
            time+= Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(5f);

    }
}
