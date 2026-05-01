using System;
using System.Collections.Generic;

[Serializable]
public class NoteData
{
    public int LPB;    // 1拍あたりの分割数（8なら8分音符単位）
    public int num;    // 位置（LPB単位の通し番号）
    public int block;  // レーン番号（今回なら 0 or 1）
    public int type;   // 1: 単押し, 2: 長押し
    public List<NoteData> notes; // 長押しの終点や中間点用
}

[Serializable]
public class MusicSheet
{
    public string name;
    public int maxBlock;
    public int BPM;
    public int offset;
    public List<NoteData> notes;
}