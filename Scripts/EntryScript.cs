using TMPro;
using UnityEngine;

public class EntryScript : MonoBehaviour
{
    public TextMeshProUGUI rank;
    public TextMeshProUGUI tier;
    public TextMeshProUGUI username;
    public TextMeshProUGUI scoreTime;
    public float time;

    public void SetColor() //Rank rengini ayarlar ->> Unty cloud değerlerine göre değişilmesi gerekiyor -> oto çekme -> ???
    {
        if(time < 100)
        {
            tier.color = ScoreBoardScript.Instance.colors[0];
        }
        else if(time < 150)
        {
            tier.color = ScoreBoardScript.Instance.colors[1];
        }
        else
        {
            tier.color = ScoreBoardScript.Instance.colors[2];
        }
    }


}
