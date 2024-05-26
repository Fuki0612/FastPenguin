using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public GameObject player;
    private Transform playerTransform;
    private float defaultX;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = player.GetComponent<Transform>();
        defaultX = playerTransform.position.x;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new(playerTransform.position.x-defaultX, transform.position.y, transform.position.z);
    }
}
