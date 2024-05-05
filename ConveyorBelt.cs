using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Progress;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField]
    private float speed, conveyorSpeed;
    [SerializeField]
    private Vector3 direction;
    [SerializeField]
    private List<GameObject> onBelt;

    private Material material;

    private float initialSpeed, initialConveyorSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //Create an instance of this texture this should only be necessary if the belts are using the same material and are moving different speeds
        material = GetComponent<MeshRenderer>().material;

        initialSpeed = speed;
        initialConveyorSpeed = conveyorSpeed;

    }

    // Update is called once per frame
    private void Update()
    {
        // Move the conveyor belt texture to make it look like it's moving
        material.mainTextureOffset += new Vector2(0, 1) * conveyorSpeed * Time.deltaTime;
    }

    // Fixed update for physics
    void FixedUpdate()
    {
        // For every item on the belt, add force to it in the direction given
        //for (int i = 0; i <= onBelt.Count - 1; i++)
        //{
        //    onBelt[i].GetComponent<Rigidbody>().AddForce(speed * direction);
        //}

        foreach (GameObject go in onBelt)
        {
            print(go.name);
            print(speed * direction);
            go.GetComponent<Rigidbody>().AddForce(speed * direction);
        }

        if (onBelt.Count >= 3 && this.CompareTag("Jammed"))//Jammed
        {
            JammedConveyorBelt();//TestOne
        } else
        {

            ResetConveyor();

        }
    }

    // When something collides with the belt
    private void OnCollisionEnter(Collision collision)
    {
        onBelt.Add(collision.gameObject);
    }

    // When something leaves the belt
    private void OnCollisionExit(Collision collision)
    {
        onBelt.Remove(collision.gameObject);
    }
    public void JammedConveyorBelt()
    {
            speed = 0;
            conveyorSpeed = 0;
            material.mainTextureOffset = new Vector2(0, 0) * conveyorSpeed * Time.deltaTime;
    }

    public void UnJammedConveyorBelt()
    {
        speed = 4f;
        conveyorSpeed = 0.25f;
        material.mainTextureOffset += new Vector2(0, 1) * conveyorSpeed * Time.deltaTime;
    }

    public void ResetConveyor()
    {

        speed = initialSpeed;
        conveyorSpeed = initialConveyorSpeed;
        material.mainTextureOffset += new Vector2(0, 1) * conveyorSpeed * Time.deltaTime;

    }

    public void RemoveFromConveyorList(GameObject box)
    {

        foreach(GameObject item in onBelt)
        {

            if (box ==  item)
            {

                onBelt.Remove(item);

            }

        }


    }
}