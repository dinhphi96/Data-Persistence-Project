using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText, BestCoreText;
    public string Name;
    public GameObject GameOverText,Button1;

    private bool m_Started = false;
    private int m_Points, BestCore;

    private bool m_GameOver = false;


    // Start is called before the first frame update
    void Start()
    {
        if (MenuManager.Instance.NameBestCoreMenu == "")
            Name = MenuManager.Instance.NameSaveMenu;
        else Name = MenuManager.Instance.NameBestCoreMenu;
        BestCore = MenuManager.Instance.BestcoreSaveMenu;
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
        BestCoreText.text = $"Best Score: {Name}: {BestCore}";
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
        if (m_Points > BestCore)
        {
            BestCore = m_Points;
            MenuManager.Instance.BestcoreSaveMenu = BestCore;
            MenuManager.Instance.NameBestCoreMenu = MenuManager.Instance.NameSaveMenu;
            Name = MenuManager.Instance.NameBestCoreMenu;
            MenuManager.Instance.SaveCore();

        }


        BestCoreText.text = $"Best Score: {Name}: {BestCore}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        Button1.SetActive(true);
    }

    public void ExitMenu()
    {
        SceneManager.LoadScene(0);
        
    }
}
