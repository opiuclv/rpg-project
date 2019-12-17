﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildForest : MonoBehaviour
{
    private NavMeshSurface nmSurf;
    private RoomTemplates theRT;

    public float mapSpawnTime;
    private float spawnTimer;
    private bool buildDone = false;
    // Start is called before the first frame update
    void Start()
    {
        nmSurf = FindObjectOfType<NavMeshSurface>();
        theRT = FindObjectOfType<RoomTemplates>();
        spawnTimer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!buildDone && theRT.spawnedBoss)
        {
            nmSurf.BuildNavMesh();
            buildDone = true;
        }
    }
}
