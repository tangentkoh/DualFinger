using UnityEngine;

public class F2 : MonoBehaviour
{
    [SerializeField] private GameObject tapEffectPrefab; // すでにあるTransformなエフェクト
    [SerializeField] private float effectZDistance = 10f; // カメラからの奥行き

    void Update()
    {
        // マウスの左クリック、またはスマホの画面タップを検知
        if (Input.GetMouseButtonDown(0))
        {
            SpawnEffect(Input.mousePosition);
        }
    }

    private void SpawnEffect(Vector2 mousePos)
    {
        if (tapEffectPrefab == null) return;

        // メインカメラを取得
        Camera mainCamera = Camera.main;
        if (mainCamera == null) return;

        // 1. スクリーン座標（平面）にカメラからの奥行き（Z値）を追加する
        // カメラが「Perspective（遠近）」の場合、Zが0だとカメラの真上になって映りません
        Vector3 screenPosWithDepth = new Vector3(mousePos.x, mousePos.y, effectZDistance);

        // 2. スクリーン座標をワールド座標（ゲーム内の3D/2D空間の座標）に変換
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(screenPosWithDepth);

        // 3. エフェクトを生成し、変換したワールド座標をそのまま代入
        GameObject effectInstance = Instantiate(tapEffectPrefab, worldPos, Quaternion.identity);
        
        // もし自動消滅スクリプトがプレハブに付いていないなら、コード側で消す
        Destroy(effectInstance, 0.5f);
    }
}