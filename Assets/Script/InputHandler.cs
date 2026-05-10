using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private KeyCode leftKey = KeyCode.LeftArrow;
    [SerializeField] private KeyCode rightKey = KeyCode.RightArrow;

    private bool isLeftPressed = false;
    private bool isRightPressed = false;

    private float goodRange = 0.1f;
    private float okRange = 0.15f;

    void Update()
    {
        // キーボード入力の状態を同期
        if (Input.GetKeyDown(leftKey)) isLeftPressed = true;
        if (Input.GetKeyUp(leftKey)) isLeftPressed = false;
        
        if (Input.GetKeyDown(rightKey)) isRightPressed = true;
        if (Input.GetKeyUp(rightKey)) isRightPressed = false;

        // 押されている間は判定を呼び続ける
        if (isLeftPressed) Judge(0);
        if (isRightPressed) Judge(1);
    }

    // UIボタンのEventTrigger(PointerDown/Up)からこれを呼ぶ
    public void SetLeftPressed(bool pressed) => isLeftPressed = pressed;
    public void SetRightPressed(bool pressed) => isRightPressed = pressed;

    void Judge(int lane)
    {
        float currentTime = PlaySceneManager.Instance.AudioTime;

        // 1. 長押し判定
        var longNotes = Object.FindObjectsByType<LongNoteController>(FindObjectsSortMode.None);
        foreach (var lNote in longNotes)
        {
            if (lNote.lane != lane) continue;
            if (lNote.CheckHit(currentTime, okRange))
            {
                ComboManager.Instance.AddScore("Good");
                return;
            }
        }

        // 2. 単押し判定 (押しっぱなしによる重複消滅を防ぐため、範囲内の1つだけ消す)
        var singleNotes = Object.FindObjectsByType<NoteController>(FindObjectsSortMode.None);
        foreach (var note in singleNotes)
        {
            if (note.lane != lane) continue;
            float diff = Mathf.Abs(note.targetTime - currentTime);

            if (diff <= okRange)
            {
                ComboManager.Instance.AddScore(diff <= goodRange ? "Good" : "Ok");
                Destroy(note.gameObject);
                return;
            }
        }
    }
}