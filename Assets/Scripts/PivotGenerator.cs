using UnityEngine;
using System.Collections.Generic;

public class PivotGenerator : MonoBehaviour
{
    [Header("Parent GameObjects containing pivots")]
    [SerializeField] private GameObject parentObject1;
    [SerializeField] private GameObject parentObject2;

    [Header("Prefabs to spawn")]
    [SerializeField] private List<GameObject> prefabsToSpawn = new List<GameObject>();

    [Header("Prefab look target")]
    [SerializeField] private GameObject target;

    [Header("Options")]
    [SerializeField] private bool matchCapsuleSize = true;
    [SerializeField] private bool destroyPlaceholders = true;
    [SerializeField] [Range(0f, 1f)] private float spawnProbability = 1f;

    void Start()
    {
        SpawnPrefabsAtPivots();
    }

    private void SpawnPrefabsAtPivots()
    {
        if (prefabsToSpawn == null || prefabsToSpawn.Count == 0)
        {
            Debug.LogError("PivotGenerator: No prefabs assigned!");
            return;
        }

        // Process first parent
        if (parentObject1 != null)
        {
            ProcessParentObject(parentObject1);
        }
        else
        {
            Debug.LogWarning("PivotGenerator: Parent Object 1 is not assigned!");
        }

        // Process second parent
        if (parentObject2 != null)
        {
            ProcessParentObject(parentObject2);
        }
        else
        {
            Debug.LogWarning("PivotGenerator: Parent Object 2 is not assigned!");
        }
    }

    private void ProcessParentObject(GameObject parent)
    {
        // Iterate through all children (pivots)
        foreach (Transform pivot in parent.transform)
        {
            // Look for MeshFilter component on the pivot
            MeshFilter meshFilter = pivot.GetComponent<MeshFilter>();

            if (meshFilter != null && meshFilter.sharedMesh != null)
            {
                // Randomly decide whether to spawn at this pivot
                if (Random.value > spawnProbability)
                {
                    Debug.Log($"Skipped spawning at {pivot.name} (probability)");

                    // Still clean up placeholder
                    if (destroyPlaceholders)
                    {
                        Destroy(pivot.gameObject);
                    }
                    else
                    {
                        pivot.gameObject.SetActive(false);
                    }
                    continue;
                }

                // Get pivot transform data
                Vector3 position = pivot.position;
                Quaternion rotation = pivot.rotation;
                Vector3 scale = Vector3.one;

                // Try to match capsule size
                if (matchCapsuleSize)
                {
                    scale = GetCapsuleScale(meshFilter, pivot.localScale);
                }

                // Randomly select a prefab from the list
                GameObject selectedPrefab = prefabsToSpawn[Random.Range(0, prefabsToSpawn.Count)];

                // Instantiate prefab
                GameObject spawnedObject = Instantiate(selectedPrefab, position, rotation);
                spawnedObject.transform.localScale = scale;
                spawnedObject.transform.SetParent(parent.transform);

                // Make the spawned object look at the player
                if (target != null)
                {
                    spawnedObject.transform.LookAt(target.transform);
                }

                Debug.Log($"Spawned {selectedPrefab.name} at {pivot.name} with scale {scale}");

                // Optionally destroy or disable the placeholder
                if (destroyPlaceholders)
                {
                    Destroy(pivot.gameObject);
                }
                else
                {
                    pivot.gameObject.SetActive(false);
                }
            }
        }
    }

    private Vector3 GetCapsuleScale(MeshFilter meshFilter, Vector3 pivotLocalScale)
    {
        // Get the bounds of the capsule mesh
        Bounds bounds = meshFilter.sharedMesh.bounds;

        // Unity's default capsule is 2 units tall and 1 unit in diameter
        // Calculate the scale based on the pivot's local scale
        Vector3 capsuleSize = new Vector3(
            bounds.size.x * pivotLocalScale.x,
            bounds.size.y * pivotLocalScale.y,
            bounds.size.z * pivotLocalScale.z
        );

        return pivotLocalScale;
    }
}
