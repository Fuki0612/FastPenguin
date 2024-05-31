using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class SlipValue : MonoBehaviour
{
    public Text SlipText;
    public int slip;
    void Start()
    {
        slip=50;
    }
    public void Click1()
    {
        if (slip < 100)
        {
               slip = slip + 5;
        }
        Update();
    }
    public void Click2()
    {
        if (slip > 20)
        {
            slip -= 5;
        }
        Update();
    }

    void Update()
    {
        StatusSum SumScript;
        GameObject obj = GameObject.Find("Sumtext");
        SumScript = obj.GetComponent<StatusSum>();

        Image addButtonObject;
        GameObject addobj = GameObject.Find("+ButtonSlip");
        addButtonObject = addobj.GetComponent<Image>();
        if (slip == 100)
        {
            addButtonObject.color = new Color(0.1411765f, 0.1490196f, 0.772549f, 0.5215686f);
        }
        else
        {
            addButtonObject.color = new Color(0.1411765f, 0.1490196f, 0.772549f, 0.9215686f);
        }

        Image subButtonObject;
        GameObject subobj = GameObject.Find("-ButtonSlip");
        subButtonObject = subobj.GetComponent<Image>();
        if (slip == 20)
        {
            subButtonObject.color = new Color(0.8823529f, 0.09927912f, 0.09927912f, 0.4705882f);
        }
        else
        {
            subButtonObject.color = new Color(0.8823529f, 0.09927912f, 0.09927912f, 0.8705882f);
        }

        SlipText.text = string.Format("ŠŠ‚é {000}", slip);
    }
}
