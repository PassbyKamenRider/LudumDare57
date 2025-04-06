using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

class SwitchSelectLevel : MonoBehaviour
{
    public int currentLevel = 0;
    public TextMeshProUGUI textShowLevel;
    public void HandleLevelSwitch(bool isLeft)
    {
        int count = GameManager.Instance.levelNames.Count;
        if (isLeft)
        {
            currentLevel = (currentLevel - 1 + count) % count;
        }
        else
        {
            currentLevel = (currentLevel + 1 ) % count;
        }
        textShowLevel.text = GameManager.Instance.levelNames[currentLevel];
    }
    public void HandleLevelStart()
    {
        string levelName = GameManager.Instance.levelNames[currentLevel];
        SceneManager.LoadScene(levelName);
    }
}