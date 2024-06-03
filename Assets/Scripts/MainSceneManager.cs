using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    public GameObject goalText;

    // Start is called before the first frame update
    void Start()
    {
        goalText.SetActive(false);
    }

    public IEnumerator Goal()
    {
        CameraMover.goal = true;
        goalText.SetActive(true);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("ResultScene");
    }
}