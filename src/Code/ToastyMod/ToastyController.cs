using UnityEngine;
using System.Collections;

/// <summary>
/// Controller responsbile to mimics the Mortal Kombat's Toasty! easter egg.
/// </summary>
public class ToastyController : MonoBehaviour {

    #region Fields
    private AudioSource m_audio;
	private Vector2 m_currentTargetPosition;
	private float m_currentSpeed;
    #endregion

    #region Editor properties
    public float WarmupSeconds = 2f;
	public Vector3 MoveSize = new Vector3(85, 0, 0);
	public float SlideInSpeed = 0.5f;
	public float SlideOutSpeed = 1f;
    #endregion

    #region Methods
    void Awake()
	{
		m_audio = GetComponentInChildren<AudioSource> ();
	}

	void Update () {
        // Update the Toasty's sprite in direction of the target position.
		transform.position = Vector2.Lerp (transform.position, m_currentTargetPosition, m_currentSpeed);
	}

	void OnEnable()
	{
        // Everty time that became enabled, starts the slide.
		StartCoroutine (Slide ());
	}

    /// <summary>
    /// Perform the slide of the Toasty's sprite.
    /// </summary>
    /// <returns>The enumerator.</returns>
    private IEnumerator Slide()
	{
        // Put the game object in the initial position and wait the warmup seconds.
		m_currentTargetPosition = transform.position;
		yield return new WaitForSeconds (WarmupSeconds);

        // The sound duration will be used as time of the slide.
		var slideSeconds = m_audio.clip.length;

		// Slide in. Sets the target position (used on Update method) and play sound.
		m_currentTargetPosition = transform.position - MoveSize;
		m_currentSpeed = SlideInSpeed;
		m_audio.Play ();

        // Wait for the sound finish.
		yield return new WaitForSeconds (slideSeconds);

        // Slide out. Sets the target position back to out of the screen.
        m_currentTargetPosition = transform.position + MoveSize;
		m_currentSpeed = SlideOutSpeed;
		yield return new WaitForSeconds (slideSeconds);

        // Deactivate the game object, then it can be used a next time.
		gameObject.SetActive(false);
	}
    #endregion
}
