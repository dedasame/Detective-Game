using TMPro;
using UnityEngine;

public class Setting : MonoBehaviour
{
    public UnityEngine.UI.Slider volumeSlider;
    public UnityEngine.UI.Slider sensSlideer;
    public bool isGame; //oyun sahnesinde mi?
    public float sens;
    public TextMeshProUGUI volumeText;
    public TextMeshProUGUI sensText;


    void Start()
    {
        if(!PlayerPrefs.HasKey("volume") && !PlayerPrefs.HasKey("sensitivity"))
        {
            PlayerPrefs.SetFloat("volume", 1);
            PlayerPrefs.SetFloat("sensitivity", 1);
        }
        else
        {
            Load();
            AudioListener.volume = volumeSlider.value;
            if(isGame)
            {
                PlayerMovement.Instance.mouseSensX = PlayerPrefs.GetFloat("sensitivity")*sens;
                PlayerMovement.Instance.mouseSensY = PlayerPrefs.GetFloat("sensitivity")*sens;
            }
        }
    }

    public void ChangeVolume() //volume ayar
    {
        AudioListener.volume = volumeSlider.value;
        volumeText.SetText((volumeSlider.value*sens).ToString("0"));
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
    }

    public void ChangeSens() //sens ayar
    {
        if(isGame)
        {
            PlayerMovement.Instance.mouseSensX = sensSlideer.value*sens;
            PlayerMovement.Instance.mouseSensY = sensSlideer.value*sens;
        }
        sensText.SetText((sensSlideer.value*sens).ToString("0"));
        PlayerPrefs.SetFloat("sensitivity", sensSlideer.value);
    }

    public void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volume");
        sensSlideer.value = PlayerPrefs.GetFloat("sensitivity");
        volumeText.SetText((volumeSlider.value*sens).ToString("0"));
        sensText.SetText((sensSlideer.value*sens).ToString("0"));
    }

    
    public void BackButton()
    {
        transform.gameObject.SetActive(false);

        if(isGame)
        {
            Time.timeScale = 1;
            InventoryManager.Instance.settingActive = false;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked; // imleci yok eder
        }
    }
    

}
