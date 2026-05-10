using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;

public class NoteSpawner : MonoBehaviour
{
    [SerializeField] private GameObject notePrefab;
    [SerializeField] private GameObject longNotePrefab;
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

        // 開始時間の計算
        float startTime = (float)data.num / data.LPB * (60f / bpm);
        controller.startTime = startTime;
        controller.checkTimes.Add(startTime);

        // 中間・終点データの追加
        if (data.notes != null)
        {
            foreach (var subNote in data.notes)
            {
                float t = (float)subNote.num / subNote.LPB * (60f / bpm);
                controller.checkTimes.Add(t);
                controller.endTime = t; // 最後の要素がendTimeになる
            }
        }
        obj.transform.position = lanePositions[data.block].position;
    }
}