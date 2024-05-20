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
        if (slip > 0)
        {
            slip -= 5;
        }
        Update();
    }

    void Update()
    {
        SlipText.text = string.Format("ŠŠ‚é {000}", slip);
    }
}
