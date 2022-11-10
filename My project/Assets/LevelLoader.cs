using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {
    public GameObject loadingScreen;
    public Slider slider;
    public TMP_Text progressText;
    public void LoadLevel(int sceneIndex)
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        
        while (!operation.isDone) {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            
            slider.value = progress;
            progressText.text = progress * 100f + "%";
            yield return null;
        }
    }
}
