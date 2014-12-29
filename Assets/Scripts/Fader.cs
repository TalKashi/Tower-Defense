using UnityEngine;
using System.Collections;
    
public class Fader : MonoBehaviour
{

    private SpriteRenderer m_FadeableObject;
    private bool StartFading = false;

    public void FixedUpdate()
    {
        if (StartFading)
        {
            if (m_FadeableObject.color.a <= 0)
            {
                Destroy(m_FadeableObject.gameObject);
            }
            else
            {
                m_FadeableObject.color = new Color(m_FadeableObject.color.r, m_FadeableObject.color.g, m_FadeableObject.color.b, m_FadeableObject.color.a - 0.02f);
            }
        }
    }

    public void Fade(SpriteRenderer i_FadeableObject)
    {
        m_FadeableObject = i_FadeableObject;
        StartFading = true;
        gameObject.SendMessage("SetDyingState");
    }
}

