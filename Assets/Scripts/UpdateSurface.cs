using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UpdateSurface : MonoBehaviour
{
    private NavMeshSurface theNMS;
    public int currentAgentTypeID;
    public int monsterAgentTypeID;
    public int defaultAgentTypeID;
    public float timer;
    private bool switchSurface = true;

    // Start is called before the first frame update
    void Start()
    {
        theNMS = GetComponent<NavMeshSurface>();
        currentAgentTypeID = theNMS.agentTypeID;
        monsterAgentTypeID = theNMS.agentTypeID;
        defaultAgentTypeID = 0;
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        currentAgentTypeID = theNMS.agentTypeID;
        if (Time.time - timer > 10f && switchSurface)
        {
            theNMS.agentTypeID = defaultAgentTypeID;
            // theNMBS.agentRadius = 1f;
            theNMS.BuildNavMesh();
            switchSurface = false;
            timer = Time.time;
        }

        if (Time.time - timer > 10f && !switchSurface)
        {
            theNMS.agentTypeID = monsterAgentTypeID;
            // theNMBS.agentRadius = 1f;
            theNMS.BuildNavMesh();
            switchSurface = true;
            timer = Time.time;
        }
    }
}
