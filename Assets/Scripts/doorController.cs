using UnityEngine;

public class doorController : MonoBehaviour
{
    private bool isOpen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchDoor()
    {
        Debug.Log("InSwitch");
        if (isOpen) { 
            gameObject.SetActive(true);
            isOpen = false;
        }
        else
        {
            gameObject.SetActive(false);
            isOpen = true;
        }
    }
}
