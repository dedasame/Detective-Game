using System.Linq;
using UnityEngine;


public class SlotManager : MonoBehaviour
{
    public int itemIndex;
    public ItemManager item;
    public GameObject child;
    public bool isFound;
    public bool isUsed;
    void Awake()
    {
        
    }

    void Start()
    {
        if(itemIndex < GameManager.Instance.stories[PlayerPrefs.GetInt("storyIndex")].GetComponent<StoryManager>().items.Count())
        {
            item = GameManager.Instance.stories[PlayerPrefs.GetInt("storyIndex")].GetComponent<StoryManager>().items[itemIndex];
        }
        
        if(!item) //item yoksa
        {
            child.SetActive(false);
        }

        else
        {
            item.slot = this;
            child.GetComponent<UnityEngine.UI.Image>().sprite = item.unfoundIcon;
        }

        if(isFound && !isUsed)
        {
            isUsed = true;
            SetItemImage();
        }
    }

    public void SetItemImage()
    {
        if(item) child.GetComponent<UnityEngine.UI.Image>().sprite = item.foundIcon;
    }

    public void SetImageAndDescription()
    {   
        if(item)
        {
            if(isFound) //item bulunmus
            {

                transform.GetComponentInParent<InventoryManager>().descriptionImage.GetComponent<UnityEngine.UI.Image>().sprite = item.foundIcon;
                transform.GetComponentInParent<InventoryManager>().descriptionImage.SetActive(true);
                transform.GetComponentInParent<InventoryManager>().itemName.SetText(item.itemName);
                transform.GetComponentInParent<InventoryManager>().itemDescription.SetText(item.description);
            }
            else // item bulunmadiysa
            {
                transform.GetComponentInParent<InventoryManager>().descriptionImage.GetComponent<UnityEngine.UI.Image>().sprite = item.unfoundIcon;
                transform.GetComponentInParent<InventoryManager>().itemName.SetText("???");
                transform.GetComponentInParent<InventoryManager>().itemDescription.SetText("???????????????????");
            }
        }
        else
        {
            transform.GetComponentInParent<InventoryManager>().descriptionImage.SetActive(false);
            transform.GetComponentInParent<InventoryManager>().itemName.SetText("");
            transform.GetComponentInParent<InventoryManager>().itemDescription.SetText("");
        }
        
    }



}
