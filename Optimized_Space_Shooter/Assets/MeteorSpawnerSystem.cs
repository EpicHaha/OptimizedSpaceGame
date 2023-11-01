using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;


public partial struct MeteorSPawnerSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        var ecb = SystemAPI.GetSingleton<BeginFixedStepSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);


    }
}
