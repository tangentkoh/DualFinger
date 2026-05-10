using UnityEngine;
using System.Collections;
using PixeLadder.EasyTransition;
using Newtonsoft.Json;

public class PlaySceneManager : MonoBehaviour
{
    public static PlaySceneManager Instance;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private NoteSpawner noteSpawner;
    public string scenename = "Result"; // 遷移先のシーン名
    public TransitionEffect yourTransitionEffect; // 遷移エフェクト
    
    public float AudioTime => audioSource.time; // NoteControllerが参照する時間

    void Awake() => Instance = this;

    void Start()
    {
        // GameDataManager(シングルトン)からJSONを取得してノーツ生成
        if (GameDataManager.Instance != null && GameDataManager.Instance.currentSheetJson != null)
        {
            // NoteEditor形式のJSONをパース
            MusicSheet sheet = JsonConvert.DeserializeObject<MusicSheet>(GameDataManager.Instance.currentSheetJson.text);
            // ノーツを生成（描画）
            noteSpawner.SpawnNotes(sheet);
            
            audioSource.clip = GameDataManager.Instance.currentAudioClip;
            // ゲーム開始の流れを実行
            StartCoroutine(PlaySequence());
        }
        else
        {
            Debug.LogError("譜面データが見つかりません。選曲シーンから始めてください。");
        }
    }

    IEnumerator PlaySequence()
    {
        // 開始前の1秒猶予
        yield return new WaitForSeconds(1.0f);
        
        // 曲を再生
        audioSource.Play();

        // 曲が終わるまで待機
        while (audioSource.isPlaying)
        {
            yield return null;
        }

        // 終了後1秒待機
        yield return new WaitForSeconds(1.0f);

        // リザルトシーンへ
        SceneTransitioner.Instance.LoadScene(scenename, yourTransitionEffect);
    }
}