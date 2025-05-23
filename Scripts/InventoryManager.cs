using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    void Awake()
    {
        Instance = this;
    }


    public GameObject menu;
    
    public GameObject inventoryIcon;
    public GameObject taskObject;
    public bool menuActive;
    public GameObject descriptionImage;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;


    public GameObject settingMenu;
    public bool settingActive;


        void Start()
    {
        descriptionImage.SetActive(false);
        itemName.SetText("");
        itemDescription.SetText("");
        menu.SetActive(false);
    }

    
    void Update()
    {
        if(Input.GetKeyDown("i") && menuActive && !settingActive)
        {
            Time.timeScale = 0;
            menu.SetActive(true);
            inventoryIcon.SetActive(false);
            taskObject.SetActive(false);
            menuActive = false;
            //PlayerMovement.Instance.canMove = false;

            Cursor.lockState = CursorLockMode.Confined; //imleci acar item secip ayrinitisina bakmak icin.
        }
        else if(Input.GetKeyDown("escape") && !menuActive)
        {
            Time.timeScale = 1;
            menu.SetActive(false);
            inventoryIcon.SetActive(true);
            taskObject.SetActive(true);
            menuActive = true;
            //PlayerMovement.Instance.canMove = true;

            descriptionImage.SetActive(false);
            itemName.SetText("");
            itemDescription.SetText("");

            Cursor.lockState = CursorLockMode.Locked; // imleci yok eder
        }
        
        //AYAR MENUSU
        else if(Input.GetKeyDown("escape") && menuActive && !settingActive) //ayarlari ac
        {
            Time.timeScale = 0;
            settingMenu.SetActive(true);
            settingActive = true;
            Cursor.lockState = CursorLockMode.Confined;
        }

    }

    
}
