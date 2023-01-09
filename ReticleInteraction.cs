using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Threading.Tasks;

public class ReticleInteraction : MonoBehaviour
{
    new Camera camera;
    public float distance = 4f;

    GameObject rayCastedGO;

    // materials for highlight
    private Material SimpleMat;
    public Material HighlightedMat;
    public Material HintMat;

    private Text optionText;
    bool hintSelected;
    bool attemptSelected;

    void Start()
    {
        camera = GetComponent<Camera>();
        hintSelected = false;
        attemptSelected = false;

        // Load the Arial font from the Unity Resources folder.
        Font arial;
        arial = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");

        // Create Canvas GameObject.
        GameObject canvasGO = new GameObject();
        canvasGO.name = "OptionCanvas";
        canvasGO.AddComponent<Canvas>();
        canvasGO.AddComponent<CanvasScaler>();
        canvasGO.AddComponent<GraphicRaycaster>();

        // Get canvas from the GameObject.
        Canvas canvas;
        canvas = canvasGO.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        // Create the Text GameObject.
        GameObject textGO = new GameObject();
        textGO.transform.parent = canvasGO.transform;
        textGO.AddComponent<Text>();
        optionText = textGO.GetComponent<Text>();
        optionText.font = arial;
        optionText.fontSize = 18;
        optionText.color = Color.black;

        // Provide Text position and size using RectTransform.
        RectTransform rectTransform;
        rectTransform = optionText.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(0, 0, 0);
        rectTransform.sizeDelta = new Vector2(600, 200);

    }

    void Update()
    {


        bool rcHit = false;
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance))
        {
            rcHit = true;
            if (rayCastedGO != hit.collider.gameObject)
            {
                rayCastedGO = hit.collider.gameObject;
                SimpleMat = rayCastedGO.GetComponent<Renderer>().material;
            }


            // highlight object
            if (rayCastedGO.layer == LayerMask.NameToLayer("InitialHint"))
            {
                rayCastedGO.GetComponent<Renderer>().material = HintMat;
                hintSelected=true;
            }
            else
            {
                rayCastedGO.GetComponent<Renderer>().material = HighlightedMat;
                attemptSelected=true;
            }

            //Click item
            if (Input.GetMouseButtonDown(0))
            {
                
                if (hintSelected)
                {
                    optionText.text = rayCastedGO.GetComponent<NumberObject>().objectType.ToString();
                    Debug.Log("Hint");
                }
                else if (attemptSelected)
                {
                    //TODO: Menu shows objectType and gives option to Change or Remove
                    optionText.text = "Options (Menu TBD)";
                    Debug.Log("Attempt");
                } else
                {
                    
                    Debug.Log("Nothing clicked");
                }

            }

        }
        if (!rcHit && rayCastedGO != null)
        {
            rayCastedGO.GetComponent<Renderer>().material = SimpleMat;
            hintSelected = false;
            attemptSelected = false;
            if (Input.GetMouseButtonDown(0))
            {
                optionText.text = "";
            }
            
        }

        //TODO: Handle "empty" locations
        //On click: bring up menu to let player select objectType
        //Call GuessHandler to generate "guess"
     
    }

}