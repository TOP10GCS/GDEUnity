using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private bool lockVertical = false;
    [SerializeField] private float rotationSpeed = 5f;

    void Update()
    {
        if (target != null)
        {
            LookAtTargetHorizontally();
        }
    }

    private void LookAtTargetHorizontally()
    {
        Vector3 direction = target.position - transform.position;

        if (lockVertical)
        {
            direction.y = 0;
        }

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
