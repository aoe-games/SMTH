using UnityEngine;

public class ToggleBetweenForgeAndAnvil : MonoBehaviour
{
    private bool _heating;
    private Animation _weaponAnimation;

    private void Start()
    {
        _weaponAnimation = gameObject.GetComponent<Animation>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_weaponAnimation.isPlaying)
            {
                return;
            }

            _weaponAnimation.Play(!_heating ? "Forge1MoveWeaponToForge" : "Forge1MoveWeaponToAnvil");

            _heating = !_heating;
        }
    }
}
