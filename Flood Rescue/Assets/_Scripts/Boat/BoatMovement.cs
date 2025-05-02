using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoatMovement : MonoBehaviour, IObserver
{
    private bool isMovingLeft;
    private bool isMovingRight;
    private bool isBoatColliding;
    [SerializeField] private Rigidbody boatRigidbody;
    [SerializeField] private Slider speedSlider;
    private void FixedUpdate() => HandleBoatMovement();
    private void HandleBoatMovement()
    {
        if (isMovingLeft || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (BoatCollision.Instance.IsBoatColliding)
            {
                ApplyTorque(-GameManager.Instance.gameData.TurnSpeed * 3);
            }
            else ApplyTorque(-GameManager.Instance.gameData.TurnSpeed);

            boatRigidbody.linearVelocity = Vector3.Lerp(boatRigidbody.linearVelocity, Vector3.zero, GameManager.Instance.gameData.TurnBreakPower);
            ApplyRoll(GameManager.Instance.gameData.TurnSpeed);
        }
        if (isMovingRight || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (BoatCollision.Instance.IsBoatColliding)
            {
                ApplyTorque(GameManager.Instance.gameData.TurnSpeed * 3);
            }
            else ApplyTorque(GameManager.Instance.gameData.TurnSpeed);
            boatRigidbody.linearVelocity = Vector3.Lerp(boatRigidbody.linearVelocity, Vector3.zero, GameManager.Instance.gameData.TurnBreakPower);
            ApplyRoll(-GameManager.Instance.gameData.TurnSpeed);
        }
        if (speedSlider != null && boatRigidbody.linearVelocity.magnitude < GameManager.Instance.gameData.MaxBoatSpeed)
        {
            ApplyForwardForce();
        }
        if (!isMovingLeft && !isMovingRight && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            NeutralizeRoll(2f);
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            speedSlider.value += Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            speedSlider.value -= Time.deltaTime;
        }
    }

    private void HandleBoatDestruction()
    {
        boatRigidbody.isKinematic = true;
        boatRigidbody.linearVelocity = Vector3.zero;
    }

    private void ApplyTorque(float torqueAmount)
    {
        Vector3 torque = new Vector3(0f, torqueAmount, 0f);
        if (boatRigidbody.linearVelocity.magnitude >= 1f || isBoatColliding)
        {
            boatRigidbody.AddRelativeTorque(torque, ForceMode.Force);
        }
        boatRigidbody.angularDamping = Mathf.Lerp(boatRigidbody.angularDamping, 4f, Time.deltaTime * 0.5f);
    }
    private void ApplyRoll(float rollAmount)
    {
        if (boatRigidbody.linearVelocity.magnitude > 2f)
        {
            Quaternion targetRotation = Quaternion.Euler(boatRigidbody.rotation.eulerAngles.x, boatRigidbody.rotation.eulerAngles.y, rollAmount * 3);
            boatRigidbody.MoveRotation(Quaternion.Lerp(boatRigidbody.rotation, targetRotation, Time.deltaTime));
        }
    }
    private void NeutralizeRoll(float damping)
    {
        Quaternion targetRotation = Quaternion.Euler(boatRigidbody.rotation.eulerAngles.x, boatRigidbody.rotation.eulerAngles.y, boatRigidbody.rotation.eulerAngles.z);
        boatRigidbody.MoveRotation(Quaternion.Lerp(boatRigidbody.rotation, targetRotation, Time.deltaTime * damping));
    }
    private void ApplyForwardForce()
    {
        float forwardForce = GameManager.Instance.gameData.MoveSpeed * speedSlider.value;
        boatRigidbody.AddForce(-transform.forward * forwardForce, ForceMode.Force);
    }


    public void OnLeftButtonPressed(BaseEventData data)
    {
        isMovingLeft = true;
    }

    public void OnLeftButtonReleased(BaseEventData data)
    {
        isMovingLeft = false;
    }

    public void OnRightButtonPressed(BaseEventData data)
    {
        isMovingRight = true;
    }

    public void OnRightButtonReleased(BaseEventData data)
    {
        isMovingRight = false;
    }



    #region Design Pattern
    private void OnEnable()
    {
        Invoke("AddObserver", 1f);
    }
    private void AddObserver() => Subject.Instance.AddObserver(this);
    private void OnDisable()
    {
        Subject.Instance.RemoveObserver(this);
    }
    public void OnNotify(ObserverEnum observerEnum)
    {
        if (observerEnum.Equals(ObserverEnum.BOAT_EXPLODE))
        {
            boatRigidbody.isKinematic = true;
        }
    }
    #endregion
}