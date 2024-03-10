using System.Collections;
using System.Collections.Generic;
using Controller;
using UnityEngine;

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
            Vector3 spawnPosition = new Vector3(Random.Range(topLeft.position.x+3, bottomRight.position.x-3), Random.Range(bottomRight.position.y+3, topLeft.position.y-3), 0);
            while (!IsWalkable(spawnPosition))
            {
                spawnPosition = new Vector3(Random.Range(topLeft.position.x+3, bottomRight.position.x-3), Random.Range(bottomRight.position.y+3, topLeft.position.y-3), 0);
            }
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

    private bool IsWalkable(Vector2 worldPosition)
    {
        Collider2D hitCollider = Physics2D.OverlapPoint(worldPosition);
        return hitCollider == null;
    }

}
