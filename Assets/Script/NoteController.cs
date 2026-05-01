using UnityEngine;

public class NoteController : MonoBehaviour
{
    public float targetTime;    // このノーツが判定ラインに来るべき秒数
    public float scrollSpeed = 10f; // スクロール速度

    void Update()
    {
        // PlaySceneManagerから現在の再生時間を取得
        // シングルトン(Instance)経由でアクセスします
        if (PlaySceneManager.Instance == null) return;

        float currentTime = PlaySceneManager.Instance.AudioTime; 
        
        // 判定時間までの残り時間を計算
        float timeDiff = targetTime - currentTime;

        // 残り時間に基づいて位置を更新
        // timeDiffが0のときに y=0 (判定ライン) になります
        Vector3 pos = transform.position;
        pos.y = timeDiff * scrollSpeed;
        transform.position = pos;

        // 判定ラインを通り過ぎて1秒（ミス）経ったら消滅
        if (timeDiff < -1.0f) 
        {
            Destroy(gameObject);
        }
    }
}