using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Vector3 offset = new Vector3(-10, 10, 0);
    [SerializeField]
    private float speedOfCamera = 2.0f;
    private Vector3 off;
    private bool atCenter = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        off = (player.transform.position + offset - transform.position);
        if (atCenter)
        {
            if (off.magnitude > 3)
            {
                atCenter = false;

            }
        }
        else
        {
            if (off.magnitude > 0.5)
            {
                transform.position += speedOfCamera * Time.deltaTime * off;
            }
            else { atCenter = true; }
            //transform.position = player.transform.position + offset;
        }
    }
}
