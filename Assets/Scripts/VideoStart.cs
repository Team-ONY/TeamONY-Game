using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoEndTransition : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        // VideoPlayer�R���|�[�l���g���擾
        videoPlayer = GetComponent<VideoPlayer>();

        // ����t�@�C����ݒ�
        videoPlayer.url = Application.dataPath + "/Movie/レコーディング 2024-06-17 140710.mp4";

        // ����Đ����J�n
        videoPlayer.Play();
    }

    void Update()
    {
        // ����̍Đ����I�����������`�F�b�N
        if (videoPlayer.frame > 0 && videoPlayer.frame == (long)videoPlayer.frameCount - 1)
        {
            Debug.Log("Video has ended. Transitioning to the Start scene.");
            // Start�V�[���ɑJ��
            SceneManager.LoadScene("Start");
        }
    }
}