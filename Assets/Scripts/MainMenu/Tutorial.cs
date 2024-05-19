using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private string _imagesPath;
    [SerializeField] private Image _image;
    [SerializeField] private GameObject customizeModal;
    private List<Sprite> _loadedImages;
    private int _imageIndex = 0;

    void Start()
    {
        _loadedImages = ImageLoader.loadProfilePictures();
        if (_loadedImages.Count > 0)
        {
            _image.sprite = _loadedImages[_imageIndex];
            Debug.Log(_loadedImages.Count);
        }
        else
        {
            Debug.LogError("Path to profile pictures isn't valid");
        }
    }

    public void nextPicture()
    {
        if (_loadedImages.Count > 0)
        {
            _imageIndex++;
            if (_imageIndex >= _loadedImages.Count)
            {
                GameManager gm = GameManager.Instance;
                GameObject gameObject = GameObject.FindGameObjectWithTag("tutorialModal");
                gameObject.SetActive(false);
                _imageIndex = 0;
                if (gm.cacheExists == false)
                {
                    customizeModal.SetActive(true);
                    gm.cacheExists = true;
                }
                else
                {
                    gameObject = GameObject.FindGameObjectWithTag("menuBackdrop");
                    gameObject.SetActive(false);
                }
                return;
            }
            _image.sprite = _loadedImages[_imageIndex];
        }
        else
        {
            Debug.LogError("Path to profile pictures isn't valid");
        }
    }
}
