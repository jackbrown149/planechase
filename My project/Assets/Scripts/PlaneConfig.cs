using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneConfig : MonoBehaviour
{
    [SerializeField]
    public Image cardImage, chaosSymbol;
    [SerializeField]
    public TMPro.TMP_Text title, desc, chaos;

    [Header("Deck and Cards")]
    [SerializeField]
    private Card currentPlane;
    private Card lastPlane;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Planeswalk()
    {

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
