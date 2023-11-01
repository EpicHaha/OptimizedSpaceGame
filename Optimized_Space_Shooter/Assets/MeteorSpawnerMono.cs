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
       Vector2 position = new Vector2(player.position.x+ Random.RandomRange(-100, 100), player.position.y +  Random.RandomRange(-100, 100));
        float rotation = Random.RandomRange(0,180);
        CurrentMeteorCount++;
     GameObject meteorInstance =    Instantiate(Meteor , position ,new Quaternion(0,0,rotation,0));
        meteorInstance.GetComponent<Meteor>().Player = player;
        meteorInstance.GetComponent<Meteor>().Spawner = this;
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