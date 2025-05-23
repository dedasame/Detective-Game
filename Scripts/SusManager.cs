using UnityEngine;

public class SusManager : MonoBehaviour
{

    void Start()
    {
        GetComponent<UnityEngine.UI.Image>().sprite = GetComponent<SusScript>().image;
    }
    public void SetSusInfo() //Supheli hakkindaki bilgiler
    {
        PrisonScript.Instance.selectedSus = GetComponent<SusScript>();
        transform.GetComponentInParent<SusCanvasManager>().susImage.GetComponent<UnityEngine.UI.Image>().sprite = GetComponent<SusScript>().image;
        transform.GetComponentInParent<SusCanvasManager>().susName.SetText(GetComponent<SusScript>().susName);
        transform.GetComponentInParent<SusCanvasManager>().susJob.SetText(GetComponent<SusScript>().job);
        transform.GetComponentInParent<SusCanvasManager>().susStatement.SetText(GetComponent<SusScript>().statement);
        transform.GetComponentInParent<SusCanvasManager>().susFootNum.SetText(GetComponent<SusScript>().footNum);
        transform.GetComponentInParent<SusCanvasManager>().susCigaretInfo.SetText(GetComponent<SusScript>().fingerPrint);
    }

}
