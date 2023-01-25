using UnityEngine;
using UnityEngine.SceneManagement;

public class ColisionHandler : MonoBehaviour
{
    public AudioClip crash;
    public AudioClip success;

    public ParticleSystem crashParticles;
    public ParticleSystem successParticles;
    private BoxCollider myColider;
    private AudioSource audioSource;
    int _scheneIndex = 0;
    public float delay = 1f;
    bool isTransitioning = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        myColider = GetComponent<BoxCollider>();
    }
    private void Update()
    {
        DebugFunctionality();
    }

    void DebugFunctionality()
    {
        if (Input.GetKeyDown(KeyCode.C))
            myColider.enabled = !myColider.enabled;
        if (Input.GetKeyDown(KeyCode.L))
        {
            SetNextSceneIndex();
            DelayedLoadLevel();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning)
            return;
        _scheneIndex = SceneManager.GetActiveScene().buildIndex;
        switch (collision.gameObject.tag)
        {
            case "Fuel":
                {
                    collision.gameObject.SetActive(false);
                    Debug.Log("Gas Hit");
                    break;
                }
            case "Finish":
                {
                    SetNextSceneIndex();
                    successParticles.Play();
                    PlaySound(success);
                    DelayedLoadLevel();
                    break;
                }
            case "Friendly":
                {
                    break;
                }
            default:
                crashParticles.Play();
                PlaySound(crash);
                DelayedLoadLevel();
                break;
        }
    }



    void SetNextSceneIndex() {
        if (SceneManager.sceneCountInBuildSettings == _scheneIndex+1)
            _scheneIndex = 0;
        else _scheneIndex += 1;
    }
    void PlaySound(AudioClip clip) {
        if (audioSource.isPlaying)
            audioSource.Stop();
        audioSource.PlayOneShot(clip);
    }
    void DelayedLoadLevel()
    {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(LoadLevel), delay);

    }
    void LoadLevel() {
        SceneManager.LoadScene(_scheneIndex);

    }
  
}
