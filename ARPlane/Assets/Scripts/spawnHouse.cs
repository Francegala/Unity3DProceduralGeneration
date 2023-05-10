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

[RequireComponent(typeof(ARRaycastManager))]
public class spawnHouse : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject myBuilding;
    
    public int largo = 4;
    public int lungo = 4;
    public int numofFloors = 1;
    public int numofBuildings = 1;
    

    public int counterBuildings = 0;

    private List<GameObject> buildingsList;
    public List<Vector3> coords;

    //Start Block for AR
    private ARRaycastManager _arRaycastManager;
    private Vector2 touchPosition;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    
    
    // End Block for AR

    
    private void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
        buildingsList = new List<GameObject>();
        coords = new List<Vector3>();
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

        if (!TryGetTouchPosition(out Vector2 touchPosition))return;
        if (_arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;
            
            
            //Start Block for AR
            lungo = (int)Random.Range(1, 5);
            largo = (int)Random.Range(1, 5);
            numofFloors = (int)Random.Range(1, 3);
            // End Block for AR
            
            foreach (var buildingCreated in buildingsList) {
                coords = new List<Vector3>();
                Destroy(buildingCreated);
                counterBuildings = 0;
            }
            
            
                var building =Instantiate(myBuilding);
                building.name = "Building"+(++counterBuildings);
                building.transform.parent = this.transform;
                buildingsList.Add(building);

                SpawnBuilding spawnBuilding = building.GetComponent<SpawnBuilding>();
                spawnBuilding.createBuildings(hitPose.position.x,hitPose.position.z);


        }

    }

   
    


}