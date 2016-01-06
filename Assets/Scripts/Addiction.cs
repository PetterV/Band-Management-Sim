using UnityEngine;
using System.Collections;

public class Addiction : MonoBehaviour {

	public int addictionLevelBamse = 0;
	public int addictionLevelMedisin = 0;
	public int addictionLevelMat = 0;
	public int addictionLevelDrekke = 0;

	public float addictionCounterBamse = 0f;
	public float addictionCounterMedisin = 0f;
	public float addictionCounterMat = 0f;
	public float addictionCounterDrekke = 0f;

	public float addictionIncreaseBamse = 0f;
	public float addictionIncreaseMedisin = 0f;
	public float addictionIncreaseMat = 0f;
	public float addictionIncreaseDrekke = 0f;

	public float addictionReduction = 0f;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void BamseIncrease (){
		addictionCounterBamse = addictionCounterBamse + addictionIncreaseBamse;
	}

	public void MedisinIncrease (){
		addictionCounterMedisin = addictionCounterMedisin + addictionIncreaseMedisin;
	}

	public void MatIncrease (){
		addictionCounterMat = addictionCounterMat + addictionIncreaseMat;
	}

	public void DrekkeIncrease (){
		addictionCounterDrekke = addictionCounterDrekke + addictionIncreaseDrekke;
	}
}
