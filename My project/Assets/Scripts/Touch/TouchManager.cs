using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{
    private PlayerInput playerInput;

    private InputAction touchPosAction, touchPressAction;

    private Vector2 pwPos, settingsPos;
    [SerializeField]
    private float pwTolerance, settingsTolerance;

    private void Start()
    {
        playerInput= GetComponent<PlayerInput>();
        touchPosAction = playerInput.actions.FindAction("Position");
        touchPressAction = playerInput.actions.FindAction("Press");

        touchPressAction.performed += Press;

        StartCoroutine(WaitToAsk());
    }

    private IEnumerator WaitToAsk()
    {
        yield return new WaitForEndOfFrame();

        pwPos = GameManager.Instance.GetPWPos();
        pwTolerance = GameManager.Instance.GetPWTolerance();
    }

    private void Press(InputAction.CallbackContext context)
    {
        Vector2 pos = touchPosAction.ReadValue<Vector2>();
        //print(pos);


        if(Vector2.Distance(settingsPos, pos)<settingsTolerance)
        {
            //they pressed settings
        }
        else if(Vector2.Distance(pwPos, pos) < pwTolerance)
        {
            //the pressed the planeswalk symbol
            print("Pressed Planeswalk");
        }
        //in this order, if the touch is ambiguous read it as a settings press

    }
}
