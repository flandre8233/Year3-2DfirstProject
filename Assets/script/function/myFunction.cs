using System;

namespace myFunction {
    public class Function {

        #region randomFunction
        public static int RandomNumber(int selectedDice) {
            Random myRand = new Random(Guid.NewGuid().GetHashCode());
            int randomNumber = (myRand.Next(1, selectedDice + 1));
            return randomNumber;
        }

        public static int TimesDiceSided(int Times, int sided_dice) {
            int totalRandomNumber = 0;
            for (int i = 1; i <= Times; i++) {
                totalRandomNumber += RandomNumber(sided_dice);
            }
            return totalRandomNumber;
        }
        #endregion

        #region sortFunction
        public static int[] SortTheArray(int[] intArrray, bool isbigtoSmall) {
            int saveNum = 0;
            for (int i = 0; i <= intArrray.Length - 1; i++) {
                for (int j = 0; j <= intArrray.Length - 2; j++) {
                    if (isbigtoSmall) {
                        if (intArrray[j] < intArrray[j + 1]) {
                            saveNum = intArrray[j + 1];
                            intArrray[j + 1] = intArrray[j];
                            intArrray[j] = saveNum;
                        }
                    }
                    else {
                        if (intArrray[j] > intArrray[j + 1]) {
                            saveNum = intArrray[j + 1];
                            intArrray[j + 1] = intArrray[j];
                            intArrray[j] = saveNum;
                        }
                    }

                }
            }
            return intArrray;
        }

        public static string[] SortTheArray(string[] stringArray, bool isbigtoSmall) {
            Array.Sort(stringArray, StringComparer.InvariantCulture);
            if (!isbigtoSmall) {
                string saveString = "";
                //Array.Reverse(stringArray);
                for (int i = 0; i <= (stringArray.Length - 1) / 2; i++) {
                    saveString = stringArray[stringArray.Length - 1 - i];
                    stringArray[stringArray.Length - 1 - i] = stringArray[i];
                    stringArray[i] = saveString;
                }
            }
            return stringArray;
        }
        #endregion

        public static int findSmallestOfBigestNumberInArray(int[] intArray, bool isfindBigest) {
            int targetNumber = 0;
            if (isfindBigest) {
                for (int i = 0; i <= (intArray.Length - 2); i++) {
                    if (intArray[i] > intArray[targetNumber]) {
                        targetNumber = i;
                    }
                }
            }
            else {
                for (int i = 0; i <= (intArray.Length - 2); i++) {
                    if (intArray[i] < intArray[targetNumber]) {
                        targetNumber = i;
                    }
                }
            }
            return targetNumber;
        }


        public static int findSmallestOfBigestNumberInArray(float[] floatArray, bool isfindBigest,int indexNo) {
            int[] targetNumber = new int[indexNo+1];  //targetnumber是floatarray裡的index

            bool firstOKNumber = false;

            if (isfindBigest) {
                for (int u = 0; u <= targetNumber.Length-1; u++) {
                    firstOKNumber = false;
                    for (int i = 0; i <= (floatArray.Length - 1); i++) {
                        if ( u != 0 && floatArray[i] <= floatArray[targetNumber[u - 1]] && targetNumber[u - 1] != i) {
                            if (!firstOKNumber) {
                                targetNumber[u] = i;
                                firstOKNumber = true;
                            }
                            if (floatArray[i] > floatArray[targetNumber[u]] && firstOKNumber) {  //要第一個targetnumber[u]裡的數面要先通過了上面測試而無需通過這個測試
                                targetNumber[u] = i;
                            }
                        }
                        if (floatArray[i] > floatArray[targetNumber[u]] && u == 0) {
                            targetNumber[u] = i;
                        }

                    }
                }
            }
                else {
                for (int u = 0; u <= targetNumber.Length - 1; u++) {
                    firstOKNumber = false;
                    for (int i = 0; i <= (floatArray.Length - 1); i++) {
                        if ( u != 0&& floatArray[i] >= floatArray[targetNumber[u-1]] && targetNumber[u - 1] != i) {
                            if (!firstOKNumber) {
                                targetNumber[u] = i;
                                firstOKNumber = true;
                            }
                            if (floatArray[i] < floatArray[targetNumber[u]] && firstOKNumber) {
                                targetNumber[u] = i;
                            }
                        }
                        if (floatArray[i] < floatArray[targetNumber[u]] && u == 0) {
                            targetNumber[u] = i;
                        }

                    }
                }
                
                }
                return targetNumber[indexNo];
            }
        
    

        #region keysFunction
        public static string keysStringConvert(string keysString) {
            switch (keysString) {
                case "OemOpenBrackets":
                    return "[";
                case "Oem6":
                    return "]";
                case "Oem1":
                    return ";";
                case "Oem7":
                    return "'";
                case "Oemcomma":
                    return ",";
                case "OemPeriod":
                    return ".";
                case "OemQuestion":
                    return "/";
                case "OemMinus":
                    return "-";
                case "OemPlus":
                    return "=";
                case "Oem5":
                    return "|";
                case "Next":
                    return "PageDown";

                default:
                    return keysString;
            }
        }
        #endregion
    }
}
