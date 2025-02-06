using UnityEngine;

public class CollectableBobbing : MonoBehaviour
{
    public float amplitude = 0.5f; // How high the bobbing goes

    public float frequency = 1f; // How fast the bobbing occurs



    private float timeCounter = 0f;



    void Update()

    {

        timeCounter += Time.deltaTime; 



        float bobOffset = amplitude * Mathf.Sin(timeCounter * frequency); // Calculate the bobbing offset



        transform.position = new Vector3(transform.position.x, transform.position.y + bobOffset, transform.position.z); 

    }


}
