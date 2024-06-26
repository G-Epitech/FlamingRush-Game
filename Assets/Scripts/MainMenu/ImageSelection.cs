using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PfpSelection : MonoBehaviour
{
    [SerializeField] private string _imagesPath;
    [SerializeField] private Image _image;
    private List<Sprite> _loadedImages;
    private int _imageIndex;

    void Start()
    {
        _loadedImages = ImageLoader.loadProfilePictures();
        GameManager gm = GameManager.Instance;
        _imageIndex = gm.data.profilePictureIdx;
        if (_loadedImages.Count > 0)
        {
            _image.sprite = _loadedImages[_imageIndex];
        } else
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
                _imageIndex = 0;
            }
            _image.sprite = _loadedImages[_imageIndex];
            GameManager gm = GameManager.Instance;
            gm.data.profilePictureIdx = _imageIndex;
        }
        else
        {
            Debug.LogError("Path to profile pictures isn't valid");
        }
    }

    public void prevPicture()
    {
        if (_loadedImages.Count > 0)
        {
            _imageIndex--;
            if (_imageIndex < 0)
            {
                _imageIndex = _loadedImages.Count - 1;
            }
            _image.sprite = _loadedImages[_imageIndex];
            GameManager gm = GameManager.Instance;
            gm.data.profilePictureIdx = _imageIndex;
        }
        else
        {
            Debug.LogError("Path to profile pictures isn't valid");
        }
    }
}
