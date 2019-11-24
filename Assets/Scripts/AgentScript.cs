using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentScript : MonoBehaviour {

    private NavMeshAgent agent;
    private NavMeshHit closestHit;
    //public GameObject playerUnit;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        if (NavMesh.SamplePosition (transform.position, out closestHit, 100f, NavMesh.AllAreas))
        {
            transform.position = closestHit.position;
            agent.enabled = true;
        }
        
		/*
        if (Input.GetMouseButtonDown(0))
        {
            playerUnit = GameObject.FindGameObjectWithTag("Player");
            agent.SetDestination(playerUnit.transform.position);
        }
        */
	}

    public void MoveAgent( Vector3 destination )
    {
        agent.SetDestination(destination);
    }
}
