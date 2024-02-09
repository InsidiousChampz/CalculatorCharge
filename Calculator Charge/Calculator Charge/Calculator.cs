using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_Charge
{
    public static class Calculator
    {
        #region "Variable"
        private const string TEXTCANNOTCHARGE = "Cannot Change";
		private const string TEXTEXIT = "No need to calculate ... Please press n for exit.";
		#endregion

		#region "Method"
		// - Method
		public static async Task<string> CalculationChargeValue(decimal inputChargeValue)
        {

            string ret = string.Empty;

            // Check Digit Input
            int lengthInput = inputChargeValue.ToString().Length;

            if (lengthInput == 1)
            {
                // 1, 2, 3, ..9
                ret = await GetValueOneToNine(inputChargeValue);
            }
            else
            {
                // Calc 10 - ..N
                if (await CheckTenOnly(inputChargeValue))
                {
                    // just 10, 20, 30, ....
                    ret = await GetValueTenOnly(inputChargeValue);
                }
                else
                {
                    // > 10
                    ret = await GetValueCalculation(inputChargeValue);
                }
            }

            return ret;
        }
        #endregion

        // - Function
        #region "Function"
        private static async Task<bool> CheckTenOnly(decimal inputChargeValue)
        {
			await Task.Delay(0);
			bool ret = false;
            decimal calcFullTen = Decimal.Divide(inputChargeValue, 10);
            int isFullTen = calcFullTen.ToString().IndexOf(".");
            if (isFullTen == -1)
            {
                ret = true;
            }

            return ret;
        }
        private static async Task<string> GetValueTenOnly(decimal inputChargeValue)
        {
            await Task.Delay(0);
            decimal calcFullTen = Decimal.Divide(inputChargeValue, 10);
            return Convert.ToInt32(calcFullTen) + " 0";
        }
        private static async Task<string> GetValueOneToNine(decimal inputChargeValue)
        {
			// Calc 1 - 9
			await Task.Delay(0);
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
        private static async Task<int> GetChargeValuePerDigit(decimal inputChargeValue, int position)
        {
			await Task.Delay(0);
			return Convert.ToInt32(inputChargeValue.ToString().Substring(position, 1));
        }
        private static async Task<bool> CheckThreeValue(int num)
        {
			await Task.Delay(0);
			bool ret = false;
            decimal dNum = Decimal.Divide(num, 3);
            int isDecimal = dNum.ToString().IndexOf(".");

            if (isDecimal == -1)
            {
                ret = true;
            }

            return ret;
        }
        private static async Task<int> GetThreeValue(int num)
        {
			await Task.Delay(0);
			int ret = 0;
            decimal dNum = Decimal.Divide(num, 3);
            int isDecimal = dNum.ToString().IndexOf(".");

            if (isDecimal == -1)
            {
                ret = Convert.ToInt32(dNum);
            }

            return ret;
        }
        private static async Task<string> GetValueCalculation(decimal inputChargeValue)
        {
            // Calc more than ten.
            string ret = "";

            if (inputChargeValue.ToString().Length > 2)
            {
                ret = TEXTEXIT;
            }
            else
            {
                int num1 = await GetChargeValuePerDigit(inputChargeValue, 0);
                int num2 = await GetChargeValuePerDigit(inputChargeValue, 1);


                if (await CheckThreeValue(num2))
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
                                retValue = await GetThreeValue(num2);
                                ret = num1 + " " + retValue;
                            }

                        }
                    }
                }
            }

            

            return ret;
        }
        private static async Task<string> GetValueCalculation_Backup(decimal inputChargeValue)
        {
            // Calc more than ten.
            string ret = "";

            int num1 = await GetChargeValuePerDigit(inputChargeValue, 0);
            int num2 = await GetChargeValuePerDigit(inputChargeValue, 1);


            if (await CheckThreeValue(num2))
            {
                ret = num1 + " " + GetThreeValue(num2);
            }
            else
            {
                num1 = num1 - 1;
                num2 = num2 + 10;

                if (num1 == 0)
                {
                    if (await CheckThreeValue(num2))
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
                        if (await CheckThreeValue(Convert.ToInt32(inputChargeValue)))
                        {
                            ret = "0 " + GetThreeValue(Convert.ToInt32(inputChargeValue));
                        }
                        else
                        {
                            if (await CheckThreeValue(num2))
                            {
                                ret = num1 + " " + GetThreeValue(num2);
                            }
                            else
                            {
                                num1 = num1 - 1;
                                num2 = num2 + 10;
                                if (await CheckThreeValue(num2))
                                {
                                    ret = num1 + " " + GetThreeValue(num2);
                                }
                            }
                        }
                    }
                    else
                    {

                        if (await CheckThreeValue(num2))
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

                                if (await CheckThreeValue(num2))
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
