using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [SerializeField] private GameObject notePrefab;
    [SerializeField] private Transform[] lanePositions; // レーン0と1の場所をInspectorで指定

    public void SpawnNotes(MusicSheet sheet)
    {
        foreach (var note in sheet.notes)
        {
            // NoteEditorの形式から秒数を算出
            float time = (float)note.num / note.LPB * (60f / sheet.BPM);
            
            // 生成
            GameObject obj = Instantiate(notePrefab, transform);
            NoteController controller = obj.GetComponent<NoteController>();
            
            // 初期データの流し込み
            controller.targetTime = time;
            
            // X座標をレーンに合わせて配置
            Vector3 pos = lanePositions[note.block].position;
            obj.transform.position = pos;
        }
    }
}