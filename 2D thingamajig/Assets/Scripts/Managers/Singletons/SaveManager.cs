using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public int highscore = 0;
    public int airtimeHighscore;
    public int candybeans;


    private static SaveManager instance;
    public static SaveManager Instance { get { return instance; } }

    private void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        CheckIfKeysExistsInRegistry();
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Save()
    {
        CheckIfKeysExistsInRegistry();
        if (GameManager.Instance.score > highscore)
        {
            Debug.Log("Saving!" + " " + GameManager.Instance.score);
            PlayerPrefs.SetInt("candybeansHighscore", GameManager.Instance.score);
        }

        if (GameManager.Instance.airScoreHighestSingle > airtimeHighscore)
        {
            Debug.Log("new airtime highscore!");
            PlayerPrefs.SetInt("airtimeHighscore", GameManager.Instance.airScoreHighestSingle);
        }

        candybeans += GameManager.Instance.score;
        PlayerPrefs.SetInt("candybeans", candybeans);
        Debug.Log("You just made some beans!");

        PlayerPrefs.Save();
    }

    public void Load()
    {
        highscore = PlayerPrefs.GetInt("candybeansHighscore");
        airtimeHighscore = PlayerPrefs.GetInt("airtimeHighscore");
        candybeans = PlayerPrefs.GetInt("candybeans");
    }

    private void CheckIfKeysExistsInRegistry()
    {
        if (!PlayerPrefs.HasKey("candybeans")) PlayerPrefs.SetInt("candybeans", 0);
        if (!PlayerPrefs.HasKey("airtimeHighscore")) PlayerPrefs.SetInt("airtimeHighscore", 0);
        if (!PlayerPrefs.HasKey("candybeansHighscore")) PlayerPrefs.SetInt("candybeansHighscore", 0);

        Load();
    }

}
