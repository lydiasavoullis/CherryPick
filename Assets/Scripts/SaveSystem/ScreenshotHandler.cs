using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotHandler : MonoBehaviour
{
    private static ScreenshotHandler instance;
    private Camera myCamera;
    private bool takeScreenshotOnNextFrame;
    private string screenshotName;
    private void Awake()
    {
        instance = this;
        myCamera = gameObject.GetComponent<Camera>();
    }
    private void OnPostRender()
    {
        if (takeScreenshotOnNextFrame) {
            takeScreenshotOnNextFrame = false;
            RenderTexture renderTexture = myCamera.targetTexture;
            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0,0,renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);
            byte[] byteArray = renderResult.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.persistentDataPath +"/saves/" +$"/{screenshotName}.png", byteArray);
            Debug.Log("Save screenshot");
            RenderTexture.ReleaseTemporary(renderTexture);
            myCamera.targetTexture = null;
         }
        
    }
    public void TakeScreenshot(int width, int height, string name) {
        screenshotName = name;
        myCamera.targetTexture = RenderTexture.GetTemporary(width,height,16);
        takeScreenshotOnNextFrame = true;
    }
    public static IEnumerator TakeScreenshot_Static(GameObject saveMenu, string name) {
        saveMenu.SetActive(false);
        string filepath = Application.persistentDataPath + "/saves/" + $"/{name}.png";
        ScreenCapture.CaptureScreenshot(filepath, 1);
        yield return new WaitForEndOfFrame();
        //instance.TakeScreenshot(width, height, name);
        saveMenu.SetActive(true);
    }
}
