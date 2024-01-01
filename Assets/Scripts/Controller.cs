using UnityEngine;
using Random = UnityEngine.Random;

public class Controller : MonoBehaviour
{
    public GameObject goHexPrism;
    private GameObject[,] _hexGrid; // 2D array to store hexagonal prisms
    private const int GridWidth = 5;
    private const int GridHeight = 5;
    private Camera _camera;
    public float forceAmount = 3f;
    
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _hexGrid = new GameObject[GridWidth, GridHeight];

        for (int i = 0; i < GridWidth; i++)
        {
            for (int j = 0; j < GridHeight; j++)
            {
                Vector3 position = new Vector3(i + (0.5f * (j % 2)), -5 + Random.Range(-0.0f, 0.0f), j / 1.1547f); 
                GameObject hexObj = Instantiate(goHexPrism, position, Quaternion.Euler(90, 0, 0));
                
                //Set the spring joint anchor
                SpringJoint springJoint = hexObj.GetComponent<SpringJoint>();
                if (springJoint != null) 
                {
                    springJoint.connectedAnchor = position;
                }
                
                _hexGrid[i, j] = hexObj; // Store the hex prism in the array
            }
        }
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                HexPrism hexPrism = hit.collider.GetComponent<HexPrism>();
                if (hexPrism != null)
                {
                    hit.rigidbody.AddForce(Vector3.down * forceAmount, ForceMode.Impulse);
                    hexPrism.TransitionState();
                }
            }
        }
    }

    
}