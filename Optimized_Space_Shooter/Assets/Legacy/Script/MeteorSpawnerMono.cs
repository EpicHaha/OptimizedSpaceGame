using UnityEngine;
using Unity.Burst;

[BurstCompile(CompileSynchronously = true)]
public class MeteorSpawnerMono : MonoBehaviour
{
   public GameObject Meteor;
    public int CurrentMeteorCount;
    public int MeteorCountPerWave;
    public int MeteorIncreasePerWave;
    [SerializeField] private Transform player;

    private void Start()
    {
        StartNewWave();
    }
    private void StartNewWave()
    {
        for (int i = 0; i < MeteorCountPerWave; i++)
        {
            SpawnMeteor();
        }
    }

    private void SpawnMeteor()
    {

        Vector2 position = new Vector2 (UnityEngine.Random.Range(-40,40), UnityEngine.Random.Range(-40, 40));

        Instantiate(Meteor , position ,Quaternion.identity);
    }


    public void CheckWave()
    {
        if (CurrentMeteorCount == 0)
        {
            MeteorCountPerWave += MeteorIncreasePerWave;
            StartNewWave();
        }
    }



}
