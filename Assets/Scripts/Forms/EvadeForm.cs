using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EvadeForm : BaseForm
{
    [SerializeField]
    GameObject UltiZone;
    [SerializeField]
    float ultimateDuration=2f;
    UnityEvent Ulti;

    protected override void Start()
    {
        base.Start();
        canSideMove = true;
        canJump = false;
        canAction = false;
        ultimateMaxCooldown = 10f;
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
            StartCoroutine(EvadeUltimate());
        }
    }
    void UltiEvent()
    {
        GameObject manager = GameObject.FindGameObjectWithTag("Manager");
        manager.GetComponent<AchievementManager>().EliatropUnlock();
    }

    IEnumerator EvadeUltimate()
    {
        canUltimate = false;
        UltiZone.SetActive(true);
        this.gameObject.GetComponent<CapsuleCollider>().isTrigger = false;
        yield return new WaitForSeconds(ultimateDuration);
        UltiZone.SetActive(false);
        this.gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
        yield return new WaitForSeconds(ultimateCooldown);
        canUltimate = true;
    }
}
