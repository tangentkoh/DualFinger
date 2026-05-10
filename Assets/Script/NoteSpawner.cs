using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json; // Newtonsoft.Jsonを使用

public class NoteSpawner : MonoBehaviour
{
    [SerializeField] private GameObject notePrefab;       // 単押し用
    [SerializeField] private GameObject longNotePrefab;   // 長押し用（縦に伸びる方）
    [SerializeField] private Transform[] lanePositions;

    public void SpawnNotes(MusicSheet sheet)
    {
        foreach (var note in sheet.notes)
        {
            if (note.type == 1) // 単押し
            {
                SpawnSingleNote(note, sheet.BPM);
            }
            else if (note.type == 2) // 長押し
            {
                SpawnLongNote(note, sheet.BPM);
            }
        }
    }

    void SpawnSingleNote(NoteData data, float bpm)
    {
        float time = (float)data.num / data.LPB * (60f / bpm);
        GameObject obj = Instantiate(notePrefab, transform);
        NoteController controller = obj.GetComponent<NoteController>();
        
        controller.targetTime = time;
        controller.lane = data.block;
        obj.transform.position = lanePositions[data.block].position;
    }

    void SpawnLongNote(NoteData data, float bpm)
    {
        GameObject obj = Instantiate(longNotePrefab, transform);
        LongNoteController controller = obj.GetComponent<LongNoteController>();
        controller.lane = data.block;

        // 1. 開始時間の計算と保持
        float start = (float)data.num / data.LPB * (60f / bpm);
        controller.startTime = start;
        controller.checkTimes.Add(start); // 最初の判定ポイント

        // 2. 中間・終点データの追加
        if (data.notes != null)
        {
            foreach (var subNote in data.notes)
            {
                float t = (float)subNote.num / subNote.LPB * (60f / bpm);
                controller.checkTimes.Add(t); // 判定リストに追加
                controller.endTime = t;       // 最終的に「一番最後の時間」が代入される
            }
        }

        // 3. 生成位置の設定
        obj.transform.position = lanePositions[data.block].position;
    }
}