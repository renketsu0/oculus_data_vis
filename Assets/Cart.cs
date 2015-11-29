﻿using UnityEngine;
using System.Collections;

public class Cart : MonoBehaviour {

		int pMark = 1; // position marker for cart
		public int pause = 0; // turn movement on and off
		public int state = 0; // which graph is being traversed
		GameObject slot;
		GameObject slot2;
		GameObject slot3;
		float speed1;
		float speed2;
		float speed3;
		
		public string currentCandidate;
		public float currentPercent;
		public string currentDate;
		public float nextPercent;
		public string nextDate;
		
		float speed = 2f;
		
		Vector3 waypointCart; //initiate travel-to point
		Vector3[] candPositions; //collection of points for first graph
		
		Vector3 waypointCart1; //initiate travel-to point
		Vector3[] candPositions1; //collection of points for second graph
		
		Vector3 waypointCart2; //initiate travel-to point
		Vector3[] candPositions2; //collection of points for third graph
		CreateLine createLine;
		
		Vector3 boost;

	// Use this for initialization
	void Start () {
		
		GameObject cart = GameObject.Find("Cart"); // cart
		GameObject slot = GameObject.Find("CartSlot"); // slot for cart to fit into on first graph
		GameObject slot2 = GameObject.Find("CartSlot1"); // slot for cart to fit into on second graph
		GameObject slot3 = GameObject.Find("CartSlot2"); // slot for cart to fit into on third graph
		
		
	    createLine = cart.GetComponent<CreateLine>();
		
		createLine.createLine(2, "Palin"); // create 1st graph that cart can ride on
		createLine.createLine(0, "Gingrich"); // create 2nd graph that cart can ride on
		createLine.createLine(1, "Romney"); // create 3rd graph that cart can ride on
		
		if (state == 1)
		{
			transform.position = createLine.Positions1[0];//initiate camera location to first point
			transform.LookAt(createLine.Positions1[1]);//initiate camera aim to second point
		}
		else if (state == 2)
		{
			transform.position = createLine.Positions2[0];//initiate camera location to first point
			transform.LookAt(createLine.Positions2[1]);//initiate camera aim to second point
		}
		else 
		{
			transform.position = createLine.Positions3[0];//initiate camera location to first point
			transform.LookAt(createLine.Positions3[1]);//initiate camera aim to second point
		}


		slot.transform.position = createLine.Positions1[0];//initiate camera location to first point
		slot.transform.LookAt(createLine.Positions1[1]);//initiate camera aim to second point
		currentPercent = createLine.Positions1[0][1];
		nextPercent = createLine.Positions1[1][1];
		Debug.Log(currentPercent);
		
		slot2.transform.position = createLine.Positions2[0];//initiate camera location to first point
		slot2.transform.LookAt(createLine.Positions2[1]);//initiate camera aim to second point
		currentPercent = createLine.Positions2[0][1];
		nextPercent = createLine.Positions2[1][1];
		Debug.Log(currentPercent);
		
		slot3.transform.position = createLine.Positions3[0];//initiate camera location to first point
		slot3.transform.LookAt(createLine.Positions3[1]);//initiate camera aim to second point
		currentPercent = createLine.Positions3[0][1];
		nextPercent = createLine.Positions3[1][1];
		Debug.Log(currentPercent);
	
		waypointCart = createLine.Positions1[1]; //initiate travel-to point to second point
		candPositions = createLine.Positions1;
		
		waypointCart1 = createLine.Positions2[1]; //initiate travel-to point to second point
		candPositions1 = createLine.Positions2;
		
		waypointCart2 = createLine.Positions3[1]; //initiate travel-to point to second point
		candPositions2 = createLine.Positions3;
		
		//boost = createLine.boost;
		
		currentDate = createLine.Position1Dates[0].Date.ToString("d");
		nextDate = createLine.Position1Dates[1].Date.ToString("d");
		currentCandidate = createLine.Position1Candidate;
		pause = 1;
	
	}
	
	void Move ()
	
