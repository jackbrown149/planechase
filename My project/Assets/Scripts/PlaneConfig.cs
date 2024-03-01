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
    private Card prevPlane;
    private Deck deck;

    // Start is called before the first frame update
    void Start()
    {
        print("Planeswalk Symbol position reads as "+pwSymbol.rectTransform.position);

        GameManager.Instance.SetPWPos(
            new Vector2(pwSymbol.rectTransform.position.x, pwSymbol.rectTransform.position.y));
        GameManager.Instance.SetSettingsPos(
            new Vector2(-999999,-999999)); //for now, but if we add a settings button we'll need this
        GameManager.Instance.SetPWTolerance(80);

        canPlaneswalk = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool canPlaneswalk;
    public void Planeswalk()
    {
        canPlaneswalk = false;
        //StartCoroutine(PlaneswalkTransition(planeTransitionTime));
        //
    }

    private IEnumerator PlaneswalkTransition(float transitionTime)
    {
        //bring in clouds
        StartCoroutine(CloudsIn(transitionTime / 2f));

        prevPlane = currentPlane;

        yield return new WaitForSeconds(transitionTime);
        
        currentPlane = deck.DrawTopCard();

        //send away clouds;
        StartCoroutine(CloudsOut(transitionTime / 2f));

        yield return new WaitForSeconds(transitionTime/2f + 0.1f);
        //brief uninteractable delay after clouds roll out
        canPlaneswalk = true;
    }

    [Header("PlaneTransitionVariables")]
    [SerializeField]
    private float planeTransitionTime;
    private float cloudTimer;
    [SerializeField]
    private GameObject cloudBottom, cloud1, cloud2, cloud3;
    [SerializeField]
    private Vector2 cloudBottomStart, cloudBottomEnd,
        cloud1Start, cloud1End, cloud2Start, cloud2End, cloud3Start, cloud3End;
    private IEnumerator CloudsIn(float animDuration)
    {
        cloudTimer = 0;
        while(cloudTimer < animDuration)
        {
            cloudTimer+= Time.deltaTime;
            yield return null;
        }
    }
    private IEnumerator CloudsOut(float animDuration) //copy CloudsIn in a bit
    {
        yield return null;
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
