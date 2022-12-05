using UnityEngine;

public class RandomProteinSpawner : MonoBehaviour
{
    [SerializeField] Transform[] transforms;
    [SerializeField] float Timer = 0;
    public bool proteinNotSpawned = true;
    [SerializeField] GameObject Prefab;
    [SerializeField] float minSpawnTime = 10f;
    [SerializeField] float maxSpawnTime = 16f;
    void Start()
    {
        proteinNotSpawned = true;
    }
    void Update()
    {
        if (proteinNotSpawned)
        {
            Timer -= Time.deltaTime;
            if (Timer <= 0)
            {
                GameObject InstantiatedPrefab = Instantiate(Prefab);
                InstantiatedPrefab.SetActive(true);
                InstantiatedPrefab.transform.position = transforms[Random.Range(0, transforms.Length)].position;
                proteinNotSpawned = false;
                Timer = Random.Range(minSpawnTime, maxSpawnTime);
            }
        }
    }
}
