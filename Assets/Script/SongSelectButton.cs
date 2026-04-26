using UnityEngine;
using PixeLadder.EasyTransition;

public class SongSelectButton : MonoBehaviour
{
    public string songTitle; // このボタンで選ぶ曲名
    public string scenename; // 遷移先のシーン名
    public TransitionEffect yourTransitionEffect; // 遷移エフェクト

    public void OnSongButtonClick()
    {
        // 1. シングルトンに曲名を保存
        GameDataManager.Instance.songName = songTitle;
        
        // 2. 前回のスコアをリセットしておく
        GameDataManager.Instance.ResetData();

        Debug.Log(songTitle + " を選択。難易度: " + GameDataManager.Instance.difficulty);

        // 3. ゲーム画面へ遷移
        SceneTransitioner.Instance.LoadScene(scenename, yourTransitionEffect);
    }
}