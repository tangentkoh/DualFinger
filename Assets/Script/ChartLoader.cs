using UnityEngine;

public class ChartLoader : MonoBehaviour
{
    public TextAsset jsonFile; // ここにMixdown.jsonをアタッチ

    void Start()
    {
        LoadChart();
    }

    void LoadChart()
    {
        MusicSheet sheet = JsonUtility.FromJson<MusicSheet>(jsonFile.text);

        foreach (var note in sheet.notes)
        {
            // 秒数を計算
            float time = (float)note.num / note.LPB * (60f / sheet.BPM);
            int lane = note.block;

            if (note.type == 1)
            {
                Debug.Log($"単押し: {time}秒, レーン: {lane}");
            }
            else if (note.type == 2)
            {
                // 長押しの末尾を取得（一番最後の要素が終点）
                var endNote = note.notes[note.notes.Count - 1];
                float endTime = (float)endNote.num / endNote.LPB * (60f / sheet.BPM);
                Debug.Log($"長押し開始: {time}秒, 終了: {endTime}秒, レーン: {lane}");
            }
        }
    }
}