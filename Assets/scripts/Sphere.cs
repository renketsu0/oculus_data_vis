﻿using UnityEngine;
using System.Collections;

public class Sphere : MonoBehaviour {
	public static Object spherePrefab;
	public Candidate candidate;
	public Score score;

	public void Initialize(Score extScore) {
		score = extScore;
		candidate = score.candidate;
		transform.localScale = new Vector3(1,1,1) * 0.01f * score.percent;
		float offset = 0.01f * score.percent;
		transform.position = new Vector3 (0,  1 - offset, 0);
		// assign or access candidate's color
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
