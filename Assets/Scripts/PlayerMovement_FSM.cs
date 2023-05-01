using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_FSM : MonoBehaviour
{
    public Player player;
    public Weapons weapon;

    public float MovementSpeed = 5f;
    public float SprintScale = 1.5f;
    public bool isStealth = false; //현재 은신상태인가 여부(은신 시 정확도 향상, 적 시야범위 축소)
    public bool StopStealthKey;
    public bool MoveStop = false; //대화, 이벤트 등 발생시 움직임 멈춤
    public bool WeaponShow; // 무기 들기 여부(T버튼 무기 꺼내기)
    public bool isPickup; //아이템 줍기 가능여부
    public bool isMetalPickup; //금속(돈)줍기 가능여부

    //public GameObject DialogueObj;
    private UIText UItext;
    public SkillSlot skillSlot;

    public Sprite[] PlayerSprite = new Sprite[9];

    Vector2 movement = new Vector2();

    Rigidbody2D rigid;
    public Animator Anim;

    [SerializeField]
    private Inventory theInventory;

    public LayerMask ItemLayer;
    public LayerMask MetalLayer;
    private RaycastHit2D hitInfo;

    public enum State
    {
        STAND,
        WALK,
        RUN,
        CROUCH,
        CROUCH_WALK,
        STEALTH,
        FIRE,
        MELEE,
        TALK,
        HIT,
        ROLL
    }
    public State state = State.STAND;


    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        UItext = FindObjectOfType<UIText>();
        skillSlot = FindObjectOfType<SkillSlot>();
    }


    void FixedUpdate()
    {
        switch (state)
        {

            case State.STAND:
                Stand();
                break;
            case State.WALK:
                Walk();
                break;
            case State.RUN:
                Run();
                break;
            case State.CROUCH:
                Crouch();
                break;
            case State.CROUCH_WALK:
                Crouch_Walk();
                break;
            case State.FIRE:
                Fire();
                break;
            case State.MELEE:
                Melee();
                break;
            case State.TALK:
                Talk();
                break;
            case State.HIT:
                Hit();
                break;
            case State.ROLL:
                Roll();
                break;
        }


    }

    void Update()
    {
        CheckItem();

        if (MoveStop == false)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            if (!WeaponShow)
            {

                Anim.SetFloat("MoveX", rigid.velocity.x);
                Anim.SetFloat("MoveY", rigid.velocity.y);
            }



            //이동
            if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
            {
                if(state != State.TALK && state != State.MELEE)
                {
                    if (Input.GetKey(KeyCode.LeftShift) && player.tp >= 0 && state != State.CROUCH)
                    {
                        ChangeState(State.RUN);

                    }
                    else if(state == State.CROUCH || state == State.CROUCH_WALK)
                    {
                        ChangeState(State.CROUCH_WALK);
                    }
                    else
                    {
                        ChangeState(State.WALK);
                    }
                    Anim.SetFloat("LastMoveX", Input.GetAxisRaw("Horizontal"));
                    Anim.SetFloat("LastMoveY", Input.GetAxisRaw("Vertical"));
                    Anim.SetBool("isWalk", true);
                }

            }

            //이동 종료시
            if (Input.GetButtonUp("Horizontal") || Input.GetButtonUp("Vertical"))
            {
                if (state == State.WALK || state == State.RUN)
                {
                    Anim.SetBool("isWalk", false);
                    ChangeState(State.STAND);
                }
                else if(state == State.CROUCH_WALK)
                {
                    ChangeState(State.CROUCH);
                }
            }

            //달리기 키 뗄시
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                if (state == State.RUN)
                {
                    player.TPRefreshOff();
                    Anim.SetBool("isWalk", false);
                    ChangeState(State.STAND);
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                if(state == State.CROUCH || state == State.CROUCH_WALK)
                {
                    ChangeState(State.STAND);
                }
                else
                {
                    ChangeState(State.CROUCH);
                }


            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ChangeState(State.ROLL);

            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                PickupItem();
                NPCDialogue();

            }
        }

    }

    public void ChangeState(State state)
    {
        switch (state) //State에서 나가기 전 마지막으로 실행되는 함수들
        {
            case State.STAND:
                StandExit();
                break;
            case State.WALK:
                WalkExit();
                break;
            case State.RUN:
                RunExit();
                break;
            case State.CROUCH:
                CrouchExit();
                break;
            case State.CROUCH_WALK:
                //Crouch_WlakExit();
                break;
            case State.FIRE:
                //FIREExit();
                break;
            case State.MELEE:
                //MELEEExit();
                break;
            case State.TALK:
                //TALKExit();
                break;
            case State.HIT:
                HitExit();
                break;
        }

        this.state = state;

        switch (state)//State로 들어갔을때 처음 실행되는 함수들
        {
            case State.STAND:
                StandEnter();
                break;
            case State.WALK:
                WalkEnter();
                break;
            case State.RUN:
                RunEnter();
                break;
            case State.CROUCH:
                CrouchEnter();
                break;
            case State.CROUCH_WALK:
                //Crouch_WalkEnter();
                break;
            case State.FIRE:
                //FIREEnter();
                break;
            case State.MELEE:
                //MELEEEnter();
                break;
            case State.TALK:
                //TALKEnter();
                break;
            case State.HIT:
                HitEnter();
                break;
        }

    }

    public void StandEnter()
    {

    }

    public void Stand()
    {
        rigid.velocity = movement * MovementSpeed * 0;
        Anim.SetFloat("MoveX", 0);
        Anim.SetFloat("MoveY", 0);
    }

    public void StandExit()
    {

    }

    public void WalkEnter()
    {

    }

    public void Walk()
    {
        rigid.velocity = movement * MovementSpeed;
    }

    public void WalkExit()
    {

    }

    public void RunEnter()
    {

    }

    public void Run()
    {
        if (player.tp >= 0)
        {
            rigid.velocity = movement * MovementSpeed * SprintScale;
            player.tp -= 0.5f;
        }
        if (player.tp <= 0)
        {
            rigid.velocity = movement * MovementSpeed;
            player.TPRefreshOff();
        }

    }

    public void RunExit()
    {

    }


    public void CrouchEnter()
    {

    }


    public void Crouch()
    {
        rigid.velocity = movement * MovementSpeed * 0;
        if (!StopStealthKey)
        {
            StopStealthKey = true;
            if (!isStealth)
            {
                Debug.Log("<은신>");
            }
            else
            {
                Debug.Log("<발각>");

            }
            Invoke("IsStealth", 0.25f);
        }
    }


    public void CrouchExit()
    {

    }

    public void Crouch_Walk()
    {
        rigid.velocity = movement * (MovementSpeed / 3);
    }

    public void Fire()
    {

    }
    public void Melee()
    {
        rigid.velocity = Vector2.zero;
    }
    public void Talk()
    {
        rigid.velocity = Vector2.zero;
    }


    public void HitEnter()
    {

    }
    public void Hit()
    {

    }

    public void HitExit()
    {

    }

    public void Roll()
    {

    }


    public void StopMove()
    {
        if (MoveStop == true)
        {
            ChangeState(State.STAND);
            MoveStop = false;
        }
        else if (MoveStop == false)
        {
            ChangeState(State.TALK);
            MoveStop = true;
        }

    }
    public void IsStealth()
    {
        StopStealthKey = false;
        if (isStealth == false)
        {
            isStealth = true;
            //Debug.Log("은신여부: " + Stealth);
        }
        else
        {
            isStealth = false;
            //Debug.Log("은신여부: " + Stealth);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            UItext.InteractionName = collision.GetComponent<ItemPickUp>().item.itemName;
        }
        else if (collision.gameObject.CompareTag("Metal"))
        {
            UItext.InteractionName = "금속: " + collision.GetComponent<ItemPickUp>().item.DropItemCount;
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Item") || collision.gameObject.CompareTag("Metal"))
        {
            UItext.InteractionName = "";
        }
    }

    public void CheckItem()
    {
        if (hitInfo = Physics2D.CircleCast(transform.position, 1f, Vector2.up, 0f, ItemLayer))
        {
            isPickup = true;
        }
        else if (hitInfo = Physics2D.CircleCast(transform.position, 1f, Vector2.up, 0f, MetalLayer))
        {
            isMetalPickup = true;
        }
        else
        {
            isPickup = false;
            isMetalPickup = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
    public void PickupItem()
    {
        if (isPickup)
        {
            theInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item, hitInfo.transform.GetComponent<ItemPickUp>().item.DropItemCount);
            Destroy(hitInfo.transform.gameObject);
            UItext.InteractionName = "";
        }
        else if (isMetalPickup)
        {
            theInventory.Metal += hitInfo.transform.GetComponent<MetalPickUp>().MetalValue;
            Destroy(hitInfo.transform.gameObject);
            UItext.InteractionName = "";
        }
    }

    public void NPCDialogue()
    {

    }
}
