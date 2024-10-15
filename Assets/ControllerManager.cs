using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebXR;

public class ControllerManager : MonoBehaviour
{
    public WebXRController controller;  // Reference to WebXR controller
    private Rigidbody currentRigidBody = null;  // Object that is currently being grabbed
    private Vector3 currentVelocity;  // Velocity of the controller
    private Vector3 previousPosition;  // Previous position of the controller for velocity calculation
    private bool isGrip = false;

    // Start is called before the first frame update
    void Start()
    {
        previousPosition = transform.position;  // Initialize previous position for velocity calculation
    }

    void Update(){
        if (controller.GetAxis(WebXRController.AxisTypes.Grip) > 0)
        {
            Pickup();  // Grab the object
            isGrip = true;
        }
        // Check if the pickup button (Grip) is released
        else if (isGrip)
        {
            Drop();  // Drop the object
            isGrip = false;
        }
         currentVelocity = (transform.position - previousPosition) / Time.deltaTime;
         previousPosition = transform.position;
    }

    // Handle picking up the object
    public void Pickup()
    {
        if (currentRigidBody == null)
            return;
        
        currentRigidBody.MovePosition(transform.position);
        currentRigidBody.MoveRotation(transform.rotation);
        
        currentRigidBody.isKinematic = true;  // Disable physics while holding the object
    }

    // Handle dropping the object
    public void Drop()
    {          
        if (currentRigidBody == null)
            return;

        currentRigidBody.isKinematic = false;  // Re-enable physics when dropping the object
        currentRigidBody.velocity = currentVelocity;  // Apply the current velocity to the object to simulate throwing
    }


    // Detect when the controller touches a grabbable object
    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            // Assign the object as the one to grab
            currentRigidBody = other.attachedRigidbody;
        }
    }

    // Detect when the controller stops touching a grabbable object
    private void OnTriggerExit(Collider other)
    {
        // Clear the reference when the controller leaves the object
        currentRigidBody = null;
    }
}
