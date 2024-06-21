using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public  enum  Derction{ Right , Left, Up , Douwn };
public class Breik_Controller : MonoBehaviour
{
    public string ColorName;
    public Game_Maneger Game_Maneger;

    public Derction derction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(derction)
        {
            case Derction.Right:
                {
                    if( Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        Game_Maneger.VerifyColorChoice(ColorName);
                    }

                    break;
                }

            case Derction.Left:
                {
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        Game_Maneger.VerifyColorChoice(ColorName);
                    }

                    break;
                }

            case Derction.Up:
                {
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        Game_Maneger.VerifyColorChoice(ColorName);
                    }

                    break;
                }

            case Derction.Douwn:
                {
                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        Game_Maneger.VerifyColorChoice(ColorName);
                    }

                    break;
                }

        }
    }


}
