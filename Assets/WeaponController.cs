using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject leftPrefab;
    [SerializeField] private GameObject rightPrefab;
    [SerializeField] private Transform leftShootLocation;
    
    [SerializeField] private Transform rightShootLocation;

    [SerializeField] List<GameObject> catalysts = new();
    [SerializeField] List<GameObject> bases = new();


        InputAction switchAction;

        public static event Action<float> TriggerSwitchWeapons;

        int currentLeftPrefab = 0;
        int currentRightPrefab = 0;
        bool isTryingToSwitchWeapons;
        bool isSwitchingWeapons;

    void Awake()
    {
        PlayerInput playerInput = GetComponent<PlayerInput>();
        switchAction = playerInput.actions["SwitchItem"];
        if(leftPrefab == null || rightPrefab == null)
        {
            SetActivePrefabs();
        }

    }
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

    void SetActivePrefabs()
    {
        leftPrefab = bases[currentLeftPrefab];
        rightPrefab = catalysts[currentRightPrefab];
    }

    void OnSwitchItem()
    {
        var switchInput = switchAction.ReadValue<float>();
        isTryingToSwitchWeapons = true;
        Debug.Log(switchInput);

        if(switchInput < 0)
        {
            //Switch left item by 1 space

            currentLeftPrefab += 1;
            if(currentLeftPrefab > bases.Count - 1)
            {
                currentLeftPrefab = 0;
            }
            
        }
        else if(switchInput > 0)
        {
            //Switch right item by 1 space
            currentRightPrefab += 1;
            if(currentRightPrefab > catalysts.Count - 1)
            {
                currentRightPrefab = 0;
            }
        }
        TriggerSwitchWeapons?.Invoke(switchInput);
        StartCoroutine(SwitchWeapons());
        isTryingToSwitchWeapons = false;

    }

    IEnumerator SwitchWeapons()
    {
        if(isTryingToSwitchWeapons && !isSwitchingWeapons)
        {
            isSwitchingWeapons = true;
        }
        else
        {
            Debug.Log("Cannot switch, already switching");
            yield break;
        }
        Debug.Log("Switching weapons");
        yield return new WaitForSeconds(.5f);
        isSwitchingWeapons = false;
        SetActivePrefabs();
        Debug.Log("weapons have been switched");


        yield return null;
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
