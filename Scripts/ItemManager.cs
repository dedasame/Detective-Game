using UnityEngine;

public class ItemManager : MonoBehaviour
{   
    public int itemIndex; // item indexi
    public string description; // item aciklamasi?
    public string itemName;
    public Sprite unfoundIcon; //bulunmadiysa icon
    public Sprite foundIcon; // bulunduysa icon
    public SlotManager slot;
    public string proofExplanation; //proof itemlarinin aciklamasi

    void Start()
    {
        slot = InventoryManager.Instance.transform.GetChild(0).GetChild(0).GetChild(itemIndex).GetComponent<SlotManager>();
        slot.item = this;
    }

}
