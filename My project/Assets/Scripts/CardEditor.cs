using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CardEditor : MonoBehaviour
{

    [SerializeField] private GameObject cardPrefab;
    Transform scroller;

    [SerializeField]
    private InputField tbHeroName, tbHeroDesc, tbHeroHealth, tbHeroAttack, tbHeroShield;

    [SerializeField]
    private InputField tbDungeonName, tbDungeonDesc, tbDungeonHealth, tbDungeonAttack, tbDungeonShield;

    [SerializeField]
    private Dropdown typeDropdown;

    [SerializeField]
    private InputField spellName, spellDescription;

    [SerializeField]
    private InputField oublietteName, oublietteHealth, oublietteAttack, oublietteShield, amountField;

    [SerializeField]
    private Transform cardsSlider;

    //[SerializeField] private CardUpdator updator;

    /*
    private void Start()
    {
        scroller = GameObject.Find("Card Scroll Layout").transform;

        CardSaver.Init();

        CardInfo[] cards = CardManager.GetCards();

        float width = 0;

        for (int i = 0; i < cards.Length; i++)
        {
            CardInfo card = cards[i];

            GameObject spawned = Instantiate(cardPrefab, scroller);
            spawned.transform.localPosition = new Vector3((((RectTransform)spawned.transform).rect.width * i) + (7 * (i + 1)), -7, 0);
            width = ((RectTransform)spawned.transform).rect.width;
            CardUpdator updator = spawned.GetComponent<CardUpdator>();
            updator.cardInfo = card;
            updator.cardId = i;

            spawned.GetComponent<Button>().onClick.AddListener(() =>
            {
                LoadEditableCard(updator.cardInfo, updator.cardId);
            });
        }

        RectTransform tr = (RectTransform)scroller.transform;
        tr.sizeDelta = new Vector2((width * cards.Length) + (7 * (cards.Length + 1)), tr.sizeDelta.y);

        tbHeroName.onValueChanged.AddListener(a => activeEdit.heroName = a);
        tbHeroDesc.onValueChanged.AddListener(a => activeEdit.heroDescription = a);
        tbHeroHealth.onValueChanged.AddListener(a => {
            if (int.TryParse(a, out int i))
            {
                activeEdit.heroHealth = i;
            }
        });
        tbHeroAttack.onValueChanged.AddListener(a => {
            if (int.TryParse(a, out int i))
            {
                activeEdit.heroAttack = i;
            }
        });
        tbHeroShield.onValueChanged.AddListener(a => {
            if (int.TryParse(a, out int i))
            {
                activeEdit.heroShield = i;
            }
        });
        tbDungeonName.onValueChanged.AddListener(a => activeEdit.dungeonName = a);
        tbDungeonDesc.onValueChanged.AddListener(a => activeEdit.dungeonDescription = a);
        tbDungeonHealth.onValueChanged.AddListener(a => {
            if (int.TryParse(a, out int i))
            {
                activeEdit.dungeonHealth = i;
            }
        });
        tbDungeonAttack.onValueChanged.AddListener(a => {
            if (int.TryParse(a, out int i))
            {
                activeEdit.dungeonAttack = i;
            }
        });
        tbDungeonShield.onValueChanged.AddListener(a => {
            if (int.TryParse(a, out int i))
            {
                activeEdit.dungeonShield = i;
            }
        });
        spellName.onValueChanged.AddListener(a =>
        {
            activeEdit.spellName = a;
        });
        spellDescription.onValueChanged.AddListener(a =>
        {
            activeEdit.spellDescription = a;
        });
        oublietteName.onValueChanged.AddListener(a =>
        {
            activeEdit.heroName = a;
        });
        oublietteHealth.onValueChanged.AddListener(a => {
            if (int.TryParse(a, out int i))
            {
                activeEdit.heroHealth = i;
            }
        });
        oublietteAttack.onValueChanged.AddListener(a => {
            if (int.TryParse(a, out int i))
            {
                activeEdit.heroAttack = i;
            }
        });
        oublietteShield.onValueChanged.AddListener(a => {
            if (int.TryParse(a, out int i))
            {
                activeEdit.heroShield = i;
            }
        });

        typeDropdown.onValueChanged.AddListener(a =>
        {
            activeEdit.type = typeDropdown.options[a].text;

            tbHeroName.transform.parent.gameObject.SetActive(false);
            spellName.transform.parent.gameObject.SetActive(false);
            oublietteName.transform.parent.gameObject.SetActive(false);

            if (activeEdit.type.Equals("Dungeon"))
            {
                tbHeroName.transform.parent.gameObject.SetActive(true);
            }
            else if (activeEdit.type.Equals("Spell"))
            {
                spellName.transform.parent.gameObject.SetActive(true);
            }
            else
            {
                oublietteName.transform.parent.gameObject.SetActive(true);
            }

            LoadEditableCard(activeEdit, cardId);
        });

        amountField.onValueChanged.AddListener(a =>
        {
            if (int.TryParse(a, out int l))
            {
                activeEdit.amount = l;
            }
        });
    }
    

    private CardInfo activeEdit = new CardInfo();
    private int cardId = -1;

    private void Update()
    {
        updator.cardInfo = activeEdit;
    }

    private void LoadEditableCard(CardInfo info, int cardId)
    {
        activeEdit = info;
        this.cardId = cardId;
        updator.cardInfo = activeEdit;

        for (int i = 0; i < typeDropdown.options.Count; i++)
        {
            if (typeDropdown.options[i].text.Equals(activeEdit.type))
            {
                typeDropdown.value = i;
            }
        }

        tbHeroName.text = info.heroName;
        tbHeroDesc.text = info.heroDescription;
        tbHeroHealth.text = info.heroHealth + "";
        tbHeroAttack.text = info.heroAttack + "";
        tbHeroShield.text = info.heroShield + "";
        tbDungeonName.text = info.dungeonName;
        tbDungeonDesc.text = info.dungeonDescription;
        tbDungeonHealth.text = info.dungeonHealth + "";
        tbDungeonAttack.text = info.dungeonAttack + "";
        tbDungeonShield.text = info.dungeonShield + "";
        oublietteName.text = info.heroName;
        oublietteHealth.text = info.heroHealth + "";
        oublietteAttack.text = info.heroAttack + "";
        oublietteShield.text = info.heroShield + "";
        spellName.text = info.spellName;
        spellDescription.text = info.spellDescription;

        amountField.text = info.amount + "";
    }

    public void SaveEditableCard()
    {
        if (activeEdit.type.Equals("Dungeon"))
        {
            activeEdit.spellName = "";
            activeEdit.spellDescription = "";
        }
        else if (activeEdit.type.Equals("Spell"))
        {
            activeEdit.heroName = "";
            activeEdit.heroDescription = "";
            activeEdit.heroHealth = 0;
            activeEdit.heroAttack = 0;
            activeEdit.heroShield = 0;
            activeEdit.dungeonName = "";
            activeEdit.dungeonDescription = "";
            activeEdit.dungeonHealth = 0;
            activeEdit.dungeonAttack = 0;
            activeEdit.dungeonShield = 0;
        }
        else
        {
            activeEdit.spellName = "";
            activeEdit.spellDescription = "";
            activeEdit.heroDescription = "";
            activeEdit.dungeonName = "";
            activeEdit.dungeonDescription = "";
            activeEdit.dungeonHealth = 0;
            activeEdit.dungeonAttack = 0;
            activeEdit.dungeonShield = 0;
        }

        if (cardId == -1)
        {
            CardManager.NewCard(activeEdit);
            cardId = CardManager.GetCards().Length;

            GameObject spawned = Instantiate(cardPrefab, scroller);
            spawned.transform.localPosition = new Vector3((((RectTransform)spawned.transform).rect.width * (cardsSlider.childCount - 1)) + (7 * ((cardsSlider.childCount - 1) + 1)), -7, 0);

            CardUpdator updator = spawned.GetComponent<CardUpdator>();
            updator.cardInfo = activeEdit;
            updator.cardId = cardsSlider.childCount - 1;

            spawned.GetComponent<Button>().onClick.AddListener(() =>
            {
                LoadEditableCard(updator.cardInfo, updator.cardId);
            });

            RectTransform tr = (RectTransform)scroller.transform;
            tr.sizeDelta = new Vector2((((RectTransform)spawned.transform).rect.width * cardsSlider.childCount) + (7 * (cardsSlider.childCount + 1)), tr.sizeDelta.y);
        }
        else CardManager.SetCard(cardId, activeEdit);

        LoadEditableCard(activeEdit, cardId);

        CardSaver.Save();
    }

    public void NewCard()
    {
        cardId = -1;
        activeEdit = new CardInfo();
        activeEdit.type = typeDropdown.options[typeDropdown.value].text;
        tbHeroName.text = "";
        tbHeroDesc.text = "";
        tbHeroHealth.text = "";
        tbHeroAttack.text = "";
        tbHeroShield.text = "";
        tbDungeonName.text = "";
        tbDungeonDesc.text = "";
        tbDungeonHealth.text = "";
        tbDungeonAttack.text = "";
        tbDungeonShield.text = "";
        oublietteName.text = "";
        oublietteHealth.text = "";
        oublietteAttack.text = "";
        oublietteShield.text = "";
        spellName.text = "";
        spellDescription.text = "";
        amountField.text = "";

        LoadEditableCard(activeEdit, cardId);
    }

    public void Revert()
    {
        if (cardId == -1) return;
        activeEdit = CardManager.GetInfo(cardId);

        LoadEditableCard(activeEdit, cardId);
    }

    public void DeleteCard()
    {
        if (cardId != -1)
        {
            float width = 0f;
            for (int i = 0; i < cardsSlider.childCount; i++)
            {
                if (i == cardId)
                {
                    Destroy(cardsSlider.GetChild(i).gameObject);
                }
                else if (i > cardId)
                {
                    CardUpdator c = cardsSlider.GetChild(i).GetComponent<CardUpdator>();
                    c.cardId--;
                    c.transform.localPosition = new Vector3((((RectTransform)c.transform).rect.width * c.cardId) + (7 * (c.cardId + 1)), -7, 0);
                    width = ((RectTransform)c.transform).rect.width;
                }
            }

            RectTransform tr = (RectTransform)scroller.transform;
            tr.sizeDelta = new Vector2((width * cardsSlider.childCount) + (7 * (cardsSlider.childCount + 1)), tr.sizeDelta.y);

            CardManager.Delete(cardId);

            CardSaver.Save();

            NewCard();
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    */

}
