using DG.Tweening;
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

    /*[SerializeField]
    private Animator animator;*/

    public bool isNutOpened = false;
    bool completed = true;
    // Start is called before the first frame update
    void Start()
    {
        color = collection.colors[(int)nutCol];
        mRender.material.color = color;
        //animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (collection.IsNutOpened() && openedNut && openedNut.isNutOpened && completed)
            {
                if (openedNut != null)
                {
                    openedNut.isNutOpened = false;
                    //openedNut.animator.enabled = true;
                    //openedNut.animator.SetBool("CloseNut", true);
                    CloseNut();
                }
            }


            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.name.Contains("nut") || hit.collider.gameObject.name.Contains("bolt"))
                    {
                        if (!collection.IsNutOpened() && !isNutOpened && completed)
                        {
                            collection.SetNutOpened(true);
                            if (collection.bolt.nuts.Count > 0)
                            {
                                if (collection.bolt.nuts.TryPop(out var nut1))
                                {
                                    //var nut1 = hit.collider.GetComponentInParent<Nut>();
                                    if (nut1 != null)
                                    {
                                        nut1.isNutOpened = true;
                                        Debug.Log(nut1.name);
                                        //nut1.animator.enabled = true;
                                        //nut1.animator.SetBool("OpenNut", true);
                                        OpenNut(nut1);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

    }


    private Nut openedNut;
    private Vector3 nutInitialPosition = Vector3.zero;
    void OpenNut(Nut nut)
    {
        nutInitialPosition = nut.transform.localPosition;
        openedNut = nut;
        completed = false;
        nut.transform.DORotate(new Vector3(0,-113,0), 1f).SetEase(Ease.Linear);
        nut.transform.DOLocalMoveY(0.0645f, 1f).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
        {
            completed = true;
            //nut.animator.SetBool("OpenNut", false);
            //openedNut.animator.Rebind();
            //openedNut.animator.enabled = false;
        });
    }


    void CloseNut()
    {
        completed = false;
        openedNut.transform.DORotate(Vector3.zero, 1f).SetEase(Ease.Linear);
        openedNut.transform.DOLocalMoveY(nutInitialPosition.y, 1f).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
        {
            completed = true;
            collection.bolt.nuts.Push(openedNut);
            //openedNut.animator.SetBool("CloseNut", false);
            //openedNut.animator.Rebind();
            //openedNut.animator.enabled = false;
            collection.SetNutOpened(false);
            openedNut = null;
        });
    }
}
