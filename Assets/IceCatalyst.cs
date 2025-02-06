using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCatalyst : Bomb
{
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
                }
                Destroy(gameObject);
    }
}
