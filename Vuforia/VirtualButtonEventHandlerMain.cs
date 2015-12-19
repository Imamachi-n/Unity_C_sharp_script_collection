using UnityEngine;
using System.Collections;
using Vuforia;

public class VirtualButtonEventHandlerMain : MonoBehaviour, IVirtualButtonEventHandler{

    // Private fields to store the models
    private GameObject model_1;

    private bool ButtonPressedSwitchRotateRight = false;
    private bool ButtonPressedSwitchRotateLeft = false;
    private bool ButtonPressedSwitchSizeUp = false;
    private bool ButtonPressedSwitchSizedown = false;
    private bool ButtonPressedSwitchReset = false;

    public float RotationSpeed = 70.0f;
    public float scalingSpeed = 1.0f;

    // Use this for initialization
    void Start () {
        // Search for all Children from this ImageTarget with type VirtualButtonBehaviour
        VirtualButtonBehaviour[] vbs = GetComponentsInChildren<VirtualButtonBehaviour>();
        for (int i = 0; i < vbs.Length; ++i)
        {
            // Register with the virtual buttons TrackableBehaviour
            vbs[i].RegisterEventHandler(this);
        }

        // Find the models based on the names in the Hierarchy
        model_1 = transform.FindChild("unitychan").gameObject;
        model_1.SetActive(true);
    }

    void Update()
    {
        if (ButtonPressedSwitchRotateRight)
        {
            model_1.transform.Rotate(0, -RotationSpeed * Time.deltaTime, 0);
        }
        if (ButtonPressedSwitchRotateLeft)
        {
            model_1.transform.Rotate(0, RotationSpeed * Time.deltaTime, 0);
        }
        if (ButtonPressedSwitchSizeUp)
        {
            model_1.transform.localScale += new Vector3(scalingSpeed, scalingSpeed, scalingSpeed);
        }
        if (ButtonPressedSwitchSizedown)
        {
            model_1.transform.localScale -= new Vector3(scalingSpeed, scalingSpeed, scalingSpeed);
            if (model_1.transform.localScale.x <= 0)
            {
                ButtonPressedSwitchSizedown = false;
            }
            else
            {
                model_1.transform.localScale -= new Vector3(scalingSpeed, scalingSpeed, scalingSpeed);
            }
        }
        if (ButtonPressedSwitchReset)
        {
            model_1.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            Vector3 targetPos = new Vector3(0,0,0);
            model_1.transform.LookAt(targetPos);
            ButtonPressedSwitchReset = false;
        }
        //RotationSpeed += Time.deltaTime;
    }

    /// <summary>
    /// Called when the virtual button has just been pressed:
    /// </summary>
    public void OnButtonPressed(VirtualButtonAbstractBehaviour vb) {
        switch (vb.VirtualButtonName)
        {
            case "RotateRight":
                //model_1.SetActive(false);
                //model_1.SetActive(true);
                //RotationSpeed += 45.0f;
                //model_1.transform.Rotate(0, RotationSpeed, 0);
                //iTween.RotateTo(model_1, iTween.Hash("y", 360, "time", 6.0f));
                ButtonPressedSwitchRotateRight = true;
                Debug.Log("Button pressed!");
                break;
            case "RotateLeft":
                //RotationSpeed -= 45.0f;
                //model_1.transform.Rotate(0, -RotationSpeed, 0);
                //iTween.RotateTo(model_1, iTween.Hash("y", -360, "time", 6.0f));
                ButtonPressedSwitchRotateLeft = true;
                Debug.Log("Button pressed!");
                //model_1.SetActive(false);
                break;
            case "SizeUp":
                ButtonPressedSwitchSizeUp = true;
                Debug.Log("Button pressed!");
                break;
            case "SizeDown":
                ButtonPressedSwitchSizedown = true;
                Debug.Log("Button pressed!");
                break;
            case "Reset":
                ButtonPressedSwitchReset = true;
                Debug.Log("Button pressed!");
                break;
        }
    }

    /// <summary>
    /// Called when the virtual button has just been released:
    /// </summary>
    public void OnButtonReleased(VirtualButtonAbstractBehaviour vb) {
        Debug.Log("Button released!");
        ButtonPressedSwitchRotateRight = false;
        ButtonPressedSwitchRotateLeft = false;
        ButtonPressedSwitchSizeUp = false;
        ButtonPressedSwitchSizedown = false;
        ButtonPressedSwitchReset = false;
    }

}
