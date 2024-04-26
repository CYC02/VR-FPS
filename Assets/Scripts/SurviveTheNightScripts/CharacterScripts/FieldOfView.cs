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

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

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

            FieldOfViewCheck();

        }
    }

    // Looking for player
    private void FieldOfViewCheck() {
        // searching objects only on targetMask
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            //found an object on the target mask (only player on target mask)

            Transform target = rangeChecks[0].transform;

            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    //if we are not hitting out obstruction mask, then we can see the target
                    canSeePlayer = true;
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                //not within fov
                canSeePlayer = false;
            }
        }
        else {
            //ensure canSeePlayer is set to false is if the player was previously within view
            //but player is no longer in view
            if (canSeePlayer) {
                canSeePlayer= false;
            }
        }
    }
}
