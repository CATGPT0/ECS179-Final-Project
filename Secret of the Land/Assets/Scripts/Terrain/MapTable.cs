using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLocation
{
    public Vector2 topLeft;
    public Vector2 bottomRight;
}

public static class MapTable
{
    public static Dictionary<string, MapLocation> locations = new Dictionary<string, MapLocation>()
    {
        { "Forest", new MapLocation() { topLeft = new Vector2(496, 1261), bottomRight = new Vector2(558, 1210) } },
        { "GrassLand1", new MapLocation() { topLeft = new Vector2(1001, 0), bottomRight = new Vector2(2000, 1000) } },
        { "GrassLand2", new MapLocation() { topLeft = new Vector2(0, 1001), bottomRight = new Vector2(1000, 2000) } },
        { "Village", new MapLocation() { topLeft = new Vector2(1001, 1001), bottomRight = new Vector2(2000, 2000) } }
    };

}
