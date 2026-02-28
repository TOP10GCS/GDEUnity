using UnityEngine;

public class FakeVisualizer : MonoBehaviour
{
    [Header("UI Elements")]
    [Tooltip("Drag your UI Image bars here.")]
    public RectTransform[] uiBars;

    [Header("Fake Audio Settings")]
    public float baseHeight = 10f;      // The quietest/lowest the bars will go
    public float maxHeight = 100f;      // The loudest/tallest the bars will go
    public float speed = 8f;            // How fast the visualizer bounces
    public float waveSpacing = 1.2f;    // How out-of-sync adjacent bars are

    void Update()
    {
        for (int i = 0; i < uiBars.Length; i++)
        {
            if (uiBars[i] != null)
            {
                // Layer two sine waves running at different speeds to create "randomness"
                float wave1 = Mathf.Sin(Time.time * speed + (i * waveSpacing));
                float wave2 = Mathf.Sin(Time.time * (speed * 0.6f) + (i * waveSpacing * 1.5f));

                // Multiply them and use Mathf.Abs to keep the bounce positive (like volume)
                float fakeVolume = Mathf.Abs(wave1 * wave2);

                // Calculate the exact height for this frame
                float targetHeight = baseHeight + (fakeVolume * (maxHeight - baseHeight));

                // Apply the height directly to the UI bar
                Vector2 currentSize = uiBars[i].sizeDelta;
                currentSize.y = targetHeight;
                uiBars[i].sizeDelta = currentSize;
            }
        }
    }
}