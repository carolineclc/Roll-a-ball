using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
// Rigidbody of the player;
 private Rigidbody rb; 

 public CharacterController controller;
 public Transform cam;

 // Variable to keep track of collected "PickUp" objects.
 private int count;

 // Movement along X and Y axes.
 private float movementX;
 private float movementY;

 // Speed at which the player moves.
 public float speed = 6f;
 public float jumpForce = 7f;

 public LayerMask groundLayer;
 public float raycastDistance = 0.6f;
 private bool isGrounded;



 // UI text component to display count of "PickUp" objects collected.
 public TextMeshProUGUI countText;

 // UI object to display winning text.
 public GameObject winTextObject;

 // Start is called before the first frame update.
 void Start()
    {
 // Get and store the Rigidbody component attached to the player.
        rb = GetComponent<Rigidbody>();

 // Initialize count to zero.
        count = 0;

 // Update the count display.
        SetCountText();

 // Initially set the win text to be inactive.
        winTextObject.SetActive(false);
    }
 
 // This function is called when a move input is detected.
 //void OnMove(InputValue movementValue)
//    {
 // Convert the input value into a Vector2 for movement.
//        Vector2 movementVector = movementValue.Get<Vector2>();

 // Store the X and Y components of the movement.
//        movementX = movementVector.x; 
//        movementY = movementVector.y; 
//    }

 // FixedUpdate is called once per fixed frame-rate frame.
 private void Update() 
    {
 // Create a 3D movement vector using the X and Y inputs.
        float movementX = Input.GetAxisRaw("Horizontal");
        float movementY = Input.GetAxisRaw("Vertical");
        
        Vector3 movement = new Vector3 (movementX, 0f, movementY).normalized;



        if (movement.magnitude >= 0.1f){
              float targetAngle =  Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
              rb.rotation =  Quaternion.Euler(0f, targetAngle, 0f);
              //controller.Move(movement * speed * Time.deltaTime);

              Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
              rb.AddForce(moveDirection.normalized * speed); 
        }

        //Ground check
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance, groundLayer))
            isGrounded = true;
        else
            isGrounded = false;
 

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded){
              rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

       

 // Apply force to the Rigidbody to move the player.
        
    }

 
 void OnTriggerEnter(Collider other) 
    {
 // Check if the object the player collided with has the "PickUp" tag.
 if (other.gameObject.CompareTag("PickUp")) 
        {
 // Deactivate the collided object (making it disappear).
            other.gameObject.SetActive(false);

 // Increment the count of "PickUp" objects collected.
            count = count + 1;

 // Update the count display.
            SetCountText();
        }


        
    }

 // Function to update the displayed count of "PickUp" objects collected.
 void SetCountText() 
    {
 // Update the count text with the current count.
        countText.text = "Count: " + count.ToString();

 // Check if the count has reached or exceeded the win condition.
 if (count >= 10)
        {
 // Display the win text.
            winTextObject.SetActive(true);
        }
    }
}