	{
		
		
		if (pMark == 0)
		{
			Debug.Log("pMark is 0.");
			pMark = pMark + 1;
			waypointCart = candPositions[pMark];
			waypointCart1 = candPositions1[pMark];
			waypointCart2 = candPositions2[pMark];
		}
		
		speed1 = speed + (speed * ((Mathf.Sqrt(100 + Mathf.Pow((candPositions[pMark][1] - candPositions[pMark - 1][1]),2)) - 10)/10));
		speed2 = speed + (speed * ((Mathf.Sqrt(100 + Mathf.Pow((candPositions1[pMark][1] - candPositions1[pMark - 1][1]),2)) - 10)/10));
		speed3 = speed + (speed * ((Mathf.Sqrt(100 + Mathf.Pow((candPositions2[pMark][1] - candPositions2[pMark - 1][1]),2)) - 10)/10));

		GameObject slot = GameObject.Find("CartSlot");
		GameObject slot2 = GameObject.Find("CartSlot1");
		GameObject slot3 = GameObject.Find("CartSlot2");
		
		
		
			if(transform.position.z >= waypointCart[2] - .5)
			{
				pMark = pMark+1;
				if (pMark >= candPositions.Length)
				{
					Application.LoadLevel(1);
				}
				
				waypointCart = candPositions[pMark];
				waypointCart1 = candPositions1[pMark];
				waypointCart2 = candPositions2[pMark];			
			}
		
		

		
					var targetRotation = Quaternion.LookRotation(waypointCart - slot.transform.position);
			        slot.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed1 * Time.deltaTime);
					slot.transform.position = Vector3.MoveTowards(slot.transform.position, waypointCart, speed1 * Time.deltaTime);
					
