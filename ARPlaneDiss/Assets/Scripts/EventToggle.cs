using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;

public class EventToggle : MonoBehaviour
{

    public static bool city = false;
    public GameObject spawn;
    public GameObject ground;
    public Text fpsDisplay;

    // Start is called before the first frame update
    void Start()
    {
        fpsDisplay = GameObject.Find("FPSCount").GetComponent<Text>();
        InvokeRepeating("ShowFPS", 0.3f, 0.5f); // if I added this to update it changes fps text on the screen to fast and numbers were changing too fast and it looked horrible like numbers one above the other
        // 0.3 is a delay to allow the game to load and the start calculating fps
        // 0.5 means to change twice per second, it is fast but still less frequent than using update
        
        Invoke("AutoStart", 5.0f);
    }

    // Update is called once per frame so create a function to be called less frequent
    void ShowFPS()
    {
        float fps = 1 / Time.unscaledDeltaTime; // calculate files per second
        // display on the UI text
        fpsDisplay.text = "FPS: " + fps.ToString("0");
    }

    public void RemoveOnScreen()
    {
        if (GameObject.Find("Building") != null)
        {
            Destroy(GameObject.Find("Building"));
        }
       
        if (GameObject.Find("Visualiser") != null)
        {
            GameObject.Find("UI").GetComponent<Text>().text = "DEMOLITION IN PROGRESS";
            Destroy(GameObject.Find("Visualiser"));
            Invoke("StopShow", 5.0f);
        }

    }

    public void ClickButton()
    {
        
        if (GameObject.Find("Building") != null)
        {
            Destroy(GameObject.Find("Building"));
        }
        if (GameObject.Find("Spawn") != null)
        {
            Destroy(GameObject.Find("Spawn"));
        }
        if (GameObject.Find("Visualiser") != null)
        {
            Destroy(GameObject.Find("Visualiser"));
        }
        if (GameObject.Find("Ground") != null)
        {
            Destroy(GameObject.Find("Ground"));
        }
        
        if(city==true)
          {
              GameObject.Find("UI").GetComponent<Text>().text = "" ;
              Instantiate(spawn, Vector3.zero,Quaternion.identity,GameObject.Find("AR Session Origin").transform).name="Spawn";
             GameObject.Find("Button").GetComponent<Button>().GetComponentInChildren<Text>().text = "Click this button to be \n able to generate a \n city"; //as now you were spawinign a city and after clicking you can spawn a building
             city = false;
          }
          else
          {
              GameObject.Find("UI").GetComponent<Text>().text = "" ;
              Instantiate(ground, Vector3.zero,Quaternion.identity,GameObject.Find("AR Session Origin").transform).name="Ground";
              GameObject.Find("Button").GetComponent<Button>().GetComponentInChildren<Text>().text =  "Click this button to be \n able to generate a \n building"; //as now you are spawning a city and you need to tell that clicking they can spawn a building
              city = true;
          }
    }


    public void ShowGenerate()
    {
        GameObject.Find("UI").GetComponent<Text>().text = "GENERATING,\n HOLD ON" ;
        
    }
    
    public void StopShow()
    {
        GameObject.Find("UI").GetComponent<Text>().text = "" ;
    }
    
    public void ExitGame() {
        Application.Quit();
    }
    public void InfoVisible()
    {
        GameObject.Find("IntroPanel").GetComponent<Image>().color = new Color(0.0f,0.0f,0.0f,1.0f);
        GameObject.Find("IntroPanel").GetComponentInChildren<Text>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        GameObject.Find("IntroPanel").GetComponentInChildren<Text>().text = "Click on the purple area to generate. \n the red button to pause generation\n  the last one to change elements.";
        Invoke("InfoInvisible", 3.0f);
    }
    
    public void InfoInvisible()
    {
        GameObject.Find("IntroPanel").GetComponent<Image>().color = new Color(0.0f,0.0f,0.0f,0.0f);
        GameObject.Find("IntroPanel").GetComponentInChildren<Text>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    }

    public void StartBuilding()
    {
        InfoInvisible();
        Instantiate(spawn, Vector3.zero,Quaternion.identity,GameObject.Find("AR Session Origin").transform).name="Spawn";
        Destroy(GameObject.Find("CityButton"));
        Destroy(GameObject.Find("BuildingButton"));
        GameObject.Find("Button").GetComponent<Button>().GetComponentInChildren<Text>().text = "Click this button to be \n able to generate a \n city"; //as now you were spawinign a city and after clicking you can spawn a building
        city = false;
    }
    public void StartCity()
    {
        InfoInvisible();
        Instantiate(ground, Vector3.zero,Quaternion.identity,GameObject.Find("AR Session Origin").transform).name="Ground";
        Destroy(GameObject.Find("CityButton"));
        Destroy(GameObject.Find("BuildingButton"));
        GameObject.Find("Button").GetComponent<Button>().GetComponentInChildren<Text>().text =  "Click this button to be \n able to generate a \n building"; //as now you are spawning a city and you need to tell that clicking they can spawn a building
        city = true;
    }
    
    public void AutoStart()
    {
        if (GameObject.Find("CityButton").activeSelf || GameObject.Find("BuildingButton").activeSelf) StartBuilding();
    }

    
}
