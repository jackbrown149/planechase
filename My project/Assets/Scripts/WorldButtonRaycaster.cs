using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WorldButtonRaycaster : MonoBehaviour
{

    GraphicRaycaster raycaster;

    private void Start()
    {
        raycaster = GetComponent<GraphicRaycaster>();
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        PointerEventData pData = new PointerEventData(EventSystem.current);
        List<RaycastResult> results = new List<RaycastResult>();
        pData.position = Input.mousePosition;

        raycaster.Raycast(pData, results);

        foreach (RaycastResult result in results)
            if (results.Count > 0)
            {
                if (result.gameObject.TryGetComponent<WorldButton>(out WorldButton button))
                {
                    if (Vector3.Distance(result.gameObject.transform.position, PlayerMovement._main.transform.position) <= 5f) button.Clicked();
                }
            }
    }

}
