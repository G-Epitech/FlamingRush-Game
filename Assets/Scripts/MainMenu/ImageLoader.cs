using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour
{
    public static List<Sprite> loadImagesFromPath(string path)
    {
        List<Sprite> sprites = new List<Sprite>();
        string[] imageFiles = Directory.GetFiles(path, "*.png");

        foreach (string file in imageFiles)
        {
            byte[] fileData = File.ReadAllBytes(file);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(fileData);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            sprites.Add(sprite);
        }
        return sprites;
    }
}
