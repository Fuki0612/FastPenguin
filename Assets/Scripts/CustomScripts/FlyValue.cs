using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class FlyValue : MonoBehaviour
{
    public Text FlyText;
    public int fly;
    // Start is called before the first frame update
    void Start()
    {
        fly=50;
    }
    public void Click1()
    {
        if (fly < 100)
        {
            fly = fly + 5;
        }
        Update();
    }
    public void Click2()
    {
        if (fly > 0)
        {
            fly -= 5;
        }
        Update();
    }

    // Update is called once per frame
    void Update()
    {
        FlyText.text = string.Format("”ò‚Ô {000}", fly);
    }
}
