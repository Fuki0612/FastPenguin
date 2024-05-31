using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public GameObject player;
    private Transform playerTransform;
    private float defaultX;
    private float defaultY;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = player.GetComponent<Transform>();
        defaultX = playerTransform.position.x;
        defaultY = playerTransform.position.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new(playerTransform.position.x-defaultX, playerTransform.position.y-defaultY, transform.position.z);
    }
}
