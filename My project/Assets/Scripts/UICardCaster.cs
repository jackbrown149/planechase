using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICardCaster : MonoBehaviour
{
    
    private GraphicRaycaster cast;

    private void Start()
    {
        cast = GetComponent<GraphicRaycaster>();
    }

    private void Update()
    {
        //if (!Input.GetMouseButtonDown(0)) return;
        PointerEventData pData = new PointerEventData(EventSystem.current);
        List<RaycastResult> results = new List<RaycastResult>();
        pData.position = Input.mousePosition;

        cast.Raycast(pData, results);

        foreach (RaycastResult result in results)
            if (results.Count > 0)
            {
                if (result.gameObject.TryGetComponent(out UICard card))
                {
                    card.hovering = true;
                    if (Input.GetMouseButtonDown(0)) card.Clicked();
                    if (Input.GetMouseButton(1)) card.flip = true;
                    return;
                }
            }
    }

}
