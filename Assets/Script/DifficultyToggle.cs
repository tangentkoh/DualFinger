using UnityEngine;
using TMPro;

public class DifficultyToggle : MonoBehaviour
{
    public TextMeshProUGUI difficultyText; // 画面に「Normal」等と表示するUI用

    public void OnDifficultyButtonClick()
    {
        // シングルトンの値を書き換える
        if (GameDataManager.Instance.difficulty == "Normal")
        {
            GameDataManager.Instance.difficulty = "Hard";
        }
        else
        {
            GameDataManager.Instance.difficulty = "Normal";
        }

        // 表示を更新
        if (difficultyText != null)
        {
            difficultyText.text = GameDataManager.Instance.difficulty;
        }

        Debug.Log("難易度を " + GameDataManager.Instance.difficulty + " に変更しました");
    }
}