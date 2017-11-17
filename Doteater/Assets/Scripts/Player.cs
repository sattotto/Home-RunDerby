using UnityEngine;
using System.Collections;
using Leap;
using System.Diagnostics;

public class Player : MonoBehaviour {

	public float moveSpead = 5f;
	public float rotationSpeed = 360f;

	public float RightMove;
	public float LeftMove;
	public float UpMove;
	public float DownMove;
	
	CharacterController characterController;
	Animator animator;
	Controller controller = new Controller();

	// Use this for initialization
	void Start () {
		characterController = GetComponent<CharacterController>();
		animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

	///	if(RightMove > 0.5)


	Frame frame = controller.Frame();
		RightMove = frame.Hands.Rightmost.GrabStrength;
		LeftMove  = frame.Hands.Leftmost.GrabStrength;
		UpMove    = frame.Hands.Rightmost.PinchStrength;
		DownMove  = frame.Hands.Leftmost.PinchStrength;

		if (RightMove > 0.5) {
			Process.Start (
				new ProcessStartInfo {
					FileName = "osascript",
					Arguments = "-e 'tell application \"System Events\" to key code 124'"
				}
			);
		}else if(LeftMove > 0.5){
			Process.Start (
				new ProcessStartInfo {
					FileName = "osascript",
					Arguments = "-e 'tell application \"System Events\" to key code 123'"
				}
			);
		}

		if (UpMove > 0.5) {
			Process.Start (
				new ProcessStartInfo {
					FileName = "osascript",
					Arguments = "-e 'tell application \"System Events\" to key code 126'"
				}
			);
		}else if(DownMove > 0.5){
			Process.Start (
				new ProcessStartInfo {
					FileName = "osascript",
					Arguments = "-e 'tell application \"System Events\" to key code 125'"
				}
			);
		}
		
		Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		if (direction.sqrMagnitude > 0.01f) {
			Vector3 forward = Vector3.Slerp(
				transform.forward,
				direction,
				rotationSpeed * Time.deltaTime / Vector3.Angle(transform.forward, direction)
			);
			transform.LookAt(transform.position + forward);
		}
		characterController.Move(direction * moveSpead * Time.deltaTime);
		
		animator.SetFloat("Speed", characterController.velocity.magnitude);
		
		if (GameObject.FindGameObjectsWithTag("Dot").Length == 0) {
			Application.LoadLevel("Win");
		}
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.tag == "Dot") {
			Destroy(other.gameObject);
		}
		if (other.tag == "Enemy") {
			Application.LoadLevel("Lose");
		}
	}
}
