using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using WebXR;

public class HandModel : MonoBehaviour
{
    public GameObject controllerPrefab; 
    private GameObject spawnedController;
    public GameObject handModelPrefab; 
    private GameObject spawnedHandModel;

    public WebXRController controller;  // Reference to WebXR controller
    private Animator handAnimator;
    
    private bool showController = false;
    // Start is called before the first frame update
    void Start()
    {
        spawnedController = Instantiate(controllerPrefab, transform);
        spawnedHandModel = Instantiate(handModelPrefab, transform);
        handAnimator = spawnedHandModel.GetComponent<Animator>();
        spawnedController.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
         UpdateAnimator();
    }
        // if(showController)
        // {
        //     spawnedHandModel.SetActive(false);
        //     spawnedController.SetActive(true);
        // }
        // else{
        //     spawnedHandModel.SetActive(true);
        //     spawnedController.SetActive(false);
           
        // }
        // if (controller.GetAxis(WebXRController.AxisTypes.Grip) > 0)
        // {
        //     Debug.Log("test1");
        // }
    void UpdateAnimator()
    {
        handAnimator.SetFloat("Grip", controller.GetAxis(WebXRController.AxisTypes.Grip));
        handAnimator.SetFloat("Trigger", controller.GetAxis(WebXRController.AxisTypes.Trigger));
    }
}
