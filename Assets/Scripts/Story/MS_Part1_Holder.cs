using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MS_Part1_Holder : MonoBehaviour
{
    private Main_Story_Part1 theMS1;

    // Use this for initialization
    void Start()
    {
        theMS1 = FindObjectOfType<Main_Story_Part1>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)  
    {
        if (other.gameObject.tag == "Player")
        {
            theMS1.ShowDialogue();
            Destroy(gameObject); // 劇情已經觸發過一次就不要了
        }
    }
}
