using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHand : MonoBehaviour
{

    public GameObject prefab;

    public bool shown = false;

    private Vector3 startPos;
    private Vector3 oddPos;

    private void Start()
    {
        GameObject o = transform.GetChild(0).gameObject;
        startPos = o.transform.localPosition;
        oddPos = o.transform.position;

        //for (int i = 0; i < 7; i++)
        //{
        //    UICard c = Instantiate(prefab, o.transform.position, Quaternion.identity, transform).GetComponent<UICard>();

        //    c.hand = this;
        //}

        Destroy(o);

        UpdateCards();
    }

    public void AddCard(int id)
    {
        UICard c = Instantiate(prefab, oddPos, Quaternion.identity, transform).GetComponent<UICard>();

        c.hand = this;

        c.SetId(id);

        UpdateCards();
    }

    private void Update()
    {
        shown = Input.GetKey(KeyCode.LeftAlt);
        if (shown)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 195, 0), Time.deltaTime * 4.5f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, -200, 0), Time.deltaTime * 4.5f);
        }
    }

    private UICard hoverTarget;
    public void SetHoverTarget(UICard target)
    {
        if (hoverTarget != null)
        {
            hoverTarget.CancelHover();
        }
        hoverTarget = target;

        bool found = false;
        UpdateCards();

        if (target == null) return;
        for (int i = 0; i < transform.childCount; i++)
        {
            UICard card = transform.GetChild(i).GetComponent<UICard>();
            if (card == target)
            {
                found = true;
                continue;
            }

            if (!found) card.targetPosition -= new Vector3(300, 0, 0);
            else card.targetPosition += new Vector3(300, 0, 0);
        }
    }

    public float spacing = 10f;

    public void UpdateCards()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            RectTransform t = ((RectTransform)transform);
            RectTransform tr = (RectTransform) t.GetChild(i);
            tr.GetComponent<UICard>().targetPosition = new Vector3((tr.rect.width * i) + (spacing * i) - (((transform.childCount-1) * (tr.rect.width+ spacing))/2), startPos.y, 0);
        }
    }

}
