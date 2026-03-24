using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public GameObject platform;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(platform, 2f);
        }
    }
}
