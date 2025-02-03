using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyGround : MonoBehaviour, IReactable
{
    [Header("Fire Catalyst reaction")]
    [SerializeField] float reactionForce = 20;
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
                RaycastHit[] hits = Physics.BoxCastAll(transform.position, new Vector3(5,5,5), Vector3.up, Quaternion.identity);

                foreach(RaycastHit hit in hits)
                {
                    hit.rigidbody.AddExplosionForce(reactionForce, transform.position,5 );
                }
            break;
        }
    }

    public void OnReaction()
    {
        throw new NotImplementedException();
    }


    //Determine what happens when a catalyst hits it
    //Fire = speed gel on ground to increase velocity
    //When player jumps on bouncy ground, add some extra oomph
}
