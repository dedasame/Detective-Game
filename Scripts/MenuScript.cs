using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject buttonPanel;
    public GameObject usernamePanel;
    public GameObject settingPanel;
    public TextMeshProUGUI username;


    void Start()
    {
        if(!PlayerPrefs.HasKey("volume") && !PlayerPrefs.HasKey("sensitivity"))
        {
            PlayerPrefs.SetFloat("volume", 1);
            PlayerPrefs.SetFloat("sensitivity", 1);
        }
        else
        {
            AudioListener.volume = PlayerPrefs.GetFloat("volume");
            Debug.Log("vol: "+ PlayerPrefs.GetFloat("volume") + "  sens: " + PlayerPrefs.GetFloat("sensitivity"));
        }
    }

    public void PlayButton()
    {
        buttonPanel.SetActive(false);
        usernamePanel.SetActive(true);
    }

    public void GameButton()
    {
        PlayerPrefs.SetString("username",username.text);
        Debug.Log(PlayerPrefs.GetString("username"));
        SceneManager.LoadScene(1);
    }

    public void SettingButton()
    {
        settingPanel.SetActive(true);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void BackButton()
    {
        settingPanel.SetActive(false);
    }
    
}