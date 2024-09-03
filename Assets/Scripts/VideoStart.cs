using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoEndTransition : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        // Get the VideoPlayer component
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.url = Application.dataPath + "/Movie/BootScreen.mp4";
        // Play the video
        videoPlayer.Play();
    }

    void Update()
    {
        // Check if the video has ended
        if (videoPlayer.frame > 0 && videoPlayer.frame == (long)videoPlayer.frameCount - 1)
        {
            Debug.Log("Video has ended. Transitioning to the Start scene.");
            SceneManager.LoadScene("Start");
        }
    }
}