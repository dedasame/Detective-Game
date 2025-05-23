using UnityEngine;
using UnityEngine.SceneManagement;

public class PrisonScript : MonoBehaviour
{
    public static PrisonScript Instance;
    private void Awake()
    {
        Instance = this;
    }

    public GameObject prisonCanvas;
    public GameObject susPrefab;
    public SusScript selectedSus;
    public GameObject wrongSusPanel;
    public bool wrongSus = false;
    
    public void OpenPrison()
    {
        prisonCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        SetSus();
    }

    public void SetSus() //suphelileri olusturur
    {
        for(int i = 0; i < GameManager.Instance.stories[GameManager.Instance.storyIndex].GetComponent<StoryManager>().susList.Length; i++)
        {
            //listelenmiş tum suphelileri olusuturp canvasta gosteriyor
            SusScript sus = Instantiate(GameManager.Instance.stories[GameManager.Instance.storyIndex].GetComponent<StoryManager>().susList[i], prisonCanvas.transform.GetChild(0).GetChild(0).transform);
            if(i == 0) sus.transform.GetComponent<SusManager>().SetSusInfo();
        }
    }

    public void CheckSus()
    {
        if(selectedSus.isMurder) TimeAndNextLevel();
        else
        {
            wrongSusPanel.SetActive(true);
            GameManager.Instance.timer += 5; //yanlış supheli seçilirse 5 saniye ceza
            wrongSus = true;
        }
    }

    void Update()
    {
        if(wrongSus)
        {
            Invoke("Dissapear", 2f);
            wrongSus = false;
        }
    }

    public void Dissapear()
    {
        wrongSusPanel.SetActive(false);
    }


    public void TimeAndNextLevel()
    {
        GameManager.Instance.timeStop = true; //zamanı durdur
        PlayerPrefs.SetFloat("time", GameManager.Instance.timer); //zamanı kaydet
        Debug.Log(PlayerPrefs.GetFloat("time"));
        SceneManager.LoadScene(3);
        
    }


    



}
