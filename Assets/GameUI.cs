using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TruotTuyet;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum State
{
    Running,
    Stop,
    Jumping
}

public class GameUI : Singleton<GameUI>
{
    public Button back;
    public Button playAgain;
    public Button menu;
    public background bg;

    private GameObject level;
    public GameObject lose;
    public GameObject tap;

    public State currentState;

    // Start is called before the first frame update
    void Start()
    {
        back.onClick.AddListener(ExitGame);
        menu.onClick.AddListener(RestartGame);
        playAgain.onClick.AddListener(playAgainGame);

        SetState(State.Stop);
    }

    private void playAgainGame()
    {
        if (GameDataManager.Instance.playerData.intDiamond >= 100)
        {
            GameDataManager.Instance.playerData.SubDiamond(100);
            //continue game
            SceneManager.LoadScene("Game");
        }
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    [Button]
    public void ShowLose()
    {
        SetState(State.Stop);
        lose.SetActive(true);
    }

    public float countTime;
    public float timeee = 1;

    public void Update()
    {
        if (currentState == State.Running)
        {
            countTime -= Time.deltaTime;

            if (countTime <= 0)
            {
                PointTMP.Instance.Add();

                countTime = timeee;
            }
        }

        if (Input.GetMouseButtonDown(0) && currentState == State.Stop && !PointerIsOverUI(Input.mousePosition))
        {
            if (GameDataManager.Instance.playerData.time > 0)
            {
                tap.SetActive(false);
                Player.Instance.Jump();
                SetState(State.Running);
            }
            else
            {
                Sub.SetActive(true);
            }
        }
    }
    
    public static bool PointerIsOverUI(Vector2 screenPos)
    {
        var hitObject = UIRaycast(ScreenPosToPointerData(screenPos));
        return hitObject != null && hitObject.layer == LayerMask.NameToLayer("UI");
    }
 
    static GameObject UIRaycast (PointerEventData pointerData)
    {
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);
 
        return results.Count < 1 ? null : results[0].gameObject;
    }
 
    static PointerEventData ScreenPosToPointerData (Vector2 screenPos)
        => new(EventSystem.current){position = screenPos};

    public GameObject Sub;

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void NextLevel()
    {
        bg.Show();
        RandomLevel();
    }

    private void RandomLevel()
    {
        /*if (level != null)
        {
            Destroy(level);
        }

        level = Instantiate(levels[Random.Range(0, levels.Length)]);*/
    }

    [Button]
    public void Jump()
    {
        Player.Instance.Jump();
    }

    private float duration = 1f;


    public void EndJump()
    {
    }

    public void SetState(State state)
    {
        currentState = state;
    }
}