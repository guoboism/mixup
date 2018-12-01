using UnityEngine;
using System.Collections;

[ExecuteInEditMode ()]
public class ExclusiveChildActivation : MonoBehaviour
{
	public bool editModeOnly = false;
	//[HideByBooleanProperty ("editModeOnly", true)]
	public bool activeChildrenOnDisable = false;
	private Transform lastActiveChild = null;

	void Update ()
	{
		if (!editModeOnly || !Application.isPlaying) {
			if (enabled) {
				if (lastActiveChild == null) {
					for (int i = 0; i < transform.childCount; i++) {
						Transform child = transform.GetChild (i);
						if (child.gameObject.activeSelf) {
							lastActiveChild = child;
							break;
						}
					}
				}

				for (int i = 0; i < transform.childCount; i++) {
					Transform child = transform.GetChild (i);
					if (child != lastActiveChild && child.gameObject.activeSelf) {
						lastActiveChild.gameObject.SetActive (false);
						lastActiveChild = child;
					}
				}
			}
		}
	}

	void OnDisable ()
	{
		if (!editModeOnly || !Application.isPlaying) {
			if (activeChildrenOnDisable) {
				for (int i = 0; i < transform.childCount; i++)
					transform.GetChild (i).gameObject.SetActive (true);
			}
		}
	}

	[ContextMenu ("Randomize Active Child")]
	void RandomizeActiveChild ()
	{
		int randomActiveChildIndex = Random.Range (0, transform.childCount);
		for (int i = 0; i < transform.childCount; i++)
			transform.GetChild (i).gameObject.SetActive (i == randomActiveChildIndex);

		lastActiveChild = transform.GetChild (randomActiveChildIndex);
	}
}
