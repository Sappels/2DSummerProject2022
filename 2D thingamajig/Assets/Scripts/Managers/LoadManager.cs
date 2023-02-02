using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadManager : MonoBehaviour
{
    private void Start()
    {
        Load();
    }
    public void Load()
    {
        GameManager.Instance.highscore = PlayerPrefs.GetInt("candybeans");
    }
}
