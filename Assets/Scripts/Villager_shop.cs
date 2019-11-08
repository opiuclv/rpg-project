using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager_shop : MonoBehaviour {

    public GameObject shopMan;
    CanvasGroup canvas_g; // 宣告canvas group方便管理

    // Use this for initialization
    private void Awake()
    {
        shopMan = GameObject.Find("ShopWindow");
        canvas_g = shopMan.GetComponent<CanvasGroup>();
	} // Awake()


    void OnTriggerStay2D(Collider2D other) // trigger有問題
    {
		if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (canvas_g.alpha == 0) 
                {
                    canvas_g.alpha = 1;
                    canvas_g.interactable = true;
                    canvas_g.blocksRaycasts = true;

                    if (transform.parent.GetComponent<VillagerMovement>() != null) // 這裡常常出問題
                    {
				   	   transform.parent.GetComponent<VillagerMovement>().canMove = false;
                    } // if

                } // if
                else
                {
                    canvas_g.alpha = 0;
                    canvas_g.interactable = false;
                    canvas_g.blocksRaycasts = false;


                } // else
            } // if
        } // if
	} // OnTriggerStay2D()

    void Update()
    {
        StartCoroutine(load());   
	} // Update()

    IEnumerator load()
    {
        yield return new WaitForSeconds(1);    //等待時間 避免同時觸發
        if (canvas_g.alpha == 1 && Input.GetKeyUp(KeyCode.Escape))
        {
            canvas_g.alpha = 0;
            canvas_g.interactable = false;
            canvas_g.blocksRaycasts = false;
        } // if
	} // load()
}
