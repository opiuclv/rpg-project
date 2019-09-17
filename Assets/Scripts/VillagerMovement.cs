using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 控制村民移動 無斜向

public class VillagerMovement : MonoBehaviour
{

    public float moveSpeed;
    private Vector2 minWalkPoint;                   // 可行走區域端點 左下 & 右上
    private Vector2 maxWalkPoint;

    private Rigidbody2D myRigidbody;

    public bool isWalking;                          // 是否移動中

    public float walkTime;                          // 移動時間 & counter
    private float walkCounter;
    public float waitTime;                          // 等待移動時間 & counter
    private float waitCounter;

    private int walkDirection;                      // 移動的方向

    public Collider2D walkZone;                     // 可行走區域
    private bool hasWalkZone;

    public bool canMove;                            // 是否可移動的狀態
    private DialogueManager theDM;

    // Use this for initialization
    void Start() {
        myRigidbody = GetComponent<Rigidbody2D>();
        theDM = FindObjectOfType<DialogueManager>();

        waitCounter = waitTime;
        walkCounter = walkTime;

        ChooseDirection();

        if (walkZone != null)
        {
            minWalkPoint = walkZone.bounds.min;         // 左下角
            maxWalkPoint = walkZone.bounds.max;         // 右上角
            hasWalkZone = true;
        }

        canMove = true;
    }

    // Update is called once per frame
    void Update() {

        if (!theDM.dialogueActive)
        {
            canMove = true;
        }

        if (!canMove)
        {
            myRigidbody.velocity = Vector2.zero;
            return;
        }

        if (isWalking)                                  // 移動中
        {
            walkCounter -= Time.deltaTime;

            switch (walkDirection)
            {
                case 0:                                                     // 向上
                    myRigidbody.velocity = new Vector2(0, moveSpeed);
                    if (hasWalkZone && transform.position.y > maxWalkPoint.y)
                    {
                        isWalking = false;
                        waitCounter = waitTime;
                    }
                    break;
                case 1:                                                     // 向右
                    myRigidbody.velocity = new Vector2(moveSpeed, 0);
                    if (hasWalkZone && transform.position.x > maxWalkPoint.x)
                    {
                        isWalking = false;
                        waitCounter = waitTime;
                    }
                    break;
                case 2:                                                     // 向下
                    myRigidbody.velocity = new Vector2(0, -moveSpeed);
                    if (hasWalkZone && transform.position.y < minWalkPoint.y)
                    {
                        isWalking = false;
                        waitCounter = waitTime;
                    }
                    break;
                case 3:                                                     // 向左
                    myRigidbody.velocity = new Vector2(-moveSpeed, 0);
                    if (hasWalkZone && transform.position.x < minWalkPoint.x)
                    {
                        isWalking = false;
                        waitCounter = waitTime;
                    }
                    break;
            }

            if (walkCounter < 0)
            {
                isWalking = false;
                waitCounter = waitTime;
            }
        }
        else                                            // 等待移動 ; 再決定方向
        {
            waitCounter -= Time.deltaTime;

            myRigidbody.velocity = Vector2.zero;

            if (waitCounter < 0)
            {
                ChooseDirection();
            }
        }
    }

    public void ChooseDirection()                       // 決定移動的方向
    {
        walkDirection = Random.Range(0, 4);
        isWalking = true;
        walkCounter = walkTime;
    }
}
