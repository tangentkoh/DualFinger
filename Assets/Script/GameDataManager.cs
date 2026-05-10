using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    // どこからでもアクセスできるように static なインスタンスを用意
    public static GameDataManager Instance { get; private set; }

    // 記憶したいデータ
    public string difficulty = "Normal"; // 難易度
    public string songName = "";         // 曲名
    public int score = 0;                // スコア
    public int goodCount = 0;
    public int okCount = 0;
    public int missCount = 0;
    public int maxCombo = 0;
    public TextAsset currentSheetJson;   // 選択された曲のJSONファイル
    public AudioClip currentAudioClip;   // 選択された曲のAudioClip

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
        goodCount = 0;
        okCount = 0;
        missCount = 0;
        maxCombo = 0;
    }
}