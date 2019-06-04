using System;
using UnityEngine;

[Serializable]
public class GameSettings
{
	public PlayerController PlayerController;
	public float PlayerDiagonalSpeedLimiter = 0.7f;
	public float PlayerRunSpeedHorizontal = 8f;
	public float PlayerRunSpeedVertical = 8f;
}
