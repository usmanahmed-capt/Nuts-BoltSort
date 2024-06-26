using UnityEngine;

public class Nut : MonoBehaviour
{
    public enum NutColor
    {
        Red, Green, Blue, Yellow, Orange, Purple, Pink
    }

    public NutColor nutCol;

    public Color color;

    public MeshRenderer mRender;

    [SerializeField]
    private NutsCollection collection;

    // Start is called before the first frame update
    void Start()
    {
        color = collection.colors[(int)nutCol];
        mRender.material.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider != null)
                {
                    if(hit.collider.gameObject.name.Contains("Nut") || hit.collider.gameObject.name.Contains("Bolt"))
                    {
                        /*if (collection.isNutOpened)
                        {
                            collection.isNutOpened = false;
                        }*/

                        if (!collection.isNutOpened)
                        {
                            collection.isNutOpened = true;
                        }
                    }
                }
            }
        }
    }
}
