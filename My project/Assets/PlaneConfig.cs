using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneConfig : MonoBehaviour
{
    public Image cardImage, chaosSymbol;
    public TMPro.TMP_Text title, desc, chaos;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChaosEnsues());
    }

    // Update is called once per frame
    void Update()
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
