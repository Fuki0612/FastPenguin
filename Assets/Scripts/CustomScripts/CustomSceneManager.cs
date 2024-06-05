using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CustomSceneManager : MonoBehaviour
{
    public static int swim;
    public static int slip;
    public static int run;
    public static int fly;
    private GameManager gameManager;
    private Text countText;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        countText = GameObject.Find("Count").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Click()
    {
        StatusSum SumScript;
        GameObject obj = GameObject.Find("Sumtext");
        SumScript = obj.GetComponent<StatusSum>();

        if (SumScript.sum <= 200)
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

            StartCoroutine(START());
        }
    }

    IEnumerator START()
    {
        countText.raycastTarget = true;
        countText.text = "3"; 
        yield return new WaitForSeconds(1);
        countText.text = "2";
        yield return new WaitForSeconds(1);
        countText.text = "1";
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("MainScene");
    }
}
