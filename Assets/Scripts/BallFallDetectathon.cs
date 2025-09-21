using UnityEngine;

public class BallFallDetectathon : MonoBehaviour
{
    [SerializeField] int player;
    private Animator anim;
    private BalanceMinigame balanceMinigGame;
    void Start()
    {
        balanceMinigGame = FindAnyObjectByType<BalanceMinigame>();
        anim = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (player == 1)
        {
            balanceMinigGame.canControl_P1 = false;
        }
        else if (player == 2)
        {
            balanceMinigGame.canControl_P2 = false;
        }
        Debug.Log("Uh oh!");
        GetComponent<Rigidbody2D>().angularVelocity = 0;
        gameObject.transform.rotation = Quaternion.identity;

        anim.SetTrigger("fall");

        ScreenShakeManager.Instance.Shake(.1f, 0.2f);
    }

    public void RestoreControl()
    {
        if (player == 1)
        {
            balanceMinigGame.canControl_P1 = true;

        }
        else if (player == 2)
        {
            balanceMinigGame.canControl_P2 = true;
        }
        Debug.Log("Reativei");
    }
}
