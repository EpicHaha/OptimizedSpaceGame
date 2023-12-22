using Unity.Jobs;
using UnityEngine;
using Unity.Collections;
using Unity.Burst;
using static UnityEngine.RuleTile.TilingRuleOutput;


[BurstCompile(CompileSynchronously = true)]
public class Meteor : Damagable
{
    Collider2D collider;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
    }

    [BurstCompile(CompileSynchronously = true)]
    private void Update()
    {
        if (Vector3.Distance(Vector3.zero, transform.position) < 10)
        {

            collider.enabled = true;
        }
        else {

            collider.enabled = false;
        }



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

}
