using UnityEngine;
using System.Collections;

public class Addiction : MonoBehaviour {

	public float level1Threshold = 50f;
	public float level2Threshold = 70f;
	public float level3Threshold = 100f;

	public float level1HappinessReduction = 0f;
	public float level2HappinessReduction = 0f;
	public float level3HappinessReduction = 0f;

	public float level1SkillReduction = 0f;
	public float level2SkillReduction = 0f;
	public float level3SkillReduction = 0f;

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

	public bool bamseCraving = false;
	public bool medisinCraving = false;
	public bool matCraving = false;
	public bool drekkeCraving = false;


	//Variabler til timer for craving
	private float bamseTimer = 0f;
	private float bamseTimerReduction;
	public float bamseTimer1 = 0f;
	public float bamseTimer2 = 0f;
	public float bamseTimer3 = 0f;

	private float medisinTimer = 0f;
	private float medisinTimerReduction;
	public float medisinTimer1 = 0f;
	public float medisinTimer2 = 0f;
	public float medisinTimer3 = 0f;

	private float matTimer = 0f;
	private float matTimerReduction;
	public float matTimer1 = 0f;
	public float matTimer2 = 0f;
	public float matTimer3 = 0f;

	private float drekkeTimer = 0f;
	private float drekkeTimerReduction;
	public float drekkeTimer1 = 0f;
	public float drekkeTimer2 = 0f;
	public float drekkeTimer3 = 0f;


	public float addictionReduction = 0f;
	private float trueAddictionReduction;



	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
		trueAddictionReduction = addictionReduction * Time.deltaTime;


	//Unngå at addictionCounter faller langt under 0.
		if (addictionCounterBamse < 0f){
			addictionCounterBamse = 0f;
		}
		if (addictionCounterMedisin < 0f){
			addictionCounterMedisin = 0f;
		}
		if (addictionCounterMat < 0f){
			addictionCounterMat = 0f;
		}
		if (addictionCounterDrekke < 0f){
			addictionCounterDrekke = 0f;
		}

	//Bamse
		addictionCounterBamse = addictionCounterBamse - trueAddictionReduction;
		if (addictionCounterBamse < level1Threshold){
			addictionLevelBamse = 0;
		}
		if (addictionCounterBamse > level1Threshold && addictionCounterBamse < level2Threshold){
			addictionLevelBamse = 1;
		}
		if (addictionCounterBamse > level2Threshold && addictionCounterBamse < level3Threshold){
			addictionLevelBamse = 2;
		}
		if (addictionCounterBamse > level3Threshold){
			addictionLevelBamse = 3;
		}

		if (addictionLevelBamse > 0){
			bamseTimerReduction = 1f * Time.deltaTime;
			bamseTimer = bamseTimer - bamseTimerReduction;
		}

		if (bamseTimer < 0){
			bamseCraving = true;
		}

		if (bamseCraving == true){
			if (addictionLevelBamse == 1){
				GetComponentInParent<BandMember>().myHappiness = GetComponentInParent<BandMember>().myHappiness - level1HappinessReduction;
				GetComponentInParent<BandMember>().skill = GetComponentInParent<BandMember>().skill - level1SkillReduction;
			}
			if (addictionLevelBamse == 2){
				GetComponentInParent<BandMember>().myHappiness = GetComponentInParent<BandMember>().myHappiness - level2HappinessReduction;
				GetComponentInParent<BandMember>().skill = GetComponentInParent<BandMember>().skill - level2SkillReduction;
			}
			if (addictionLevelBamse == 3){
				GetComponentInParent<BandMember>().myHappiness = GetComponentInParent<BandMember>().myHappiness - level3HappinessReduction;
				GetComponentInParent<BandMember>().skill = GetComponentInParent<BandMember>().skill - level3SkillReduction;
			}
		}

	//Medisin
		addictionCounterMedisin = addictionCounterMedisin - trueAddictionReduction;
		if (addictionCounterMedisin < level1Threshold){
			addictionLevelMedisin = 0;
		}
		if (addictionCounterMedisin > level1Threshold && addictionCounterMedisin < level2Threshold){
			addictionLevelMedisin = 1;
		}
		if (addictionCounterMedisin > level2Threshold && addictionCounterMedisin < level3Threshold){
			addictionLevelMedisin = 2;
		}
		if (addictionCounterMedisin > level3Threshold){
			addictionLevelMedisin = 3;
		}

		if (addictionLevelMedisin > 0){
			medisinTimerReduction = 1f * Time.deltaTime;
			medisinTimer = medisinTimer - medisinTimerReduction;
		}

		if (medisinTimer < 0){
			medisinCraving = true;
		}

		if (medisinCraving == true){
			if (addictionLevelMedisin == 1){
				GetComponentInParent<BandMember>().myHappiness = GetComponentInParent<BandMember>().myHappiness - level1HappinessReduction;
				GetComponentInParent<BandMember>().skill = GetComponentInParent<BandMember>().skill - level1SkillReduction;
			}
			if (addictionLevelMedisin == 2){
				GetComponentInParent<BandMember>().myHappiness = GetComponentInParent<BandMember>().myHappiness - level2HappinessReduction;
				GetComponentInParent<BandMember>().skill = GetComponentInParent<BandMember>().skill - level2SkillReduction;
			}
			if (addictionLevelMedisin == 3){
				GetComponentInParent<BandMember>().myHappiness = GetComponentInParent<BandMember>().myHappiness - level3HappinessReduction;
				GetComponentInParent<BandMember>().skill = GetComponentInParent<BandMember>().skill - level3SkillReduction;
			}
		}

