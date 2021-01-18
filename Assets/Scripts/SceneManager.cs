using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
	public Vector3 StartPosition;

	public GameObject PlayerPrefab;

	void Start()
	{
		Instantiate(PlayerPrefab, StartPosition, Quaternion.identity);
	}
}
