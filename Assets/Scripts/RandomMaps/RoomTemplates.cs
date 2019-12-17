using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour {

	public GameObject[] bottomRooms;
	public GameObject[] topRooms;
	public GameObject[] leftRooms;
	public GameObject[] rightRooms;
	public GameObject[] lastRooms;
	public GameObject closedRoom;

	public List<GameObject> rooms;

	public float waitTime;
	public bool spawnedBoss;
	private int rand; // 隨機Boss房
	public GameObject[] bossRooms;

	void Update(){

		if(waitTime <= 0 && spawnedBoss == false){
			for (int i = 0; i < rooms.Count; i++) {
				if(i == rooms.Count-1){
					rand = Random.Range (0, bossRooms.Length);
					Instantiate(bossRooms[rand], rooms[i].transform.position + new Vector3(10,-10,0), Quaternion.identity);
					spawnedBoss = true;
				}
			}
		} else {
			waitTime -= Time.deltaTime;
		}
	}
}
