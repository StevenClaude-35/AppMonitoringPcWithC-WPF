using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfAppMoniteurPC
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /*
         Aiguille :
        Rotation -90° = 0% d'utilsation 
                 180°+ = 100% 
        => 270° de mouvement / 100 = 2.7° par %
         */
        public MainWindow()
        {
            InitializeComponent();
            GetAllSystemInfos();

            //Récup des infos du disque
            GetDrivesInfos();

            //Timer pour une mise à jour en temps réel

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.75);
            timer.Tick += timer_Tick;
            timer.Start();

        }
        //Fonction timer qui refresh l'ecran 2 fois par sec

        void timer_Tick(object sender,EventArgs e)
        {
            //Mise à jour Info CPU
            cpu.Content = RefreshCpuInfos();

            //MAJ infos RAM
            RefreshRamInfos();

            //MAJ Température
            RefreshTempInfos();

            //MAJ infos network
            RefreshNetworkInfos();
        }

        public void GetDrivesInfos()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            List<Disque> disques = new List<Disque>();
            foreach(DriveInfo info in allDrives)
            {
                if (info.IsReady == true)
                {
                    Console.WriteLine("Disque " + info.Name + "prêt !");
                }
                disques.Add(new Disque(info.Name, info.DriveFormat, FormatBytes(info.TotalSize), FormatBytes(info.AvailableFreeSpace)));
            }
            listeDisques.ItemsSource = disques;
        }

        private static string FormatBytes(long bytes)
        {
            string[] Suffix = { "B", "KB", "MB", "GB", "TB" };
            int i;
            double dblSByte = bytes;
            for (i = 0; i < Suffix.Length && bytes >= 1024; i++, bytes /= 1024)
            {
                dblSByte = bytes / 1024.0;
            }

            return String.Format("{0:0.##} {1}", dblSByte, Suffix[i]);
        }

        public void RefreshNetworkInfos()
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
                return;

            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface ni in interfaces)
            {
                //Les données envoyées
                if(ni.GetIPv4Statistics().BytesSent > 0)
                netMont.Content = ni.GetIPv4Statistics().BytesSent / 1000 + " KB";
                //Les données récues
                if(ni.GetIPv4Statistics().BytesReceived >0)
                netDes.Content = ni.GetIPv4Statistics().BytesReceived / 1000 + "KB";
            }
        }


        public void RefreshTempInfos()
        {
            Double temperature = 0;
            String instanceName = "";

            ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"root\WMI", "SELECT * FROM MSAcpi_ThermalZoneTemperature");
            try
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    temperature = Convert.ToDouble(obj["CurrentTemperature"].ToString());
                    // Convertir °F en °C
                    temperature = (temperature - 2732) / 10.0;
                    instanceName = obj["InstanceName"].ToString();
                }
                temp.Content = temperature + "°C";
            }
            catch (ManagementException ex)
            {

                Console.WriteLine(ex.Message);
            }
            
        }

        public void RefreshRamInfos()
        {
            ramTotal.Content = "Total : " + FormatSize(GetTotalPhys());
            ramUsed.Content = "Utilisé : " + FormatSize(GetUsedPhys());
            ramFree.Content = "Disponible : " + FormatSize(GetAvailPhys());

            string[] maxVal = FormatSize(GetTotalPhys()).Split(' ');
            barName.Maximum = float.Parse(maxVal[0]);
            string[] memVal = FormatSize(GetUsedPhys()).Split(' ');
            barName.Value = float.Parse(memVal[0]);

        }

        public string RefreshCpuInfos()
        {
            PerformanceCounter cpuCounter = new PerformanceCounter();
            cpuCounter.CategoryName = "Processor";
            cpuCounter.CounterName = "% Processor Time";
            cpuCounter.InstanceName = "_Total";

            dynamic firstVal = cpuCounter.NextValue();
            System.Threading.Thread.Sleep(75);
            dynamic val = cpuCounter.NextValue();

            //tourner l'image de l'aiguille

            RotateTransform rotateTransform = new RotateTransform((val * 2.7f) -90);
            imgAiguille.RenderTransform = rotateTransform;

            decimal roundVal = Convert.ToDecimal(val);
            roundVal = Math.Round(roundVal, 2);

            return roundVal + "%";
        }

        //Exemple de fonction pour la RAM

        public Object getRamcounter()
        {
            PerformanceCounter ramcounter = new PerformanceCounter();
            ramcounter.CategoryName = "Memory";
            ramcounter.CounterName = "Available MBytes";

            dynamic firstValue = ramcounter.NextValue();
            System.Threading.Thread.Sleep(100);
            dynamic val = ramcounter.NextValue();

            decimal roundVar = Convert.ToDecimal(val);
            roundVar = Math.Round(roundVar, 2);

            return roundVar ;


           
        }
        /* Travailler avec la mémoire*/
        #region Fonctions spécifiques à la RAM
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GlobalMemoryStatusEx(ref MEMORY_INFO mi);

        // Structure de l'info de la mémoire
        [StructLayout(LayoutKind.Sequential)]
        public struct MEMORY_INFO
        {
            public uint dwLength; // Taille structure
            public uint dwMemoryLoad; // Utilisation mémoire
            public ulong ullTotalPhys; // Mémoire physique totale
            public ulong ullAvailPhys; // Mémoire physique dispo
            public ulong ullTotalPageFile;
            public ulong ullAvailPageFile;
            public ulong ullTotalVirtual; // Taille mémoire virtuelle
            public ulong ullAvailVirtual; // Mémoire virtuelle dispo
            public ulong ullAvailExtendedVirtual;
        }
        static string FormatSize(double size)
        {
            double d = (double)size;
            int i = 0;
            while ((d > 1024) && (i < 5))
            {
                d /= 1024;
                i++;
            }
            string[] unit = { "B", "KB", "MB", "GB", "TB" };
            return (string.Format("{0} {1}", Math.Round(d, 2), unit[i]));
        }

        public static MEMORY_INFO GetMemoryStatus()
        {
            MEMORY_INFO mi = new MEMORY_INFO();
            mi.dwLength = (uint)Marshal.SizeOf(mi);
            GlobalMemoryStatusEx(ref mi);
            return mi;
        }

        // Récupération mémoire physique totale dispo
        public static ulong GetAvailPhys()
        {
            MEMORY_INFO mi = GetMemoryStatus();
            return mi.ullAvailPhys;
        }
        // Récupération mémoire utilisée
        public static ulong GetUsedPhys()
        {
            MEMORY_INFO mi = GetMemoryStatus();
            return (mi.ullTotalPhys - mi.ullAvailPhys);
        }

        //Récupération de la mémoire physique totale
        public static ulong GetTotalPhys()
        {
            MEMORY_INFO mi = GetMemoryStatus();
            return mi.ullTotalPhys;
        }



        #endregion

        public void GetAllSystemInfos()
        {
            //Récpération des Infos du PC
            systemInfo si = new systemInfo();
            osName.Content = si.GetOsInfos("os");
            archiName.Content = si.GetOsInfos("architecture");
            procName.Content = si.GetCpuInfos();
            gpuName.Content = si.GetGpuinfos();
        }

        private void infoMsg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("https://formation-facile.fr");
        }
    }

    public class systemInfo
    {
        //Récupération Infos OS
        public string GetOsInfos(string param)
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from win32_OperatingSystem");
            foreach (ManagementObject mo in mos.Get())
            {
                switch (param)
                {
                    case "os":
                        return mo["Caption"].ToString();
                    case "architecture":
                        return mo["OSArchitecture"].ToString();
                    case "OSVersion":
                        return mo["CSDVersion"].ToString();
                }
            }
            return "";

        }


        // Récupération Infos CPU

        public string GetCpuInfos()
        {
            RegistryKey processor_name = Registry.LocalMachine.OpenSubKey(@"Hardware\Description\System\CentralProcessor\0", RegistryKeyPermissionCheck.ReadSubTree);
            if (processor_name != null)
            {
                return processor_name.GetValue("ProcessorNameString").ToString();
            }
            return "";
        }

        // Récuperation Infos GPU
        public string GetGpuinfos()
        {
            using (var searcher = new ManagementObjectSearcher("select * from win32_videoController"))
                foreach (ManagementObject obj in searcher.Get())
                {
                    Console.WriteLine("Name - " + obj["Name"]);
                    Console.WriteLine("DeviceID - " + obj["DeviceID"]);
                    Console.WriteLine("AdaterRAM - " + obj["AdapterRAM"]);
                    Console.WriteLine("AdapterDACType - " + obj["AdapterDACType"]);
                    Console.WriteLine("Monochrome - " + obj["monochrome"]);
                    Console.WriteLine("InstalledDisplayDrivers - " + obj["InstalledDisplayDrivers"]);
                    Console.WriteLine("DriverVersion - " + obj["DriverVersion"]);
                    Console.WriteLine("VideoArchitecture - " + obj["VideoArchitecture"]);
                    Console.WriteLine("VideoMemoryType - " + obj["VideoMemoryType"]);


                    return obj["Name"].ToString() + " (Version Driver : " + obj["DriverVersion"].ToString() + ")";

                }
            return "";
        }
    }

    public class Disque
    {
        private string name;
        private string format;
        private string totaleSpace;
        private string freeSpace;

        public Disque(string n,string f,string t,string l)
        {
            name = n;
            format = f;
            totaleSpace = t;
            freeSpace = l;
        }

        public override string ToString()
        {
            return name + " (" + format + ") " + freeSpace + " libres / " + totaleSpace;
        }
    }
}
