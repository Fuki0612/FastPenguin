using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusSum : MonoBehaviour
{
    public Text SumText;
    public int sum;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        SwimValue swimScript;
        GameObject obj1 = GameObject.Find("TextSwim");
        swimScript = obj1.GetComponent<SwimValue>();

        SlipValue slipScript;
        GameObject obj2 = GameObject.Find("TextSlip");
        slipScript = obj2.GetComponent<SlipValue>();

        RunValue runScript;
        GameObject obj3 = GameObject.Find("TextRun");
        runScript = obj3.GetComponent<RunValue>();

        FlyValue flyScript;
        GameObject obj4 = GameObject.Find("TextFly");
        flyScript = obj4.GetComponent<FlyValue>();

        sum = swimScript.swim + slipScript.slip + flyScript.fly + runScript.run;
        Image buttonObject;
        GameObject obj5 = GameObject.Find("Button");
        buttonObject = obj5.GetComponent<Image>();
        if (sum < 200 || 250 < sum )
        {
            buttonObject.color = new Color(0.6745098f,1.0f, 0.1921569f, 0.2078431f);
        }else
        {
            buttonObject.color = new Color(0.6745098f, 1.0f, 0.1921569f, 0.8078431f);
        }
        SumText.text = string.Format("{000}", sum);
    }
}
