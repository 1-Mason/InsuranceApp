using System.Collections.Concurrent;
using System.Xml;
using System.Globalization;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Transactions;
using System.Xml.Linq;


namespace InsuranceApp
{

    // ADD BOUNDRY AND INVALID CASE

    internal class Program
    {

        // Global Variables

        static List<string> DEVICENAME = new List<string>() { "EX2", "ASUS", "XP", "S23", "OTHER" };
        static List<int> deviceAmount = new List<int>() { 0, 0, 0 };
        static float sumDeviceInput = 0;
        static float mostExpensiveDevice = 0;
        static string mostExpensiveDeviceName = "";
        static int numberOfLaptops = 0;
        static int numberOfDesktops = 0;
        static int numberOfOtherDevices = 0;


        // Constant Variable
        static readonly float DISCOUNT = 0.10f;

        // Summative values



        // Methods or Functions


        // Checks if the user wants to continue inputting devices or stop input

        static string CheckProceed()
        {
            string proceed;

            while (true)
            {
                Console.WriteLine("Press <Enter> to add another device or type 'Stop' to quit.");
                proceed = Console.ReadLine().ToUpper();

                if (proceed.Equals("") || proceed.Equals("STOP"))
                {
                    return proceed;
                }

                Console.WriteLine("ERROR: Invalid input entered");
            }
        }

        // Calculate total devices inputted 
        static int SumDeviceInput(List<int> deviceAmount)
        {
            int sumDeviceInput = 0;

            foreach (int device in deviceAmount)
            {
                sumDeviceInput += device;
            }

            return sumDeviceInput;
        }

        // Checks input from user if the name contains special characters 
        static string CheckDeviceName()
        {
            //local declarations
            string deviceName;
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            while (true)
            {
                Console.WriteLine("Enter the device name:");
                deviceName = textInfo.ToUpper(Console.ReadLine());

                if (!deviceName.Equals("") && !deviceName.Any(char.IsSymbol))
                {
                    return deviceName;
                }

                Console.WriteLine("Error: Invalid device name entered");
            }


        }

        // Checks input from user if the category contains special characters
        static string CheckDeviceCategory()
        {
            //local declarations
            string userInput;
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            while (true)
            {
                Console.Write("Type of Device: (Laptop, Desktop, Other)\n");
                userInput = textInfo.ToUpper(Console.ReadLine());

                if (!userInput.Equals("") && !userInput.Any(char.IsSymbol))
                {
                    return userInput;
                }

                Console.WriteLine("Error: Invalid device category entered");
            }

            // Changes the decimal point after the finacial calculations to 2 decimal points
            static string FormatToDollar(float value)
            {
                return string.Format(("{0:0.00}"), value);

            }






            static void OneDevice()
            {
                // local declerations 
                string deviceName;
                int numberOfDevices;
                float totalDeviceCost;




                // Input device name
                deviceName = CheckDeviceName();


                // Input the number of devices in each name
                Console.WriteLine("Enter the number of devices:");
                numberOfDevices = Convert.ToInt32(Console.ReadLine());

                // Input Device Cost
                Console.WriteLine("Enter cost for each device:");
                float deviceCost = Convert.ToInt32(Console.ReadLine());

                // device value loss equation

                double percentage = 5.0;
                double newDeviceValue = deviceCost;






                // Device Category counter

                string category = CheckDeviceCategory().Trim().ToUpper();

                if (category == "LAPTOP")
                {
                    numberOfLaptops += numberOfDevices;
                }
                else if (category == "DESKTOP")
                {
                    numberOfDesktops += numberOfDevices;
                }
                else
                {
                    numberOfOtherDevices += numberOfDevices;
                }


                //Display cost of  the devices
                totalDeviceCost = numberOfDevices * deviceCost;
                sumDeviceInput += totalDeviceCost;

                Console.WriteLine("\n----- Insurance Summary -----");
                Console.WriteLine($"\nTotal cost for {numberOfDevices} x {deviceName} = {totalDeviceCost}.");
                Console.WriteLine("Month:   Value Loss:");

                //loop 6 times
                double subtractionAmount;
                for (int monthCounter = 1; monthCounter <= 6; monthCounter++)
                {
                    subtractionAmount = (percentage / 100) * newDeviceValue;
                    newDeviceValue = newDeviceValue - subtractionAmount;

                    Console.WriteLine($"{monthCounter}\t\t{Math.Round(newDeviceValue, 2)}");

                }
                Console.WriteLine($"Category: {CheckDeviceCategory}\n");

                // Display most expensive device name and cost

                if (deviceCost > mostExpensiveDevice)
                {
                    mostExpensiveDevice = deviceCost;
                    mostExpensiveDeviceName = deviceName;
                }

            }



            static int NumberOfDevices(List<int> deviceAmount)
            {
                int numberOfDevices = 0;

                foreach (int device in deviceAmount)
                {
                    numberOfDevices += device;
                }

                return numberOfDevices;
            }

            // When run...
            static void Main(string[] args)
            {


                string proceed = "";
                while (proceed.Equals(""))
                {
                    OneDevice();

                    proceed = CheckProceed();

                }

                Console.WriteLine("\n----- Summative Report -----");
                Console.WriteLine($"\nNumber of Laptops: {numberOfLaptops}");
                Console.WriteLine($"Number of Desktops: {numberOfDesktops}");
                Console.WriteLine($"Number of Other Devices: {numberOfOtherDevices}");

                Console.WriteLine($"\nTotal amount value of insurance: ${FormatToDollar(sumDeviceInput)}");

                Console.WriteLine($"\nMost expensive device is - {mostExpensiveDeviceName} @ ${FormatToDollar(mostExpensiveDevice)}");

            }
        }
    }

}