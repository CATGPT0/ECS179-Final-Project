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
        for (int i = 0; i < 5; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(topLeft.position.x+8, bottomRight.position.x-8), Random.Range(bottomRight.position.y+8, topLeft.position.y-8), 0);
            spawnPosition = GetNearestPoint(spawnPosition);
            GameObject skeleton = SpawnEngine.Spawn(skeletonPrefab, spawnPosition, Quaternion.identity, 1);
            Transform a = GameObject.Find("Monsters").transform;
            skeleton.transform.SetParent(a);
            skeleton.GetComponentInChildren<SkeletonEvent>().onMonsterDeath += playerController.Player.ReceiveXP;
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
