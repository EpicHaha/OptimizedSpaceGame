using UnityEngine;
using Unity.Burst;

[BurstCompile(CompileSynchronously = true)]
public class MeteorSpawnerMono : MonoBehaviour
{
   [SerializeField] private  GameObject Meteor;
    private static int MeteorCountPerWave = 10;
    private static int MeteorIncreasePerWave = 5;



    public struct CurrentMeteors
    {
        public int CurrentMeteorCount;
    }
   public CurrentMeteors data;

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

        /*        GameObject GO = MeteorPooler.SharedInstance.GetPooledObject(0);
                GO.transform.position = position;
                GO.SetActive(true);
        */
        data.CurrentMeteorCount++;
        Instantiate(Meteor , position ,Quaternion.identity);
    }


    public void CheckWave()
    {
        if (data.CurrentMeteorCount < 1)
        {
            MeteorCountPerWave += MeteorIncreasePerWave;
            StartNewWave();
        }
    }



}
