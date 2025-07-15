using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] GameObject player;
    [SerializeField] float speed = 1;
    [SerializeField] int playerNum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //print("I am running :)");
        float horizontal = Input.GetAxis(playerNum == 1 ? "P1_Horizontal": "P2_Horizontal");
        //print(horizontal);
        if (horizontal > 0 && transform.localPosition.x < (playerNum == 1 ? -3.66: 2.6))
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
        else if (horizontal < 0 && transform.localPosition.x > (playerNum == 1 ? -9.1:-2.33))
        {
            //Friend friend = new Friend(gameObject);
            //friend.SpawnFriend();
            transform.position += -transform.right * speed * Time.deltaTime;
        }


    }
}
