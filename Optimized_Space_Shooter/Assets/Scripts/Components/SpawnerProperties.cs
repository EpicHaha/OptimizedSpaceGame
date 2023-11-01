using Unity.Entities;
using Unity.Mathematics;
public struct SpawnerProperties : IComponentData
{
    public float2 fieldDimensions;
    public int Wave;
    public int MeteorsPerWave;
    public Entity MeteorPrefab;

}
