using UnityEngine;
using UnityEngine.UI;

public class basicbutton : MonoBehaviour
{
    public Button button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button.onClick.AddListener(SayHello);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SayHello() {
        print("Hello");
    }
}
