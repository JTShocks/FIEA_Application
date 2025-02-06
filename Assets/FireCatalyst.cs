using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCatalyst : Bomb
{

    [SerializeField] internal float explodeForce = 10;

    public override void OnCollisionEnter(Collision other)
    {

        rb.velocity = Vector3.zero;
        contactNormal = other.contacts[0].normal;
        rb.isKinematic = true;
        Explode();
    }

    internal override void OnExplode()
    {
        base.OnExplode();
            Collider[] numHit = Physics.OverlapSphere(rb.position, explodeRadius);

            foreach(Collider hit in numHit)
            {
                Vector3 contact = hit.ClosestPoint(transform.position);

                
                IReactable reactable = hit.GetComponent<IReactable>();
                if(reactable != null)
                {
                    reactable.React(bombElement);
                    continue;
                }



                Vector3 dir = contact - hit.transform.position;
                dir = -dir.normalized;
                Debug.Log("Knock back direction: " + dir);
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if(rb != null)
                {
                    rb.AddForce( dir * explodeForce, ForceMode.Impulse);
                }
                
            }
            Destroy(gameObject);
    }
}
