using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private KeyCode leftKey = KeyCode.LeftArrow;
    [SerializeField] private KeyCode rightKey = KeyCode.RightArrow;
    public AudioSource audioSource;
    public AudioClip tSound;

    // ボタンの状態管理
    private bool isLeftPressed = false;
    private bool isRightPressed = false;

    // 判定の許容範囲（秒）
    private float goodRange = 0.05f;
    private float okRange = 0.1f;

    void Update()
    {
        // 1. キーボードの状態を同期
        // GetKeyは「押されている間ずっとtrue」、GetKeyDownは「押した瞬間だけtrue」
        if (Input.GetKeyDown(leftKey)) isLeftPressed = true;
        if (Input.GetKeyUp(leftKey)) isLeftPressed = false;

        if (Input.GetKeyDown(rightKey)) isRightPressed = true;
        if (Input.GetKeyUp(rightKey)) isRightPressed = false;

        // 2. 押されている間、判定処理を呼ぶ
        // 第2引数には「今この瞬間に押されたか」を渡す
        if (isLeftPressed) Judge(0, Input.GetKeyDown(leftKey));
        if (isRightPressed) Judge(1, Input.GetKeyDown(rightKey));
    }

    // --- UIボタンの EventTrigger から呼ぶメソッド ---
    public void SetLeftPressed(bool pressed)
    {
        isLeftPressed = pressed;
        // 押し始めた瞬間だけ、判定メソッドに「JustDown」として伝える
        if (pressed) 
        {
            // 判定処理を呼ぶ
            Judge(0, true);
        }
    }

    public void SetRightPressed(bool pressed)
    {
        isRightPressed = pressed;
        if (pressed) 
        {
            Judge(1, true);
        }
    }

    void Judge(int lane, bool isJustDown)
    {
        float currentTime = PlaySceneManager.Instance.AudioTime;

        // --- A. ロングノーツ判定 ---
        // 押しっぱなし状態でも、判定リスト(checkTimes)の時間が来ればGoodになる
        var longNotes = Object.FindObjectsByType<LongNoteController>(FindObjectsSortMode.None);
        foreach (var lNote in longNotes)
        {
            if (lNote.lane != lane) continue;

            if (lNote.CheckHit(currentTime, okRange))
            {
                ComboManager.Instance.AddScore("Good");
                audioSource.PlayOneShot(tSound);
                FManager.Instance.PlayTapEffect(lane);
                return; // 1つの入力で複数の判定を取らないように抜ける
            }
        }

        // --- B. 単押し判定 ---
        // 単押しは「押した瞬間(isJustDown)」しか受け付けない
        if (isJustDown)
        {
            var singleNotes = Object.FindObjectsByType<NoteController>(FindObjectsSortMode.None);
            foreach (var note in singleNotes)
            {
                if (note.lane != lane) continue;

                float diff = Mathf.Abs(note.targetTime - currentTime);
                if (diff <= okRange)
                {
                    ComboManager.Instance.AddScore(diff <= goodRange ? "Good" : "Ok");
                    audioSource.PlayOneShot(tSound);
                    FManager.Instance.PlayTapEffect(lane);
                    Destroy(note.gameObject);
                    return;
                }
            }
        }
    }
}