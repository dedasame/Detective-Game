using UnityEngine;

public class StoryScript : MonoBehaviour
{
    public bool ok;
    void Start()
    {
        Invoke("SetOk", 7);
    }

    void Update()
    {
        if(ok && Input.GetMouseButtonDown(0))
        {
            BlinkScript.Instance.PanelDissapearaToBlack();   
        }
    }

    public void SetOk()
    {
        ok = true;
        Debug.Log("ok");
    }

}
