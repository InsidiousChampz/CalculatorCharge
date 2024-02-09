using System;
using System.Threading.Tasks;
using static Calculator_Charge.Calculator;

namespace Calculator_Charge
{
    class Program
    {
        #region "Variable"
        private const string TEXTHEADER = "Calculator Charge\r";
        private const string TEXTHEADERDIVIDER = "------------------------\n";
        private const string TEXTDETAIL = "Type a Charge Value, and then press Enter";
        private const string TEXTCANNOTCHARGE = "Cannot Change";
        private const string TEXTRESULT = "Charge is : ";
        private static string inputChargeValue = "";
        private static string retValue = "";
        #endregion

        #region "Method"
        static void Main(string[] args)
        {
            try
            {
               
                bool endApp = false;
                // Display title.
                Console.WriteLine(TEXTHEADER);
                Console.WriteLine(TEXTHEADERDIVIDER);

                // Ask the charge value.
                Console.WriteLine(TEXTDETAIL);

                while (!endApp)
                {

                    inputChargeValue = Console.ReadLine();

                    if (inputChargeValue == "n")
                    {
                        // exit app.
                        endApp = true;
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(inputChargeValue))
                        {
							// calculation.
							retValue = CalculationCharge(Convert.ToInt32(inputChargeValue)).Result;
							// show result.
							Console.WriteLine(retValue);
						}
                       
                    }
                }

                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(0);
                return;
            }

        }
        private static async Task<string> CalculationCharge(decimal inputChargeValue)
        {
            string ret;

            if (inputChargeValue == 0)
            {
                ret = TEXTCANNOTCHARGE;
            }
            else
            {
                ret = await CalculationChargeValue(inputChargeValue);
            }

            return TEXTRESULT + ret;

        }

        #endregion
    }
}
