using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BunnyForm : BaseForm
{
    [SerializeField]
    UnityEvent HealEvent;
    [SerializeField]
    UnityEvent SpeedUpEvent;
    [SerializeField]
    float ultimateDuration=1f;
    UnityEvent Ulti;

    protected override void Start()
    {
        base.Start();
        canSideMove = false;
        canJump = true;
        canAction = false;
        ultimateMaxCooldown = 120f;
        canUltimate = true;

        if (Ulti == null)
        {
            Ulti = new UnityEvent();
        }
        Ulti.AddListener(UltiEvent);
    }

    public override void Ultimate()
    {
        if (canUltimate)
        {
            base.Ultimate();
            Ulti.Invoke();
            StartCoroutine(BunnyUltimate());
        }
    }
    void UltiEvent()
    {
        GameObject manager = GameObject.FindGameObjectWithTag("Manager");
        manager.GetComponent<AchievementManager>().AppleUnlock();
    }

    IEnumerator BunnyUltimate()
    {
        canUltimate = false;
        HealEvent.Invoke();
        SpeedUpEvent.Invoke();
        this.gameObject.transform.position -= new Vector3(0, 1, 0);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(ultimateDuration);
        Time.timeScale = 1;
        this.gameObject.transform.position += new Vector3(0, 1, 0);
        yield return new WaitForSeconds(ultimateCooldown);
        canUltimate = true;
    }
}
