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
        if (swim > 0)
        {
            swim -= 5;
        }
        Update();
    }

    // Update is called once per frame
    void Update()
    {
        SwimText.text = string.Format("ЙjВо {000}", swim);
    }
}
