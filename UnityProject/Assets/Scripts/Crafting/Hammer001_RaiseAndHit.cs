using UnityEngine;

public class Hammer001_RaiseAndHit : MonoBehaviour
{
    private bool _heating;
    private Animation _smithHammerAnimation;
    private AudioSource _audioData;

    [SerializeField]
    public AudioClip[] impacts;
    
    private void Start()
    {
        _smithHammerAnimation = gameObject.GetComponent<Animation>();
        _audioData = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (_smithHammerAnimation.isPlaying)
            {
                return;
            }

            _smithHammerAnimation.Play(!_heating ? "SmithHammer001_Raise" : "SmithHammer001_Raise");
            _audioData.PlayOneShot(impacts[Random.Range(0,impacts.Length)], 1.0f);

            _heating = !_heating;
        }
    }
}