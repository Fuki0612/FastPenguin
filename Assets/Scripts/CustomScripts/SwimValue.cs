using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class SwimValue : MonoBehaviour
{
    public Text SwimText;
    public int swim;
    // Start is called before the first frame update
    void Start()
    {
        swim=50;
    }
    public void Click1()
    {
        if (swim < 100)
        {
            swim = swim + 5;
        }
        Update();
    }
    public void Click2()
    {
        if (swim > 20)
        {
            swim -= 5;
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
        GameObject addobj = GameObject.Find("+ButtonSwim");
        addButtonObject = addobj.GetComponent<Image>();
        if (swim == 100)
        {
            addButtonObject.color = new Color(0.1411765f, 0.1490196f, 0.772549f, 0.5215686f);
        }else
        {
            addButtonObject.color = new Color(0.1411765f, 0.1490196f, 0.772549f, 0.9215686f);
        }

        Image subButtonObject;
        GameObject subobj = GameObject.Find("-ButtonSwim");
        subButtonObject = subobj.GetComponent<Image>();
        if (swim == 20)
        {
            subButtonObject.color = new Color(0.8823529f, 0.09927912f, 0.09927912f, 0.4705882f);
        }
        else
        {
            subButtonObject.color = new Color(0.8823529f, 0.09927912f, 0.09927912f, 0.8705882f);
        }

        SwimText.text = string.Format("ЙjВо {000}", swim);
    }
}
