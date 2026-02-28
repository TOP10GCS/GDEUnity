using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform player;

    public void Update()
    {
        this.gameObject.transform.LookAt(player);
    }
}
