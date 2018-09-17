using UnityEngine;

/// <summary>
/// Player class
/// </summary>
public class Player : MonoBehaviour
{
    /// <summary>
    /// Rotation smooth time
    /// </summary>
    [SerializeField]
    private float rotationSmoothTime = 1.0f;

    /// <summary>
    /// Move smooth time
    /// </summary>
    [SerializeField]
    private float moveSmoothTime = 1.0f;

    /// <summary>
    /// Ship
    /// </summary>
    [SerializeField]
    private Ship ship;

    /// <summary>
    /// Move point middle
    /// </summary>
    [SerializeField]
    private Transform movePointMiddle;

    /// <summary>
    /// Move point left
    /// </summary>
    [SerializeField]
    private Transform movePointLeft;

    /// <summary>
    /// Move point right
    /// </summary>
    [SerializeField]
    private Transform movePointRight;

    /// <summary>
    /// Touch camera
    /// </summary>
    [SerializeField]
    private Camera touchCamera;

    /// <summary>
    /// Utility
    /// </summary>
    private Utility utility;

    /// <summary>
    /// Last aim position
    /// </summary>
    private Vector3 lastAimPosition = Vector3.zero;

    /// <summary>
    /// Ship
    /// </summary>
    public Ship Ship
    {
        get
        {
            return ship;
        }
    }

    /// <summary>
    /// Awake
    /// </summary>
    private void Awake()
    {
        // Init vars
        utility = Utility.GetInstance();

        // Load current ship
        ship = Instantiate(utility.getSelectedShip().gameObject, transform).GetComponent<Ship>();
    }

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        if (ship != null)
        {
            lastAimPosition = ship.transform.position + Vector3.up;
        }
    }

    /// <summary>
    /// Late update
    /// </summary>
    private void LateUpdate()
    {
        // Game is running
        if (utility.mode.state == ModeStates.Run)
        {

            // On mouse click and not on ship
            if (Input.GetMouseButton(0) && !IsMouseOver())
            {

                // Ship faces mouse and shoots
                FaceMouse();
                ship.fire();
            }

            // Activate shield if has at least 1 second duration and mouse is over it
            ship.shield.active = IsMouseOver() && ship.shield.duration > 0;

            // Update bonus slider (minus 1 to improve performance)
            if (ship.bonusDurationLeft > 0)
            {
                utility.mode.sliderBonus.value = (int)(ship.bonusDurationLeft / ship.currentBonus.duration * 100) - 1;
            }

            // Update shield slider (add 1 to improve performance)
            if (ship.shield.getDurationPercentage() != 100)
            {
                utility.mode.sliderShield.value = ship.shield.getDurationPercentage() + 1;
            }

            /**
             * This features is disabled for now till next update.

            // Move point based on device rotation (no rotation, stay middle)
            Transform movePoint = movePointMiddle;

            // Rotated phone enough?
            if (Mathf.Abs (Input.acceleration.x) > 0.3f) {

                // Set move point to righ/left
                movePoint = Input.acceleration.x > 0 ? movePointRight : movePointLeft;
            }

            // Set ship to move to point
            GetComponent<MoveTo> ().target = movePoint;
            */
        }
    }

    /// <summary>
    /// Fixed update
    /// </summary>
    private void FixedUpdate()
    {
        // Face up and move
        if (ship != null)
        {
            ship.transform.rotation = Quaternion.Lerp(ship.transform.rotation, Quaternion.identity, rotationSmoothTime * Time.fixedDeltaTime);
            ship.transform.position = new Vector3(Mathf.Lerp(ship.transform.position.x, lastAimPosition.x, moveSmoothTime * Time.fixedDeltaTime), ship.transform.position.y, ship.transform.position.z);
        }
    }

    /// <summary>
    /// Check if player mouse or finger is on the ship
    /// </summary>
    /// <returns>"true" if player mouse or finger is on the ship, otherwise "false"</returns>
    public bool IsMouseOver()
    {
        Vector3 mouse = touchCamera.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = transform.position.z;
        return GetComponent<Collider2D>().bounds.Contains(mouse) && Input.GetMouseButton(0);
    }

    /// <summary>
    /// Instantly turn and face where mouse and finger is
    /// </summary>
    public void FaceMouse()
    {
        Vector3 pos = ship.transform.position;
        Vector3 input = Input.mousePosition;
        input.z = pos.z;
        lastAimPosition = touchCamera.ScreenToWorldPoint(input);
        ship.transform.rotation = Quaternion.AngleAxis((Mathf.Atan2(lastAimPosition.y - pos.y, lastAimPosition.x - pos.x) - (Mathf.PI * 0.5f)) * 180.0f / Mathf.PI, Vector3.forward);
    }
}
