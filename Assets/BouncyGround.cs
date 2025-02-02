using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyGround : MonoBehaviour, IReactable
{

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
