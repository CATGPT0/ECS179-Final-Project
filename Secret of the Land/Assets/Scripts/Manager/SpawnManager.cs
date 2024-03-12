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
        //SpawnSkeleton();
        SpawnSlime();
    }

    void SpawnSkeleton()
    {
        for (int i = 0; i < 20; i++)
        {
        Vector3 spawnPosition = new Vector3(Random.Range(transform.position.x+20, transform.position.x-20), Random.Range(transform.position.y+20, transform.position.y-20), 0);
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

    void SpawnSlime()
    {
        for (int i = 0; i < 20; i++)
        {
        Vector3 spawnPosition = new Vector3(Random.Range(transform.position.x+20, transform.position.x-20), Random.Range(transform.position.y+20, transform.position.y-20), 0);
        spawnPosition = GetNearestPoint(spawnPosition);
        int level = 1;
        GameObject slime = SpawnEngine.Spawn(slimePrefab, spawnPosition, Quaternion.identity, level);
        Transform a = GameObject.Find("MonsterManager").transform;
        slime.transform.SetParent(a);
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
