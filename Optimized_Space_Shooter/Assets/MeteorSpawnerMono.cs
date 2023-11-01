using Player;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEditor.PackageManager;
using UnityEngine;
using Unity.Burst;

public class MeteorSpawnerMono : MonoBehaviour
{
   public GameObject Meteor;
    public int CurrentMeteorCount;
    public int MeteorCountPerWave;
    public int MeteorIncreasePerWave;
    [SerializeField] private Transform player;

    private void Awake()
    {
    }

    private void Update()
    {
        if(CurrentMeteorCount == 0)
        {
            MeteorCountPerWave += MeteorIncreasePerWave;
            StartNewWave();
        }
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
       Vector2 position = new Vector2(player.position.x+ Random.Range(-100, 100), player.position.y +  Random.Range(-100, 100));
        CurrentMeteorCount++;
        Instantiate(Meteor , position ,Quaternion.identity);
    }


    public struct SpawnerProperties : IComponentData
    {
        public Entity Prefab;
        public int CurrentMeteorCount;
        public int MeteorCountPerWave;
        public int MeteorIncreasePerWave;

    }

    public class SpawnerBaker : Baker<MeteorSpawnerMono>
    {
        public override void Bake(MeteorSpawnerMono authoring)
        {
            // add logic here later
            SpawnerProperties sp = default;


            sp.Prefab = GetEntity(authoring.Meteor);
            sp.CurrentMeteorCount = authoring.CurrentMeteorCount;
            sp.MeteorCountPerWave = authoring.MeteorCountPerWave;
            sp.MeteorIncreasePerWave = authoring.MeteorIncreasePerWave;

            AddComponent(sp);
        }
    }
}