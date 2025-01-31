using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform shootLocation;
    void OnFire()
    {
        GameObject bolt = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        SpawnBolt(bolt);
    }

    private void SpawnBolt(GameObject instance)
    {
        //Spawn the prefab and check if it is a projectile
        IProjectile projectile = instance.GetComponent<IProjectile>();
        if(projectile == null)
        {
            return;
        }
        
        Vector3 spawnLocation = new Vector3
        (
            shootLocation.transform.position.x,
            shootLocation.transform.position.y,
            shootLocation.transform.position.z
        );

        instance.transform.position = spawnLocation;

        projectile.Shoot(spawnLocation, shootLocation.transform.forward, projectile.speed);
    }
}
