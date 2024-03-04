using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldTabber : MonoBehaviour
{

    private InputField input;

    private void Start()
    {
        input = GetComponent<InputField>();
    }

    private void Update()
    {
        if (!input.isFocused) return;
        if (Input.GetKeyDown(KeyCode.Tab) && !Input.GetKey(KeyCode.LeftShift))
        {
            if (input.navigation.selectOnRight != null) input.navigation.selectOnRight.Select();
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
        {
            if (input.navigation.selectOnLeft != null) input.navigation.selectOnLeft.Select();
        }
        input.text.Replace("\t", "");
    }

}
