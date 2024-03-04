using UnityEngine;
using UnityEngine.Events;

public class WorldButton : MonoBehaviour
{
    public UnityEvent eventBoi;

    public void Clicked()
    {
        eventBoi.Invoke();
    }

}
