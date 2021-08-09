using UnityEngine;
using Global.Shooting;

public class ShootController : MonoBehaviour
{
    #region Inspector variables

#pragma warning disable

    [SerializeField] private BaseWeapon baseWeapon;
    [SerializeField] private Transform cannonTransform;
    [SerializeField] private Transform bulletPool;
    [SerializeField] private Vector2 mousePos;

#pragma warning restore

    #endregion Inspector variables

    #region private variables

    private Coroutine coroutineShoot;

    #endregion private variables

    #region properties

    public BaseWeapon CurrentWeapon => baseWeapon;

    #endregion properties

    #region private void

    private void GetReadyShootByWeapon()
    {
        if (Input.GetKey(KeyCode.Mouse0) && coroutineShoot == null)
        {
            coroutineShoot = StartCoroutine(baseWeapon.Shoot(mousePos, cannonTransform, bulletPool, () => coroutineShoot = null));
        }
    }

    private Vector2 Rotation(Transform transformObject)
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return (mousePos - (Vector2)transformObject.position).normalized;
    }

    #region Unity function

    private void Update()
    {
        cannonTransform.up = Rotation(cannonTransform);
        GetReadyShootByWeapon();
    }

    #endregion Unity function

    #endregion private void
}