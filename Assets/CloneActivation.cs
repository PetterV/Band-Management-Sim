using UnityEngine;
using System.Collections;

public class CloneActivation : MonoBehaviour {

	public bool active = false;

	public void Activation() {
		GetComponentInParent<BandMember>().enabled = true;
		GetComponentInParent<Innfallsystemet>().enabled = true;
	}
}
