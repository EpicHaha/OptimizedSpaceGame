using Unity.Burst;
using Unity.Entities;
using UnityEngine.UIElements.Experimental;

[BurstCompile]

[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial struct SpawnMeteorSystem : ISystem
{

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<SpawnerProperties>();
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {

    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        state.Enabled = false;

        var spawnerEntity = SystemAPI.GetSingletonEntity<SpawnerProperties>();
        var spawner = SystemAPI.GetAspectRW<SpawnerAspect>(spawnerEntity);

        var buffer = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);


        for (int i = 0; i < spawner.NumberOfMeteors; i++)
        {
            var NewMeteor = buffer.Instantiate(spawner.MeteorPrefab);
            var newMeteorTransform = spawner.GetRandomTransform();
/*
            buffer.SetComponent(NewMeteor, newMeteorTransform);
*/
        }

        buffer.Playback(state.EntityManager);
    }
}
