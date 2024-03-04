using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneMaker : MonoBehaviour
{
    [SerializeField]
    private Image cardImage, outerBG, nextImageButton, saveButton;
    [SerializeField]
    private TMPro.TMP_InputField title, subtitle, desc, chaos;

    [SerializeField]
    private Sprite[] planeBG;
    private byte imageID;

    [Header("Deck and Cards")]
    [SerializeField]
    private Card newPlane;


    private void Start()
    {
        imageID = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CycleImage()
    {
        imageID++;
        if (imageID >= planeBG.Length) imageID = 0;

        cardImage.sprite = planeBG[imageID];
    }
    

    public void SaveCard()
    {
        newPlane = new Card(title.text, subtitle.text, imageID, desc.text, chaos.text);
        using (var stream = File.Open("DefaultDeck.csv", FileMode.Append))
        using (var writer = new StreamWriter(stream))
        using (var csv = new CsvWriter(writer, config))
        {
            csv.WriteRecords(newPlane);
        }
    }
    
}
