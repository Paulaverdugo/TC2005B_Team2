/*
    WebGl requires video players to work differently than in the editor.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MenuCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        VideoPlayer videoPlayer = gameObject.GetComponent<VideoPlayer>();
        videoPlayer.url = System.IO.Path.Combine (Application.streamingAssetsPath, "Copia de rain.mp4");
        videoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
