using Unity.Jobs;
using UnityEngine;
using Unity.Collections;
using Unity.Burst;
using static UnityEngine.RuleTile.TilingRuleOutput;


[BurstCompile(CompileSynchronously = true)]
public class Meteor : Damagable
{
    public Vector2 targetPosition;


    [BurstCompile(CompileSynchronously = true)]
    private void Update()
    {

        NativeArray<Vector2> result = new NativeArray<Vector2>(1, Allocator.TempJob);
        DetermineNextPosition Job = new DetermineNextPosition
        {
            currentPosition = transform.position,
            targetPosition = Vector2.zero,
            nextPosition = result
        };

        JobHandle jobHandle = Job.Schedule();
        jobHandle.Complete();
        transform.position = result[0];
         //transform.position = Vector2.Lerp(transform.position, Vector3.zero, 0.001f);
        result.Dispose();

    }
}

[BurstCompile(CompileSynchronously = true)]
public struct DetermineNextPosition : IJob
{
    public Vector2 currentPosition;
    public Vector2 targetPosition;


    public NativeArray<Vector2> nextPosition;

    public void Execute()
    {
        nextPosition[0] = Vector2.Lerp(currentPosition, targetPosition, 0.001f);
    }
}
