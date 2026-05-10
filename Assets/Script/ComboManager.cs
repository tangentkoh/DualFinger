using UnityEngine;
using TMPro;

public class ComboManager : MonoBehaviour
{
    public static ComboManager Instance;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text comboText;

    private int currentCombo = 0;

    void Awake() => Instance = this;

    void Start()
    {
        UpdateUI();
    }

    public void AddScore(string rating)
    {
        int baseScore = 0;

        if (rating == "Good")
        {
            baseScore = 1000;
            GameDataManager.Instance.goodCount++;
            currentCombo++;
        }
        else if (rating == "Ok")
        {
            baseScore = 800;
            GameDataManager.Instance.okCount++;
            currentCombo++;
        }
        else // Miss
        {
            GameDataManager.Instance.missCount++;
            currentCombo = 0;
        }

        // コンボボーナスの計算 (10コンボごとに10点、最大100点)
        int comboBonus = Mathf.Min((currentCombo / 10) * 10, 100);
        
        if (baseScore > 0) // Miss以外
        {
            GameDataManager.Instance.score += (baseScore + comboBonus);
        }

        // 最大コンボ更新
        if (currentCombo > GameDataManager.Instance.maxCombo)
        {
            GameDataManager.Instance.maxCombo = currentCombo;
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        // 8桁表示
        scoreText.text = GameDataManager.Instance.score.ToString("D8");
        // コンボ表示
        comboText.text = currentCombo > 0 ? currentCombo + " Combo" : "";
    }
}