	//Mat
		addictionCounterMat = addictionCounterMat - trueAddictionReduction;
		if (addictionCounterMat < level1Threshold){
			addictionLevelMat = 0;
		}
		if (addictionCounterMat > level1Threshold && addictionCounterMat < level2Threshold){
			addictionLevelMat = 1;
		}
		if (addictionCounterMat > level2Threshold && addictionCounterMat < level3Threshold){
			addictionLevelMat = 2;
		}
		if (addictionCounterMat > level3Threshold){
			addictionLevelMat = 3;
		}

		if (addictionLevelMat > 0){
			matTimerReduction = 1f * Time.deltaTime;
			matTimer = matTimer - matTimerReduction;
		}

		if (matTimer < 0){
			matCraving = false;
		}

		if (matCraving == true){
			if (addictionLevelMat == 1){
				GetComponentInParent<BandMember>().myHappiness = GetComponentInParent<BandMember>().myHappiness - level1HappinessReduction;
				GetComponentInParent<BandMember>().skill = GetComponentInParent<BandMember>().skill - level1SkillReduction;
			}
			if (addictionLevelMat == 2){
				GetComponentInParent<BandMember>().myHappiness = GetComponentInParent<BandMember>().myHappiness - level2HappinessReduction;
				GetComponentInParent<BandMember>().skill = GetComponentInParent<BandMember>().skill - level2SkillReduction;
			}
			if (addictionLevelMat == 3){
				GetComponentInParent<BandMember>().myHappiness = GetComponentInParent<BandMember>().myHappiness - level3HappinessReduction;
				GetComponentInParent<BandMember>().skill = GetComponentInParent<BandMember>().skill - level3SkillReduction;
			}
		}

	//Drekke
		addictionCounterDrekke = addictionCounterDrekke - trueAddictionReduction;
		if (addictionCounterDrekke < level1Threshold){
			addictionLevelDrekke = 0;
		}
		if (addictionCounterDrekke > level1Threshold && addictionCounterDrekke < level2Threshold){
			addictionLevelDrekke = 1;
		}
		if (addictionCounterDrekke > level2Threshold && addictionCounterDrekke < level3Threshold){
			addictionLevelDrekke = 2;
		}
		if (addictionCounterDrekke > level3Threshold){
			addictionLevelDrekke = 3;
		}

		if (addictionLevelDrekke > 0){
			drekkeTimerReduction = 1f * Time.deltaTime;
			drekkeTimer = drekkeTimer - drekkeTimerReduction;
		}

		if (drekkeTimer < 0){
			drekkeCraving = true;
		}

		if (drekkeCraving == true){
			if (addictionLevelDrekke == 1){
				GetComponentInParent<BandMember>().myHappiness = GetComponentInParent<BandMember>().myHappiness - level1HappinessReduction;
				GetComponentInParent<BandMember>().skill = GetComponentInParent<BandMember>().skill - level1SkillReduction;
			}
			if (addictionLevelDrekke == 2){
				GetComponentInParent<BandMember>().myHappiness = GetComponentInParent<BandMember>().myHappiness - level2HappinessReduction;
				GetComponentInParent<BandMember>().skill = GetComponentInParent<BandMember>().skill - level2SkillReduction;
			}
			if (addictionLevelDrekke == 3){
				GetComponentInParent<BandMember>().myHappiness = GetComponentInParent<BandMember>().myHappiness - level3HappinessReduction;
				GetComponentInParent<BandMember>().skill = GetComponentInParent<BandMember>().skill - level3SkillReduction;
			}
		}
	}

	public void BamseIncrease (){
		addictionCounterBamse = addictionCounterBamse + addictionIncreaseBamse;
		bamseCraving = false;
		if (addictionLevelBamse == 1){
			bamseTimer = bamseTimer1;
		}
		if (addictionLevelBamse == 2){
			bamseTimer = bamseTimer2;
		}
		if (addictionLevelBamse == 3){
			bamseTimer = bamseTimer3;
		}
	}

	public void MedisinIncrease (){
		addictionCounterMedisin = addictionCounterMedisin + addictionIncreaseMedisin;
		medisinCraving = false;
		if (addictionLevelMedisin == 1){
			medisinTimer = medisinTimer1;
		}
		if (addictionLevelMedisin == 2){
			medisinTimer = medisinTimer2;
		}
		if (addictionLevelMedisin == 3){
			medisinTimer = medisinTimer3;
		}
	}

	public void MatIncrease (){
		matCraving = false;
		addictionCounterMat = addictionCounterMat + addictionIncreaseMat;
		if (addictionLevelMat == 1){
			matTimer = matTimer1;
		}
		if (addictionLevelMat == 2){
			matTimer = matTimer2;
		}
		if (addictionLevelMat == 3){
			matTimer = matTimer3;
		}
	}

	public void DrekkeIncrease (){
		drekkeCraving = false;
		addictionCounterDrekke = addictionCounterDrekke + addictionIncreaseDrekke;
		if (addictionLevelDrekke == 1){
			drekkeTimer = drekkeTimer1;
		}
		if (addictionLevelDrekke == 2){
			drekkeTimer = drekkeTimer2;
		}
		if (addictionLevelBamse == 3){
			bamseTimer = bamseTimer3;
		}
	}
}
