using UnityEngine;
using PixeLadder.EasyTransition;

public class change : MonoBehaviour
{

    public string scenename;
    public TransitionEffect yourTransitionEffect;
    public AudioSource audioSource;
    public AudioClip tSound;
    private bool isTransitioning = false;
    public void ChangeScene()
    {
        if (!isTransitioning)
        {
            isTransitioning = true;
            if (audioSource != null && tSound != null)
            {
                audioSource.PlayOneShot(tSound);
            }
            Debug.Log("Changing scene to: " + scenename);
            SceneTransitioner.Instance.LoadScene(scenename, yourTransitionEffect);
        }
    }
}