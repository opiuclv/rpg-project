using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager_shop : MonoBehaviour {

    public GameObject shopMan;
    CanvasGroup canvas_g;

    // Use this for initialization
    private void Awake()
    {
        shopMan = GameObject.Find("ShopWindow");
        canvas_g = shopMan.GetComponent<CanvasGroup>();
    }


    void OnTriggerStay2D(Collider2D other) // trigger有問題
    {
        if (other.gameObject.name == "Player")
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (canvas_g.alpha == 0)
                {
                    canvas_g.alpha = 1;
                    canvas_g.interactable = true;
                    canvas_g.blocksRaycasts = true;
                    if (transform.parent.GetComponent<VillagerMovement>() != null)
                    {
                        transform.parent.GetComponent<VillagerMovement>().canMove = false;
                    }

                }
            }
        }
    }

    void Update()
    {
        StartCoroutine(load());   
    }
    IEnumerator load()
    {
        yield return new WaitForSeconds(1);    //等待時間 避免同時觸發
        if (canvas_g.alpha == 1 && Input.GetKeyUp(KeyCode.Escape))
        {
            canvas_g.alpha = 0;
            canvas_g.interactable = false;
            canvas_g.blocksRaycasts = false;
        }
    }
}
