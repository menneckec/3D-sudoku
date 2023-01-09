using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SudokuGenerator : MonoBehaviour
{
    //Possible game iterations = 12*size
    string[] sampleSolutions = new string[3]{
        "276491583438952169195836472524318697981627354367945218743169825659284731812573946",
        "514672389897135642632984751159467238376258194248391576463519827721843965985726413",
        "274518396968473512315692847587921463126347958439856271652739184841265739793184625"
        };
    public int[,] solution;
    int difficulty = 50;
    public GameObject generatedObject;

    private Text text;


    // Start is called before the first frame update
    void Start()
    {
        Font arial;
        arial = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        Canvas hud = GameObject.Find("HUD").transform.Find("Pause_Menu_Panel/SolutionCanvas").GetComponent<Canvas>();

        GameObject textGO = new GameObject();
        textGO.transform.parent = hud.transform;
        textGO.AddComponent<Text>();
        text = textGO.GetComponent<Text>();
        text.font = arial;
        text.color = Color.black;
        text.text = "";
        text.fontSize = 18;
        text.alignment = TextAnchor.MiddleCenter;

        RectTransform rectTransform;
        rectTransform = text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(0, 0, 0);
        rectTransform.sizeDelta = new Vector2(600, 200);
        //Prompt for difficulty
        //Beginner (default) = 50
        //Easy = 40
        //Normal = 33
        //Hard = 26
        //Critical = 17
        GenerateRandomSolution();
        int[,] hints = SetClues(solution, difficulty);
        LockClues(hints);
        DrawPuzzle(hints);

    }

    // Update is called once per frame
    void Update()
    {

    }

    //picks from the possible solution array and randomly transforms it
    public void GenerateRandomSolution()
    {
        //randomly select from sample set
        int i = Random.Range(0, sampleSolutions.Length - 1);
        int[,] possibleSol = StringToMatrix(sampleSolutions[i]);
        //randomly decide whether to rotate
        int rot = Random.Range(0, 3);
        int[,] r = Rotate(rot, possibleSol);
        int flip = Random.Range(0, 2);
        int[,] f = Flip(flip, r);
        solution = f;

        Debug.Log("Solution generated");

    }

    //Determines which numbers are initially revealed to the player
    private int[,] SetClues(int[,] m, int K)
    {
        //Object properties
        //i*j = objectLocation (0-80)
        //m[i,j] = objectType (1-9)

        //Empty matrix
        int[,] clues = new int[9, 9];

        //Determines which clues to "show"
        for (int i = 0; i < K; i++)
        {
            int show = Random.Range(0, 80);
            int row = show / 9;
            int column = show % 9;

            //Duplicate entries
            if (clues[row, column] == m[row, column])
            {
                //Try again
                i--;
            }

            clues[row, column] = m[row, column];
        }

        Debug.Log("Clues set");

        return clues;


    }

    private void LockClues(int[,] m)
    {
        GameObject temp = generatedObject;
        int HintLayer = LayerMask.NameToLayer("InitialHint");
        int SolutionLayer = LayerMask.NameToLayer("Solution");
        
        NumberObject num = temp.GetComponent<NumberObject>();
        Destroy(generatedObject);


        //Generate hints
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                num.objectLocation = new Vector3(i * 3, 3, j * 3);
                Vector3 location = num.objectLocation;

                //Initial hints
                if (m[i, j] != 0)
                {
                    temp.SetActive(true);
                    temp.name = "Hint " + m[i, j].ToString();
                    num.objectType = m[i, j];
                    temp.layer = HintLayer;
                    //TODO: Change the model of the object based on its objectType
                    Instantiate(temp, location, Quaternion.identity);
                }
                else
                {
                    //Invisible group of objects to test solution
                    temp.SetActive(false);
                    temp.name = "Solution " + solution[i, j].ToString();
                    num.objectType = solution[i, j];
                    temp.layer = SolutionLayer;
                    Instantiate(temp, location, Quaternion.identity);
                }
            }
        }

        Debug.Log("Initial clues locked. Ready to play!");

    }

    private void DrawPuzzle(int[,] m)
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                text.text += m[i, j].ToString() + "  ";
            }
            text.text += "\n";
        }

    }

    //===================================
    //Functions to generate solutions
    //===================================

    private int[,] Rotate(int degrees, int[,] m)
    {
        for (int a = 0; a < degrees; a++)
        {
            // Traverse each cycle
            for (int i = 0; i < 9 / 2; i++)
            {
                for (int j = i; j < 9 - i - 1; j++)
                {

                    // Swap elements of each cycle
                    // in clockwise direction
                    int temp = m[i, j];
                    m[i, j] = m[8 - j, i];
                    m[8 - j, i] = m[8 - i, 8 - j];
                    m[8 - i, 8 - j] = m[j, 8 - i];
                    m[j, 8 - i] = temp;
                }
            }
        }

        Debug.Log("This solution was rotated");

        return m;
    }

    private int[,] Flip(int type, int[,] m)
    {
        switch (type)
        {
            case 1:
                //flip horizontal
                break;
            case 2:
                //flip vertical
                break;
            default:
                //do nothing
                break;
        }

        Debug.Log("This solution was flipped");

        return m;
    }

    private int[,] StringToMatrix(string s)
    {
        Debug.Log("Selected solution: " + s);
        int[,] matrix = new int[9, 9];
        int row = matrix.GetLength(0);
        int col = matrix.GetLength(1);

        for (int i = 0; i < row * col; i++)
        {
            matrix[i / col, i % col] = int.Parse(s[i].ToString());
        }

        Debug.Log("Matrix conversion complete");

        return matrix;


    }



}
