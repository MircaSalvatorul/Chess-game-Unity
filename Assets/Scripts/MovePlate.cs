using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;

    GameObject reference = null;

    //Board positions, not world

    int matrixX;
    int matrixY;

    //false movement, attack true
    public bool attack = false;

    // Start is called before the first frame update
    public void Start()
    {
        if (attack)//change color to red
        { gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f); }
    }

    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        if (attack)
        {
            GameObject cp = controller.GetComponent<GameScript>().GetPosition(matrixX, matrixY);
            Destroy(cp);
        }

        controller.GetComponent<GameScript>().SetPositionEmpty(reference.GetComponent<Chessman>().GetXboard(),
            reference.GetComponent<Chessman>().GetYboard());

        reference.GetComponent<Chessman>().SetXboard(matrixX);
        reference.GetComponent<Chessman>().SetYboard(matrixY);
        reference.GetComponent<Chessman>().SetCoords();

        controller.GetComponent<GameScript>().SetPosition(reference);

        reference.GetComponent<Chessman>().DestroyMovePlate();
    }

    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    public void SetReference(GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetReference()
    {
        return reference;
    }
}
