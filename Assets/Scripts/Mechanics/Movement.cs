using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    public GameObject waypointObject;
    NavMeshAgent nav;
    List<Transform> waypoints = null;
    int current = 0;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        // Get the waypoints
        if (waypointObject.GetComponent<WaypointContainer>() != null)
        {
            waypoints = waypointObject.GetComponent<WaypointContainer>().waypoints;
        }

        if (waypoints.Count > 0)
        {
            nav.SetDestination(waypoints[0].position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoints != null && waypoints.Count > 0)
        {
            if (nav.remainingDistance < nav.stoppingDistance)
            {
                current = (current + 1) % waypoints.Count;
                nav.SetDestination(waypoints[current].position);
            }
        }
    }
}
