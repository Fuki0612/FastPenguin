using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomSceneManager : MonoBehaviour
{
    public static int swim;
    public static int slip;
    public static int run;
    public static int fly;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Click()
    {
        SwimValue swimScript;
        GameObject obj1 = GameObject.Find("TextSwim");
        swimScript = obj1.GetComponent<SwimValue>();
        swim = swimScript.swim;

        SlipValue slipScript;
        GameObject obj2 = GameObject.Find("TextSlip");
        slipScript = obj2.GetComponent<SlipValue>();
        slip = slipScript.slip;

        RunValue runScript;
        GameObject obj3 = GameObject.Find("TextRun");
        runScript = obj3.GetComponent<RunValue>();
        run = runScript.run;

        FlyValue flyScript;
        GameObject obj4 = GameObject.Find("TextFly");
        flyScript = obj4.GetComponent<FlyValue>();
        fly = flyScript.fly;

        gameManager.StatusSet(swim, slip, run, fly);

        SceneManager.LoadScene("MainScene");
    }
}
