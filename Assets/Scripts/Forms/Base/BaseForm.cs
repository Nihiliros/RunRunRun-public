using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseForm : MonoBehaviour
{
    List<Vector3> lanes = new List<Vector3>();
    protected bool canSideMove;
    protected bool canJump;
    protected float ultimateMaxCooldown;
    protected float ultimateCooldown;
    protected bool canAction;
    protected bool canUltimate;
    public bool hasUltimate=true;
    protected int laneNb;
    protected bool canTakeDamage = true;
    UnityEvent ultimateScore;
    UnityEvent isJumping;
    UnityEvent isNotJumping;

    protected virtual void Start()
    {
        ultimateCooldown = 0.0f;
        lanes = this.gameObject.GetComponent<CharacterController>().lanes;
        laneNb = this.gameObject.GetComponent<CharacterController>().laneNb;

        //Creation of events
        if(ultimateScore==null)
        {
            ultimateScore = new UnityEvent();
        }
        ultimateScore.AddListener(ScoreUltimate);

        if (isJumping == null)
        {
            isJumping = new UnityEvent();
        }
        isJumping.AddListener(isJumpingEvent);

        if (isNotJumping == null)
        {
            isNotJumping = new UnityEvent();
        }
        isNotJumping.AddListener(isNotJumpingEvent);
    }

    protected virtual void Update()
    {
        if (ultimateCooldown > 0.0f)
        {
            ultimateCooldown -= Time.deltaTime;
        }
    }

    public virtual int Moving(string direction, int value)
    {
        if (direction == "Side"&&canSideMove)
        {
            if (value > 0)
            {
                laneNb++;
                this.gameObject.transform.position = new Vector3(lanes[laneNb].x,this.transform.position.y, lanes[laneNb].z);
            }
            else
            {
                laneNb--;
                this.gameObject.transform.position = new Vector3(lanes[laneNb].x, this.transform.position.y, lanes[laneNb].z);
            }
        }

        if(direction == "Up" && canJump)
        {
            StartCoroutine(Jumping());
            canJump = false;
        }
        return laneNb;
    }

    public virtual void Action(){}

    public virtual void Ultimate()
    {
        ultimateCooldown = ultimateMaxCooldown;
        ultimateScore.Invoke();
    }

    protected void ScoreUltimate()
    {
        GameObject manager = GameObject.FindGameObjectWithTag("Manager");
        manager.GetComponent<ScoreManagement>().AddScore(500);
        manager.GetComponent<AchievementListener>().UltiAdd();
    }

    protected void isJumpingEvent()
    {
        GameObject manager = GameObject.FindGameObjectWithTag("Manager");
        manager.GetComponent<ActionUIManager>().GetJumpDeacvtivateUI();
    }

    protected void isNotJumpingEvent()
    {
        GameObject manager = GameObject.FindGameObjectWithTag("Manager");
        manager.GetComponent<ActionUIManager>().GetJumpAcvtivateUI();
    }

    public void ResetLane(int lane)
    {
        laneNb = lane;
    }

    //Custom Jump Function (still basic but more flexible for my gameplay needs)
    IEnumerator Jumping()
    {
        isJumping.Invoke();
        float timer = 0;
        while (timer < 1)
        {
            yield return new WaitForFixedUpdate();
            timer += 0.1f;
            this.gameObject.transform.position+= new Vector3(0,0.1f,0);
        }
        timer = 0;
        while (timer < 1)
        {
            yield return new WaitForFixedUpdate();
            timer += 0.05f;
            this.gameObject.transform.position += new Vector3(0, 0.01f, 0);
        }
        timer = 0;
        while (timer < 1)
        {
            yield return new WaitForFixedUpdate();
            timer += 0.1f;
            this.gameObject.transform.position -= new Vector3(0, 0.12f, 0);
        }
        canJump = true;
        isNotJumping.Invoke();
    }


    //Getters for UI/Gameplay events
    public bool GetCanBeDamaged()
    {
        return canTakeDamage;
    }

    public bool GetCanMove()
    {
        return canSideMove;
    }

    public bool GetCanJump()
    {
        return canJump;
    }
    public bool GetCanAction()
    {
        return canAction;
    }
    public bool GetCanUlti()
    {
        return hasUltimate;
    }

    public float GetUltiCool()
    {
        return ultimateCooldown;
    }

    public float GetUltiMaxCooldown()
    {
        return ultimateMaxCooldown;
    }

}