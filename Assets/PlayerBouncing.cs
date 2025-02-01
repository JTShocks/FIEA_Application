using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBouncing : MonoBehaviour
{

    PlayerMovement player;
    bool isTryingToBounce;
    float lastBounceTime;
    Vector3 bounce;
    void Awake()
    {
        player = GetComponent<PlayerMovement>();
    }
    void OnEnable(){ player.OnBeforeMove += OnBeforeMove;}


    void OnDisable(){player.OnBeforeMove -= OnBeforeMove;}

    private void OnBeforeMove()
    {
        if(isTryingToBounce && player.isGrounded)
        {
            //Vector3 bounce = Vector3.Reflect(player.velocity,contactNormal);
            player.velocity.y += bounce.y*2;
            isTryingToBounce = false;
        }

    }

    void OnBounce(Vector3 bounce)
    {
        Debug.Log("Recieved!");
        isTryingToBounce = true;
        lastBounceTime = Time.time;
        this.bounce = bounce;
    }
}
