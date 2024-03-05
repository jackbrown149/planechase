using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        string newFileName = "./Assets/Scripts/DefaultDeck.csv";

        string planeDetails = "\n"+ newPlane.ToString();

        if (!File.Exists(newFileName))
        {
            string clientHeader = "Client Name(ie. Billto_desc)" + "," + "Mid_id,billing number(ie billto_id)" + "," + "business unit id" + Environment.NewLine;

            File.WriteAllText(newFileName, clientHeader);
        }

        File.AppendAllText(newFileName, planeDetails);
        


        
        //List<Card> newPlaneRecord = new List<Card>
        //{
        //    newPlane,
        //};
        //var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        //{
        //    HasHeaderRecord = false,
        //};

        //using (var stream = File.Open("DefaultDeck.csv", FileMode.Append))
        //using (var writer = new StreamWriter(stream))
        //using (var csv = new CsvWriter(writer, config))
        //{
        //    csv.WriteRecords(newPlaneRecord.AsEnumerable());
        //    //csv.WriteField(newPlane.ToString(), false);
        //}
    }

    public void ReturnToGame()
    {
        SceneManager.LoadScene(0);
    }
    
}
