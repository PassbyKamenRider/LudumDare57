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
        if (isLeft)
        {
            currentLevel = (currentLevel - 1) % GameManager.Instance.levelNames.Count;
        }
        else
        {
            currentLevel = (currentLevel + 1 ) % GameManager.Instance.levelNames.Count;
        }
        textShowLevel.text = GameManager.Instance.levelNames[currentLevel];
    }
    public void HandleLevelStart()
    {
        string levelName = GameManager.Instance.levelNames[currentLevel];
        SceneManager.LoadScene(levelName);
    }
}