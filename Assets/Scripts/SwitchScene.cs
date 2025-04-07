using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public GlobalEvent eventToSubscribe;
    public string toScene;
    public void Switch(string toScene)
    {
        SceneManager.LoadScene(toScene);
    }
    void Start()
    {
        if (eventToSubscribe == GlobalEvent.None)
        {
            return;
        }
        EventManager.Instance.AddListener(eventToSubscribe, () => Switch(toScene));
    }

}
