using UnityEngine;

public class PivotPointSpawner : MonoBehaviour
{
    public Vector3 offCol;
    public Vector3 offRow;
    public int rowCount, colCount;
    public Vector3 startPoint;
    public GameObject pivot;
    public GameObject neigbourrow;
    public GameObject neigbourcol;
    public float curvature = 0.5f; // Controls how much each row curves
    public int chairDifferencePerRow = 1; // How many chairs differ between rows (-1 decreases, +1 increases)
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offCol = startPoint-neigbourcol.transform.position;
        offRow = startPoint - neigbourrow.transform.position;
        for (int i = 0; i < rowCount; i++)
        {
            int chairsInThisRow = colCount + (i * chairDifferencePerRow);
            
            for (int j = 0; j < chairsInThisRow; j++)
            {
                // Calculate base position
                Vector3 position = startPoint + (offRow * i);

                // Center the chairs in the row

                // Apply column offset
                position += offCol;
  
                
                Instantiate(pivot, position, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
