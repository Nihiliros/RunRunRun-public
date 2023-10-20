using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

//Manages the 8 UI icons on the bottom-left of the screen
public class ActionUIManager : MonoBehaviour
{
    UnityEvent getFormCoolDown;
    float formCooldown;
    float formCooldownMaxTime;
    [SerializeField]
    List<Image> formCooldownImages;
    [SerializeField]
    List<TextMeshProUGUI> formCooldownText;
    bool isFormCooldown;
    UnityEvent getFormAction;
    GameObject player;
    BaseForm currentForm;
    [SerializeField]
    GameObject RunCross;
    [SerializeField]
    GameObject JumpCross;
    [SerializeField]
    GameObject ActionCross;
    [SerializeField]
    GameObject UltimateCross;
    float ultiCooldown;
    float ultiCooldownMaxTime;
    bool isUltiCooldown;
    [SerializeField]
    Image ultiImage;
    [SerializeField]
    TextMeshProUGUI ultiText;
    UnityEvent getUltiCoolDown;
    [SerializeField]
    List<Image> ActionsImages;
    [SerializeField]
    Image JumpImage;
    float moveCooldown;
    float moveCooldownMaxTime;
    bool isMoveCooldown;
    [SerializeField]
    Image moveImage;
    [SerializeField]
    TextMeshProUGUI moveText;
    UnityEvent getMoveCoolDown;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        SetStarterFormCooldown();
        SetStarterUltiCooldown();
        SetStarterMoveCooldown();

        //Creation of events
        if (getFormCoolDown == null)
        {
            getFormCoolDown = new UnityEvent();
        }
        getFormCoolDown.AddListener(GetFormCooldownEvent);

        if (getFormAction == null)
        {
            getFormAction = new UnityEvent();
        }
        getFormAction.AddListener(GetFormActionEvent);

        if (getUltiCoolDown == null)
        {
            getUltiCoolDown = new UnityEvent();
        }
        getUltiCoolDown.AddListener(GetUltiCooldownEvent);

        if (getMoveCoolDown == null)
        {
            getMoveCoolDown = new UnityEvent();
        }
        getMoveCoolDown.AddListener(GetMoveCooldownEvent);
        StartCoroutine(StartCooldown());
    }

    private void Update()
    {
        if (isFormCooldown)
        {
            formCooldownUI();
        }

        if (isUltiCooldown)
        {
            UltiCooldownUI();
        }

        if (isMoveCooldown)
        {
            MoveCooldownUI();
        }
    }

    //Manages the UI Cooldown for the changes of form icons
    void formCooldownUI()
    {
        formCooldown -= Time.deltaTime;

        if (formCooldown < 0.0f)
        {
            isFormCooldown = false;
            SetStarterFormCooldown();
        }
        else
        {
            SetFormCooldownValues();
        }
    } 
    void SetStarterFormCooldown()
    {
        foreach (Image image in formCooldownImages)
        {
            image.fillAmount = 0.0f;
        }
        foreach (TextMeshProUGUI text in formCooldownText)
        {
            text.text = "";
        }
    }
    void SetFormCooldownValues()
    {
        foreach (Image image in formCooldownImages)
        {
            image.fillAmount = formCooldown/formCooldownMaxTime;
        }
        foreach (TextMeshProUGUI text in formCooldownText)
        {
            text.text = Mathf.CeilToInt(formCooldown).ToString();
        }
    }
    public void CoolDownForm()
    {
        getFormCoolDown.Invoke();
        getFormAction.Invoke();
        if (currentForm.hasUltimate)
        {
            getUltiCoolDown.Invoke();
        }
    }
    void GetFormCooldownEvent()
    {
        formCooldown = player.GetComponent<CharacterController>().GetFormCooldown();
        formCooldownMaxTime = formCooldown;
        isFormCooldown = true;
    }
    void GetFormActionEvent()
    {
        currentForm = player.GetComponent<CharacterController>().GetCurrentForm();
        SetupActionUI();
    }

    //Manages the UI Cooldown for the ultimate icon
    void UltiCooldownUI()
    {
        ultiCooldown -= Time.deltaTime;

        if (ultiCooldown < 0.0f)
        {
            isUltiCooldown = false;
            SetStarterUltiCooldown();
        }
        else
        {
            SetUltiCooldownValues();
        }
    }
    void SetStarterUltiCooldown()
    {
        ultiImage.fillAmount = 0.0f;
        ultiText.text = "";
    }
    void SetUltiCooldownValues()
    {
        ultiImage.fillAmount = ultiCooldown / ultiCooldownMaxTime;
        ultiText.text = Mathf.CeilToInt(ultiCooldown).ToString();
    }
    public void CoolDownUlti()
    {
        getUltiCoolDown.Invoke();
    }
    void GetUltiCooldownEvent()
    {
        ultiCooldown = player.GetComponent<CharacterController>().GetCurrentForm().GetUltiCool();
        ultiCooldownMaxTime = player.GetComponent<CharacterController>().GetCurrentForm().GetUltiMaxCooldown();
        isUltiCooldown = true;
    }
    
    //Manages the UI Cooldown for the Move Action Icon (called only in Buster Form)
    void MoveCooldownUI()
    {
        moveCooldown -= Time.deltaTime;

        if (moveCooldown < 0.0f)
        {
            isMoveCooldown = false;
            SetStarterMoveCooldown();
        }
        else
        {
            SetMoveCooldownValues();
        }
    }
    void SetStarterMoveCooldown()
    {
        moveImage.fillAmount = 0.0f;
        moveText.text = "";
    }
    void SetMoveCooldownValues()
    {
        moveImage.fillAmount = moveCooldown / moveCooldownMaxTime;
        moveText.text = Mathf.CeilToInt(moveCooldown).ToString();
    }
    public void CoolDownMove()
    {
        getMoveCoolDown.Invoke();
    }
    void GetMoveCooldownEvent()
    {
        moveCooldown = player.GetComponent<BusterForm>().moveCooldown;
        moveCooldownMaxTime = moveCooldown;
        isMoveCooldown = true;
    }

    //Manages the UI fot the Jump Icon when jumping
    public void GetJumpDeacvtivateUI()
    {
        JumpImage.fillAmount = 1.0f;
    }
    public void GetJumpAcvtivateUI()
    {
        JumpImage.fillAmount = 0.0f;
    }

    IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(0.1f);
        getFormAction.Invoke();
    }

    //Setup UI for Icons when they are Usable by the form or not
    void SetupActionUI()
    {
        if (!currentForm.GetCanMove())
        {
            RunCross.SetActive(true);
            ActionsImages[0].fillAmount = 1.0f;
        }
        else
        {
            RunCross.SetActive(false);
            ActionsImages[0].fillAmount = 0.0f;
        }

        if (!currentForm.GetCanJump())
        {
            JumpCross.SetActive(true);
            ActionsImages[1].fillAmount = 1.0f;
        }
        else
        {
            JumpCross.SetActive(false);
            ActionsImages[1].fillAmount = 0.0f;
        }

        if (!currentForm.GetCanAction())
        {
            ActionCross.SetActive(true);
            ActionsImages[2].fillAmount = 1.0f;
        }
        else
        {
            ActionCross.SetActive(false);
            ActionsImages[2].fillAmount = 0.0f;
        }

        if (!currentForm.GetCanUlti())
        {
            UltimateCross.SetActive(true);
            ActionsImages[3].fillAmount = 1.0f;
        }
        else
        {
            UltimateCross.SetActive(false);
            ActionsImages[3].fillAmount = 0.0f;
        }
    }
}
