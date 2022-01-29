using UnityEngine;


public class AnimatorController : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }


    #region Methods

    public void Walk()
    {
        _animator.SetFloat("Speed", 1);
        _animator.SetBool("Is jump", false);
    }

    public void Idle()
    {
        _animator.SetFloat("Speed", 0);
        _animator.SetBool("Is jump", false);
    }

    public void Jump()
    {
        _animator.SetBool("Is jump", true);
    }

    #endregion
}
