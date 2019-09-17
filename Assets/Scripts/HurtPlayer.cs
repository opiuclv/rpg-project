using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour {

    public int damageToGive;
    private int currentDamage;
    public GameObject damageNumber;

    private PlayerStats thePS;


	// Use this for initialization
	void Start () {
        thePS = FindObjectOfType<PlayerStats>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.name == "Player") // 有點類似復活機制
        {
            currentDamage = damageToGive - thePS.currentDefence;
            if(currentDamage < 0)
            {
                currentDamage = 0;
            }

            other.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(currentDamage); // 連接到PlayerHealthManageer中的函式

            var clone = (GameObject)Instantiate(damageNumber, other.transform.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<FloatingNumbers>().damageNumber = currentDamage;
        }
    }
}
