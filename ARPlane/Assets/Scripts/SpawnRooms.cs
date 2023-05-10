using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRooms : MonoBehaviour
{
    private int numofRooms;
    private int counterRooms = 0 ;
    public GameObject myPrefab;
    private SpawnBuilding spawnB;

    public void createFloor(spawnHouse spawnH, float z, float copyX, float copyY, float copyZ)
    {
        spawnB = gameObject.GetComponentInParent<global::SpawnBuilding>();
        Vector3 position;
        counterRooms = 0;
        numofRooms = spawnH.largo * spawnH.lungo;

        for (int i = 1; i <= (numofRooms); i++)
        {
            if (copyZ == z+spawnH.lungo)
            {
                copyX +=1;
                copyZ = z;
            }
            position = new Vector3(copyX, copyY, copyZ);
            createRoom(position,this.gameObject,spawnH);
            copyZ++;

        }
        
    }
    
    
    private bool checkExist(Vector3 newPosition, spawnHouse spawnH)
    {
        foreach (var coordinata in spawnH.coords)
        {
            if (coordinata.x == newPosition.x && coordinata.y == newPosition.y && coordinata.z == newPosition.z)return false;
        }

        return true;
    }
    
    private void createRoom(Vector3 position,GameObject Floor,spawnHouse spawnH)
    {
     Vector3 newPosition;
     var Room = new GameObject("Room"+spawnH.counterBuildings+"."+spawnB.counterFloors+"."+(++counterRooms));
     Room.transform.parent = Floor.transform;
            
            newPosition = new Vector3(position.x, (position.y), position.z);
            if (checkExist(newPosition, spawnH))
            {
                spawnH.coords.Add(newPosition);
                var southWall = Instantiate(myPrefab, newPosition, Quaternion.identity);
                southWall.name = "Wall South";
                southWall.transform.parent = Room.transform;
            }

            newPosition = new Vector3(position.x, (position.y), (position.z + 1f));
            if (checkExist(newPosition, spawnH))
            {
                spawnH.coords.Add(newPosition);
                var estWall = Instantiate(myPrefab, newPosition, Quaternion.identity);
                estWall.name = "Wall East";
                estWall.transform.parent = Room.transform;
            }


            newPosition = new Vector3((position.x - .5f), (position.y), (position.z + .5f));
            if (checkExist(newPosition, spawnH))
            {
                spawnH.coords.Add(newPosition);
                var westWall = Instantiate(myPrefab, newPosition, Quaternion.Euler(new Vector3(0, 90, 0)));
                westWall.name = "Wall West";
                westWall.transform.parent = Room.transform;
            }


            newPosition = new Vector3((position.x + .5f), (position.y), (position.z + .5f));
            if (checkExist(newPosition, spawnH))
            {
                spawnH.coords.Add(newPosition);
                var northWall = Instantiate(myPrefab, newPosition, Quaternion.Euler(new Vector3(0, 90, 0)));
                northWall.name = "Wall North";
                northWall.transform.parent = Room.transform;
            }
     

    }
}
