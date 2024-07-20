using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Reference to the VideoPlayer component
    public GameObject menuPanel;    // Reference to the menu panel GameObject
    public GameObject startImg;

    void Start()
    {
        // Ensure the menu panel is hidden at the start
        startImg.SetActive(true);

        // Subscribe to the video player's loopPointReached event
        videoPlayer.loopPointReached += OnVideoFinished;

        // Play the video
        videoPlayer.Play();
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        // Show the menu panel when the video finishes
        startImg.SetActive(false);
        menuPanel.SetActive(true);
    }
}
