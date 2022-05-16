using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshVisibilityStatus : MonoBehaviour
{
	public event Action becameInvisible;
	
	private void OnBecameInvisible()
	{
		becameInvisible?.Invoke();
	}
}
