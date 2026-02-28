using UnityEngine;
using UnityEngine.UI; // Required for interacting with Unity UI

[RequireComponent(typeof(AudioSource))]
public class UMicVisualizer : MonoBehaviour
{
    [Header("UI Elements")]
    [Tooltip("Drag your UI Image bars here.")]
    public RectTransform[] uiBars;

    [Header("Visualizer Settings")]
    public float heightMultiplier = 500f; // How tall the UI bars get
    public float minHeight = 5f;          // So the bars never completely disappear
    public float lerpSpeed = 15f;         // How smooth the movement is

    private AudioSource audioSource;
    // We only need a small sample size for a simple UI
    private float[] spectrumData = new float[64];

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Setup the Microphone
        if (Microphone.devices.Length > 0)
        {
            string defaultMic = Microphone.devices[0];
            Debug.Log("Találj meg.");
            Debug.Log(defaultMic);
            audioSource.clip = Microphone.Start(defaultMic, true, 10, AudioSettings.outputSampleRate);
            audioSource.loop = true;

            while (!(Microphone.GetPosition(defaultMic) > 0)) { }
            audioSource.Play();
        }
        else
        {
            Debug.LogError("No microphone detected!");
        }
    }

    void Update()
    {
        
        // Get the audio spectrum data
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);
        Debug.Log("Band 0 Data: " + spectrumData[0]);
        // Loop through however many UI bars you assigned in the inspector
        for (int i = 0; i < uiBars.Length; i++)
        {
            if (uiBars[i] != null)
            {
                // Grab the current UI size
                Vector2 currentSize = uiBars[i].sizeDelta;

                // Calculate the new height (we use the first few frequencies for the UI)
                float targetHeight = (spectrumData[i] * heightMultiplier) + minHeight;

                // Smoothly interpolate the height
                currentSize.y = Mathf.Lerp(currentSize.y, targetHeight, Time.deltaTime * lerpSpeed);

                // Apply it back to the UI element
                uiBars[i].sizeDelta = currentSize;
            }
        }
    }
}