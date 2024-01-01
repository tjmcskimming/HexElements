using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    // Start is called before the first frame update
    
    public float moveSpeed = 5.0f; // Speed of camera movement
    public float zoomSpeed = 10.0f; // Adjust this value to change zoom speed
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int right = 0;
        int forward = 0;

        // check movement keys
        if (Input.GetKey(KeyCode.UpArrow))
        {
            forward += 1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            forward -= 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            right -= 1;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            right += 1;
        }

        // Calculate the movement vector in world coordinates
        Vector3 movement = new(right, 0, forward);

        // Transform the movement to be relative to the world coordinate system
        Vector3 worldMovement = transform.TransformDirection(movement);
        worldMovement.y = 0; // Ensure movement is only on the horizontal plane

        // Apply the movement
        transform.Translate(worldMovement * (moveSpeed * Time.deltaTime), Space.World);
        
        // Zoom with scroll wheel
        float zoomAmount = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(0, 0, zoomAmount * zoomSpeed, Space.Self);
    }

}
