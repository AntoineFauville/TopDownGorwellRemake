using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
	[Inject] private GameSettings _gameSettings;

	public Rigidbody2D body;

	private float horizontal;
	private float vertical;
	private float moveLimiter;
	private float runSpeedHorizontal;
	private float runSpeedVectical;
    
    void Start()
	{
		moveLimiter = _gameSettings.PlayerDiagonalSpeedLimiter;
		runSpeedHorizontal = _gameSettings.PlayerRunSpeedHorizontal;
		runSpeedVectical = _gameSettings.PlayerRunSpeedVertical;
	}

	void Update()
	{
		// Gives a value between -1 and 1
		horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
		vertical = Input.GetAxisRaw("Vertical"); // -1 is down
	}

	void FixedUpdate()
	{
		if (horizontal != 0 && vertical != 0) // Check for diagonal movement
		{
			// limit movement speed diagonally, so you move at 70% speed
			horizontal *= moveLimiter;
			vertical *= moveLimiter;
		}

		body.velocity = new Vector2(horizontal * runSpeedHorizontal, vertical * runSpeedVectical);
	}
}
