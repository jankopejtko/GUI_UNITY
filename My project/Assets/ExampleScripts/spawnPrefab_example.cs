using UnityEngine;

public class spawnPrefab_example : MonoBehaviour
{
    public Transform pivot;
    [SerializeField] private GameObject Prefab;
    private GameObject spawnedPrefab;

    //todo add timer for despawning the prefab after a certain amount of time
    private float despawnTimer = 15f; //15 seconds
    //todo add timer for despawning the prefab after a certain amount of time

    public void SpawnPrefab()
    {
        spawnedPrefab = Instantiate(Prefab, pivot.position, pivot.rotation);
        spawnedPrefab.tag = "spawnedPrefab"; //tag the spawned prefab so we can find it later for despawning
    }

    //despawn object after timer runs out
    private void Update()
    {
        Destroy(spawnedPrefab, despawnTimer);
    }
    //despawn object after timer runs out

    //todo despawn all spawned prefabs method
    public void DespawnAllPrefabs()
    {
        GameObject[] prefabs = GameObject.FindGameObjectsWithTag("spawnedPrefab");
        foreach (GameObject prefab in prefabs)
        {
            Destroy(prefab);
        }
    }
    //todo despawn all spawned prefabs method
}
