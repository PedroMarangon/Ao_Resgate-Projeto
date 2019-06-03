using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "Falling", menuName="ScriptableObjects/Falling", order=0)]
public class Falling : ScriptableObject {

	#region Variables
	public FallingType type;
	[ShowAssetPreview] public Sprite sprite;
	public Color colorMultiplier = Color.white;
	[BoxGroup("BOX COLLIDER")] public Vector2 offset;
	[BoxGroup("BOX COLLIDER")] public Vector2 size = Vector2.one;
	[MinValue(0)] public int Pontuation;
	public int rotateSpeed;
	#endregion
}

public enum FallingType { Person, Obj }