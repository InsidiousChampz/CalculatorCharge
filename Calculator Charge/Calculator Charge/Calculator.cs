using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator_Charge
{
    public static class Calculator
    {
        #region "Variable"
        private const string TEXTCANNOTCHARGE = "Cannot Change";
        #endregion

        #region "Method"
        // - Method
        public static string CalculationChargeValue(decimal inputChargeValue)
        {

            string ret = "";

            // Check Digit Input
            int lengthInput = inputChargeValue.ToString().Length;

            if (lengthInput == 1)
            {
                // 1, 2, 3, ..9
                ret = GetValueOneToNine(inputChargeValue);
            }
            else
            {
                // Calc 10 - ..N
                if (CheckTenOnly(inputChargeValue))
                {
                    // just 10, 20, 30, ....
                    ret = GetValueTenOnly(inputChargeValue);
                }
                else
                {
                    // > 10
                    ret = GetValueCalculation(inputChargeValue);
                }
            }

            return ret;
        }
        #endregion

        // - Function
        #region "Function"
        private static bool CheckTenOnly(decimal inputChargeValue)
        {
            bool ret = false;
            decimal calcFullTen = Decimal.Divide(inputChargeValue, 10);
            int isFullTen = calcFullTen.ToString().IndexOf(".");
            if (isFullTen == -1)
            {
                ret = true;
            }

            return ret;
        }
        private static string GetValueTenOnly(decimal inputChargeValue)
        {
            decimal calcFullTen = Decimal.Divide(inputChargeValue, 10);
            return Convert.ToInt32(calcFullTen) + " 0";
        }
        private static string GetValueOneToNine(decimal inputChargeValue)
        {
            // Calc 1 - 9
            string ret;
            decimal calcThreeValue = Decimal.Divide(inputChargeValue, 3);
            int isThreeDecimal = calcThreeValue.ToString().IndexOf(".");
            if (isThreeDecimal == -1)
            {
                ret = "0 " + calcThreeValue;
            }
            else
            {
                ret = TEXTCANNOTCHARGE;
            }

            return ret;
        }
        private static int GetChargeValuePerDigit(decimal inputChargeValue, int position)
        {
            return Convert.ToInt32(inputChargeValue.ToString().Substring(position, 1));
        }
        private static bool CheckThreeValue(int num)
        {
            bool ret = false;
            decimal dNum = Decimal.Divide(num, 3);
            int isDecimal = dNum.ToString().IndexOf(".");

            if (isDecimal == -1)
            {
                ret = true;
            }

            return ret;
        }
        private static int GetThreeValue(int num)
        {
            int ret = 0;
            decimal dNum = Decimal.Divide(num, 3);
            int isDecimal = dNum.ToString().IndexOf(".");

            if (isDecimal == -1)
            {
                ret = Convert.ToInt32(dNum);
            }

            return ret;
        }

        private static string GetValueCalculation(decimal inputChargeValue)
        {
            // Calc more than ten.
            string ret = "";

            if (inputChargeValue.ToString().Length > 2)
            {
                ret = "No Need to Calculate ... Please Exit.";
            }
            else
            {
                int num1 = GetChargeValuePerDigit(inputChargeValue, 0);
                int num2 = GetChargeValuePerDigit(inputChargeValue, 1);


                if (CheckThreeValue(num2))
                {
                    ret = num1 + " " + GetThreeValue(num2);
                }
                else
                {
                    if (num1 == 0)
                    {
                        ret = TEXTCANNOTCHARGE;
                    }
                    else
                    {
                        int retValue = 0;
                        while (retValue == 0)
                        {
                            num1 = num1 - 1;
                            num2 = num2 + 10;

                            if (num1 == -1)
                            {
                                ret = TEXTCANNOTCHARGE;
                                break;
                            }
                            else
                            {
                                retValue = GetThreeValue(num2);
                                ret = num1 + " " + retValue;
                            }

                        }
                    }
                }
            }

            

            return ret;
        }
        private static string GetValueCalculation_Backup(decimal inputChargeValue)
        {
            // Calc more than ten.
            string ret = "";

            int num1 = GetChargeValuePerDigit(inputChargeValue, 0);
            int num2 = GetChargeValuePerDigit(inputChargeValue, 1);


            if (CheckThreeValue(num2))
            {
                ret = num1 + " " + GetThreeValue(num2);
            }
            else
            {
                num1 = num1 - 1;
                num2 = num2 + 10;

                if (num1 == 0)
                {
                    if (CheckThreeValue(num2))
                    {
                        ret = num1 + " " + GetThreeValue(num2);
                    }
                    else
                    {
                        ret = TEXTCANNOTCHARGE;
                    }
                }
                else
                {
                    if (num1 == 0)
                    {
                        if (CheckThreeValue(Convert.ToInt32(inputChargeValue)))
                        {
                            ret = "0 " + GetThreeValue(Convert.ToInt32(inputChargeValue));
                        }
                        else
                        {
                            if (CheckThreeValue(num2))
                            {
                                ret = num1 + " " + GetThreeValue(num2);
                            }
                            else
                            {
                                num1 = num1 - 1;
                                num2 = num2 + 10;
                                if (CheckThreeValue(num2))
                                {
                                    ret = num1 + " " + GetThreeValue(num2);
                                }
                            }
                        }
                    }
                    else
                    {

                        if (CheckThreeValue(num2))
                        {
                            ret = num1 + " " + GetThreeValue(num2);
                        }
                        else
                        {
                            if (num1 == 0)
                            {
                                decimal cValue = Decimal.Divide(inputChargeValue, 3);
                                int isCDecimal = cValue.ToString().IndexOf(".");
                                ret = "0 " + cValue;
                            }
                            else
                            {
                                num1 = num1 - 1;
                                num2 = num2 + 10;

                                if (CheckThreeValue(num2))
                                {
                                    ret = num1 + " " + GetThreeValue(num2);
                                }
                            }
                        }
                    }
                }

            }



            return ret;
        }
        #endregion
    }
}
