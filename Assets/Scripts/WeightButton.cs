using UnityEngine;

public class WeightButton : MonoBehaviour
{
    public enum activationColor
    {
        red, blue, green
    }
        
    public activationColor color;
    private static readonly int Color1 = Shader.PropertyToID("_Color");

    // Start is called before the first frame update
    void Start()
    {
        setColor(color, gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void setColor(activationColor color, GameObject fillin)
    {
        MaterialPropertyBlock props = new MaterialPropertyBlock();
        Color startColor = new Color();

        switch (color)
        {
            case activationColor.red:
            {
                startColor = Color.red;
                break;
            }
            case activationColor.blue:
            {
                startColor = Color.blue;
                break;
            }
            case activationColor.green:
            {
                startColor = Color.green;
                break;
            }
        }
        
        props.SetColor(Color1, startColor);
        fillin.GetComponent<Renderer>().SetPropertyBlock(props);
    }
}
