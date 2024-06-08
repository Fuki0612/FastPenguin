using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text;


public class ResultSceneManager : MonoBehaviour
{
    public Text pastRecord;
    public Text record;
    public Text message;
    public Sprite first;
    public Sprite notFirst;
    public GameObject penguin;
    private SpriteRenderer penguinSprite;
    private float pastScore;
    private float score;
    // Start is called before the first frame update
    void Start()
    {
        penguinSprite = penguin.GetComponent<SpriteRenderer>();
        using (FileStream fileStream = new("score.txt", FileMode.OpenOrCreate, FileAccess.Read))
        {
            using StreamReader streamReader = new(fileStream, Encoding.UTF8);
            if (!streamReader.EndOfStream)
            {
                pastScore = float.Parse(streamReader.ReadLine());
            }
            else
            {
                pastScore = 9999.99f;
            }
        }
        score = CharacterManager.timeScore;
        record.text = String.Format("‘O‰ñ‚Ü‚Å‚Ì1ˆÊ\n{0:#.##}•b", pastScore);
        pastRecord.text = String.Format("¡‰ñ‚Ì‹L˜^\n{0:#.##}•b", score);
        if (pastScore > score)
        {
            message.text = "YOU ARE FIRST PENGUIN!";
            penguinSprite.sprite = first;
            using (FileStream fileStream = new("score.txt", FileMode.Truncate, FileAccess.Write))
            {
                using StreamWriter streamWriter = new(fileStream, Encoding.UTF8);
                streamWriter.Write(score.ToString());
            }
        }
        else
        {
            message.text = "YOU ARE NOT FIRST...";
            penguinSprite.sprite = notFirst;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}
