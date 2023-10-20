using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController : MonoBehaviour
{
    public List<Vector3> lanes;
    [HideInInspector]
    public int laneNb=2;
    bool canMove = true;
    List<BaseForm> forms = new List<BaseForm>();
    BaseForm currentForm;
    [SerializeField]
    float changeFormCooldown=15f;
    bool canChangeForm = true;
    [SerializeField]
    GameObject formLook;
    [SerializeField]
    List<Material> formMat = new List<Material>();
    [SerializeField]
    UnityEvent Damageplayer;
    bool isLastFormUnlocked;
    int lastFormRequirements;
    UnityEvent Fall;
    UnityEvent TryUltimate;
    UnityEvent UnlockUltimate;
    GameObject manager;
    float timeUlti=0f;
    bool isAchvTimeUltiGet = false;
    UnityEvent WaveReset;
    UnityEvent FormCoolDown;
    UnityEvent UltiCoolDown;

    private void Start()
    {
        forms.Add(this.GetComponent<EvadeForm>());
        forms.Add(this.GetComponent<BusterForm>());
        forms.Add(this.GetComponent<BunnyForm>());
        forms.Add(this.GetComponent<UltimateForm>());
        currentForm = forms[0];
        lastFormRequirements = 100;
        isLastFormUnlocked = false;

        //Creation of events
        manager = GameObject.FindGameObjectWithTag("Manager");
        if (Fall == null)
        {
            Fall = new UnityEvent();
        }
        Fall.AddListener(FallEvent);

        if (TryUltimate == null)
        {
            TryUltimate = new UnityEvent();
        }
        TryUltimate.AddListener(TryUltimateEvent);
        
        if (UnlockUltimate == null)
        {
            UnlockUltimate = new UnityEvent();
        }
        UnlockUltimate.AddListener(UnlockUltimateEvent);
        
        if (WaveReset == null)
        {
            WaveReset = new UnityEvent();
        }
        WaveReset.AddListener(WaveResetEvent);

        if (FormCoolDown == null)
        {
            FormCoolDown = new UnityEvent();
        }
        FormCoolDown.AddListener(FormCooldownEvent);

        if (UltiCoolDown == null)
        {
            UltiCoolDown = new UnityEvent();
        }
        UltiCoolDown.AddListener(UltiCoolDownEvent);

    }

    void Update()
    {
        //Calls the various operations the player can make
        ChangeForm();
        Move();
        Action();
        Ulti();

        //Unlocks an achievement when 10 sec. are passed with a certain form
        if(!isAchvTimeUltiGet)
        {
            if (currentForm == forms[3])
            {
                timeUlti += Time.deltaTime;
                if (timeUlti >= 10)
                {
                    manager.GetComponent<AchievementManager>().UGoodUnlock();
                }
            }
            else
            {
                timeUlti = 0;
            }
        }
    }

    //Actions that the player makes calls functions of the class of the current form
    void Move()
    {
        if (canMove)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                laneNb = currentForm.Moving("Side", -1);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                laneNb = currentForm.Moving("Side", 1);
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                laneNb = currentForm.Moving("Up", 1);
            }

            if (this.transform.position == lanes[0] || this.transform.position == lanes[lanes.Count - 1])
            {
                canMove = false;
                StartCoroutine(FallingCooldown());
            }
        }
    }
    void Action()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            currentForm.Action();
        }
    }
    void Ulti()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentForm.Ultimate();
            UltiCoolDown.Invoke();
        }
    }

    //Changes the form of the player
    void ChangeForm()
    {
        if (canChangeForm)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && currentForm != forms[0])
            {
                currentForm = forms[0];
                formLook.GetComponent<Renderer>().material = formMat[0];
                StartCoroutine(FormCooldown());
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && currentForm != forms[1])
            {
                currentForm = forms[1];
                formLook.GetComponent<Renderer>().material = formMat[1];
                StartCoroutine(FormCooldown());
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && currentForm != forms[2])
            {
                currentForm = forms[2];
                formLook.GetComponent<Renderer>().material = formMat[2];
                StartCoroutine(FormCooldown());
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) && currentForm != forms[3])
            {
                if (isLastFormUnlocked)
                {
                    currentForm = forms[3];
                    formLook.GetComponent<Renderer>().material = formMat[3];
                    float newCooldown = changeFormCooldown;
                    changeFormCooldown = 0;
                    FormCoolDown.Invoke();
                    changeFormCooldown = newCooldown;
                }
                else
                {
                    //Manages the Unlocking of the 4th form && 2 of the achievements linked with the process
                    TryUltimate.Invoke();
                    lastFormRequirements--;
                    if (lastFormRequirements <= 0)
                    {
                        UnlockUltimate.Invoke();
                        isLastFormUnlocked = true;
                        currentForm = forms[3];
                        formLook.GetComponent<Renderer>().material = formMat[3];
                        float newCooldown = changeFormCooldown;
                        changeFormCooldown = 0;
                        FormCoolDown.Invoke();
                        changeFormCooldown = newCooldown;
                    }
                }
            }
            WaveReset.Invoke();
            currentForm.ResetLane(laneNb);
        }
    }
    
    public bool GetDamageStatus()
    {
        return currentForm.GetCanBeDamaged();
    }

    IEnumerator FallingCooldown()
    {
        yield return new WaitForSeconds(1.5f);
        canMove = true;
        laneNb = 2;
        currentForm.ResetLane(laneNb);
        this.gameObject.transform.position = lanes[laneNb];
        Damageplayer.Invoke();
        Fall.Invoke();
    }

    IEnumerator FormCooldown()
    {
        FormCoolDown.Invoke();
        canChangeForm = false;
        yield return new WaitForSeconds(changeFormCooldown);
        canChangeForm = true;
    }

    //Calls of Events to unlock Achievements
    void FallEvent()
    {
        manager.GetComponent<AchievementListener>().AddSuicideCount();
        manager.GetComponent<AchievementManager>().RickRollUnlock();
        if (currentForm == forms[3])
        {
            manager.GetComponent<AchievementManager>().WhatUnlock();
        }
    }
    void TryUltimateEvent()
    {
        manager.GetComponent<AchievementManager>().WorthUnlock();
    }
    void UnlockUltimateEvent()
    {
        manager.GetComponent<AchievementManager>().FineUnlock();
    }


    void WaveResetEvent()
    {
        GameObject wave = GameObject.FindGameObjectWithTag("WaveManager");
        wave.GetComponent<WavePassedManager>().WaveReset();
    }

    void FormCooldownEvent()
    {
        manager.GetComponent<ActionUIManager>().CoolDownForm();
    }
    void UltiCoolDownEvent()
    {
        manager.GetComponent<ActionUIManager>().CoolDownUlti();
    }

    public string GetForm()
    {
        if (currentForm == forms[0])
        {
            return "Evade";
        }
        else if (currentForm == forms[1])
        {
            return "Buster";
        }
        else if (currentForm == forms[2])
        {
            return "Bunny";
        }
        else if (currentForm == forms[3])
        {
            return "Ultimate";
        }
        else
        {
            return null;
        }

    }

    public float GetFormCooldown()
    {
        return changeFormCooldown;
    }

    public BaseForm GetCurrentForm()
    {
        return currentForm;
    }
}
