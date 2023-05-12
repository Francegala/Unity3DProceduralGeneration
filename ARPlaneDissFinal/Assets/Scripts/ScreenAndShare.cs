using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ScreenAndShare : MonoBehaviour
{
    public static int countScreenshot =0;
    public void ScreenshotAndShare()
    {
        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0,0,Screen.width,Screen.height),0,0);
        ss.Apply();

        string filePath = Path.Combine(Application.temporaryCachePath, "ShareImg.png");
        File.WriteAllBytes(filePath,ss.EncodeToPNG());
        Destroy(ss);
        
        new NativeShare()
            .AddFile(filePath)
            .SetSubject("")
            .SetText("")
            .SetCallback((res, tar)=>Debug.Log($"result: {res},selected app: {tar}"))
            .Share();
    }
    
    public void ScreenshotAndSave()
    {
        countScreenshot++;
        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0,0,Screen.width,Screen.height),0,0);
        ss.Apply();

        // Save the screenshot to Gallery/Photos
        string name = string.Format("{0}_Capture{1}_{2}.png", Application.productName, countScreenshot, System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
        NativeGallery.SaveImageToGallery(ss, Application.productName + " Captures", name);

        GameObject.Find("SaveText").GetComponentInChildren<Text>().color = new Color(1.0f,0.0f,0.0f,1.0f);
        Invoke("EmptyMessage", 3.0f);
        Destroy(ss);

    }

    void EmptyMessage()
    {
        GameObject.Find("SaveText").GetComponentInChildren<Text>().color = new Color(1.0f,0.0f,0.0f,0.0f);
    }
}
