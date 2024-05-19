using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialMoveBackground : MonoBehaviour
{
    [SerializeField] private RawImage _rawImage;
    [SerializeField] private float _x, _y;

    [SerializeField] private Texture[] _textures;

    public void SetBackground(uint lifes)
    {
        _rawImage.texture = _textures[0];
        if (lifes == 2)
        {
            _rawImage.texture = _textures[1];
        }
        if (lifes == 1)
        {
            _rawImage.texture = _textures[2];
        }
        if (lifes == 0)
        {
            _rawImage.texture = _textures[3];
        }
    }

    void Update()
    {
        _rawImage.uvRect = new Rect(_rawImage.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, _rawImage.uvRect.size);
    }
}
