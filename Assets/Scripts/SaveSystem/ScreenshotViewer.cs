using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ScreenshotViewer : MonoBehaviour
{
    //this goes on every save file button
    GameObject screenshowViewer;
    Image image;
    void Start()
    {
        screenshowViewer = GameObject.FindGameObjectWithTag("ScreenshotViewer");
        image = screenshowViewer.GetComponent<Image>();
    }
    public void ShowScreenshot() {
        
        string filename = this.GetComponent<TextMeshProUGUI>().text;
        string filepath = Application.persistentDataPath + "/" + "saves" +"/"+ filename + ".png";
        ;
        if (File.Exists(filepath)) {
            byte[] bytes = File.ReadAllBytes(filepath);
            Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            texture.filterMode = FilterMode.Trilinear;
            texture.LoadImage(bytes);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, Screen.width, Screen.height), new Vector2(0.5f, 0.0f), 1.0f);

            image.sprite = sprite;
        }
        //get screenshot by filename.png
        //set screenshot viewer to filename.png
        //screenshowViewer.GetComponent<Image>().sprite;
    }
    public void HideScreenshot() {
        image.sprite = null;
    }
   
}
