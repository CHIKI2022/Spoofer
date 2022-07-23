using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Principal;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.Threading;

namespace HighSpoofer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(32, 12);
            Random r = new Random();
            Console.Title = "Spoofer";
            int sleeptime = 30;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Press Any Key To Start !");
            Console.ReadKey();
            int count = 0;


            while (true)
            {
                Console.Clear();
                Console.Title = ("Spoofed " + count + " Time");
                Helpers.SpoofHwProfileGUID();
                Console.ForegroundColor = (ConsoleColor)r.Next(0, 10);
                Console.WriteLine("GUID Changed");
                ChangeGUID();
                Console.ForegroundColor = (ConsoleColor)r.Next(0, 10);
                Console.WriteLine("HWID Changed");
                ChangeHWID();
                Console.ForegroundColor = (ConsoleColor)r.Next(0, 10);
                Console.WriteLine("MacAddress Changed");
                Helpers.SpoofMacAddress();
                Console.ForegroundColor = (ConsoleColor)r.Next(0, 10);
                Console.WriteLine("ProductID Changed");
                Helpers.SpoofProductID();
                count++;
                Thread.Sleep(sleeptime);
            }




        }

        static void ChangeHWID()
        {
            string text = string.Concat(new string[]
            {
                Helpers.GenerateString(5),
                "-",
                Helpers.GenerateString(5),
                "-",
                Helpers.GenerateString(5),
                "-",
                Helpers.GenerateString(5)
            });
            try
            {
                using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\IDConfigDB\\Hardware Profiles\\0001", true))
                {
                    bool flag2 = registryKey != null;
                    if (flag2)
                    {
                        object value = registryKey.GetValue("HwProfileGuid");
                        bool flag3 = value != null;
                        if (flag3)
                        {
                            registryKey.SetValue("HwProfileGuid", text);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("FATAL ERROR");
            }
            Console.WriteLine();
        }
        static void ChangeGUID()
        {
            string value = Guid.NewGuid().ToString();
            RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            registryKey = registryKey.OpenSubKey("SOFTWARE\\Microsoft\\Cryptography", true);
            registryKey.SetValue("MachineGuid", value);
            Console.WriteLine();
        }
    }
}

