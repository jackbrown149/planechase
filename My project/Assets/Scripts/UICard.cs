using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICard : MonoBehaviour
{

    public Vector3 targetPosition = Vector3.zero;

    public UIHand hand;

    public bool hovering = false;
    public bool flip = false;

    [SerializeField] private CardUpdator updator;

    private int cardId = -1;

    private bool beenHovering = false;
    private int index;

    public void SetId(int id)
    {
        cardId = id;
        updator.UpdateID(id);
    }

    public void CancelHover()
    {
        beenHovering = false;
        transform.SetSiblingIndex(index);
    }

    private void Update()
    {
        if (hovering)
        {
            if (!beenHovering)
            {
                hand.SetHoverTarget(this);
                beenHovering = true;
                index = transform.GetSiblingIndex();
                //transform.SetSiblingIndex(transform.parent.childCount - 1);
            }
        }
        else
        {
            if (beenHovering)
            {
                beenHovering = false;
                //transform.SetSiblingIndex(index);
                hand.SetHoverTarget(null);
            }
        }
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition + (hovering ? new Vector3(0, 100, 0) : Vector3.zero), Time.deltaTime * 3f);
        hovering = false;

        transform.eulerAngles = new Vector3(0, 0, (flip ? 180 : 0));
        flip = false;
    }

    public void Clicked()
    {
        transform.parent = null;
        hand.UpdateCards();
        RaycastHit hit;

        if (Physics.Raycast(new Ray(Camera.main.transform.position, Camera.main.transform.forward), out hit, 10f))
        {
            CardManager.SpawnFromID(cardId, hit.point, Quaternion.LookRotation(hit.normal, PlayerMovement._main.transform.forward));
        }
        else
        {
            CardManager.SpawnFromID(cardId, Camera.main.transform.position + (Camera.main.transform.forward * 4), Quaternion.LookRotation(Camera.main.transform.forward));
        }
        Destroy(gameObject);
    }
}
