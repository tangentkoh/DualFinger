using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    // どこからでもアクセスできるように static なインスタンスを用意
    public static GameDataManager Instance { get; private set; }

    // 記憶したいデータ
    public string difficulty = "Normal"; // 難易度
    public string songName = "";         // 曲名
    public int score = 0;                // スコア

    private void Awake()
    {
        // シングルトンの確立
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーン遷移しても壊さない
        }
        else
        {
            Destroy(gameObject); // すでに存在してれば重複分を削除
        }
    }

    // スコアのリセット用メソッド
    public void ResetData()
    {
        score = 0;
    }
}