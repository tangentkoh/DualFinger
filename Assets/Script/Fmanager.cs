using UnityEngine;

public class FManager : MonoBehaviour
{
    public static FManager Instance;

    [SerializeField] private GameObject tapEffectPrefab; // タップ時に出すプレハブ
    [SerializeField] private Transform[] lanePositions;  // レーン0, 1のX座標用

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    // 特定のレーンでエフェクトを再生する
    public void PlayTapEffect(int lane)
    {
        // 判定ラインの高さ（-4f）に合わせる
        Vector3 spawnPos = new Vector3(lanePositions[lane].position.x, -4f, 0);
        
        // エフェクトを生成
        GameObject effect = Instantiate(tapEffectPrefab, spawnPos, Quaternion.identity);
        
        // 1秒後に自動で消えるようにする
        Destroy(effect, 1f);
    }
}