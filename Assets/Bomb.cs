using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Bomb : MonoBehaviour, IProjectile
{

    [SerializeField]
    private ParticleSystem impactEffect;
    private Rigidbody rb;
    private MeshRenderer mesh;

    [SerializeField]
    private float damage = 0f;
    [SerializeField]
    private float explodeRadius = 0f;
    [SerializeField]
    private float explodeForce = 0f;
    [SerializeField] private float projectileSpeed;
    public float speed { get => projectileSpeed; set => speed = value; }

    public Action TriggerBombExplode;

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
            impactEffect.transform.forward = -1 * transform.forward;
            impactEffect.Play();
            rb.velocity = Vector3.zero;
            
            Explode();
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
