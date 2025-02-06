using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyGround : MonoBehaviour, IReactable
{
    [Header("Fire Catalyst reaction")]
    [SerializeField] float reactionForce = 20;

    [Header("Sound Effects")]
    [SerializeField] AudioClip bounceSound;
    [SerializeField] AudioClip reactSound;

    //[Header("VFX")]
    void Awake()
    {
        //Put the bouncy component on this
    }
    Vector3 objectVelocity;


    void OnCollisionEnter(Collision collision)
    {
        Vector3 velocity = collision.relativeVelocity;
        Debug.Log("Bouncy ground impact:" + velocity);
        //Play bounce SFX
    }

    public void React(Bomb.Element element)
    {
        switch(element)
        {
            case Bomb.Element.Fire:
                //Bouncy ground explodes and pushes player high into the sky
                //physics cast to find all rigidbodies and have them instantly move upwards
                Debug.Log("Reaction with Fire!");
                RaycastHit[] hits = Physics.BoxCastAll(transform.position + (Vector3.up*2.5f), new Vector3(8,5,8), Vector3.up, Quaternion.identity);

                foreach(RaycastHit hit in hits)
                {
                    hit.rigidbody.AddForce(reactionForce *Vector3.up, ForceMode.Impulse);
                }
                Destroy(gameObject);
            break;
        }
    }

    public void OnReaction()
    {
        //Destroy the bouncy ground
    }


    //Determine what happens when a catalyst hits it
    //Fire = speed gel on ground to increase velocity
    //When player jumps on bouncy ground, add some extra oomph

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + (Vector3.up*2.5f),new Vector3(8,5,8));
        
    }
}
