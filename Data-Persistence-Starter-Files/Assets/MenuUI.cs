using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public TextMeshProUGUI textBestCore;
    public InputField textName;
    // Start is called before the first frame update
    private void Start()
    {
        MenuManager.Instance.LoadCore();
        if (MenuManager.Instance.BestcoreSaveMenu > 0)
            textBestCore.text = $"Best Score: {MenuManager.Instance.NameBestCoreMenu}: {MenuManager.Instance.BestcoreSaveMenu}";
        else textBestCore.text = $"WELCOME!";
    }


    public void StartNew()
    {
        SceneManager.LoadScene(1);
        if (textName.text == "")
            MenuManager.Instance.NameSaveMenu = "Player";
        else
            MenuManager.Instance.NameSaveMenu = textName.text;
    }

    public void ResetCore()
    {
        MenuManager.Instance.BestcoreSaveMenu = 0;
        MenuManager.Instance.NameBestCoreMenu = "";
        MenuManager.Instance.NameSaveMenu = "";
        MenuManager.Instance.SaveCore();
        textBestCore.text = $"WELCOME!";
    }

    public void Exit()
    {
        MenuManager.Instance.SaveCore();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
