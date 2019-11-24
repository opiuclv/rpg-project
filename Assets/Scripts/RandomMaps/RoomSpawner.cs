using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour {

	public int openingDirection;
	// 1 --> need bottom door
	// 2 --> need top door
	// 3 --> need left door
	// 4 --> need right door


	private RoomTemplates templates;
	private int rand;
	public bool spawned = false;

	public float waitTime = 4f;

	void Start(){
		Destroy(gameObject, waitTime);
		templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
		Invoke("Spawn", 0.1f);
	}

    GameObject FindClosestSpawner()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("SpawnPoint");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            if (go != gameObject)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = go;
                    distance = curDistance;
                }
            }
        }
        return closest;
    }

    void Spawn(){
        
        GameObject closest = FindClosestSpawner();
        float dis = (closest.transform.position - transform.position).sqrMagnitude;
        if (dis < 1f)
        {
            if (closest.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            } //if
            spawned = true;
        }

        if (spawned == false){
			if(openingDirection == 1){ // 找出口向下的房間
				if (templates.rooms.Count < 10) {
					rand = Random.Range (0, templates.bottomRooms.Length);
					Instantiate (templates.bottomRooms [rand], transform.position, templates.bottomRooms [rand].transform.rotation);
				} // if
				else{
					rand = Random.Range (2, 3);
					Instantiate (templates.lastRooms [rand], transform.position, templates.lastRooms [rand].transform.rotation);
				} // else
			} // if
			else if(openingDirection == 2){ // 找出口向上的房間
				if (templates.rooms.Count < 10) {
					rand = Random.Range (0, templates.topRooms.Length);
					Instantiate (templates.topRooms [rand], transform.position, templates.topRooms [rand].transform.rotation);
				} // if
				else{
					rand = Random.Range (0, 1);
					Instantiate (templates.lastRooms [rand], transform.position, templates.lastRooms [rand].transform.rotation);
				} // else
			} // else if
			else if(openingDirection == 3){ // 找出口向左的房間
				if (templates.rooms.Count < 10) {
					rand = Random.Range (0, templates.leftRooms.Length);
					Instantiate (templates.leftRooms [rand], transform.position, templates.leftRooms [rand].transform.rotation);
				} // if
				else{
					rand = Random.Range (6, 7);
					Instantiate (templates.lastRooms [rand], transform.position, templates.lastRooms [rand].transform.rotation);
				} // else
			} // else if
			else if(openingDirection == 4){ // 找出口向右的房間
				if (templates.rooms.Count < 10) {
					rand = Random.Range (0, templates.rightRooms.Length);
					Instantiate (templates.rightRooms [rand], transform.position, templates.rightRooms [rand].transform.rotation);
				} // if
				else{
					rand = Random.Range (4, 5);
					Instantiate (templates.lastRooms [rand], transform.position, templates.lastRooms [rand].transform.rotation);
				} // else
			} // else if
			spawned = true;
		} //if
	} //Spawn()
    /*
	void OnTriggerEnter2D(Collider2D other){
        
		if(other.CompareTag("SpawnPoint")){
			if(other.GetComponent<RoomSpawner>().spawned == false && spawned == false){
				Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
				Destroy(gameObject);
			} //if
			spawned = true;
		} // if
	}
    */
}
