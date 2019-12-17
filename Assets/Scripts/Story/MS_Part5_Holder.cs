using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MS_Part5_Holder : MonoBehaviour
{
    private Main_Story_Part5 theMS5;

    // Use this for initialization
    void Start()
    {
        theMS5 = FindObjectOfType<Main_Story_Part5>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            theMS5.ShowDialogue();
            Destroy(gameObject); // 劇情已經觸發過一次就不要了
        }
    }
}
