using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    [SerializeField] GlobalEvent switchSceneEvent, resetSceneEvent;
    public string toScene;
    void Start()
    {
        if (switchSceneEvent == GlobalEvent.None)
        {
            return;
        }
        EventManager.Instance.AddListener(switchSceneEvent, () => Switch(toScene));
        EventManager.Instance.AddListener(resetSceneEvent, () => SceneManager.LoadScene(SceneManager.GetActiveScene().name));
    }

    public void Switch(string toScene)
    {
        SceneManager.LoadScene(toScene);
    }

    public void ResetScene()
    {
        GameData.isRunningDFS = false;
        EventManager.Instance.Invoke(GlobalEvent.ResetScene);   
    }

    public void RunAgent()
    {
        EventManager.Instance.Invoke(GlobalEvent.RunAgent);
    }
}
