using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject leftPrefab;
    [SerializeField] private GameObject rightPrefab;
    [SerializeField] private Transform leftShootLocation;
    
    [SerializeField] private Transform rightShootLocation;
    void OnFire()
    {
        GameObject bolt = Instantiate(leftPrefab, Vector3.zero, Quaternion.identity);
        SpawnBolt(bolt, leftShootLocation);
    }
    void OnAltFire()
    {
        GameObject bolt = Instantiate(rightPrefab, Vector3.zero, Quaternion.identity);
        SpawnBolt(bolt, rightShootLocation);
    }

    private void SpawnBolt(GameObject instance, Transform spawnLocation)
    {
        //Spawn the prefab and check if it is a projectile
        IProjectile projectile = instance.GetComponent<IProjectile>();
        if(projectile == null)
        {
            return;
        }

        instance.transform.position = spawnLocation.position;
        
        //Set to same layer so they do not collide
        instance.layer = this.gameObject.layer;

        projectile.Shoot(spawnLocation.position, spawnLocation.forward);
    }
}
