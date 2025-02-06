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

    public void React(Bomb.Element element)
    {
        switch(element)
        {
            case Bomb.Element.Fire:
                //Bouncy ground explodes and pushes player high into the sky
                //physics cast to find all rigidbodies and have them instantly move upwards
                Debug.Log("Reaction with Fire!");
                RaycastHit[] hits = Physics.BoxCastAll(transform.position + (Vector3.up*2.5f), new Vector3(5,5,5), Vector3.up, Quaternion.identity, 5);

                foreach(RaycastHit hit in hits)
                {
                    if(hit.rigidbody != null)
                    {
                        hit.rigidbody.AddForce(reactionForce *Vector3.up, ForceMode.Impulse);
                    }
                }

                OnReaction();

            break;
        }
    }

    public void OnReaction()
    {
        //Destroy the bouncy ground
        Destroy(gameObject);
    }


    //Determine what happens when a catalyst hits it
    //Fire = speed gel on ground to increase velocity
    //When player jumps on bouncy ground, add some extra oomph

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + (Vector3.up*2.5f),new Vector3(5,5,5));
        
    }
}
