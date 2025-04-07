using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Player : Controller
{
    public Animator anim;
    public Animator getDamageAnim;

    public PlayerItem playerItem;
    public PlayerHUD playerHUD;

    public float maxBreath;
    public float breath
    {
        get { return _breath; }
        set { _breath = Mathf.Min(value, maxBreath); }
    }

    private float _breath;

    public InteractableObject currentInteraction;
    public float interactionRange;
    public Text interactionText;
    public RectTransform interactionUI;

    public float moveSpeed;
    public float moveSpeedBuff;
    public float moveSpeedWeight;

    public bool isInvisible;

    private bool isCanAttack = true;
    public float attackDamage;
    public Transform attackRange;

    private float teleportValue;
    public ParticleSystem teleportEffect;

    private float puzzleResetValue;
    public GameObject resetUI;
    public Image resetFill;

    public override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (playerHUD != null)
            playerHUD.UpdateHUD(this);

        if (isDeath) return;

        GetInteraction();

        if (GamePlayManager.instance.isCutScene)
        {
            anim.SetBool("isWalk", false);
            return;
        }

        UpdateTeleport();

        DecreaseBreath();

        if (teleportValue > 0)
        {
            anim.SetBool("isWalk", false);
            return;
        }

        UpdateMove();

        UpdateInteraction();

        UpdateAttack();

        ResetPuzzle();
    }

    private void GetInteraction()
    {
        Collider[] objs = Physics.OverlapSphere(transform.position, interactionRange, LayerMask.GetMask("Item"));

        if (objs.Length > 0)
        {
            interactionUI.GetComponent<Animator>().SetBool("isShow", true);
            currentInteraction = objs.OrderBy(a => Vector3.Distance(transform.position, a.transform.position)).First().GetComponent<InteractableObject>();
            interactionText.text = currentInteraction.myName;

            Vector3 pos = Camera.main.WorldToViewportPoint(currentInteraction.transform.position);
            pos.x = pos.x * Screen.width - (Screen.width / 2f);
            pos.y = pos.y * Screen.height - (Screen.height / 2f);
            pos.z = 0;

            interactionUI.anchoredPosition = pos;
        }
        else
        {
            interactionUI.GetComponent<Animator>().SetBool("isShow", false);
            currentInteraction = null;
            interactionText.text = string.Empty;
        }
    }

    private void UpdateTeleport()
    {
        if (Input.GetKey(KeyCode.T))
        {
            teleportValue += Time.deltaTime;

            if (!teleportEffect.isPlaying)
                teleportEffect.Play();
        }
        else
        {
            teleportValue = 0;

            if (teleportEffect.isPlaying)
                teleportEffect.Stop();
        }

        if (teleportValue >= 1f)
        {
            GamePlayManager.instance.isCutScene = true;
            teleportValue = 0f;
            teleportEffect.Stop();

            FadeManager.instance.SetFade(1f, Color.white, () =>
            {
                transform.position = Vector3.zero;
            },
            () =>
            {
                GamePlayManager.instance.isCutScene = false;
            });
        }
    }

    private void DecreaseBreath()
    {
        float value = 1 + GamePlayManager.instance.stageIndex * 0.2f;

        breath -= Time.deltaTime * value;

        if (breath <= 0)
            Death();
    }

    private void UpdateMove()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(h, 0, v).normalized;

        GetComponent<Rigidbody>().MovePosition(transform.position + dir * moveSpeed * moveSpeedBuff * moveSpeedWeight * (isCanAttack ? 1.0f : 0.5f));
        if (dir != Vector3.zero)
        {
            anim.SetBool("isWalk", true);
            float angle = Mathf.Atan2(h, v) * Mathf.Rad2Deg;
            anim.transform.eulerAngles = new Vector3(0, Mathf.LerpAngle(anim.transform.eulerAngles.y, angle, Time.deltaTime * 20f), 0);
        }
        else
        {
            anim.SetBool("isWalk", false);
        }
    }

    private void UpdateInteraction()
    {
        if (Input.GetKeyDown(KeyCode.F) && currentInteraction != null)
        {
            currentInteraction.Interaction();
        }
    }

    private void UpdateAttack()
    {
        if (!isCanAttack) return;

        if (Input.GetKey(KeyCode.Space))
        {
            StopCoroutine(nameof(AttackCoroutine));
            StartCoroutine(nameof(AttackCoroutine));
        }
    }

    private IEnumerator AttackCoroutine()
    {
        isCanAttack = false;

        anim.SetTrigger("Attack");

        Vector3 pos = attackRange.transform.position;
        Vector3 size = Vector3.Scale(attackRange.transform.lossyScale, attackRange.GetComponent<BoxCollider>().size);

        Collider[] objs = Physics.OverlapBox(pos, size / 2f, attackRange.rotation, LayerMask.GetMask("Enemy"));

        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].TryGetComponent(out Controller enemy))
            {
                enemy.GetDamage(attackDamage);
            }
        }

        yield return new WaitForSeconds(0.5f);

        isCanAttack = true;
    }

    private void ResetPuzzle()
    {
        if (Input.GetKey(KeyCode.R))
        {
            puzzleResetValue += Time.deltaTime;
        }
        else
        {
            puzzleResetValue -= Time.deltaTime;
        }

        puzzleResetValue = Mathf.Clamp01(puzzleResetValue);
        resetFill.fillAmount = puzzleResetValue;
        resetUI.SetActive(puzzleResetValue > 0);

        if (puzzleResetValue >= 1f)
        {
            puzzleResetValue = 0;
            GamePlayManager.instance.currentRoom.myPuzzle.PuzzleReset();
            resetUI.SetActive(false);
        }
    }

    public override void Initialize()
    {
        UpdateState();

        base.Initialize();

        breath = maxBreath;

    }

    private void UpdateState()
    {
        switch (GameManager.instance.breathState)
        {
            case 0:
                maxBreath = 100;
                break;

            case 1:
                maxBreath = 150;
                break;

            case 2:
                maxBreath = 250;
                break;

            case 3:
                maxBreath = 400;
                break;
        }

        switch (GameManager.instance.backpackState)
        {
            case 0:
                playerItem.maxWeight = 150;
                playerItem.maxItemCount = 4;
                break;

            case 1:
                playerItem.maxWeight = 250;
                playerItem.maxItemCount = 6;
                break;

            case 2:
                playerItem.maxWeight = 450;
                playerItem.maxItemCount = 8;
                break;
        }

        switch (GameManager.instance.weaponState)
        {
            case 0:
                attackDamage = 1f;
                break;

            case 1:
                attackDamage = 1.5f;
                break;

            case 2:
                attackDamage = 2.5f;
                break;

            case 3:
                attackDamage = 4f;
                break;
        }
    }

    public override void GetDamage(float damage)
    {
        base.GetDamage(damage);
        getDamageAnim.SetTrigger("GetDamage");
    }

    public override void Death()
    {
        base.Death();
        anim.SetTrigger("Death");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
