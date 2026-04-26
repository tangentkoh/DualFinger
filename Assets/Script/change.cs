using UnityEngine;
using PixeLadder.EasyTransition;

public class change : MonoBehaviour
{

    public string scenename;
    public TransitionEffect yourTransitionEffect;
    private bool isTransitioning = false;
    public void ChangeScene()
    {
        if (!isTransitioning)
        {
            isTransitioning = true;
            Debug.Log("Changing scene to: " + scenename);
            SceneTransitioner.Instance.LoadScene(scenename, yourTransitionEffect);
        }
    }
}