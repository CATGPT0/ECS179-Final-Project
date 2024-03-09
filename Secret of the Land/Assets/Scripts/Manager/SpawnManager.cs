using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject skeletonPrefab;
    [SerializeField]
    private Transform topLeft;
    [SerializeField]
    private Transform bottomRight;

    void Start()
    {
        // for (int i = 0; i < 5; i++)
        // {
        //     Vector3 spawnPosition = new Vector3(Random.Range(topLeft.position.x, bottomRight.position.x), Random.Range(bottomRight.position.y, topLeft.position.y), 0);
        //     Instantiate(skeletonPrefab, spawnPosition, Quaternion.identity);
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
