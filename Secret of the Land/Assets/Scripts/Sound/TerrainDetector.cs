using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainDetector : MonoBehaviour
{
    public enum TerrainType
    {
        Grass,
        Road
    }
    private TerrainType type;
    public TerrainType Type
    {
        get { return type; }
        private set { type = value; }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Grass"))
        {
            Debug.Log("Grass detected");
            type = TerrainType.Grass;
        }
        else if (other.CompareTag("Road"))
        {
            Debug.Log("Road detected");
            type = TerrainType.Road;
        }
    }
}
