using Unity.Entities;
using Unity.Mathematics;

    public struct PlayerMovementInput : IComponentData
    {
        public float2 Value;
    }

    public struct PlayerMoveSpeed : IComponentData
    {
        public float Value;
    }

    public struct PlayerTag : IComponentData { }

    public struct FireProjectileTag : IComponentData, IEnableableComponent { }

    public struct ProjectilePrefab : IComponentData
    {
        public Entity Value;
    }

