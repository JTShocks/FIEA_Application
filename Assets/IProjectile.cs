using UnityEngine;

public interface IProjectile
{
    void Shoot(Vector3 position, Vector3 direction, float speed);
    void OnCollisionEnter(Collision other);
    void OnParticleSystemStopped();
}
