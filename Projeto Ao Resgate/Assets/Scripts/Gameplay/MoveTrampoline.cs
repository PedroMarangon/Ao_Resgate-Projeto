//Maded by Pedro M Marangon
using UnityEngine;
using NaughtyAttributes;

public class MoveTrampoline : MonoBehaviour {

	GameManager manager;

	[BoxGroup("Audio")] public AudioSource trampoline;
	[BoxGroup("Audio")] public AudioSource people;

	public GameObject UI;
	[BoxGroup("Grow Trampoline")] public float growScale = 1.5f;
	[BoxGroup("Global variables")] public float speed = 5f;
	[BoxGroup("Global variables")] public int score = 0;
	[BoxGroup("Global variables")] public float timeToDestroy = .5f;
	[BoxGroup("Accelerometer")][InfoBox("This is the variables that works with " +
								"Accelerometer enabled")]public bool hasAccelerometer = false;
	[BoxGroup("Accelerometer")]public float minValueForMove;

	[BoxGroup("Buttons")] public ButtonPressed left;
	[BoxGroup("Buttons")] public ButtonPressed right;
	[BoxGroup("Grow Trampoline")] [SerializeField] private Sprite normalSprite;
	[BoxGroup("Grow Trampoline")][SerializeField]private Sprite growSprite;

	// Use this for initialization
	void Start () {
#if UNITY_EDITOR
		PlayerPrefs.SetInt("ctrl", 1);
#endif
		GetComponent<SpriteRenderer>().sprite = normalSprite;
			transform.localScale = new Vector3(1,.5f,1);

		if (PlayerPrefs.GetInt("power01") == 1 || PlayerPrefs.GetInt("power02") == 1) {
			transform.localScale *= growScale;
			GetComponent<SpriteRenderer>().sprite = growSprite;
		}

		manager = GameManager.instance;
		Time.timeScale = 1;
		UI.SetActive(PlayerPrefs.GetInt("ctrl") == 1);//If ctrl=1 (buttons), sets to true
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
	
	// Update is called once per frame
	void Update () {

#if UNITY_EDITOR
		PlayerPrefs.SetInt("ctrl", 1);
#endif

		if (PlayerPrefs.GetInt("ctrl") == 0) {
			if (SystemInfo.supportsAccelerometer) {
				if (Input.acceleration.x > minValueForMove || Input.acceleration.x < -minValueForMove) {
					Vector2 move = Input.acceleration * speed * Time.deltaTime;
					move.y = 0;
					transform.Translate(move);
					transform.position = new Vector3(Mathf.Clamp(transform.position.x,-.8f, .8f),
												transform.position.y,transform.position.z);
				}
			} else {
				Debug.Log("Don't supports Accelerometer");
			}
		}
		else {
			if (left.buttonPressed) {
				transform.Translate(-(speed * Time.deltaTime), 0, 0);
				transform.position = new Vector3(Mathf.Clamp(transform.position.x,-.8f, .8f),
											transform.position.y,transform.position.z);
			} else
			if (right.buttonPressed) {
				transform.Translate(speed * Time.deltaTime, 0, 0);
				transform.position = new Vector3(Mathf.Clamp(transform.position.x, -.8f, .8f),
											transform.position.y, transform.position.z);
			}

		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.GetComponent<Fall>() != null) {
			Falling fall = other.GetComponent<Fall>().falling;
			switch (fall.type) {
				case FallingType.Obj:
					if (score > 0) {
						if (score-fall.Pontuation <= 0) {
							return;
						}
						score -= fall.Pontuation;
						manager.UpdateUI(score);
					}//else manager.DoTimesUp();
				break;
				case FallingType.Person:
					int pont = fall.Pontuation;
					//Debug.Log("Power01:"+);
					if((PlayerPrefs.GetInt("power01") == 2) || (PlayerPrefs.GetInt("power02") == 2)) {
						Debug.Log("Multiplied!");
						pont *= 2;
					}
					score += pont;
					people.Play();
					trampoline.Play();
					manager.UpdateUI(score);
				break;
			}
			Destroy(other.gameObject, timeToDestroy);
		}
	}

}
