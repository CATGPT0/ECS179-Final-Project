using System.Collections;
using System.Collections.Generic;
using Controller;                                                                                   
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject skeletonPrefab;
    [SerializeField]
    private GameObject slimePrefab;
    [SerializeField]
    private PlayerController playerController;
    private Transform monsterManager;

    void Awake()
    {
        playerController = FindFirstObjectByType<PlayerController>();
        monsterManager = GameObject.Find("MonsterManager").transform;
    }
    void Start()
    {
        SpawnSkeleton(MapTable.locations["Forest"].topLeft, MapTable.locations["Forest"].bottomRight, 10, Random.Range(1, 3));
        SpawnSlime(MapTable.locations["Forest"].topLeft, MapTable.locations["Forest"].bottomRight, 10, Random.Range(1, 3));
        SpawnSkeleton(MapTable.locations["Forest2"].topLeft, MapTable.locations["Forest2"].bottomRight, 10, Random.Range(3, 6));
        SpawnSlime(MapTable.locations["Forest2"].topLeft, MapTable.locations["Forest2"].bottomRight, 10, Random.Range(3, 6));
    }

    void SpawnSkeleton(Vector2 topLeft, Vector2 bottomRight, int amount = 1, int level = 1)
    {
        for (int i = 0; i < amount; i++)
        {
        Vector3 spawnPosition = new Vector3(Random.Range(topLeft.x, bottomRight.x), Random.Range(bottomRight.y, topLeft.y), 0);
        spawnPosition = GetNearestPoint(spawnPosition);
        GameObject skeleton = SpawnEngine.Spawn(skeletonPrefab, spawnPosition, Quaternion.identity, level);
        skeleton.transform.SetParent(monsterManager);
        SkeletonFSM skeletonFSM = skeleton.GetComponent<SkeletonFSM>();
        skeleton.GetComponentInChildren<SkeletonEvent>().onMonsterDeath.AddListener(() => { playerController.Player.ReceiveXP(Table.CalculateXP(level,
                                                                                                                                            playerController.Player.properties.Level,
                                                                                                                                            skeletonFSM.properties.ThisType)); });
        }
    }

    void SpawnSlime(Vector2 topLeft, Vector2 bottomRight, int amount = 1, int level = 1)
    {
        for (int i = 0; i < amount; i++)
        {
        Vector3 spawnPosition = new Vector3(Random.Range(topLeft.x, bottomRight.x), Random.Range(bottomRight.y, topLeft.y), 0);
        spawnPosition = GetNearestPoint(spawnPosition);
        GameObject slime = SpawnEngine.Spawn(slimePrefab, spawnPosition, Quaternion.identity, level);
        slime.transform.SetParent(monsterManager);
        SlimeFSM slimeFSM = slime.GetComponent<SlimeFSM>();
        slime.GetComponentInChildren<SlimeEvent>().onMonsterDeath.AddListener(() => { playerController.Player.ReceiveXP(Table.CalculateXP(level,
                                                                                                                                            playerController.Player.properties.Level,
                                                                                                                                            slimeFSM.properties.ThisType)); });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector2 GetNearestPoint(Vector2 oldPoint)
    {
        bool canWalk = NavMesh.SamplePosition(oldPoint, out NavMeshHit hit, 3.0f, NavMesh.AllAreas);

        if (canWalk)
        {
            Debug.Log("Spawned at " + hit.position + "successfully!");
        }
        else
        {
            Debug.LogError("Can't spawn skeleton at " + oldPoint);
        }
        return hit.position;
    }

    bool CanSpawnHere(Vector2 position, Vector2 size, LayerMask obstacleLayer)
    {
        // Optionally, you can add a rotation argument if your objects can be rotated
        Quaternion noRotation = Quaternion.identity;
        
        // Perform the overlap box check
        Collider2D hit = Physics2D.OverlapBox(position, size, 0, obstacleLayer);
        
        // If hit is null, no collider was overlapped
        return hit == null;
    }
}
