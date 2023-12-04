using B83.Image.GIF;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ActuallyTrying : MonoBehaviour
{
    [SerializeField] string fileName;
    [SerializeField] string spriteSheetName;

    /*void Start()
    {
        gifLoader = new GIFLoader();
        string filePath = "D:/GitHub/HackNSlash/Salitre/AssetsvSprites/Flashbacks" + fileName;
        gifImage = gifLoader.Load(filePath);

        if (gifImage != null)
        {
            StartCoroutine(PlayGIF());
        }
        else
        {
            Debug.LogError("Failed to load GIF.");
        }
    }

    IEnumerator PlayGIF()
    {
        yield return StartCoroutine(gifImage.RunAnimation(OnUpdateCallback));
    }

    void OnUpdateCallback(Texture2D texture)
    {
        // Assuming you have a RawImage component on your GameObject
        RawImage rawImage = GetComponent<RawImage>();
        rawImage.texture = texture;
    }*/

    void Start()
    {
        CreateSpritesheetFromGIF("D:/GitHub/HackNSlash/Salitre/Assets/Sprites/Flashbacks/" + fileName + ".gif", "D:/GitHub/HackNSlash/Salitre/Assets/Sprites/Flashbacks/" + spriteSheetName + ".png");
    }

    private void CreateSpritesheetFromGIF(string gifFilePath, string spritesheetSavePath)
    {
        // Load GIF file
        GIFLoader loader = new GIFLoader();
        GIFImage gifImage = loader.Load(gifFilePath);

        if (gifImage == null)
        {
            Debug.LogError("Failed to load GIF file.");
            return;
        }

        // Assuming each frame has the same dimensions
        int frameWidth = gifImage.screen.width;
        int frameHeight = gifImage.screen.height;

        // Assuming you have a list of Color32 arrays for each frame
        List<Color32[]> framesData = new List<Color32[]>();

        // Iterate through each frame and add it to the framesData list
        for (int i = 0; i < gifImage.imageData.Count; i++)
        {
            Color32[] frameData = new Color32[frameWidth * frameHeight];
            gifImage.DrawPartialFrameTo(i, frameData, frameWidth, frameHeight);
            framesData.Add(frameData);
        }

        // Create a Texture2D for the spritesheet
        Texture2D spritesheet = new Texture2D(frameWidth * gifImage.imageData.Count, frameHeight);

        // Set pixels for each frame in the spritesheet
        for (int i = 0; i < gifImage.imageData.Count; i++)
        {
            Color32[] frameData = framesData[i];
            spritesheet.SetPixels32(i * frameWidth, 0, frameWidth, frameHeight, frameData);
        }

        // Apply changes to the texture
        spritesheet.Apply();

        // Save spritesheet as PNG (optional)
        byte[] pngBytes = spritesheet.EncodeToPNG();
        File.WriteAllBytes(spritesheetSavePath, pngBytes);

        Debug.Log("Spritesheet created and saved: " + spritesheetSavePath);
    }
}
