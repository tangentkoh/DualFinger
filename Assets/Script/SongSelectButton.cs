using UnityEngine;
using PixeLadder.EasyTransition;

public class SongSelectButton : MonoBehaviour
{
    public string songTitle; // このボタンで選ぶ曲名
    public string scenename; // 遷移先のシーン名
    public TransitionEffect yourTransitionEffect; // 遷移エフェクト
    public AudioSource audioSource;
    public AudioClip tSound;

    public void OnSongButtonClick()
    {
        // 1. シングルトンに曲名を保存
        GameDataManager.Instance.songName = songTitle;
        
        // 2. 前回のスコアをリセットしておく
        GameDataManager.Instance.ResetData();

        Debug.Log(songTitle + " を選択。難易度: " + GameDataManager.Instance.difficulty);

        // 3. ゲーム画面へ遷移
        if (audioSource != null && tSound != null)
        {
            audioSource.PlayOneShot(tSound);
        }
        SceneTransitioner.Instance.LoadScene(scenename, yourTransitionEffect);
    }
}