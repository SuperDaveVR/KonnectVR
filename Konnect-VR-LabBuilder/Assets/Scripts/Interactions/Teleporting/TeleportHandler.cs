using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportHandler : MonoBehaviour
{
    [SerializeField] private InputActionAsset actionAsset;
    [SerializeField] private XRRayInteractor _leftRay;
    [SerializeField] private XRRayInteractor _leftGrab;
    [SerializeField] private MonoBehaviour _leftMovementOrTurn;
    [SerializeField] private XRRayInteractor _rightRay;
    [SerializeField] private XRRayInteractor _rightGrab;
    [SerializeField] private MonoBehaviour _rightMovementOrTurn;
    [SerializeField] private TeleportationProvider _teleportationProvider;
    private bool _leftActive = false;
    private bool _rightActive = false;
    private bool _isActive = false;

    private InputAction _leftTeleport;
    private InputAction _rightTeleport;

    // Start is called before the first frame update
    void Start()
    {
        _teleportationProvider.enabled = false;

        _leftRay.enabled = false;
        _rightRay.enabled = false;
        _rightGrab.enabled = true;
        _leftGrab.enabled = true;

        var leftActivate = actionAsset.FindActionMap("XRI LeftHand").FindAction("Teleport Mode Activate");
        leftActivate.Enable();
        leftActivate.performed += OnTeleportActivateLeft;

        var rightActivate = actionAsset.FindActionMap("XRI RightHand").FindAction("Teleport Mode Activate");
        rightActivate.Enable();
        rightActivate.performed += OnTeleportActivateRight;

        _leftTeleport = actionAsset.FindActionMap("XRI LeftHand").FindAction("Teleport Select");
        _rightTeleport = actionAsset.FindActionMap("XRI RightHand").FindAction("Teleport Select");
    }

    private void OnTeleportActivateLeft(InputAction.CallbackContext context)
    {
        if (!_leftActive)
        {
            _leftActive = true;
            _leftRay.enabled = true;
            _leftGrab.enabled = false;
            _leftMovementOrTurn.enabled = false;
            _leftTeleport.Enable();
            _leftTeleport.performed += OnTeleportLeft;
        }
        else
        {
            OnTeleportCancelLeft();
        }
    }

    private void OnTeleportActivateRight(InputAction.CallbackContext context)
    {
        if (!_rightActive)
        {
            _rightActive = true;
            _rightRay.enabled = true;
            _rightGrab.enabled = false;
            _rightMovementOrTurn.enabled = false;
            _rightTeleport.Enable();
            _rightTeleport.performed += OnTeleportRight;
        }
        else
        {
            OnTeleportCancelRight();
        }
    }

    private void OnTeleportCancelLeft()
    {
        _leftActive = false;
        _leftRay.enabled = false;
        _leftGrab.enabled = true;
        _leftMovementOrTurn.enabled = true;
        _leftTeleport.Disable();

        //Disable the target reticule on ray disable.
        var targeting = _leftRay.gameObject.transform.Find("TargetReticule").gameObject;
        targeting.SetActive(false);
    }

    private void OnTeleportCancelRight()
    {
        _rightActive = false;
        _rightRay.enabled = false;
        _rightGrab.enabled = true;
        _rightMovementOrTurn.enabled = true;
        _rightTeleport.Disable();

        //Disable the target reticule on ray disable.
        var targeting = _rightRay.gameObject.transform.Find("TargetReticule").gameObject;
        targeting.SetActive(false);
    }

    private void OnTeleportLeft(InputAction.CallbackContext context)
    {
        if (!_leftActive)
            return;
        if (!_leftRay.TryGetCurrent3DRaycastHit(out RaycastHit hit) || !IsValidRay(_leftRay))
        {
            OnTeleportCancelLeft();
            return;
        }

        OnTeleport(hit);
    }

    private void OnTeleportRight(InputAction.CallbackContext context)
    {
        if (!_rightActive)
            return;
        if (!_rightRay.TryGetCurrent3DRaycastHit(out RaycastHit hit) || !IsValidRay(_rightRay))
        {
            OnTeleportCancelRight();
            return;
        }

        OnTeleport(hit);
    }

    private void OnTeleport(RaycastHit hit)
    {
        TeleportRequest teleportRequest = new TeleportRequest()
        {
            destinationPosition = hit.point
        };

        _teleportationProvider.enabled = true;
        _teleportationProvider.QueueTeleportRequest(teleportRequest);

        StartCoroutine(WaitForTeleport());
    }

    private bool IsValidRay(XRRayInteractor ray)
    {
        var rayInfo = ray.TryGetHitInfo(out Vector3 position, out Vector3 normal, out int positionInLine, out bool isValidTarget);
        return isValidTarget;
    }

    private IEnumerator WaitForTeleport()
    {
        yield return new WaitForEndOfFrame();
        _teleportationProvider.enabled = false;
    }
}
