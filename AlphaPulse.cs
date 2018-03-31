// ----------------------------------------------------------------------------
// <summary>
//   Component to pulse tranparent alpha chanal in current or custom material.
// </summary>
// <author>espair@mail.ru</author>
// ----------------------------------------------------------------------------

using UnityEngine;

public class AlphaPulse : MonoBehaviour {

    public Material material;
    public float alphaMin = 0.8f;
    public float alphaMax = 1f;
    public float speed = 1f;
    public bool run = true;

    Color color;
    Color defaultColor;
    bool revers;
    float time;

    private void OnEnable()
    {
        color = defaultColor;
    }

    private void OnDisable()
    {
        color = defaultColor;
        //run = false;
    }

    private void Start ()
    {
        if (GetComponent<Renderer>())
        {
            if (material == null)
            {
                material = gameObject.GetComponent<Renderer>().material;
            }
            StandardShaderUtils.ChangeRenderMode(material, StandardShaderUtils.BlendMode.Transparent);
            color = material.color;
            defaultColor = material.color;
        }
        else
        {
            run = false;
        }
    }

    private void Update()
    {
        if (alphaMin < 0) alphaMin = 0;
        if (alphaMax > 1) alphaMax = 1;
        if (speed <= 0) speed = 0.01f;
        //if (!GetComponent<Renderer>().enabled) run = false;
        //else run = true;
        if (run)
        {
            time = Time.deltaTime / 10 + speed / 100;
            if (revers)
            {
                if (color.a <= alphaMax && color.a > alphaMin)
                {
                    color.a -= time;
                }
                else
                {
                    if (color.a <= alphaMin)
                    {
                        revers = false;
                        color.a = alphaMin;
                    }
                }
            }
            else
            {
                if (color.a >= alphaMin && color.a < alphaMax)
                {
                    color.a += time;
                }
                else
                {
                    if (color.a >= alphaMax)
                    {
                        revers = true;
                        color.a = alphaMax;
                    }
                }
            }
        }
        else
        {
            color = defaultColor;
        }
    }

    private void FixedUpdate()
    {
        if (material != null)
        {
            material.color = color;
        }
        else
        {
            run = false;
        }
    }
}
