using UnityEngine;

public interface IProjectile
{
    float speed {get;set;}
    void Shoot(Vector3 position, Vector3 direction, float speed);
    void OnCollisionEnter(Collision other);
    void OnParticleSystemStopped();
}
