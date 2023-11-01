using UnityEngine;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using System;

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
/*
        NativeArray<Vector2> result = new NativeArray<Vector2>(1, Allocator.TempJob);
        RandomPosition Job = new RandomPosition
        {
            index = CurrentMeteorCount+1,
            SpawnPosition = result
        };

        JobHandle jobHandle = Job.Schedule();
        jobHandle.Complete();

        Vector2 position = Job.SpawnPosition[0];
        result.Dispose();

        CurrentMeteorCount++;
*/

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


/*
[BurstCompile(CompileSynchronously = true)]
public struct RandomPosition : IJob
{
    public int index;

    public NativeArray<Vector2> SpawnPosition;
    public void Execute()
    {
        var seed = (uint)(index);
        var rnd = new Unity.Mathematics.Random(seed);
        SpawnPosition[0] =  new Vector2( rnd.NextInt(-100, 100), rnd.NextInt(-100, 100)); 

    }
}*/
