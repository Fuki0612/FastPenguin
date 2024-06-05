using UnityEngine;
using UnityEngine.UI;

public class FlyValue : MonoBehaviour
{
    public Text FlyText;
    public int fly;
    // Start is called before the first frame update
    void Start()
    {
        fly = 0;
    }
    public void Click1()
    {
        if (fly == 0)
        {
            fly = 50;
        }
        else if (fly < 100)
        {
            fly += 5;
        }
    }
    public void Click2()
    {
        if (fly == 50)
        {
            fly = 0;
        }
        else if (fly > 50)
        {
            fly -= 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        StatusSum SumScript;
        GameObject obj = GameObject.Find("Sumtext");
        SumScript = obj.GetComponent<StatusSum>();

        Image addButtonObject;
        GameObject addobj = GameObject.Find("+ButtonFly");
        addButtonObject = addobj.GetComponent<Image>();
        if (fly == 100)
        {
            addButtonObject.color = new Color(0.1411765f, 0.1490196f, 0.772549f, 0.5215686f);
        }
        else
        {
            addButtonObject.color = new Color(0.1411765f, 0.1490196f, 0.772549f, 0.9215686f);
        }

        Image subButtonObject;
        GameObject subobj = GameObject.Find("-ButtonFly");
        subButtonObject = subobj.GetComponent<Image>();
        if (fly == 0)
        {
            subButtonObject.color = new Color(0.8823529f, 0.09927912f, 0.09927912f, 0.4705882f);
        }
        else
        {
            subButtonObject.color = new Color(0.8823529f, 0.09927912f, 0.09927912f, 0.8705882f);
        }

        FlyText.text = string.Format("”ò‚Ô {000}", fly);
    }
}
