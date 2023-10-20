using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BusterForm : BaseForm
{
    [SerializeField]
    UnityEvent DestroyAll;
    public float moveCooldown = 2;
    bool canMove = true;
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    GameObject bulletSpawn;
    UnityEvent Spam;
    UnityEvent Ulti;
    UnityEvent isMoving;

    protected override void Start()
    {
        base.Start();
        canSideMove = true;
        canJump = false;
        canAction = true;
        ultimateMaxCooldown = 30f;
        canUltimate = true;

        //Creation of events
        if (Spam == null)
        {
            Spam = new UnityEvent();
        }
        Spam.AddListener(SpamEvent);

        if (Ulti == null)
        {
            Ulti = new UnityEvent();
        }
        Ulti.AddListener(UltiEvent);

        if (isMoving == null)
        {
            isMoving = new UnityEvent();
        }
        isMoving.AddListener(isMovingEvent);
    }

    public override int Moving(string direction,int value)
    {
        if (canMove)
        {
            isMoving.Invoke();
            base.Moving(direction, value);
            canMove = false;
            StartCoroutine(MoveCooldown());
        }
        return laneNb;
    }

    public override void Action()
    {
        base.Action();
        GameObject shotBullet = Instantiate(bullet, bulletSpawn.transform.position, bullet.transform.rotation);
        Spam.Invoke();
    }

    public override void Ultimate()
    {
        if (canUltimate)
        {
            base.Ultimate();
            Ulti.Invoke();
            StartCoroutine(BusterUltimate());
        }
    }

    //Calling of events
    void SpamEvent()
    {
        GameObject manager = GameObject.FindGameObjectWithTag("Manager");
        manager.GetComponent<AchievementListener>().SpamAdd();
    }
    void UltiEvent()
    {
        GameObject manager = GameObject.FindGameObjectWithTag("Manager");
        manager.GetComponent<AchievementManager>().QueenUnlock();
    }
    void isMovingEvent()
    {
        GameObject manager = GameObject.FindGameObjectWithTag("Manager");
        manager.GetComponent<ActionUIManager>().CoolDownMove();
    }

    IEnumerator BusterUltimate()
    {
        canUltimate = false;
        DestroyAll.Invoke();
        yield return new WaitForSeconds(ultimateCooldown);
        canUltimate = true;
    }

    IEnumerator MoveCooldown()
    {
        yield return new WaitForSeconds(moveCooldown);
        canMove = true;
    }
}
