using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPickup : MonoBehaviour {

    public int value;
    public ShopManager theMM;

	// Use this for initialization
	void Start () {
        theMM = FindObjectOfType<ShopManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
        if(other.gameObject.name == "Player")
        {
            theMM.AddMoney(value);
            Destroy(gameObject);
        }
	}
}
