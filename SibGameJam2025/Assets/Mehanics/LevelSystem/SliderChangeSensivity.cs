using SaveSystem;
using UnityEngine;
using UnityEngine.UI;

public class SliderChangeSensivity : MonoBehaviour
{
    private Slider slider;
    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void SetSensivity()
    {
        SaveData.sensivity = slider.value;
    }
}
