using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyGround : MonoBehaviour
{
    Vector3 objectVelocity;
    void OnCollisionEnter(Collision collision)
    {

        Vector3 velocity = collision.relativeVelocity;
        Debug.Log("Bouncy ground impact:" + velocity);
    }

    void OnTriggerEnter(Collider collider)
    {
        //Clock the velocity of the object
        objectVelocity = collider.GetComponent<Rigidbody>().velocity;
        //Vector3 bounce = Vector3.Reflect(objectVelocity,collider.contact.normal);
        


    }
}
