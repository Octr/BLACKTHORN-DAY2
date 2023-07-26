using UnityEngine;


/// <summary>
/// Used for reading and processing inputs. Try to keep all calls to UnityEngine.Input such as Input.GetAxis and Input.GetKeyDown here.
/// Have as a component on the player object and makes references to this class if you need to get inputs
/// </summary>
public class InputReader : MonoBehaviour
{
    [Tooltip("The layer mask used in 'MouseScreenPositionToWorldPosition' method to signify what is the ground layer. MAKE SURE GROUND SURFACE IS SET TO THE SAME LAYER OR CHANGE THIS VALUE")]
    [SerializeField] private LayerMask _groundMask;
    [Tooltip("Do you want to move the player with the keyboard or the mouse? Switch the boolean to change controls")]
    [SerializeField] private bool _hasMouseControl;
    [Tooltip("Button to press for the player to fire. Can be remapped in Inpsector")]
    [SerializeField] private KeyCode _fireKeyCode;
    [SerializeField] private KeyCode _panLeftKeyCode;
    [SerializeField] private KeyCode _panRightKeyCode;

    public bool HasMouseControl { get { return _hasMouseControl; } }
    public Vector3 MoveInput { get { return _moveInput; } }
    public Vector3 MouseWorldPosition { get { return _mouseWorldPosition; } }
    public bool FireInput { get { return _fireInput; } }
    public bool PanLeftInput { get { return _panLeftInput; } }
    public bool PanRightInput { get { return _panRightInput; } }

    private Camera _mainCamera;
    private Vector3 _moveInput;
    private Vector3 _mouseWorldPosition;
    private bool _fireInput;
    private bool _panLeftInput;
    private bool _panRightInput;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        ProcessMovementInput();
        ProcessFireInput();
        ProcessMouseScreenPositionToWorldPosition();
        ProcessPanningInput();
    }

    private void ProcessMouseScreenPositionToWorldPosition()
    {
        Vector2 mouseScreenPosition = Input.mousePosition;
        
        //Turn mouse screen position into a ray that fires out from the camera
        Ray screenCastPoint = _mainCamera.ScreenPointToRay(mouseScreenPosition);

        //Any hits to objects with a layer set to _groundMask will set the mouse world position to the hit point
        if (Physics.Raycast(screenCastPoint, out RaycastHit hit, Mathf.Infinity, _groundMask))
        {
            _mouseWorldPosition = hit.point;
        }
    }

    private void ProcessFireInput()
    {
        _fireInput = Input.GetKeyDown(_fireKeyCode);
    }

    private void ProcessMovementInput()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        _moveInput = new Vector3(xInput, 0, yInput);
    }

    private void ProcessPanningInput()
    {
        //Watch out for if both mouse inputs are pressed down at the same time.
        //Shouldn't cause issues since one will call then the opposite will counter-act it as though nothing happened. Hopefully.
        _panLeftInput = Input.GetKeyDown(_panLeftKeyCode);

        _panRightInput = Input.GetKeyDown(_panRightKeyCode);
    }
}