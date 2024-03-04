using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class CardUpdator : MonoBehaviour
{

    public CardInfo cardInfo;
    public int cardId = -1;

    private int lastCardId = -1;

    public bool hideZeroes = false;

    private TextMeshProUGUI[] texts;

    [SerializeField]
    private Sprite dungeon, oubliette, spell;

    private Image image;

    [ExecuteAlways]
    private void Awake()
    {
        texts = new TextMeshProUGUI[transform.childCount];

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i] = transform.GetChild(i).GetComponent<TextMeshProUGUI>();
        }

        image = GetComponent<Image>();
    }

    private bool first = true;

    [ExecuteAlways]
    private void Update()
    {
        if (first)
        {
            first = false;
            return;
        }
        if (!hideZeroes) cardInfo = CardManager.GetInfo(cardId);
        if (lastCardId != cardId)
        {
            lastCardId = cardId;
            cardInfo = CardManager.GetInfo(cardId);
        }

        if (image != null)
        {
            if (cardInfo.type == null) cardInfo.type = "Dungeon";
            if (cardInfo.type.Equals("Dungeon"))
            {
                image.sprite = dungeon;
            }
            else if (cardInfo.type.Equals("Spell"))
            {
                image.sprite = spell;
            }
            else
            {
                image.sprite = oubliette;
            }
        }

        if (cardInfo.type.Equals("Spell"))
        {
            texts[0].text = cardInfo.spellName;
        }
        else
        {
            texts[0].text = cardInfo.heroName;
        }

        if (cardInfo.type.Equals("Dungeon")) texts[1].enabled = true;
        else texts[1].enabled = false;
        texts[1].text = cardInfo.heroDescription;

        if ((cardInfo.heroHealth == 0 && hideZeroes) || cardInfo.type.Equals("Spell") || cardInfo.type.Equals("Oubliette")) texts[2].enabled = false;
        else texts[2].enabled = true;
        texts[2].text = cardInfo.heroHealth + "";

        if ((cardInfo.heroAttack == 0 && hideZeroes) || cardInfo.type.Equals("Spell") || cardInfo.type.Equals("Oubliette")) texts[3].enabled = false;
        else texts[3].enabled = true;
        texts[3].text = cardInfo.heroAttack + "";

        if ((cardInfo.heroShield == 0 && hideZeroes) || cardInfo.type.Equals("Spell") || cardInfo.type.Equals("Oubliette")) texts[4].enabled = false;
        else texts[4].enabled = true;
        texts[4].text = cardInfo.heroShield + "";

        if (cardInfo.type.Equals("Dungeon"))
        {
            texts[5].enabled = true;
            texts[6].enabled = true;
        }
        else
        {
            texts[5].enabled = false;
            texts[6].enabled = false;
        }
        texts[5].text = cardInfo.dungeonName;
        texts[6].text = cardInfo.dungeonDescription;

        if ((cardInfo.dungeonHealth == 0 && hideZeroes) || cardInfo.type.Equals("Spell") || cardInfo.type.Equals("Oubliette")) texts[7].enabled = false;
        else texts[7].enabled = true;
        texts[7].text = cardInfo.dungeonHealth + "";

        if ((cardInfo.dungeonAttack == 0 && hideZeroes) || cardInfo.type.Equals("Spell") || cardInfo.type.Equals("Oubliette")) texts[8].enabled = false;
        else texts[8].enabled = true;
        texts[8].text = cardInfo.dungeonAttack + "";

        if ((cardInfo.dungeonShield == 0 && hideZeroes) || cardInfo.type.Equals("Spell") || cardInfo.type.Equals("Oubliette")) texts[9].enabled = false;
        else texts[9].enabled = true;
        texts[9].text = cardInfo.dungeonShield + "";

        // Oubliette

        if ((cardInfo.heroHealth == 0 && hideZeroes) || cardInfo.type.Equals("Spell") || cardInfo.type.Equals("Dungeon")) texts[10].enabled = false;
        else texts[10].enabled = true;
        texts[10].text = cardInfo.heroHealth + "";

        if ((cardInfo.heroAttack == 0 && hideZeroes) || cardInfo.type.Equals("Spell") || cardInfo.type.Equals("Dungeon")) texts[11].enabled = false;
        else texts[11].enabled = true;
        texts[11].text = cardInfo.heroAttack + "";

        if ((cardInfo.heroShield == 0 && hideZeroes) || cardInfo.type.Equals("Spell") || cardInfo.type.Equals("Dungeon")) texts[12].enabled = false;
        else texts[12].enabled = true;
        texts[12].text = cardInfo.heroShield + "";

        if (cardInfo.type.Equals("Spell")) texts[13].enabled = true;
        else texts[13].enabled = false;
        texts[13].text = cardInfo.spellDescription;
    }

    public void UpdateID(int id)
    {
        cardId = id;
        cardInfo = CardManager.GetInfo(id);
    }

}
