using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBomb : Bomb
{
    [SerializeField] GameObject bouncyGroundPrefab;
    internal override void OnExplode()
    {
        base.OnExplode();
        //Logic for the bouncy bomb

        //Spawn a mesh with the desired physics material for player bouncing

        //Create plane
        //Create mesh
        //Create rb
        //Give a duration

        //Can create a prefab that holds all of these values and makes it easier to edit fine details
        Quaternion spawnRotation = Quaternion.FromToRotation(Vector3.up, contactNormal);
        Instantiate(bouncyGroundPrefab, transform.position, spawnRotation);
    }
}
