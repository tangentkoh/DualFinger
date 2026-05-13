using UnityEngine;
using PixeLadder.EasyTransition;

public class SongSelectButton : MonoBehaviour
{
    public string songTitle; // このボタンで選ぶ曲名
    [SerializeField] private TextAsset normalSheet;
    [SerializeField] private TextAsset hardSheet;
    [SerializeField] private AudioClip musicClip;
    public string scenename; // 遷移先のシーン名
    public TransitionEffect yourTransitionEffect; // 遷移エフェクト
    public AudioSource audioSource;
    public AudioClip tSound;

    public void OnSongButtonClick()
    {
        // 1. シングルトンに曲名を保存
        var data = GameDataManager.Instance;
        data.songName = songTitle;
        data.currentAudioClip = musicClip;

        if (data.difficulty == "Hard")
        {
            data.currentSheetJson = hardSheet;
        }
        else
        {
            data.currentSheetJson = normalSheet;
        }
        
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