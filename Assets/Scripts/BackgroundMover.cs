using UnityEngine;

public class BackgroundMover : MonoBehaviour
{
    private RectTransform _rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            _rectTransform.anchoredPosition += new Vector2(-1f, 0f);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _rectTransform.anchoredPosition += new Vector2(1f, 0f);
        }
    }
}
