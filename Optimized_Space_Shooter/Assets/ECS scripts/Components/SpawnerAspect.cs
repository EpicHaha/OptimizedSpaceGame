using System.Numerics;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;


public readonly partial struct SpawnerAspect : IAspect
{
    public readonly Entity Entity;

    private readonly RefRO<LocalTransform> _transform;
    private LocalTransform Transform => _transform.ValueRO;


    private readonly RefRO<SpawnerProperties> _properties;
    private readonly RefRW<SpawnerRandom> _random;

    public int NumberOfMeteors => _properties.ValueRO.MeteorsPerWave * _properties.ValueRO.Wave;

    public Entity MeteorPrefab => _properties.ValueRO.MeteorPrefab;

    public LocalTransform GetRandomTransform()
    {
        return new LocalTransform
        {
            Position = GetRandomPosition(),
            Rotation = quaternion.identity,
            Scale = 1f
        };
    }

    private float3 GetRandomPosition()
    {
        float3 randomPosition;

        randomPosition = _random.ValueRW.Value.NextFloat3(MinCorner, MaxCorner);
        return randomPosition;
    }
    private float3 MinCorner => Transform.Position - HalfDimensions;
    private float3 MaxCorner => Transform.Position + HalfDimensions;
    private float3 HalfDimensions => new()
    {
        x = _properties.ValueRO.fieldDimensions.x * 0.5f,
        y = _properties.ValueRO.fieldDimensions.y * 0.5f,
        z= 0f
    };



}

