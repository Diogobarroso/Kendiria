using TMPro;
using UnityEngine;

public class CounterController : MonoBehaviour
{
    [SerializeField] private TMP_Text _counterText;
    [SerializeField] private Animator _animator;

    public void AnimateCounter()
    {
        _animator.SetTrigger("AnimateCounter");
    }

    public void CounterController_SetFive()
    {
        _counterText.text = "5";
    }

    public void CounterController_SetFour()
    {
        _counterText.text = "4";
    }

    public void CounterController_SetThree()
    {
        _counterText.text = "3";
    }

    public void CounterController_SetTwo()
    {
        _counterText.text = "2";
    }

    public void CounterController_SetOne()
    {
        _counterText.text = "1";
    }

    public void CounterController_SetGo()
    {
        _counterText.text = "GO!";
    }
}
