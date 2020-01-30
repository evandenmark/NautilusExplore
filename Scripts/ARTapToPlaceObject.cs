using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]

public class ARTapToPlaceObject : MonoBehaviour
{
    /// <summary>
    /// This controls the placement of Augmented Reality objects and the placement logo. 
    /// </summary>
    ///

    [SerializeField]

    public GameObject placementIndicator;
    public GameObject hercGameObj;
    public GameObject argusGameObj;
    public MenuScript menuScript;

    private ARRaycastManager arRaycastManager;
    private Pose placementPose;
    private bool placementPoseIsValid = false;
    private bool hercIsPlaced = false;
    private bool argusIsPlaced = false;
    private GameObject instantiatedHerc;
    private GameObject instantiatedArgus;

    public enum VehicleStates { Herc, Argus };
    public VehicleStates currentVehicleState;

    // Start is called before the first frame update
    void Start()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        currentVehicleState = VehicleStates.Herc;
    }

    // Update is called once per frame
    void Update()
    {
        if (menuScript.currentMenuState == MenuScript.MenuStates.Vehicle)
        {
            UpdatePlacementPose();
            UpdatePlacementIndicator();

        }
    }

    public void DestroyOrPlaceObject()
    {
        Debug.Log("A");
        if (currentVehicleState == VehicleStates.Herc)
        {
            Debug.Log("B");
            if (hercIsPlaced)
            {
                Debug.Log("C");
                DestroyHerc();
            } else
            {
                Debug.Log("D");
                placeHerc();
            }
        } else if (currentVehicleState == VehicleStates.Argus)
        {
            Debug.Log("E");
            if (argusIsPlaced)
            {
                Debug.Log("F");
                DestroyArgus();
            }
            else
            {
                Debug.Log("G");
                placeArgus();
            }
        }
    }

    public void placeHerc()
    {
        instantiatedHerc = Instantiate(hercGameObj, placementPose.position, placementPose.rotation);
        hercIsPlaced = true;
    }

    public void placeArgus()
    {
        instantiatedArgus = Instantiate(argusGameObj, placementPose.position, placementPose.rotation);
        argusIsPlaced = true;
    }

    private void UpdatePlacementIndicator()
    {
        //controls the position of the placement logo on the plane
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }

    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        arRaycastManager.Raycast(screenCenter, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
        placementPoseIsValid = hits.Count > 0;

        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;
            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }

    public void DestroyHercAndArgus()
    {
        //upon exiting the AR session, all vehicles are destroyed
        DestroyHerc();
        DestroyArgus();
    }

    public void DestroyHerc()
    {
        if (hercIsPlaced)
        {
            Destroy(instantiatedHerc);
            hercIsPlaced = false;
        }
    }

    public void DestroyArgus()
    {
        if (argusIsPlaced)
        {
            Destroy(instantiatedArgus);
            argusIsPlaced = false;
        }
    }

    public void TransitionToHercVehicleState()
    {
        currentVehicleState = VehicleStates.Herc;
    }

    public void TransitionToArgusVehicleState()
    {
        currentVehicleState = VehicleStates.Argus;
    }

    public void scaleVehicle(float value)
    {
        if (currentVehicleState == VehicleStates.Herc && hercIsPlaced)
        {
            instantiatedHerc.transform.localScale = new Vector3(value, value, value);

        } else if (currentVehicleState == VehicleStates.Argus && argusIsPlaced)
        {
            instantiatedArgus.transform.localScale = new Vector3(value, value, value);
        }
    }

}
