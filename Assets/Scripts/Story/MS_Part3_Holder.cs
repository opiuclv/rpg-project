using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MS_Part3_Holder : MonoBehaviour
{
	private Main_Story_Part3 theMS3;

	// Use this for initialization
	void Start()
	{
		theMS3 = FindObjectOfType<Main_Story_Part3>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnTriggerEnter2D(Collider2D other)  
	{
		if (other.gameObject.tag == "Player")
		{
			theMS3.ShowDialogue();
			Destroy(gameObject); // 劇情已經觸發過一次就不要了
		}
	}
}
