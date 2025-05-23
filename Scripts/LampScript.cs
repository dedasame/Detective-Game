using UnityEngine;

public class LampScript : MonoBehaviour
{
    void Start()
    {
        Invoke("Blink", Random.Range(5, 20f));
        
    }
    void Blink(){
        GetComponent<Animator>().SetTrigger("blink");
        Invoke("Blink", Random.Range(5, 20f));
    }
}
