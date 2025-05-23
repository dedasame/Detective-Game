using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BlinkScript : MonoBehaviour
{
    static public BlinkScript Instance;
    void Awake()
    {
        Instance = this;
    }

    bool isBlinking;
    bool isDiff;
    public Image panel;

    void Start()
    {
        panel = GetComponentInChildren<Image>();
        PanelBlackToDisappear();
    }
    void Update()
    {
        if(isBlinking) //siyah -> transparent
        {
            panel.color = Color.Lerp(panel.color, Color.clear, 1f*Time.fixedDeltaTime);
            if(panel.color.a < 0.1f)
            {
                panel.color = Color.clear;
                isBlinking = false;
                panel.gameObject.SetActive(false);
            }
        }

        if(isDiff) //transparet -> siyah
        {
            panel.color = Color.Lerp(panel.color, Color.black, 1f*Time.fixedDeltaTime);
            if(panel.color.a > 0.9f)
            {
                panel.color = Color.black;
                isDiff = false;
                //Sonraki sahneye geç
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
    public void PanelBlackToDisappear()
    {
        isBlinking = true;
        panel.gameObject.SetActive(true);
        panel.color = Color.black;
    }

    public void PanelDissapearaToBlack()
    {
        isDiff = true;
        panel.gameObject.SetActive(true);
        panel.color = Color.clear;
    }

}
