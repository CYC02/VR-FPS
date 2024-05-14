/**
 * MICHAEL CALLE
 * VR Project
 */

/**
 * HERE IS A LIST OF YOUTUBE TUTORIALS I GOT INSPIRATION FROM:
 * 
 * Bullet and gun mechanic:
 * https://www.youtube.com/watch?v=EwiUomzehKU&ab_channel=Unity3DSchool
 * 
 * Flocking behavior:
 * https://www.youtube.com/watch?v=mjKINQigAE4&list=PL5KbKbJ6Gf99UlyIqzV1UpOzseyRn5H1d&ab_channel=BoardToBitsGames
 * 
 * Grabbing with the VR hands:
 * https://www.youtube.com/watch?v=FyhNnbZR28I&ab_channel=MuddyWolf
 * 
 * NOTE: These tutorials only laid a foundation. The 2D flocking techniques had to be rebuilt into a 3D world space as well
 * as making it compatible with the destruction of individual agents from the bullets.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior behavior;

    [Range(10, 500)]
    public int startingCount = 250;
    const float AgentDensity = 0.08f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < startingCount; i++)
        {
            FlockAgent newAgent = Instantiate (
                agentPrefab,
                Random.insideUnitCircle * startingCount * AgentDensity,
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                transform
            );
            
            //Ensures agents are positioned along the floor
            newAgent.transform.position = new Vector3(newAgent.transform.position.x, 10, newAgent.transform.position.y);
            newAgent.name = "Agent " + i;
            agents.Add(newAgent);
        }
    }

    void Update()
    {
        for (int i = 0; i < agents.Count; i++)
        {
            FlockAgent agent = agents[i];
            if (agent == null) {
                agents.RemoveAt(i);
            }

            List<Transform> context = GetNearbyObjects(agent);

            Vector2 move = behavior.CalculateMove(agent, context, this);
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);
        }
    }

    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();

        //If in 3D, Collider3D Physics overlap sphere...
        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighborRadius);
        foreach(Collider c in contextColliders)
        {
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }
}
