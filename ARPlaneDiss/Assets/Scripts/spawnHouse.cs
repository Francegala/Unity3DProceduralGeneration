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
public class spawnHouse : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject myBuilding;
    private List<GameObject> buildingsList  = new List<GameObject>();
    public GameObject cityVisualiser;
    
    //Start Block for AR
    private ARRaycastManager _arRaycastManager;
    private Vector2 touchPosition;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    // End Block for AR
    
    private void Awake()
    {
        _arRaycastManager = this.transform.parent.GetComponent<ARRaycastManager>();
        buildingsList = new List<GameObject>();
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
        if (
            (GameObject.Find("Visualiser") == null)||
            ((GameObject.Find("Visualiser").GetComponent<Visualiser>().pauseRepeating == false)&&
            (GameObject.Find("Visualiser").GetComponent<Visualiser>().pauseButton == false))
        )
        {
            if (!TryGetTouchPosition(out Vector2 touchPosition)) return;
            if (_arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
            {
                var hitPose = hits[0].pose;
                if (GameObject.Find("Visualiser") != null)
                {
                    Destroy(GameObject.Find("Visualiser"));
                }                
                var visualiser = Instantiate(cityVisualiser, hitPose.position, Quaternion.identity,this.gameObject.transform).name="Visualiser";
                this.transform.position =
                    new Vector3(hitPose.position.x-10, hitPose.position.y - 10, hitPose.position.z-10);
            }
        }
    }

    public void Create(int lungoVar, int largoVar, int floorsVar)
    {
        if (buildingsList.Count > 0)
        {
            foreach (var buildingCreated in buildingsList) {
                Destroy(buildingCreated);
            }
        }
        var building =Instantiate(myBuilding);
            building.name = "Building";
            buildingsList.Add(building);
            building.GetComponent<SpawnBuilding>().createBuildings(0,0, lungoVar,  largoVar,  floorsVar);
            building.transform.localScale /= Random.Range(3,5);
        
    }

    public void DestroyBase()
    {
        if (buildingsList.Count > 0)
        {
            foreach (var buildingCreated in buildingsList) {
                Destroy(buildingCreated);
            }
        }   
    }
}