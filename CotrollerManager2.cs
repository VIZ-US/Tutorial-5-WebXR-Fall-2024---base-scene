using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebXR;


public class ControllerManager : MonoBehaviour
{
    // Only using the Grip button for pickup/drop
    // [SerializeField] private WebXRController.ButtonTypes pickupButton = WebXRController.ButtonTypes.Grip;

    public WebXRController controller;  // Reference to WebXR controller
    private Rigidbody currentRigidBody = null;  // Object that is currently being grabbed
    private Vector3 currentVelocity;  // Velocity of the controller
    private Vector3 previousPosition;  // Previous position of the controller for velocity calculation
    private bool trigger = false; 
    // Start is called before the first frame update
    void Start()
    {
        // Make sure the WebXRController component is attached to the controller object
        // controller = gameObject.GetComponent<WebXRController>();
        // if (controller == null)
        // {
        //     Debug.LogError("WebXRController component missing! Please attach the WebXRController script.");
        //     return;  // Exit if the component is missing to avoid null reference errors
        // }

        previousPosition = transform.position;  // Initialize previous position for velocity calculation
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("This is a message from WebXR in Unity.");
        if (controller.GetAxis(WebXRController.AxisTypes.Trigger) > 0)
        //  || controller.GetAxis(WebXRController.AxisTypes.Grip) > 0);
        // Calculate the controller's velocity
        {
            trigger = true;
            Pickup();  // Grab the object
        }

        // Check if the pickup button (Grip) is released
        else if (trigger)
        {
            Drop();  // Drop the object
            trigger = false;
        }
         currentVelocity = (transform.position - previousPosition) / Time.deltaTime;
         previousPosition = transform.position;
    }

    // Handle picking up the object
    public void Pickup()
    {
        if (currentRigidBody == null)
            return;
        Debug.Log("test");
        Debug.Log(currentRigidBody);
        // Attach the object to the controller
        currentRigidBody.MovePosition(transform.position);
        // currentRigidBody.MoveRotation(transform.rotation);
        currentRigidBody.MoveRotation(transform.rotation);
        
        // currentRigidBody.transform.SetParent(transform);
        currentRigidBody.isKinematic = true;  // Disable physics while holding the object
    }

    // Handle dropping the object
    public void Drop()
    {
        if (currentRigidBody == null)
            return;

        // Detach the object from the controller
        // currentRigidBody.transform.SetParent(null);
        currentRigidBody.isKinematic = false;  // Re-enable physics when dropping the object
        currentRigidBody.velocity = currentVelocity;  // Apply the current velocity to the object to simulate throwing
        // currentRigidBody = null;  // Clear reference after dropping the object
    }

    // Detect when the controller touches a grabbable object
    private void OnTriggerEnter(Collider other)
    {
            Debug.Log(other.name);
        if (other.attachedRigidbody != null)
        {
            // Assign the object as the one to grab
            currentRigidBody = other.attachedRigidbody;
            Debug.Log(other.name);
        }
    }
    // private void OnTriggerStay(Collider other)
    // {
    //         Debug.Log(other.name);
    //     if (other.attachedRigidbody != null)
    //     {
    //         // Assign the object as the one to grab
    //         currentRigidBody = other.attachedRigidbody;
    //         Debug.Log(other.name);
    //     }
    // }
    // Detect when the controller stops touching a grabbable object
    private void OnTriggerExit(Collider other)
    {
        // if (other.attachedRigidbody != null && currentRigidBody == other.attachedRigidbody)
        {
            // Clear the reference when the controller leaves the object
            currentRigidBody = null;
        }
    }
}