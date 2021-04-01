using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, PlayerInputActions.IPlayerActions
{
    [SerializeField]
    private PlayerInputActions mPIA;

    private PlayerMovement pmScript;
    private TricksController tcScript;

    #region Input Redirection Functions
    public void OnBrake(InputAction.CallbackContext context)
    {
        pmScript.OnBrake();
        //throw new System.NotImplementedException();
    }

    public void OnHospitalFlip(InputAction.CallbackContext context)
    {
        tcScript.OnHospitalFlip();
        throw new System.NotImplementedException();
    }

    public void OnKickflip(InputAction.CallbackContext context)
    {
        tcScript.OnKickflip();
        throw new System.NotImplementedException();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        // Not needed
        throw new System.NotImplementedException();
    }

    public void OnMoveForward(InputAction.CallbackContext context)
    {
        pmScript.OnMoveForward();
        Debug.Log("working?");
        //throw new System.NotImplementedException();
    }

    public void OnOllie(InputAction.CallbackContext context)
    {
        tcScript.OnOllie();
        throw new System.NotImplementedException();
    }

    public void OnPopShuvit(InputAction.CallbackContext context)
    {
        tcScript.OnPopShuvit();
        throw new System.NotImplementedException();
    }

    public void OnReset(InputAction.CallbackContext context)
    {
        // THIS SHOULD BE REMOVED FOR BUILD
        throw new System.NotImplementedException();
    }

    public void OnTurn(InputAction.CallbackContext context)
    {
        pmScript.OnTurn(context.ReadValue<Vector2>());
        throw new System.NotImplementedException();
    }

    public void OnWheelie(InputAction.CallbackContext context)
    {
        tcScript.OnWheelie();
        throw new System.NotImplementedException();
    }

    #endregion

    #region Enable / Disable

    private void OnEnable()
    {
        mPIA.Player.Enable();
    }

    private void OnDisable()
    {
        mPIA.Player.Disable();
    }

    #endregion

    private void Awake()
    {
        mPIA = new PlayerInputActions();
        mPIA.Player.SetCallbacks(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        pmScript = GetComponent<PlayerMovement>();
        tcScript = GetComponent<TricksController>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the game is NOT paused...
        if (!GameManager.instance.gameIsPaused)
        {
            // Put everything in here!!!

        }
    }
}
