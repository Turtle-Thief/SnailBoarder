using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace SuperiorArrays {


    class Sray
    {



        public static string[] AppendStringArray(string[] sArray, string addition)
        {
            string[] tempArray = new string[sArray.Length + 1];
            for (int i = 0; i < sArray.Length; i++)
            {
                tempArray[i] = sArray[i];
            }
            tempArray[tempArray.Length - 1] = addition;
            return tempArray;
        }


        public static float[] AppendFloatArray(float[] fArray, float addition)
        {
            float[] tempArray = new float[fArray.Length + 1];
            for (int i = 0; i < fArray.Length; i++)
            {
                tempArray[i] = fArray[i];
            }
            tempArray[tempArray.Length - 1] = addition;
            return tempArray;
        }

        public static GameObject[] AppendGameObjectArray(GameObject[] gArray, GameObject addition)
        {
            GameObject[] tempArray = new GameObject[gArray.Length + 1];
            for (int i = 0; i < gArray.Length; i++)
            {
                tempArray[i] = gArray[i];
            }
            tempArray[tempArray.Length - 1] = addition;
            return tempArray;
        }

    }




        





}
