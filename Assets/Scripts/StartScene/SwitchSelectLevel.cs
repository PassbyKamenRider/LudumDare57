using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

class SwitchSelectLevel : MonoBehaviour
{
    public AudioSource clickAudio;
    public int currentLevel = 0;
    public Image levelImage;
    public Sprite[] levelSprites;
    public void HandleLevelSwitch(bool isLeft)
    {
        int count = GameData.levelReached+1;
        if (isLeft)
        {
            currentLevel = (currentLevel - 1 + count) % count;
        }
        else
        {
            currentLevel = (currentLevel + 1 ) % count;
        }
        levelImage.sprite = levelSprites[currentLevel];
    }
    public void HandleLevelStart()
    {
        string levelName = GameManager.Instance.levelNames[currentLevel];
        SceneManager.LoadScene(levelName);
    }

    public void PlayClickAudio()
    {
        clickAudio.Play();
    }
}