using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
//Start Block for AR
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
// End Block for AR

public class spawnHouseBuilding : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject myBuilding;
    private List<GameObject> buildingsList  = new List<GameObject>();
    
    
    //Start Block for AR
    private ARRaycastManager _arRaycastManager;
    private Vector2 touchPosition;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    public bool pauseGenerating = false;

    // End Block for AR

    private void Awake()
    {
        GameObject.Find("PauseButton").GetComponentInChildren<Text>().text = "Click here to Pause";
        _arRaycastManager = this.transform.parent.GetComponent<ARRaycastManager>();
        buildingsList = new List<GameObject>();
        pauseGenerating = false;
        GameObject.Find("PauseButton").GetComponent<Button>().onClick.AddListener(Pause);
    }
    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
        
    }

    private void Update()
    {
        if (!pauseGenerating)
        {
            if (!TryGetTouchPosition(out Vector2 touchPosition)) return;
            if (_arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
            {
                
                GameObject.Find("EventSystem").GetComponent<EventToggle>().ShowGenerate();

                var hitPose = hits[0].pose;

                if (buildingsList.Count > 0)
                {
                    foreach (var buildingCreated in buildingsList)
                    {
                        Destroy(buildingCreated);
                    }
                }
                

                var building = Instantiate(myBuilding);
                building.name = "Building";
                buildingsList.Add(building);
                int size = Random.Range(1, 3);
                building.GetComponent<SpawnBuilding>()
                    .createBuildings(hitPose.position.x, hitPose.position.z, size + 1, size, size + 2);
                building.transform.parent = this.gameObject.transform;
                building.AddComponent<Light>();
                building.GetComponent<Light>().type = LightType.Point;
                building.GetComponent<Light>().range = 10;
                building.GetComponent<Light>().intensity = 1;

            }
        }
    }
    
    public void Pause()
    {
        if (pauseGenerating == false)
        {
            pauseGenerating = true;
            GameObject.Find("PauseButton").GetComponentInChildren<Text>().text = "In Pause";
        }
        else
        {
            pauseGenerating = false;
            GameObject.Find("PauseButton").GetComponentInChildren<Text>().text = "Click here to Pause";

        }
    }

}