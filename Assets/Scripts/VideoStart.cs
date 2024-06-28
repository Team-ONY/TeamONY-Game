using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoEndTransition : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        // VideoPlayerコンポーネントを取得
        videoPlayer = GetComponent<VideoPlayer>();

        // 動画ファイルを設定
        videoPlayer.url = Application.dataPath + "/Movie/レコーディング 2024-06-17 140710.mp4";

        // 動画再生を開始
        videoPlayer.Play();
    }

    void Update()
    {
        // 動画の再生が終了したかをチェック
        if (videoPlayer.frame > 0 && videoPlayer.frame == (long)videoPlayer.frameCount - 1)
        {
            Debug.Log("Video has ended. Transitioning to the Start scene.");
            // Startシーンに遷移
            SceneManager.LoadScene("Start");
        }
    }
}