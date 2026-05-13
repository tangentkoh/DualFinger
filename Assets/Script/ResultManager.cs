using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using unityroom.Api;

public class ResultManager : MonoBehaviour
{
    [Header("UI Text References")]
    [SerializeField] private TMP_Text songNameText;
    [SerializeField] private TMP_Text difficultyText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text goodText;
    [SerializeField] private TMP_Text okText;
    [SerializeField] private TMP_Text missText;

    void Start()
    {
        // GameDataManagerから値を読み取って表示
        if (GameDataManager.Instance != null)
        {
            var data = GameDataManager.Instance;

            songNameText.text = data.songName;
            difficultyText.text = data.difficulty;
            scoreText.text = data.score.ToString("D8"); // 8桁表示
            
            goodText.text = "Good:" + data.goodCount;
            okText.text   = "Ok:" + data.okCount;
            missText.text = "Miss:" + data.missCount;

            UnityroomApiClient.Instance.SendScore(1, data.score, ScoreboardWriteMode.HighScoreDesc);
        }
    }
}