using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.TimeZoneInfo;

public class PlaneConfig : MonoBehaviour
{
    [SerializeField]
    private Image cardImage, pwSymbol, chaosSymbol, outerBG;
    [SerializeField]
    private TMPro.TMP_Text title, subtitle, desc, chaos;

    [SerializeField]
    private Sprite[] planeBG;

    [Header("Deck and Cards")]
    [SerializeField]
    private Card currentPlane;
    private Card prevPlane;

    // Start is called before the first frame update
    void Start()
    {
        print("Planeswalk Symbol position reads as "+pwSymbol.rectTransform.position);

        GameManager.Instance.SetPWPos(
            new Vector2(pwSymbol.rectTransform.position.x, pwSymbol.rectTransform.position.y));
        GameManager.Instance.SetSettingsPos(
            new Vector2(-999999,-999999)); //for now, but if we add a settings button we'll need this
        GameManager.Instance.SetPWTolerance(80);

        canPlaneswalk = false;

        StartCoroutine(RequestDeckAfterDelay(3));
    }

    private IEnumerator RequestDeckAfterDelay(int delay)
    {
        for(int i = 0; i<delay; i++)
        {
            yield return null;
        }
        currentPlane = Deck.Instance.DrawTopCard();
        DisplayCard();

        StartCoroutine(CloudsOut(planeTransitionTime));
        yield return new WaitForSeconds(planeTransitionTime + 0.2f);
        //brief uninteractable delay after clouds roll out
        canPlaneswalk = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool canPlaneswalk;
    public void Planeswalk()
    {
        if (!canPlaneswalk) return;

        canPlaneswalk = false;
        StartCoroutine(PlaneswalkTransition(planeTransitionTime));
        //

    }

    private void DisplayCard()
    {
        title.text = currentPlane.title;
        subtitle.text = currentPlane.subtitle;
        cardImage.sprite = planeBG[currentPlane.imageID];
        desc.text = currentPlane.planeText;
        chaos.text= currentPlane.chaosText;

        outerBG.sprite = cardImage.sprite;
    }

    private IEnumerator PlaneswalkTransition(float transitionTime)
    {
        //bring in clouds
        StartCoroutine(CloudsIn(transitionTime / 2f));

        prevPlane = currentPlane;

        yield return new WaitForSeconds(transitionTime);
        
        currentPlane = Deck.Instance.DrawTopCard();
        DisplayCard();

        //send away clouds;
        StartCoroutine(CloudsOut(transitionTime / 2f));

        yield return new WaitForSeconds(transitionTime/2f + 0.1f);
        //brief uninteractable delay after clouds roll out
        canPlaneswalk = true;
        print("finished card transition");
        Deck.Instance.PutOnBottom(prevPlane);
    }

    [Header("PlaneTransitionVariables")]
    [SerializeField]
    private float planeTransitionTime;
    private float cloudTimer;
    [SerializeField]
    private RectTransform cloudBottom, cloud1, cloud2, cloud3;
    [SerializeField]
    private Vector2 cloudBottomStart, cloudBottomEnd,
        cloud1Start, cloud1End, cloud2Start, cloud2End, cloud3Start, cloud3End;
    private IEnumerator CloudsIn(float animDuration)
    {
        cloudTimer = 0;
        Vector2 temp = Vector2.zero;

        while (cloudTimer < animDuration)
        {
            temp = Vector2.Lerp(cloudBottomStart, cloudBottomEnd, cloudTimer / animDuration);
            cloudBottom.localPosition = temp;

            temp = Vector2.Lerp(cloud1Start, cloud1End, cloudTimer / animDuration);
            cloud1.localPosition = temp;

            temp = Vector2.Lerp(cloud2Start, cloud2End, cloudTimer / animDuration);
            cloud2.localPosition = temp;

            temp = Vector2.Lerp(cloud3Start, cloud3End, cloudTimer / animDuration);
            cloud3.localPosition = temp;


            cloudTimer += Time.deltaTime;
            yield return null;
        }
    }
    private IEnumerator CloudsOut(float animDuration) //copy CloudsIn in a bit
    {
        cloudTimer = 0;
        Vector2 temp = Vector2.zero;

        while (cloudTimer < animDuration)
        {
            temp = Vector2.Lerp(cloudBottomEnd, cloudBottomStart, cloudTimer / animDuration);
            cloudBottom.localPosition = temp;

            temp = Vector2.Lerp(cloud1End, cloud1Start, cloudTimer / animDuration);
            cloud1.localPosition = temp;

            temp = Vector2.Lerp(cloud2End, cloud2Start, cloudTimer / animDuration);
            cloud2.localPosition = temp;

            temp = Vector2.Lerp(cloud3End, cloud3Start, cloudTimer / animDuration);
            cloud3.localPosition = temp;


            cloudTimer += Time.deltaTime;
            yield return null;
        }
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
