using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerPath : MonoBehaviour {
    private VideoPlayer videoPlayer;
    public string path;

    void Start() {
        videoPlayer = gameObject.GetComponent<VideoPlayer>();
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, path);
    }
}
