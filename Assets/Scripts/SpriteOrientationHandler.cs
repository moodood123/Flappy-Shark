using UnityEngine;

public class SpriteOrientationHandler : MonoBehaviour
{
    [SerializeField] private AnimationCurve _speedToAngleRelation;

    [SerializeField] private float _magnitude;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Transform _target;

    private void Update()
    {
        _target.localRotation = Quaternion.Euler(0f, 0f, GetAngle() * _magnitude);
    }
    
    private float GetAngle()
    {
        return _speedToAngleRelation.Evaluate(_rb.linearVelocityY);
    }
}
