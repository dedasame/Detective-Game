using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    void Awake()
    {
        Instance = this;
    }

    public int storyIndex;
    public GameObject[] stories;
    public Color doneColor;
    public int foundedItem;
    public GameObject taskIcon;
    public TextMeshProUGUI taskText;
    bool thunder = false;
    float counter = 0;
    public bool secondThunder = false;
    public bool inRoad = true;
    public GameObject rainParticle;
    public float timer;
    public bool timeStop;
    public TextMeshProUGUI timerText;
    public GameObject detective;
    public AudioSource thunderSound;

    void Start()
    {
        if (!PlayerPrefs.HasKey("storyIndex"))//Baslangic hikaye yoksa 
        {   
            PlayerPrefs.SetInt("storyIndex", 0); //ilk hikayeyi ata
            PlayerPrefs.Save(); 
        }
        storyIndex = PlayerPrefs.GetInt("storyIndex");


        Invoke("Thunder", 3); //3 saniye sonra ilk yildirim

        if(!PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetFloat("volume", 1);
        }
        else
        {
            AudioListener.volume = PlayerPrefs.GetFloat("volume");
        }

        if(!PlayerPrefs.HasKey("sensitivity"))
        {
            PlayerPrefs.SetFloat("sensitivity", 1);
        }
        else
        {
            PlayerMovement.Instance.mouseSensX = PlayerPrefs.GetFloat("sensitivity")*100;
            PlayerMovement.Instance.mouseSensY = PlayerPrefs.GetFloat("sensitivity")*100;
        }
    }
    void Update()
    {
        if(thunder){
            counter+= Time.fixedDeltaTime;  
            if(counter>.5f && !secondThunder) //ikinci yildirim
            {
                secondThunder = true;
                RenderSettings.ambientIntensity = 20;
                GetComponent<AudioSource>().Play();
            }
            RenderSettings.ambientIntensity = Mathf.Lerp(RenderSettings.ambientIntensity, 1, 3*Time.fixedDeltaTime); //yavas yavas azalt
            if(RenderSettings.ambientIntensity < 1.1f && counter>2)
            {
                thunder = false;
                RenderSettings.ambientIntensity =1;
            }
        }

        if(!timeStop) Timer(); 
    }

    void Timer() //oyun icinde gecen sureyi hesapla
    {
        timer += Time.fixedDeltaTime;
        timerText.SetText(timer.ToString("0.##"));
        PlayerPrefs.SetFloat("timer", timer);
    }
    
    void Thunder()
    {
        if(inRoad)
        {
            thunderSound.Play();
            RenderSettings.ambientIntensity = 5;
            var rnd = Random.Range(3, 10f);
            Invoke("Thunder", rnd);
            thunder = true;
            counter = 0;
            secondThunder = false;
        }
    }
    public void TaskUpdate() //yeni item eklendi -> text duzelt -> kontrol tum item toplandi ise yesil yak
    {
        //inventory -> animasyonunu calistir !!! 
        taskText.SetText("Bulunan item sayısı: " + foundedItem + "/" + stories[storyIndex].GetComponent<StoryManager>().items.Count());
        if(foundedItem == stories[storyIndex].GetComponent<StoryManager>().items.Count()) //tum itemlar toplandi ise
        {
            taskText.color = doneColor;
            taskIcon.GetComponent<Image>().color = doneColor;
            Invoke("NextTask", 2);
        }       
    }
    
    public void NextTask()
    {
        taskText.color = Color.white;
        taskIcon.GetComponent<Image>().color = Color.white;
        taskText.SetText("Polis aracına binerek karakola git.");
    }
    
}
