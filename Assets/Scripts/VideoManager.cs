using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject textObject;

    void Start()
    {
        // 動画が終了したときのイベントを設定
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // 動画が終了したら動画を非表示にする
        videoPlayer.gameObject.SetActive(false);
        
        // テキストを表示する
        textObject.SetActive(true);
    }
}