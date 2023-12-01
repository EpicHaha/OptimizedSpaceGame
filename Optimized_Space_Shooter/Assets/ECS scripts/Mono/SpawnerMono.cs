using UnityEngine;
using Unity.Mathematics;
using Unity.Entities;

public class SpawnerMono : MonoBehaviour
{
    public float2 fieldDimensions;
    public int Wave;
    public int MeteorsPerWave;
    public GameObject MeteorPrefab;
    public uint RandomSeed;

}



public class SpawnerBaker : Baker<SpawnerMono>
{
    public override void Bake(SpawnerMono authoring)
    {
        AddComponent(new SpawnerProperties
        {
            fieldDimensions = authoring.fieldDimensions,
            Wave = authoring.Wave,
            MeteorsPerWave = authoring.MeteorsPerWave,
            MeteorPrefab = GetEntity(authoring.MeteorPrefab)
        });

        AddComponent(new SpawnerRandom { Value = Unity.Mathematics.Random.CreateFromIndex(authoring.RandomSeed) });
    }
}
