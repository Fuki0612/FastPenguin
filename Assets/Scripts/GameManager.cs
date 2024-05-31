using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameObject instance;
    public static int statusSwim = 50;
    public static int statusSlip = 50;
    public static int statusRun = 50;
    public static int statusFly = 50;

    private void Awake()
    {
        if (instance == null)
        {
            instance = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //プレイヤーのステータスセット
    public void StatusSet(int swim, int slip, int run, int fly)
    {
        statusSwim = swim;
        statusSlip = slip;
        statusRun= run;
        statusFly = fly;
    }
}
