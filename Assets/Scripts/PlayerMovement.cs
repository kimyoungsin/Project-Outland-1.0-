using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Player player;
    public Weapons weapon;

    public float MovementSpeed = 4f;
    public float SprintScale = 1.5f;
    public bool Stealth = false;
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


    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        UItext = FindObjectOfType<UIText>();
        skillSlot = FindObjectOfType<SkillSlot>();
    }


    void FixedUpdate()
    {
        CheckItem();
        if (MoveStop == false)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            Anim.SetFloat("MoveX", rigid.velocity.x);
            Anim.SetFloat("MoveY", rigid.velocity.y);



            if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
            {
                if (WeaponShow == false)
                {
                    Anim.SetBool("isWalk", true);
                }
                Anim.SetFloat("LastMoveX", Input.GetAxisRaw("Horizontal"));
                Anim.SetFloat("LastMoveY", Input.GetAxisRaw("Vertical"));
            }

            if (Input.GetKey(KeyCode.LeftShift))
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
            else
            {
                rigid.velocity = movement * MovementSpeed;
            }
        }
        



    }

    void Update()
    {
        

        if(MoveStop == false)
        {
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                player.TPRefreshOff();
            }
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                if (!StopStealthKey)
                {
                    StopStealthKey = true;
                    if (!Stealth)
                    {
                        //Debug.Log("<은신>");

                        MovementSpeed = MovementSpeed / 3;
                    }
                    else
                    {
                        //Debug.Log("<발각>");

                        MovementSpeed = 4;

                    }
                    Invoke("IsStealth", 0.25f);
                }

            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                PickupItem();
                NPCDialogue();

            }
        }
        
    }

    public void StopMove()
    {
        if(MoveStop == true)
        {
            MoveStop = false;
        }
        else if(MoveStop == false)
        {
            MoveStop = true;
        }
        
    }
    void IsStealth()
    {
        StopStealthKey = false;
        if (Stealth == false)
        {
            Stealth = true;
            //Debug.Log("은신여부: " + Stealth);
        }
        else
        {
            Stealth = false;
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
            UItext.InteractionName = "금속: " + collision.GetComponent<MetalPickUp>().MetalValue;
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
        if(hitInfo = Physics2D.CircleCast(transform.position, 1f, Vector2.up, 0f, ItemLayer))
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
        if(isPickup)
        {
            theInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item, hitInfo.transform.GetComponent<ItemPickUp>().item.DropItemCount);
            Destroy(hitInfo.transform.gameObject);
            UItext.InteractionName = "";
        }
        else if(isMetalPickup)
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
