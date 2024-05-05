using System.Collections;
using UnityEngine;
//using static UnityEditor.Progress;

public class PlayerControl: MonoBehaviour
{
    public Inventory inventory;

    public float reachDistance = 100f;//orginal was 5f

    public LayerMask itemLayer;

    public Rigidbody playerRigidbody;// Reference to the player's Rigidbody component

    public PlayerStateManager stateManager; // Reference to the PlayerStateManager script

    public float boxRemovedCounter;

    // cached reference
    ChangeScene sceneloader;


    public enum RotationAxis
    {
        MouseX = 1,
        MouseY = 2
    }

    public RotationAxis axes = RotationAxis.MouseX;

    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;

    public float sensHorizontal = 10.0f;
    public float sensVertical = 10.0f;

    public float _rotationX = 0;

    void Start()
    {
        inventory = new Inventory();

        stateManager = FindObjectOfType<PlayerStateManager>();//Code to get rid of Null
        if (stateManager == null)
        {
            Debug.LogError("PlayerStateManager not found in the scene!");
        }

        sceneloader = FindObjectOfType<ChangeScene>();

        boxRemovedCounter = 0;
    }

    void Update()
    {
        if (axes == RotationAxis.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensHorizontal, 0);
        }
        else if (axes == RotationAxis.MouseY)
        {
            _rotationX -= Input.GetAxis("Mouse Y") * sensVertical;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert); //Clamps the vertical angle within the min and max limits (45 degrees)

            float rotationY = transform.localEulerAngles.y;

            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            PickUpItem();
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            DropItem();
            boxRemovedCounter++;
        }
        
        //This is here for when the plyaer is going to grab the box
        GameObject stickObject = GameObject.Find("Stick");
        if(Input.GetKeyDown(KeyCode.E) && inventory.items.Contains(stickObject))
        {
            PickUpItemBox();
        }

        if(boxRemovedCounter >= 3) 
        {
            WinGame();
        }
       
    }

    void PickUpItem()
    {
        // Raycast to detect items within reach
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, reachDistance))
        {
            GameObject item = hit.collider.gameObject;
            Debug.Log("Hit: " + item.name); // Add this line to check if the raycast hits an object
            Debug.Log("Object tag: " + item.tag); // Add this line to check the object's tag

            // Check if the item can be picked up
            if (item.CompareTag("Pickupable"))
            {
                // Add item to inventory
                inventory.AddItem(item);
                stateManager.SwitchState(stateManager.HasStick);//Test:This is to see if it actually swithes it state 


                //Top foreach
                foreach (GameObject go in inventory.items)
                {
                    print(go.name);
                }
                //Bottom foreach

                // Disable item's renderer and collider
                item.GetComponent<Renderer>().enabled = false;
                item.GetComponent<Collider>().enabled = false;

                // Enable item's Rigidbody to apply gravity
                Rigidbody itemRigidbody = item.GetComponent<Rigidbody>();
                if (itemRigidbody != null)
                {
                    itemRigidbody.isKinematic = false;
                    itemRigidbody.useGravity = true;
                }
                
            }

        }
    }

    void DropItem()
    {
        if (inventory.items.Count > 0)
        {
            // Get the last item from the inventory
            GameObject item = (GameObject)inventory.items[inventory.items.Count - 1];

            // Enable item's renderer and collider
            item.GetComponent<Renderer>().enabled = true;
            item.GetComponent<Collider>().enabled = true;

            //Disable item's Rigidbody to stop applying gravity
            Rigidbody itemRigidbody = item.GetComponent<Rigidbody>();
            if (itemRigidbody != null ) 
            {
                itemRigidbody.isKinematic = false;
                itemRigidbody.useGravity = true;
            }

            // Set item's position near the player
            Vector3 dropOffset = new Vector3(0f, 0.5f, 1.5f); // Adjust as needed
            item.transform.position = transform.position + transform.forward + dropOffset;

            // Remove item from inventory
            inventory.RemoveItem(item);
        }
    }
    void PickUpItemBox()
    {// Raycast to detect items within reach
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, reachDistance))
        {
            GameObject item = hit.collider.gameObject;
            Debug.Log("Hit: " + item.name); // Add this line to check if the raycast hits an object
            Debug.Log("Object tag: " + item.tag); // Add this line to check the object's tag

            // Check if the item can be picked up
            if (item.CompareTag("StickPickupable"))
            {
                // Add item to inventory
                inventory.AddItem(item);

                //Top foreach
                foreach (GameObject go in inventory.items)
                {
                    print(go.name);
                }
                //Bottom foreach

                // Disable item's renderer and collider
                item.GetComponent<Renderer>().enabled = false;
                item.GetComponent<Collider>().enabled = false;
                //item.SetActive(false);

                GameObject.FindGameObjectWithTag("Jammed").GetComponent<ConveyorBelt>().RemoveFromConveyorList(item);

                // Enable item's Rigidbody to apply gravity
                Rigidbody itemRigidbody = item.GetComponent<Rigidbody>();
                if (itemRigidbody != null)
                {
                    itemRigidbody.isKinematic = false;
                    itemRigidbody.useGravity = true;
                }

            }

        }
    }

    void WinGame() 
    {
        sceneloader.ChangeSceneFunction("Win Screen");
    }
}
