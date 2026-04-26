using UnityEngine;
using TMPro;

public class GameUIController : MonoBehaviour
{
    public TextMeshProUGUI songNameText;     // 曲名表示用
    public TextMeshProUGUI difficultyText;   // 難易度表示用

    void Start()
    {
        // シングルトンが存在するか確認してからデータを反映
        if (GameDataManager.Instance != null)
        {
            songNameText.text = GameDataManager.Instance.songName;
            difficultyText.text = GameDataManager.Instance.difficulty;
        }
        else
        {
            songNameText.text = "song";
            difficultyText.text = "diff";
        }
    }
}