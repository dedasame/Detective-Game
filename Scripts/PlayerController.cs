using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    void Awake()
    {
        Instance = this;
    }
    
    public TextMeshProUGUI placeText;
    public TextMeshProUGUI infoText;
    public float rayDistance = 4f;
    public GameObject flashLight;
    public bool isOn;
    public TextMeshProUGUI dialogText;



    public AudioSource itemCollectSound;
    public AudioSource doorSound;
    public AudioSource carSound;
   // public AudioSource prisonSound;
    //public AudioSource detectiveTalkSound;
    public AudioSource flashTicSound;

    public GameObject road;
    
    
    void Update()
    {
        //surekli bir ray atar
        Ray ray = new Ray(transform.position, transform.forward); // Kameranın bakış yönünden ray gönder
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, rayDistance,1<<7)) //7. layera sahip interactive 
        {
            //KANIT
            if (hit.collider.CompareTag("object"))
            {
                infoText.SetText("E");

                if(Input.GetKeyDown(KeyCode.E)) //itemi toplama
                {
                    //ITEM TOPLAMA ISLEMLERI---
                    itemCollectSound.Play();
                    if(hit.collider.GetComponent<ItemManager>())
                    {
                        hit.collider.GetComponent<ItemManager>().slot.isFound = true; // bulundu
                        hit.collider.GetComponent<ItemManager>().slot.SetItemImage(); // bulunan item resmi ayarlandi
                        hit.collider.gameObject.SetActive(false); 
                        GameManager.Instance.foundedItem++;
                        GameManager.Instance.TaskUpdate();
                    }
                    
                }
            }

            //KAPI
            else if(hit.collider.CompareTag("entrance"))
            {
                infoText.SetText("E");
                if(Input.GetKeyDown(KeyCode.E))
                {
                    doorSound.Play();
                    BlinkScript.Instance.PanelBlackToDisappear();
                    transform.position = hit.collider.GetComponent<DoorManagement>().doorPos.transform.position;
                    transform.rotation = hit.collider.GetComponent<DoorManagement>().doorPos.transform.rotation;
                    placeText.SetText(hit.collider.GetComponent<DoorManagement>().placeName);
                    if(hit.collider.GetComponent<DoorManagement>().placeName == "Bla Bla Sokağı")
                    {
                        GameManager.Instance.inRoad = true;
                        GameManager.Instance.rainParticle.SetActive(true);
                        //sesi ac -> daha net bir yagmur sesi
                        road.GetComponent<AudioSource>().Play();

                    }
                    else
                    {
                        GameManager.Instance.inRoad = false;
                        GameManager.Instance.rainParticle.SetActive(false);
                        GameManager.Instance.Invoke("Thunder", 3);
                        //sesi de kapat -> daha boguk bir yagmur sesi
                        road.GetComponent<AudioSource>().Stop();
                    }
                }
            }
            
            //ARABA
            else if(hit.collider.CompareTag("car"))
            {
                carSound.Play();
                infoText.SetText(hit.collider.GetComponent<CarScript>().infoText);
                if(Input.GetKeyDown(KeyCode.E))
                {
                    if(GameManager.Instance.foundedItem == 10)
                    {
                        BlinkScript.Instance.PanelBlackToDisappear();
                        transform.position = hit.collider.GetComponent<CarScript>().tpPos;
                        placeText.SetText(hit.collider.GetComponent<CarScript>().placeName);
                        GameManager.Instance.taskText.color = GameManager.Instance.doneColor;
                        GameManager.Instance.taskIcon.GetComponent<Image>().color = GameManager.Instance.doneColor;
                        Invoke("Nextt", 2);
                        if(hit.collider.GetComponent<CarScript>().placeName == "Bla Bla Sokağı")
                        {
                            GameManager.Instance.inRoad = true;
                            GameManager.Instance.rainParticle.SetActive(true);
                            //sesi ac -> daha net bir yagmur sesi
                        }
                        else
                        {
                            GameManager.Instance.inRoad = false;
                            GameManager.Instance.rainParticle.SetActive(false);
                            GameManager.Instance.Invoke("Thunder", 3);
                            //sesi de kapat -> daha boguk bir yagmur sesi?
                        }   
                    }
                    else
                    {
                        SetDialog(hit.collider.GetComponent<CarScript>().dialogText);
                    } 
                }
            }

            //PRISON
            else if(hit.collider.CompareTag("prison"))
            {
                infoText.SetText("E");
                if(Input.GetKeyDown(KeyCode.E))
                {
                    //prisonSound.Play();
                    PlayerMovement.Instance.canMove = false;
                    hit.collider.GetComponentInParent<PrisonScript>().OpenPrison();
                    GameManager.Instance.taskIcon.SetActive(false);
                    GameManager.Instance.taskText.SetText("");
                }
            }

            //DETECTIVE
            else if(hit.collider.CompareTag("detective"))
            {
                infoText.SetText("E");
                if(Input.GetKeyDown(KeyCode.E))
                {
                    //detectiveTalkSound.Play();
                    SetDialog(hit.collider.GetComponent<DetectiveScript>().dialogText);  
                }
            }
        }
        else
        {
            infoText.SetText("");
        }


        //FLASHLIGHT KONTROLU
        if(Input.GetKeyDown(KeyCode.F) && PlayerMovement.Instance.canMove) 
        {
            flashTicSound.Play();
            //kapali ise 
            if(!isOn)
            {
                flashLight.SetActive(true);
                isOn = true;
            }
            else
            {
                flashLight.SetActive(false);
                isOn = false;
            } 
        }
    }


    public void SetDialog(string text)
    {
        dialogText.SetText(text);
        dialogText.GetComponent<Animator>().SetTrigger("dialog");
    }


    public void Nextt()
    {
        GameManager.Instance.taskText.color = Color.white;
        GameManager.Instance.taskIcon.GetComponent<Image>().color = Color.white;
        GameManager.Instance.taskText.SetText("Şüphelilerin sorgularını incele ve katili bul.");
    }

}
