using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bomb : MonoBehaviour, IProjectile
{

    [SerializeField]
    private ParticleSystem impactEffect;
    private Rigidbody rb;
    private MeshRenderer mesh;

    [Header("Generic Bomb Fields")]
    [SerializeField]
    private float damage = 0f;
    [SerializeField]
    private float explodeRadius = 0f;
    [SerializeField] private float projectileSpeed = 10f;

    [HideInInspector]
    public float speed { get => projectileSpeed; set => speed = value; }

    public Action TriggerBombExplode;

    internal Vector3 contactNormal;

    void OnEnable()
    {
        TriggerBombExplode += OnExplode;
    }
    void OnDisable()
    {
        TriggerBombExplode -= OnExplode;
    }
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mesh = GetComponent<MeshRenderer>();

    }
    public void OnCollisionEnter(Collision other)
    {
        if(other.collider.material.bounciness <= .1f)
        {
            //Only explode if the hit is something non-bouncy
            //impactEffect.transform.forward = -1 * transform.forward;
            //impactEffect.Play();
            rb.velocity = Vector3.zero;
            contactNormal = other.contacts[0].normal;
            Explode();
        }

    }

    public void OnParticleSystemStopped()
    {
        Destroy(gameObject);
    }

    public void Shoot(Vector3 position, Vector3 direction)
    {
        rb.velocity = Vector3.zero;
        transform.position = position;
        transform.forward = direction;

        rb.AddForce(direction * speed, ForceMode.VelocityChange);
    }

    void Explode()
    {
        //Invoke the event for everything on the object that might care about the explosion
        TriggerBombExplode?.Invoke();
    }

    internal virtual void OnExplode()
    {
        //Logic for what happens when the bomb explodes
    }
}
