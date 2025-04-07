using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    [SerializeField] GameObject loadingVisualizer;
    [SerializeField] GlobalEvent switchSceneEvent, resetSceneEvent;
    public string toScene;
    void Start()
    {
        if (switchSceneEvent == GlobalEvent.None)
        {
            return;
        }
        EventManager.Instance.AddListener(switchSceneEvent, () => Switch(toScene));
        EventManager.Instance.AddListener(resetSceneEvent, () => Switch(SceneManager.GetActiveScene().name));
    }

    public void Switch(string toScene)
    {
        // SceneManager.LoadScene(toScene);
        StartCoroutine(IEResetScene(toScene));
    }

    public void ResetScene()
    {
        EventManager.Instance.Invoke(GlobalEvent.ResetScene);   
    }
    private IEnumerator IEResetScene(string toScene)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(toScene);
        loadingVisualizer.SetActive(true);
        while (!op.isDone)
        {
            yield return null;
        }
        loadingVisualizer.SetActive(false);
    }

    public void RunAgent()
    {
        EventManager.Instance.Invoke(GlobalEvent.RunAgent);
    }
}
