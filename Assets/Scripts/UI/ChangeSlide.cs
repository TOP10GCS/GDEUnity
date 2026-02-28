using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeSlide : MonoBehaviour
{
    public Material boardMat;

    public void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            Debug.Log("One pressed");
            GenerateRandomHueColor();
        }
    }
    public void GenerateRandomHueColor()
    {
        Color random = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
        boardMat.color = random;
        boardMat.SetVector("_EmissionColor", new Vector4(random.r, random.g, random.b) * 0.1f);
    }
}
