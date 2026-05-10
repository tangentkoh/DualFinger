using UnityEngine;
using System.Collections.Generic;

public class LongNoteController : MonoBehaviour
{
    public int lane;
    public float scrollSpeed = 10f;
    public float startTime;
    public float endTime;

    [SerializeField] private Transform bodyTransform; 
    public List<float> checkTimes = new List<float>();

    private float baseScaleX;

    void Start()
    {
        baseScaleX = bodyTransform.localScale.x;
    }

    void Update()
    {
        if (PlaySceneManager.Instance == null) return;
        float currentTime = PlaySceneManager.Instance.AudioTime;

        // 1. 位置の更新（下端基準）
        float startDiff = startTime - currentTime;
        Vector3 pos = transform.position;
        pos.y = (startDiff * scrollSpeed) - 4f; // 判定ライン Y=-4
        transform.position = pos;

        // 2. スケールの更新（見た目の全長を維持）
        float duration = endTime - startTime;
        float length = duration * scrollSpeed;
        bodyTransform.localScale = new Vector3(baseScaleX, Mathf.Max(length, 0), 1);

        // 3. 自動Miss判定
        if (checkTimes.Count > 0 && (checkTimes[0] - currentTime) < -0.2f)
        {
            ComboManager.Instance.AddScore("Miss");
            checkTimes.RemoveAt(0);
        }

        // 4. 消滅タイミング（終点が過ぎるまで消さない）
        if (currentTime > endTime + 0.2f) 
        {
            Destroy(gameObject);
        }
    }

    public bool CheckHit(float currentTime, float range)
    {
        if (checkTimes.Count == 0) return false;

        float diff = Mathf.Abs(checkTimes[0] - currentTime);
        if (diff <= range)
        {
            checkTimes.RemoveAt(0);
            return true;
        }
        return false;
    }
}