using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : IState
{
    protected FSM machine;
    protected Properties properties;
    protected Vector2 targetPos;
    protected TerrainDetector.TerrainType currentTerrainType;

    public PatrolState(FSM machine)
    {
        this.machine = machine;
        this.properties = machine.properties;
    }
    public virtual void OnEnter()
    {

    }
    public virtual void OnExit()
    {

    }
    public virtual void OnUpdate()
    {

    }

    protected Vector3 FindWalkablePoint(Vector3 center, float radius)
{
    Vector3 randomDirection = Random.insideUnitSphere * radius;
    randomDirection += center;
    NavMeshHit hit;
    Vector3 finalPosition = Vector3.zero;
    if (NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas))
    {
        finalPosition = hit.position;
    }
    return finalPosition;
}
}
