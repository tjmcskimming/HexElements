using System;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject goHexPrism;
    
    private Dictionary<Vector2Int, GameObject> _hexGrid; // Dictionary to store hexagonal prisms
    private const int GridWidth = 5;
    private const int GridHeight = 5;
    private const float HexSize = 1;
    private const float SqrtOf3 = 1.7320508075688772f; // Square root of 3
    private const float HorzDist = SqrtOf3 * HexSize;
    private const float VertDist = 1.5f * HexSize;
    private Vector3 Pdir = new Vector3(-HorzDist/2, 0.0f, VertDist);
    private Vector3 Qdir = new Vector3(HorzDist, 0.0f, 0.0f);
    
    private Camera _camera;
    public float forceAmount = 3f;

    void Start()
    {
        _camera = Camera.main;
        _hexGrid = new Dictionary<Vector2Int, GameObject>();

        for (int i = 0; i < GridWidth; i++)
        {
            for (int j = 0; j < GridHeight; j++)
            {
                Vector3 position = i * Pdir + j * Qdir; 
                GameObject hexObj = Instantiate(goHexPrism, position, Quaternion.Euler(90, 0, 0));

                // Set the spring joint anchor
                SpringJoint springJoint = hexObj.GetComponent<SpringJoint>();
                if (springJoint != null) 
                {
                    springJoint.connectedAnchor = position;
                }
                
                Vector2Int key = new Vector2Int(i, j);
                _hexGrid[key] = hexObj; // Store the hex prism in the dictionary
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