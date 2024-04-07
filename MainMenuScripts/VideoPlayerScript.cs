using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPlayerScript : MonoBehaviour
{
    [SerializeField] RawImage _image;
    [SerializeField] VideoPlayer _videoPlayer;

    private void FixedUpdate()
    {
        _image.texture = _videoPlayer.texture;
        _videoPlayer.Play();
    }
}
