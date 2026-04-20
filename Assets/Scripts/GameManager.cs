using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using System.Collections;

public class TurnNode
{
    public Vector3 playerPosition;
    public Vector3 enemyPosition;
    public float playerHealth;
    public int playerAttack;
    public float playerSpeed;

    public TurnNode Next;
    public TurnNode Prev;


    public TurnNode(Vector3 pos, Vector3 ePos, float hp, int atk, float speed)
    {
        playerPosition = pos;
        enemyPosition = ePos;
        playerHealth = hp;
        playerAttack = atk;
        playerSpeed = speed;
    }
}

public class GameManager : MonoBehaviour
{
    public PlayerController playerScript;
    public EnemyController enemyScript;
    public TextMeshProUGUI statsText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI IndicatorText;
    public CinemachineCamera[] cameras;
    public GameObject gameOverPanel;


    private TurnNode head;
    private TurnNode last;
    private TurnNode pivot;

    private int currentCameraIndex = 0;
    private float timer = 8f;
    private bool isReplaying = false;
    private bool isPlayerTurn = true;

    void Update()
    {
        if (isReplaying == true)
        {
            return;
        }

        timer = timer - Time.deltaTime;

        if (timer > 0)
        {
            if(isPlayerTurn)
            {
                playerScript.canMove = true;
                if (enemyScript != null)
                {
                    enemyScript.canMove = false;
                }
                if (IndicatorText != null)
                {
                    IndicatorText.text = "TURNO: JUGADOR";
                }
            }
            else
            {
                playerScript.canMove = false;
                if (enemyScript != null)
                {
                    enemyScript.canMove = true;
                }
                if (IndicatorText != null)
                {
                    IndicatorText.text = "TURNO: ENEMIGO";
                }
            }
        }

        if (timerText != null)
        {
            timerText.text = "Tiempo: " + Mathf.Ceil(timer).ToString();
        }

        if (timer <= 0)
        {
            playerScript.canMove = false;
            if (enemyScript != null)
            {
                enemyScript.canMove = false;
            }
            SaveTurnState();
            isPlayerTurn = !isPlayerTurn;
        }
    }

    public void SaveTurnState()
    {
        timer = 8f;

        Vector3 pPos = playerScript.transform.position;
        Vector3 ePos = enemyScript.transform.position;
        float hp = playerScript.health;
        int atk = playerScript.attack;
        float speed = playerScript.moveSpeed;

        TurnNode newNode = new TurnNode(pPos, ePos, hp, atk, speed);

        if (pivot != null && pivot != last)
        {
            last = pivot;
            last.Next = null;
        }

        if (head == null)
        {
            head = newNode;
            last = newNode;
        }
        else
        {
            last.Next = newNode;
            newNode.Prev = last;
            last = newNode;
        }

        pivot = last;
        UpdateUI();
        ChangeCamera();
    }

    public void StartAutoReplay()
    {
        if (head != null && isReplaying == false)
        {
            StartCoroutine(ReplayRoutine());
        }
    }

    IEnumerator ReplayRoutine()
    {
        isReplaying = true;
        TurnNode tempPivot = head;

        while (tempPivot != null)
        {
            playerScript.transform.position = tempPivot.playerPosition;
            enemyScript.transform.position = tempPivot.enemyPosition;
            playerScript.health = tempPivot.playerHealth;
            playerScript.attack = tempPivot.playerAttack;
            playerScript.moveSpeed = tempPivot.playerSpeed;

            playerScript.ActualizarUI();

            tempPivot = tempPivot.Next;
            yield return new WaitForSeconds(1f);
        }

        isReplaying = false;
        pivot = last;
        UpdateUI();
    }

    public void GoToPreviousTurn()
    {
        if (pivot != null && pivot.Prev != null)
        {
            pivot = pivot.Prev;
            ApplyTurnState();
            UpdateUI();
        }
    }

    public void GoToNextTurn()
    {
        if (pivot != null && pivot.Next != null)
        {
            pivot = pivot.Next;
            ApplyTurnState();
            UpdateUI();
        }
    }

    void ApplyTurnState()
    {
        playerScript.transform.position = pivot.playerPosition;
        enemyScript.transform.position = pivot.enemyPosition;
        playerScript.health = pivot.playerHealth;
        playerScript.attack = pivot.playerAttack;
        playerScript.moveSpeed = pivot.playerSpeed;
        playerScript.ActualizarUI();
    }

    public void UpdateUI()
    {
        if (statsText != null && pivot != null)
        {
            statsText.text = "Turno Actual:\n" + "HP: " + pivot.playerHealth + "\n" + "ATK: " + pivot.playerAttack + "\n" + "VEL: " + pivot.playerSpeed;
        }
    }

    void ChangeCamera()
    {
        if (cameras.Length > 0)
        {
            cameras[currentCameraIndex].Priority = 0;
            currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;
            cameras[currentCameraIndex].Priority = 10;
        }
    }

    public void CancelTimeTravel()
    {
        if (last != null)
        {
            pivot = last;
            ApplyTurnState();
            UpdateUI();
        }
    }

    public void PlayerDie()
    {
        isReplaying = true;
        playerScript.canMove = false;
        if (enemyScript != null)
        {
            enemyScript.canMove = false;
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }
}
