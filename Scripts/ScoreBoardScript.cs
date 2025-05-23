using UnityEngine;
using Unity.Services.Authentication;
using UnityEngine.SceneManagement;


public class ScoreBoardScript : MonoBehaviour
{
    public Color[] colors;
    public GameObject[] leaderBoardEntries;
    public GameObject playerInfo;


    public static ScoreBoardScript Instance;
    void Awake()
    {
        Instance = this;
        foreach(GameObject entry in leaderBoardEntries)
        {
            entry.SetActive(false);
        }  
    }

    public void AddScore()
    {
        //AuthenticationService.Instance.UpdatePlayerNameAsync("");
        AuthenticationService.Instance.UpdatePlayerNameAsync(PlayerPrefs.GetString("username"));
        GetComponentInParent<LeaderboardsSample>().AddScore(); 
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }


}


