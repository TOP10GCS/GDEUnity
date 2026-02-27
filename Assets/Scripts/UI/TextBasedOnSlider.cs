using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextBasedOnSlider : MonoBehaviour
{
    public TMP_Text text;
    public Slider slider;
    
    public void UpdateText()
    {
        Debug.Log(slider.value);
        if (slider.value >= 0f && slider.value < 25f)
        {
            Debug.Log("in");
            text.text = "Casual";
        }
        else if(slider.value >= 25 && slider.value < 50)
        {
            text.text = "Semi-formal";
        }
        else if(slider.value >= 50f && slider.value < 75f)
        {
            text.text = "Formal";
        }
        else if(slider.value >= 75f && slider.value <= 100f)
        {
            text.text = "High-stakes";
        }
    }
}
