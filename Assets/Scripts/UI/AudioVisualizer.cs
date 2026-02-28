using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioVisualizer : MonoBehaviour
{
    [Header("Visualizer Settings")]
    public int numberOfSamples = 64; // Must be a power of 2 (64, 128, 256, etc.)
    public GameObject visualizerPrefab;
    public float spacing = 1.2f;
    public float heightMultiplier = 100f;
    public float lerpSpeed = 10f;

    private AudioSource audioSource;
    private float[] spectrumData;
    private GameObject[] visualizerBars;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spectrumData = new float[numberOfSamples];
        visualizerBars = new GameObject[numberOfSamples];

        // 1. Spawn the visualizer bars
        for (int i = 0; i < numberOfSamples; i++)
        {
            GameObject bar = Instantiate(visualizerPrefab, transform);
            bar.transform.localPosition = new Vector3(i * spacing, 0, 0);
            visualizerBars[i] = bar;
        }

        // 2. Setup the Microphone
        if (Microphone.devices.Length > 0)
        {
            string defaultMic = Microphone.devices[0];
            // Start recording: mic name, loop, length in seconds, sample rate
            audioSource.clip = Microphone.Start(defaultMic, true, 10, AudioSettings.outputSampleRate);
            audioSource.loop = true;

            // Wait until the microphone actually starts recording before playing
            while (!(Microphone.GetPosition(defaultMic) > 0)) { }
            audioSource.Play();
        }
        else
        {
            Debug.LogError("No microphone detected! Please plug one in.");
        }
    }

    void Update()
    {
        // 3. Analyze the audio spectrum (FFT)
        // BlackmanHarris is a windowing type that reduces audio artifacts
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);

        // 4. Apply the data to the visualizer bars
        for (int i = 0; i < numberOfSamples; i++)
        {
            if (visualizerBars[i] != null)
            {
                // Get the current scale
                Vector3 currentScale = visualizerBars[i].transform.localScale;

                // Calculate the target scale based on the audio frequency (add 1 so it never scales to 0)
                float targetY = (spectrumData[i] * heightMultiplier) + 1f;

                // Smoothly transition to the new scale using Lerp
                currentScale.y = Mathf.Lerp(currentScale.y, targetY, Time.deltaTime * lerpSpeed);

                visualizerBars[i].transform.localScale = currentScale;
            }
        }
    }
}
