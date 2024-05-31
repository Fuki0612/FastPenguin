using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class RunValue : MonoBehaviour
{
    public Text RunText;
    public int run;
    // Start is called before the first frame update
    void Start()
    {
        run=50;
    }
    public void Click1()
    {
        if (run < 100)
        {
            run = run + 5;
        }
        Update();
    }
    public void Click2()
    {
        if (run > 20)
        {
            run -= 5;
        }
        Update();
    }

    // Update is called once per frame
    void Update()
    {
        StatusSum SumScript;
        GameObject obj = GameObject.Find("Sumtext");
        SumScript = obj.GetComponent<StatusSum>();

        Image addButtonObject;
        GameObject addobj = GameObject.Find("+ButtonRun");
        addButtonObject = addobj.GetComponent<Image>();
        if (run == 100)
        {
            addButtonObject.color = new Color(0.1411765f, 0.1490196f, 0.772549f, 0.5215686f);
        }
        else
        {
            addButtonObject.color = new Color(0.1411765f, 0.1490196f, 0.772549f, 0.9215686f);
        }

        Image subButtonObject;
        GameObject subobj = GameObject.Find("-ButtonRun");
        subButtonObject = subobj.GetComponent<Image>();
        if (run == 20)
        {
            subButtonObject.color = new Color(0.8823529f, 0.09927912f, 0.09927912f, 0.4705882f);
        }
        else
        {
            subButtonObject.color = new Color(0.8823529f, 0.09927912f, 0.09927912f, 0.8705882f);
        }

        RunText.text = string.Format("‘–‚é {000}", run);
    }
}
