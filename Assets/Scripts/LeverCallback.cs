using UnityEngine;
using UnityEngine.Events;

public class LeverCallback : MonoBehaviour
{
    [SerializeField]
    private UnityEvent dragLever;
    [SerializeField]
    private Transform dragObject;
    private Quaternion currentRotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dragObject = transform.Find("Pivot");
        currentRotation = dragObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.CompareTag("Player"))
        {
            Debug.Log("InEnter");
            PlayerController.action += DragLever;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        Debug.Log("InExit");
        if (collision.CompareTag("Player"))
        {
            Debug.Log("InExit");
            PlayerController.action -= DragLever;
        }
    }

    private void DragLever()
    {
        Debug.Log("Drag");
        dragLever.Invoke();
        if (dragObject.rotation.z == 0)
        {
            dragObject.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y,
                90);
        }
        else {
            dragObject.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y,
                0);
        }

    }
}