					var targetRotation2 = Quaternion.LookRotation(waypointCart1 - slot2.transform.position);
					slot2.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation2, speed2 * Time.deltaTime);
					slot2.transform.position = Vector3.MoveTowards(slot2.transform.position, waypointCart1, speed2 * Time.deltaTime);

					var targetRotation3 = Quaternion.LookRotation(waypointCart2 - slot3.transform.position);
					slot3.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation3, speed3 * Time.deltaTime);
					slot3.transform.position = Vector3.MoveTowards(slot3.transform.position, waypointCart2, speed3 * Time.deltaTime);
					
					currentDate = createLine.Position1Dates[pMark - 1].Date.ToString("d");
					nextDate = createLine.Position1Dates[pMark].Date.ToString("d");
				
				if (state == 1)
				{
					//transform.parent = slot.transform;
					transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
					transform.position = Vector3.MoveTowards(slot.transform.position, waypointCart, speed1 * Time.deltaTime);
					currentPercent = candPositions[pMark-1][1];
					nextPercent = candPositions[pMark][1];
					currentCandidate = createLine.Position1Candidate;
					
					Debug.Log(currentDate);
					
				}
				else if (state == 2)
				{
					transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation2, speed * Time.deltaTime);
					transform.position = Vector3.MoveTowards(slot2.transform.position, waypointCart1, speed2 * Time.deltaTime);
					currentPercent = candPositions1[pMark -1][1];
					nextPercent = candPositions1[pMark][1];
					currentCandidate = createLine.Position2Candidate;
					Debug.Log(currentDate);
					
				}
				else
				{
					
					transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation3, speed3 * Time.deltaTime);
					transform.position = Vector3.MoveTowards(slot3.transform.position, waypointCart2, speed3 * Time.deltaTime);
					currentPercent = candPositions2[pMark][1];
					nextPercent = candPositions2[pMark][1];
					currentCandidate = createLine.Position3Candidate;
					
					Debug.Log(currentDate);
				}
		//Debug.Log (pMark);
		//Debug.Log (currentPercent);
				
				//transform.position = Vector3.MoveTowards(transform.position, waypointCart1, speed * Time.deltaTime);
				//GetComponent<Camera>().transform.position += boost;
		
	}
	
	void MoveBackwards ()	//Same as Move, but modified to make the movement go backwards
	{
		speed1 = speed + (speed * ((Mathf.Sqrt(100 + Mathf.Pow((candPositions[pMark][1] - candPositions[pMark + 1][1]),2)) - 10)/10));
		speed2 = speed + (speed * ((Mathf.Sqrt(100 + Mathf.Pow((candPositions1[pMark][1] - candPositions1[pMark + 1][1]),2)) - 10)/10));
		speed3 = speed + (speed * ((Mathf.Sqrt(100 + Mathf.Pow((candPositions2[pMark][1] - candPositions2[pMark + 1][1]),2)) - 10)/10));

		GameObject slot = GameObject.Find("CartSlot");
		GameObject slot2 = GameObject.Find("CartSlot1");
		GameObject slot3 = GameObject.Find("CartSlot2");

		
			if(transform.position.z <= waypointCart[2])
			{
				if (pMark !=0)
				{
					pMark = pMark -1;
				    waypointCart = candPositions[pMark];
				    waypointCart1 = candPositions1[pMark];
				    waypointCart2 = candPositions2[pMark];
					
				}
				
				
				
				
				
	
			}		      
			
					var targetRotation = Quaternion.LookRotation(candPositions[pMark +1] - slot.transform.position);
			        slot.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed1 * Time.deltaTime);
					slot.transform.position = Vector3.MoveTowards(slot.transform.position, candPositions[pMark], speed1 * Time.deltaTime);
					
					var targetRotation2 = Quaternion.LookRotation(candPositions1[pMark +1] - slot2.transform.position);
			        slot2.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation2, speed2 * Time.deltaTime);
					slot2.transform.position = Vector3.MoveTowards(slot2.transform.position, candPositions1[pMark], speed2 * Time.deltaTime);		
					
					var targetRotation3 = Quaternion.LookRotation(candPositions2[pMark +1] - slot3.transform.position);
			        slot3.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation3, speed3 * Time.deltaTime);
					slot3.transform.position = Vector3.MoveTowards(slot3.transform.position, candPositions2[pMark], speed3 * Time.deltaTime);
				
				if (state == 1) //If 
				{
					//transform.parent = slot.transform;
					transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
					transform.position = Vector3.MoveTowards(slot.transform.position, candPositions[pMark], speed1 * Time.deltaTime);
					Debug.Log(pMark + 1);
				}
				else if (state == 2)
				{
					transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation2, speed * Time.deltaTime);
					transform.position = Vector3.MoveTowards(slot2.transform.position, candPositions1[pMark], speed2 * Time.deltaTime);	
				}
				else
				{	
					transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation3, speed * Time.deltaTime);
					transform.position = Vector3.MoveTowards(slot3.transform.position, candPositions2[pMark], speed3 * Time.deltaTime);			
				}
				
				//transform.position = Vector3.MoveTowards(transform.position, waypointCart1, speed * Time.deltaTime);
				//GetComponent<Camera>().transform.position += boost;
		
	}
	
		
	
	// Update is called once per frame
	void Update () {
	
    if (Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            if (state == 1)
                state = 3;
            else
                state--;
            print("button 7 shift right");
        }	
		
    if (Input.GetKeyDown(KeyCode.JoystickButton6))
        {
            if (state == 3)
                state = 1;
            else
                state++;
            print("button 6 shift left");
        }

	if (Input.GetKeyDown("space")) // switches cart location to other graphs
	{
		if (state == 1)
		{
			state = 2;
		}
		else if (state == 2)
		{
			state = 3;
		}
		else
		{
			state = 1;
		}
	}
	
	if (Input.GetKey("right") || Input.GetKey(KeyCode.JoystickButton5)) //fast forward
	{
		speed = 7;
		Move();
            print("button 5 fast forward");
	}

	if (Input.GetKeyDown("p") || Input.GetKeyDown(KeyCode.JoystickButton9)) //pause
	{
		if(pause == 1)
		{
			pause = 0;
		}
		else if(pause == 0)
		{
			pause = 1;
		}
		else
		{
			pause = 0;
		}
            print("button 9 pause");
	}
	
	if (Input.GetKey("left") || Input.GetKey(KeyCode.JoystickButton4)) //rewind
	{
		MoveBackwards();
            print("button 4 rewind");
	}

	
	if (!Input.anyKey) // if no key is pressed and the game is not paused move forward.
	{
		if (pause == 0)
		{
			speed = 2;
			Move();
		}
	}
	
	//;
	
	}
}
