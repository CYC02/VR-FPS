using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;


/*
 * reference: https://www.youtube.com/watch?v=j1-OyLo77ss
 */
public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0,360)] public float angle;

    public GameObject player;

    //public LayerMask targetMask;
    public LayerMask playerMask;
    public LayerMask alliesMask;
    public LayerMask obstructionMask;
    public LayerMask resourceMask;
    private LayerMask targetMask;

    public bool canSeeTarget;

    private Transform target;

    public enum Target {
        Player,
        Allies,
        Resource
    }

    public Target currentTarget;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        StartCoroutine(FOVRoutine());
    }

    /* Coroutine
     * A delay is use so this is not called all the time.
     * Searching for player in view.
     */
    private IEnumerator FOVRoutine() {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return wait;

            switch (currentTarget)
            {
                case Target.Player:
                    targetMask = playerMask;
                    break;
                case Target.Allies:
                    targetMask = alliesMask;
                    break;
                case Target.Resource:
                    targetMask = resourceMask;
                    break;
                default:
                    targetMask = playerMask;
                    break;
            }

            FieldOfViewCheck(targetMask);

        }
    }

    // Looking for player
    private void FieldOfViewCheck(LayerMask targetMask) {
        // searching objects only on targetMask
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            //found an object on the target mask

            target = rangeChecks[0].transform;

            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    //if we are not hitting out obstruction mask, then we can see the target
                    canSeeTarget = true;
                }
                else
                {
                    canSeeTarget = false;
                }
            }
            else
            {
                //not within fov
                canSeeTarget = false;
            }
        }
        else {
            //ensure canSeeTarget is set to false is if the player was previously within view
            //but target is no longer in view
            if (canSeeTarget) {
                canSeeTarget= false;
            }
        }
    }

    public Transform GetTargetTransform() { return target; }
}
