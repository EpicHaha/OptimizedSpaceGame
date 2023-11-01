using Unity.Entities;

namespace Player
{
    public struct PlayerProperties : IComponentData
    {
        public float Speed;
        public float Heath;
        public float Damage;

    }
    
}