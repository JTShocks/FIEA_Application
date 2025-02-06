using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueBomb : Bomb
{
    [SerializeField] GameObject glueEffectPrefab;

    internal override void OnExplode()
    {
        base.OnExplode();


        //Spawn the prefab as a child of the hit object.
        //Get the component that reacts to IGlueable

        //When you glue a creature or object, it increments the amount of glue needed to freeze
        //When max glue is reached, the target calls the glue function to freeze movement/ movement multiplier is set to 0

        //Acts like cobwebs and makes the player fall slower

        //Can create a prefab that holds all of these values and makes it easier to edit fine details
        Quaternion spawnRotation = Quaternion.FromToRotation(Vector3.up, contactNormal);
        Instantiate(glueEffectPrefab, transform.position, spawnRotation);
    }
}
