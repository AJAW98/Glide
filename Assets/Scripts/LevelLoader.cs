using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public static LevelLoader instance = null;

    [SerializeField] Animator animator;
    [SerializeField] float transitionTime = 1f;

    void Awake()
    {
        if (instance == null) {
            instance = this;
        } else if (instance != this)
            Destroy(gameObject);
    }       

    public void LoadScene(int sceneIndex) {
        if (animator == null) {
            Debug.LogError("No animator attached!");
            return;
        }

        StartCoroutine(LoadLevel(sceneIndex));
    }

    public void LoadNextScene() {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void RestartScene() {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }


    IEnumerator LoadLevel(int buildIndex) {
        animator.SetTrigger("New Trigger");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(buildIndex);
    }

    public void OnFadeComplete() {

    }

}
