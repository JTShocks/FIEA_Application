using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueBall : MonoBehaviour, IReactable
{

    [SerializeField] float speedClamp = 0.5f;
    public void OnReaction()
    {
        throw new System.NotImplementedException();
    }

    public void React(Bomb.Element element)
    {

        switch(element)
        {
            case Bomb.Element.Ice:
                //Turn enemy into a ice cube
            break;
            case Bomb.Element.Fire:
                //Melt the glue
                Destroy(gameObject);
            break;
        }
    }

    void OnCollisionStay(Collision other)
    {
        Rigidbody rb = other.collider.GetComponent<Rigidbody>();

        if(rb!=null)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, speedClamp);
        }

    }
}
