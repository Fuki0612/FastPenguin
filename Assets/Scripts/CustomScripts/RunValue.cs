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
        if (run > 0)
        {
            run -= 5;
        }
        Update();
    }

    // Update is called once per frame
    void Update()
    {
        RunText.text = string.Format("‘–‚é {000}", run);
    }
}
