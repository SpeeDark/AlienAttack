using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ObscureCameraView : MonoBehaviour
{
    [SerializeField] private Image panel;

    [Space(15f)]
    [SerializeField] private float _speed = 1f;

    public UnityEvent Closed;

    public void Close()
    {
        StartCoroutine(Closing());
    }

    public void Open()
    {
        StartCoroutine(Opening());
    }

    private IEnumerator Closing()
    {
        panel.gameObject.SetActive(true);

        while (panel.color.a < 1f)
        {
            panel.color += new Color(0f, 0f, 0f, 0.05f * _speed);

            yield return new WaitForFixedUpdate();
        }

        Closed.Invoke();
    }

    private IEnumerator Opening()
    {
        while (panel.color.a > 0f)
        {
            panel.color -= new Color(0f, 0f, 0f, 0.05f * _speed);

            yield return new WaitForFixedUpdate();
        }

        panel.gameObject.SetActive(false);
    }
}