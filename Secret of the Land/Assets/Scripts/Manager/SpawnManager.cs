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
    private Transform topLeft;
    [SerializeField]
    private Transform bottomRight;
    [SerializeField]
    private PlayerController playerController;

    void Awake()
    {
        playerController = FindFirstObjectByType<PlayerController>();
    }
    void Start()
    {
        //SpawnPlayer();
        SpawnSkeleton();
    }

    void SpawnSkeleton()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(transform.position.x+8, transform.position.x-8), Random.Range(transform.position.y+8, transform.position.y-8), 0);
        spawnPosition = GetNearestPoint(spawnPosition);
        int level = 1;
        GameObject skeleton = SpawnEngine.Spawn(skeletonPrefab, spawnPosition, Quaternion.identity, level);
        Transform a = GameObject.Find("MonsterManager").transform;
        skeleton.transform.SetParent(a);
        SkeletonFSM skeletonFSM = skeleton.GetComponent<SkeletonFSM>();
        skeleton.GetComponentInChildren<SkeletonEvent>().onMonsterDeath.AddListener(() => { playerController.Player.ReceiveXP(Table.CalculateXP(level,
                                                                                                                                            playerController.Player.properties.Level,
                                                                                                                                            skeletonFSM.properties.ThisType)); });
        }
    }

    // void SpawnPlayer()
    // {
    //     Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, 0);
    //     int level = playerController.Player.properties.Level;
    //     GameObject player = SpawnEngine.Spawn(playerController.gameObject, spawnPosition, Quaternion.identity, level);
    //     Transform a = GameObject.Find("PlayerManager").transform;
    //     player.transform.SetParent(a);
    // }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector2 GetNearestPoint(Vector2 oldPoint)
    {
        bool canWalk = NavMesh.SamplePosition(oldPoint, out NavMeshHit hit, 2.0f, NavMesh.AllAreas);

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
}
