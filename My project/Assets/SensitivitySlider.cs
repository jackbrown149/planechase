using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensitivitySlider : MonoBehaviour
{

    [SerializeField] private Text senseText;

    private void Start()
    {
        GetComponent<Slider>().value = Settings.sensitivity;
        senseText.text = "Sensitivity: " + Settings.sensitivity.ToString("0.00");
    }

    public void Changed(float a)
    {
        Settings.sensitivity = a;
        senseText.text = "Sensitivity: " + Settings.sensitivity.ToString("0.00");
    }

}
