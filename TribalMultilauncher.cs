//Copyright 2025 Gilgamech Technologies
//Author: Stephen Gillie
//Created 11/28/2023
//Updated 11/28/2023
// GT App Framework 1.1
//Update Notes: 






//////////////////////////////////////////====================////////////////////////////////////////
//////////////////////====================--------------------====================////////////////////
//====================--------------------      Init vars     --------------------====================//
//////////////////////====================--------------------====================////////////////////
//////////////////////////////////////////====================////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace TribalMultiLauncherNamespace {
    public class TribalMultiLauncher : Form {
//{ Ints
        public int build = 186;//Get-RebuildCsharpApp TribalMultiLauncher
		public string appName = "TribalMultiLauncher";
		public string StoreName = "Not Loaded";
		public string StoreCoords = "Not Loaded";
		public string webHook = "Not Loaded";
		public string appTitle = "TribalMultiLauncher - Build ";
		public static string downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

		public string T1DownloadString = "https://tr1bes.us/configs/downloads.php?id=23";
		public string T1InstallString = downloadsPath + "\\retro_141_installerv2.exe";
		public string T1LaunchString = "C:\\retro_141_tribes\\Tribes.exe";
		public string T1DiscordString = "https://discord.gg/bf4RHMq";
		public string T1WebsiteString = "https://tr1bes.us/";

		public string T2DownloadString = "https://playt2.com/config";
		public string T2InstallString = downloadsPath + "\\Tribes2Config_2.3_Preview.exe";
		public string T2LaunchString = "C:\\Dynamix\\Tribes2\\GameData\\Tribes2.exe";
		public string T2DiscordString = "https://discord.gg/EMMVhQJgf9";
		public string T2WebsiteString = "https://playt2.com";

		public string TADownloadString = "https://store.steampowered.com/app/17080/Tribes_Ascend/";
		public string TAInstallString = "https://store.steampowered.com/app/17080/Tribes_Ascend/";
		public string TALaunchString = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Tribes\\Binaries\\Win32\\TribesAscend.exe";
		public string TADiscordString = "https://discord.gg/dd8JgzJ";
		public string TAWebsiteString = "https://store.steampowered.com/app/17080/Tribes_Ascend/";

		public string TVDownloadString = "https://downloads.tribesrevengeance.net/TribesVengeance.zip";
		public string TVInstallString = downloadsPath + "\\TribesVengeance.zip";
		public string TVLaunchString = "C:\\Dynamix\\Tribes2\\GameDaTV\\Tribes2.exe";
		public string TVDiscordString = "https://discord.gg/uXHjZ95";
		public string TVWebsiteString = "https://tribesrevengeance.net/";

		public string MA2DownloadString = "https://store.steampowered.com/app/1231210/Midair_2/";
		public string MA2InstallString = "https://store.steampowered.com/app/1231210/Midair_2/";
		public string MA2LaunchString = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Midair 2\\Midair2\\Binaries\\Win64\\Midair2-Win64-Shipping.exe";
		public string MA2DiscordString = "https://discord.com/invite/GRErnjrGh3";
		public string MA2WebsiteString = "https://midair2.gg/";



        public Button T1DownloadButton, T1InstallButton, T1LaunchButton, T1DiscordButton, T1WebsiteButton;
        public Button T2DownloadButton, T2InstallButton, T2LaunchButton, T2DiscordButton, T2WebsiteButton;
        public Button TADownloadButton, TAInstallButton, TALaunchButton, TADiscordButton, TAWebsiteButton;
        public Button TVDownloadButton, TVInstallButton, TVLaunchButton, TVDiscordButton, TVWebsiteButton;
        public Button T3DownloadButton, T3InstallButton, T3LaunchButton, T3DiscordButton, T3WebsiteButton;
        public Button MA2DownloadButton, MA2InstallButton, MA2LaunchButton, MA2DiscordButton, MA2WebsiteButton;
        public Button BSDownloadButton, BSInstallButton, BSLaunchButton, BSDiscordButton, BSWebsiteButton;

		public TextBox shopRevenueBox = new TextBox();
		JavaScriptSerializer serializer = new JavaScriptSerializer();

		public Label T1Label  = new Label();
		public Label T2Label  = new Label();
		public Label TALabel  = new Label();
		public Label TVLabel  = new Label();
		public Label T3Label  = new Label();
		public Label MA2Label  = new Label();
		public Label BSLabel  = new Label();

		public RichTextBox outBox = new RichTextBox();
		public System.Drawing.Bitmap myBitmap;
		public System.Drawing.Graphics pageGraphics;
		public ContextMenuStrip contextMenu1;
		
		public string[] parsedHtml = new string[1];
		public bool WebhookPresent = false;

		public Point newMouse = Cursor.Position;
		public Point oldMouse = new Point(0,0);

		public int checkSeconds = 10;
		public int sleepSec = 5;
		public bool shouldRun = true;
		private NotifyIcon trayIcon;

		
		// public static string WindowsUsername = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
		// public static string MainFolder = "C:\\Users\\"+WindowsUsername+"\\AppData\\Roaming\\.minecraft\\";
		// public static string logFolder = MainFolder+"\\logs"; //Logs folder;
		public static string logFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.minecraft\\logs";
		//public string logFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Packages\\Microsoft.MinecraftUWP_8wekyb3d8bbwe\\LocalState\\logs"
		public string LatestLog = logFolder+"\\latest.log";
		
		//ui
		public Panel pagePanel;
		public int displayLine = 0;
		public int sideBufferWidth = 0;
		//outBox.Font = new Font("Calibri", 14);

public enum GameStates
{
    None,
    Installed,
    Downloaded
}

		public GameStates T1State = GameStates.None;
		public GameStates T2State = GameStates.None;
		public GameStates TAState = GameStates.None;
		public GameStates TVState = GameStates.None;
		public GameStates MA2State = GameStates.None;
		
		//Grid
		public static int gridItemWidth = 60;
		public static int gridItemHeight = 30;
		
		public static int row0 = gridItemHeight*0;
		public static int row1 = gridItemHeight*1;
		public static int row2 = gridItemHeight*2;
		public static int row3 = gridItemHeight*3;
		public static int row4 = gridItemHeight*4;
 		public static int row5 = gridItemHeight*5;
 		public static int row6 = gridItemHeight*6;
 		public static int row7 = gridItemHeight*7;
 		public static int row8 = gridItemHeight*8;
 		public static int row9 = gridItemHeight*9;
 		public static int row10 = gridItemHeight*10;
 			
 		public static int col0 = gridItemWidth*0;
 		public static int col1 = gridItemWidth*1;
 		public static int col2 = gridItemWidth*2;
 		public static int col3 = gridItemWidth*3;
 		public static int col4 = gridItemWidth*4;
 		public static int col5 = gridItemWidth*5;
 		public static int col6 = gridItemWidth*6;
 		public static int col7 = gridItemWidth*7;
 		public static int col8 = gridItemWidth*8;
 		public static int col9 = gridItemWidth*9;
 		public static int col10 = gridItemWidth*10;



		public int WindowWidth = col8+20;
		public int WindowHeight = row7+5;
		
		public bool debuggingView = false;




//////////////////////////////////////////====================////////////////////////////////////////
//////////////////////====================--------------------====================////////////////////
//====================--------------------    Boilerplate     --------------------====================//
//////////////////////====================--------------------====================////////////////////
//////////////////////////////////////////====================////////////////////////////////////////
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new TribalMultiLauncher());
        }// end Main

        public TribalMultiLauncher() {
			LoadSetting("StoreName", ref StoreName, "Moar"); 
			LoadSetting("StoreCoords", ref StoreCoords, "StoreCoords"); 
			LoadSetting("webHook", ref webHook, "webHook"); 
			this.Text = appTitle + build;
			this.Size = new Size(WindowWidth,WindowHeight);
			this.Resize += new System.EventHandler(this.OnResize);
			this.AutoScroll = true;
			buildMenuBar();

			drawLabel(ref T1Label, col0, row0, col2, row1, "Tribes (1) 1.41 Loader:");
 			drawButton(ref T1DownloadButton, col2, row0, col2, row1, "Download", T1DownloadButton_Click);
 			drawButton(ref T1InstallButton, col2, row0, col2, row1, "Install", T1InstallButton_Click);
 			drawButton(ref T1LaunchButton, col2, row0, col2, row1, "Launch", T1LaunchButton_Click);
 			drawButton(ref T1DiscordButton, col4, row0, col2, row1, "Discord", T1DiscordButton_Click);
 			drawButton(ref T1WebsiteButton, col6, row0, col2, row1, "Website", T1WebsiteButton_Click);

			drawLabel(ref T2Label, col0, row1, col2, row1, "Tribes 2 v2.3 Preview:");
 			drawButton(ref T2DownloadButton, col2, row1, col2, row1, "Download", T2DownloadButton_Click);
 			drawButton(ref T2InstallButton, col2, row1, col2, row1, "Install", T2InstallButton_Click);
 			drawButton(ref T2LaunchButton, col2, row1, col2, row1, "Launch", T2LaunchButton_Click);
 			drawButton(ref T2DiscordButton, col4, row1, col2, row1, "Discord", T2DiscordButton_Click);
 			drawButton(ref T2WebsiteButton, col6, row1, col2, row1, "Website", T2WebsiteButton_Click);

			drawLabel(ref TALabel, col0, row2, col2, row1, "Tribes Ascend (Steam):");
 			drawButton(ref TADownloadButton, col2, row2, col2, row1, "Download", TADownloadButton_Click);
 			drawButton(ref TAInstallButton, col2, row2, col2, row1, "Install", TAInstallButton_Click);
 			drawButton(ref TALaunchButton, col2, row2, col2, row1, "Launch", TALaunchButton_Click);
 			drawButton(ref TADiscordButton, col4, row2, col2, row1, "Discord", TADiscordButton_Click);
 			drawButton(ref TAWebsiteButton, col6, row2, col2, row1, "Website", TAWebsiteButton_Click);

			drawLabel(ref TVLabel, col0, row3, col2, row1, "Tribes ReVengenace:");
 			drawButton(ref TVDownloadButton, col2, row3, col2, row1, "Download", TVDownloadButton_Click);
 			drawButton(ref TVInstallButton, col2, row3, col2, row1, "Install", TVInstallButton_Click);
 			drawButton(ref TVLaunchButton, col2, row3, col2, row1, "Launch", TVLaunchButton_Click);
 			drawButton(ref TVDiscordButton, col4, row3, col2, row1, "Discord", TVDiscordButton_Click);
 			drawButton(ref TVWebsiteButton, col6, row3, col2, row1, "Website", TVWebsiteButton_Click);

			drawLabel(ref MA2Label, col0, row4, col2, row1, "MidAir 2:");
 			drawButton(ref MA2DownloadButton, col2, row4, col2, row1, "Download", MA2DownloadButton_Click);
 			drawButton(ref MA2InstallButton, col2, row4, col2, row1, "Install", MA2InstallButton_Click);
 			drawButton(ref MA2LaunchButton, col2, row4, col2, row1, "Launch", MA2LaunchButton_Click);
 			drawButton(ref MA2DiscordButton, col4, row4, col2, row1, "Discord", MA2DiscordButton_Click);
 			drawButton(ref MA2WebsiteButton, col6, row4, col2, row1, "Website", MA2WebsiteButton_Click);

RefreshStatus();

			

        } // end TribalMultiLauncher

		public void buildMenuBar (){
			this.Menu = new MainMenu();
			
			MenuItem item = new MenuItem("File");
			this.Menu.MenuItems.Add(item);
				item.MenuItems.Add("Open log folder", new EventHandler(Open_Log_Folder)); 
			
			// item = new MenuItem("Edit");
			// this.Menu.MenuItems.Add(item);
				// item.MenuItems.Add("Shop name", new EventHandler(Edit_Store_Name));
				// item.MenuItems.Add("Shop coordinates", new EventHandler(Edit_Store_Coords)); 
				// item.MenuItems.Add("Webhook", new EventHandler(Edit_Webhook)); 
			
			item = new MenuItem("Help");
			this.Menu.MenuItems.Add(item);
				item.MenuItems.Add("About", new EventHandler(About_Click));
	   }// end buildMenuBar
		
 		public void RefreshStatus() {
			
			if (File.Exists(T1LaunchString)) {
				T1State = GameStates.Installed;
			} else if (File.Exists(T1InstallString)) {
				T1State = GameStates.Downloaded;
			}

			if (File.Exists(T2LaunchString)) {
				T2State = GameStates.Installed;
			} else if (File.Exists(T2InstallString)) {
				T2State = GameStates.Downloaded;
			}

			if (File.Exists(TALaunchString)) {
				TAState = GameStates.Installed;
			} else if (File.Exists(TAInstallString)) {
				TAState = GameStates.Downloaded;
			}

			if (File.Exists(TVLaunchString)) {
				TVState = GameStates.Installed;
			} else if (File.Exists(TVInstallString)) {
				TVState = GameStates.Downloaded;
			}

			if (File.Exists(MA2LaunchString)) {
				MA2State = GameStates.Installed;
			} else if (File.Exists(MA2InstallString)) {
				MA2State = GameStates.Downloaded;
			}

			T1Label.Text = T1Label.Text + "\n" + T1State.ToString();
			T2Label.Text = T2Label.Text + "\n" + T2State.ToString();
			TALabel.Text = TALabel.Text + "\n" + TAState.ToString();
			TVLabel.Text = TVLabel.Text + "\n" + TVState.ToString();
			MA2Label.Text = MA2Label.Text + "\n" + MA2State.ToString();
			
			if (T1State == GameStates.None) {
				T1DownloadButton.Visible = true;
				T1InstallButton.Visible = false;
				T1LaunchButton.Visible = false;
			} else if (T1State == GameStates.Downloaded) {
				T1DownloadButton.Visible = false;
				T1InstallButton.Visible = true;
				T1LaunchButton.Visible = false;
			} else if (T1State == GameStates.Installed) {
				T1DownloadButton.Visible = false;
				T1InstallButton.Visible = false;
				T1LaunchButton.Visible = true;
			} 

			if (T2State == GameStates.None) {
				T2DownloadButton.Visible = true;
				T2InstallButton.Visible = false;
				T2LaunchButton.Visible = false;
			} else if (T2State == GameStates.Downloaded) {
				T2DownloadButton.Visible = false;
				T2InstallButton.Visible = true;
				T2LaunchButton.Visible = false;
			} else if (T2State == GameStates.Installed) {
				T2DownloadButton.Visible = false;
				T2InstallButton.Visible = false;
				T2LaunchButton.Visible = true;
			} 

			if (TAState == GameStates.None) {
				TADownloadButton.Visible = true;
				TAInstallButton.Visible = false;
				TALaunchButton.Visible = false;
			} else if (TAState == GameStates.Downloaded) {
				TADownloadButton.Visible = false;
				TAInstallButton.Visible = true;
				TALaunchButton.Visible = false;
			} else if (TAState == GameStates.Installed) {
				TADownloadButton.Visible = false;
				TAInstallButton.Visible = false;
				TALaunchButton.Visible = true;
			} 

			if (TVState == GameStates.None) {
				TVDownloadButton.Visible = true;
				TVInstallButton.Visible = false;
				TVLaunchButton.Visible = false;
			} else if (TVState == GameStates.Downloaded) {
				TVDownloadButton.Visible = false;
				TVInstallButton.Visible = true;
				TVLaunchButton.Visible = false;
			} else if (TVState == GameStates.Installed) {
				TVDownloadButton.Visible = false;
				TVInstallButton.Visible = false;
				TVLaunchButton.Visible = true;
			} 

			if (MA2State == GameStates.None) {
				MA2DownloadButton.Visible = true;
				MA2InstallButton.Visible = false;
				MA2LaunchButton.Visible = false;
			} else if (MA2State == GameStates.Downloaded) {
				MA2DownloadButton.Visible = false;
				MA2InstallButton.Visible = true;
				MA2LaunchButton.Visible = false;
			} else if (MA2State == GameStates.Installed) {
				MA2DownloadButton.Visible = false;
				MA2InstallButton.Visible = false;
				MA2LaunchButton.Visible = true;
			} 
		} 



//////////////////////////////////////////====================////////////////////////////////////////
//////////////////////====================--------------------====================////////////////////
//===================--------------------    Event Handlers   --------------------====================
//////////////////////====================--------------------====================////////////////////
//////////////////////////////////////////====================////////////////////////////////////////

		
		//T1
        public void T1DownloadButton_Click(object sender, EventArgs e) {
			System.Diagnostics.Process.Start(T1DownloadString);
        }// end T1LaunchButton_Click

        public void T1InstallButton_Click(object sender, EventArgs e) {
			System.Diagnostics.Process.Start(T1InstallString);
        }// end T1InstallButton_Click

        public void T1LaunchButton_Click(object sender, EventArgs e) {
			System.Diagnostics.Process.Start(T1LaunchString);
        }// end T1LaunchButton_Click

        public void T1DiscordButton_Click(object sender, EventArgs e) {
			System.Diagnostics.Process.Start(T1DiscordString);
        }// end T1LaunchButton_Click

        public void T1WebsiteButton_Click(object sender, EventArgs e) {
			System.Diagnostics.Process.Start(T1WebsiteString);
        }// end T1LaunchButton_Click

		//T2
        public void T2DownloadButton_Click(object sender, EventArgs e) {
			System.Diagnostics.Process.Start(T2DownloadString);
        }// end T2LaunchButton_Click

        public void T2InstallButton_Click(object sender, EventArgs e) {
			System.Diagnostics.Process.Start(T2InstallString);
        }// end T2InstallButton_Click

        public void T2LaunchButton_Click(object sender, EventArgs e) {
			System.Diagnostics.Process.Start(T2LaunchString);
        }// end T2LaunchButton_Click

        public void T2DiscordButton_Click(object sender, EventArgs e) {
			System.Diagnostics.Process.Start(T2DiscordString);
        }// end T2LaunchButton_Click

        public void T2WebsiteButton_Click(object sender, EventArgs e) {
			System.Diagnostics.Process.Start(T2WebsiteString);
        }// end T2LaunchButton_Click

		//TA
        public void TADownloadButton_Click(object sender, EventArgs e) {
			System.Diagnostics.Process.Start(TADownloadString);
        }// end TALaunchButton_Click

        public void TAInstallButton_Click(object sender, EventArgs e) {
			System.Diagnostics.Process.Start(TAInstallString);
        }// end TAInstallButton_Click

        public void TALaunchButton_Click(object sender, EventArgs e) {
			System.Diagnostics.Process.Start(TALaunchString);
        }// end TALaunchButton_Click

        public void TADiscordButton_Click(object sender, EventArgs e) {
			System.Diagnostics.Process.Start(TADiscordString);
        }// end TALaunchButton_Click

        public void TAWebsiteButton_Click(object sender, EventArgs e) {
			System.Diagnostics.Process.Start(TAWebsiteString);
        }// end TALaunchButton_Click

		//TV
        public void TVDownloadButton_Click(object sender, EventArgs e) {
			System.Diagnostics.Process.Start(TVDownloadString);
        }// end TVLaunchButton_Click

        public void TVInstallButton_Click(object sender, EventArgs e) {
			System.Diagnostics.Process.Start(TVInstallString);
        }// end TVInstallButton_Click

        public void TVLaunchButton_Click(object sender, EventArgs e) {
			System.Diagnostics.Process.Start(TVLaunchString);
        }// end TVLaunchButton_Click

        public void TVDiscordButton_Click(object sender, EventArgs e) {
			System.Diagnostics.Process.Start(TVDiscordString);
        }// end TVLaunchButton_Click

        public void TVWebsiteButton_Click(object sender, EventArgs e) {
			System.Diagnostics.Process.Start(TVWebsiteString);
        }// end TVLaunchButton_Click

		//MA2
        public void MA2DownloadButton_Click(object sender, EventArgs e) {
			System.Diagnostics.Process.Start(MA2DownloadString);
        }// end MA2LaunchButton_Click

        public void MA2InstallButton_Click(object sender, EventArgs e) {
			System.Diagnostics.Process.Start(MA2InstallString);
        }// end MA2InstallButton_Click

        public void MA2LaunchButton_Click(object sender, EventArgs e) {
			System.Diagnostics.Process.Start(MA2LaunchString);
        }// end MA2LaunchButton_Click

        public void MA2DiscordButton_Click(object sender, EventArgs e) {
			System.Diagnostics.Process.Start(MA2DiscordString);
        }// end MA2LaunchButton_Click

        public void MA2WebsiteButton_Click(object sender, EventArgs e) {
			System.Diagnostics.Process.Start(MA2WebsiteString);
        }// end MA2LaunchButton_Click

		//T3
        public void T3DownloadButton_Click(object sender, EventArgs e) {
			System.Diagnostics.Process.Start("PathOrUrl");
        }// end T3LaunchButton_Click

        public void T3InstallButton_Click(object sender, EventArgs e) {
			System.Diagnostics.Process.Start("PathOrUrl");
        }// end T3InstallButton_Click

        public void T3LaunchButton_Click(object sender, EventArgs e) {
			System.Diagnostics.Process.Start("PathOrUrl");
        }// end T3LaunchButton_Click

        public void T3DiscordButton_Click(object sender, EventArgs e) {
			System.Diagnostics.Process.Start("PathOrUrl");
        }// end T3LaunchButton_Click

        public void T3WebsiteButton_Click(object sender, EventArgs e) {
			System.Diagnostics.Process.Start("PathOrUrl");
        }// end T3LaunchButton_Click

		//BS
        public void BSDownloadButton_Click(object sender, EventArgs e) {
			System.Diagnostics.Process.Start("PathOrUrl");
        }// end BSLaunchButton_Click

        public void BSInstallButton_Click(object sender, EventArgs e) {
			System.Diagnostics.Process.Start("PathOrUrl");
        }// end BSInstallButton_Click

        public void BSLaunchButton_Click(object sender, EventArgs e) {
			System.Diagnostics.Process.Start("PathOrUrl");
        }// end BSLaunchButton_Click

        public void BSDiscordButton_Click(object sender, EventArgs e) {
			System.Diagnostics.Process.Start("https://discord.gg/Broadside");
        }// end BSLaunchButton_Click

        public void BSWebsiteButton_Click(object sender, EventArgs e) {
			System.Diagnostics.Process.Start("PathOrUrl");
        }// end BSLaunchButton_Click




		public void OnResize(object sender, System.EventArgs e) {
		}
		//Menu
		//File
		public void Open_Log_Folder(object sender, EventArgs e) {
			Process.Start(logFolder);
		}// end Open_Log_Folder

			
		//Help
		public void About_Click (object sender, EventArgs e) {
			string AboutText = "TribalMultiLauncher" + Environment.NewLine;
			AboutText += "Made to help Tribals connect. Not associated with" + Environment.NewLine;
			AboutText += "any of the mentioned products or their developers." + Environment.NewLine;
			AboutText += "" + Environment.NewLine;
			AboutText += "Version 1." + build + Environment.NewLine;
			AboutText += "(C) 2023-2025 Gilgamech Technologies" + Environment.NewLine;
			AboutText += "" + Environment.NewLine;
			AboutText += "Report bugs & request features:" + Environment.NewLine;
			AboutText += "https://github.com/Gilgamech/TribalMultiLauncher/issues" + Environment.NewLine;
			MessageBox.Show(AboutText);
		} // end Link_Click






//////////////////////////////////////////====================////////////////////////////////////////
//////////////////////====================--------------------====================////////////////////
//====================--------------------      Main     --------------------====================//
//////////////////////====================--------------------====================////////////////////
//////////////////////////////////////////====================////////////////////////////////////////



//////////////////////////////////////////====================////////////////////////////////////////
//////////////////////====================--------------------====================////////////////////
//===================--------------------   Utility Functions  --------------------====================
//////////////////////====================--------------------====================////////////////////
//////////////////////////////////////////====================////////////////////////////////////////
/*Powershell functional equivalency imperatives
		Get-Clipboard = Clipboard.GetText();
		Get-Date = Timestamp.Now.ToString("M/d/yyyy");
		Get-Process = public Process[] processes = Process.GetProcesses(); or var processes = Process.GetProcessesByName("Test");
		New-Item = Directory.CreateDirectory(Path) or File.Create(Path);
		Remove-Item = Directory.Delete(Path) or File.Delete(Path);
		Get-ChildItem = string[] entries = Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly);
		Start-Process = System.Diagnostics.Process.Start("PathOrUrl");
		Stop-Process = StopProcess("ProcessName");
		Start-Sleep = Thread.Sleep(GitHubRateLimitDelay);
		Get-Random - Random rnd = new Random(); or int month  = rnd.Next(1, 13);  or int card   = rnd.Next(52);
		Create-Archive = ZipFile.CreateFromDirectory(dataPath, zipPath);
		Expand-Archive = ZipFile.ExtractToDirectory(zipPath, extractPath);
		Sort-Object = .OrderBy(n=>n).ToArray(); and -Unique = .Distinct(); Or Array.Sort(strArray); or List
		
		Get-VM = GetVM("VMName");
		Start-VM = SetVMState("VMName", 2);
		Stop-VM = SetVMState("VMName", 4);
		Stop-VM -TurnOff = SetVMState("VMName", 3);
		Reboot-VM = SetVMState("VMName", 10);
		Reset-VM = SetVMState("VMName", 11);
		
		Diff
		var inShopDataButNotInOldData = ShopData.Except(OldData);
		var inOldDataButNotInShopData = OldData.Except(ShopData);
		
*/
		public string findIndexOf(string pageString,string startString,string endString,int startPlus,int endPlus){
			return pageString.Substring(pageString.IndexOf(startString)+startPlus, pageString.IndexOf(endString) - pageString.IndexOf(startString)+endPlus);
        }// end findIndexOf

		public void DeGZip (string infile) {
			string outfile = infile.Replace(".gz","");
			FileStream compressedFileStream = File.Open(infile, FileMode.Open);
			FileStream outputFileStream = File.Create(outfile);
			var decompressor = new GZipStream(compressedFileStream, CompressionMode.Decompress);
			decompressor.CopyTo(outputFileStream);
		}
		//JSON
		public dynamic FromJson(string string_input) {
			dynamic dynamic_output = new System.Dynamic.ExpandoObject();
			dynamic_output = serializer.Deserialize<dynamic>(string_input);
			return dynamic_output;
		}

		public string ToJson(dynamic dynamic_input) {
			string string_out;
			string_out = serializer.Serialize(dynamic_input);
			return string_out;
		}
		//CSV
		public Dictionary<string, dynamic>[] FromCsv(string csv_in) {
			//CSV isn't just a 2d object array - it's an array of Dictionary<string,object>, whose string keys are the column headers. 
			string[] Rows = csv_in.Replace("\r\n","\n").Replace("\"","").Split('\n');
			string[] columnHeaders = Rows[0].Split(',');
			Dictionary<string, dynamic>[] matrix = new Dictionary<string, dynamic> [Rows.Length];
			try {
				for (int row = 1; row < Rows.Length; row++){
					matrix[row] = new Dictionary<string, dynamic>();
					//Need to enumerate values to create first row.
					string[] rowData = Rows[row].Split(',');
					try {
						for (int col = 0; col < rowData.Length; col++){
							//Need to record or access first row to match with values. 
							matrix[row].Add(columnHeaders[col].ToString(), rowData[col]);
						}
					} catch {
					}
				}
			} catch {
			}
			return matrix;
		}

		public string ToCsv(Dictionary<string, dynamic>[] matrix) {
			string csv_out = "";
			//Arrays seem to have a buffer row above and below the data.
			int topRow = 1;
			Dictionary<string, dynamic> headerRow = matrix[topRow];
			//Write header row (th). Support for multi-line headers maybe someday but not today. 
			if (headerRow != null) {
				string[] columnHeaders = new string[headerRow.Keys.Count];
				headerRow.Keys.CopyTo(columnHeaders, 0);
				//var a = matrix[0].Keys;
				foreach (string columnHeader in columnHeaders){
						csv_out += columnHeader.ToString()+",";
				}
				csv_out = csv_out.TrimEnd(',');
				// Write data rows (td).
				for (int row = topRow; row < matrix.Length -1; row++){
					csv_out += "\n";
					foreach (string columnHeader in columnHeaders){
						csv_out += matrix[row][columnHeader]+",";
					}
					csv_out = csv_out.TrimEnd(',');
				}
			}
			csv_out += "\n";
			return csv_out;
		}
		//File
		//Non-locking alternative: System.IO.File.ReadAllBytes(Filename);
		public string GetContent(string Filename, bool NoErrorMessage = false, bool Debug = false) {
			string fileString = null;
			try {
			//outBox.Text = "fileString Start" +  Environment.NewLine + outBox.Text;
			
			FileStream logFileStream = new FileStream(Filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			StreamReader logFileReader = new StreamReader(logFileStream);
			while (!logFileReader.EndOfStream) {
				// outBox.Text = "(GetContent) fileString While."+ fileString.Length + Environment.NewLine + outBox.Text;
				fileString = logFileReader.ReadLine();
			if (Debug == true) {
				outBox.Text = "(GetContent) fileString.Length."+ fileString.Length + Environment.NewLine + outBox.Text;
			}
			}
			if (Debug == true) {
				outBox.Text = "(GetContent) FileStream Success."+ fileString.Length + Environment.NewLine + outBox.Text;
			}
			logFileReader.Close();
			logFileStream.Close();
			} catch (Exception e){
				if (Debug == true) {
					outBox.Text = "(GetContent) Error."+ e.Message + Environment.NewLine + outBox.Text;
				}
			}
			return fileString;
		}
		public string OldGetContent(string Filename, bool NoErrorMessage = false) {
			string string_out = "";
			try {
				// Open the text file using a stream reader.
				using (var sr = new StreamReader(Filename)) {
					// Read the stream as a string, and write the string to the console.
					string_out = sr.ReadToEnd();
				}
			} catch (Exception e){
				outBox.Text = "(OldGetContent) Error."+ e.Message + Environment.NewLine + outBox.Text;
			}
			return string_out;
		}

		public void OutFile(string path, object content, bool Append = false) {
			//From SO: Use "typeof" when you want to get the type at compilation time. Use "GetType" when you want to get the type at execution time. "is" returns true if an instance is in the inheritance tree.
			if (TestPath(path) == "None") {
				File.Create(path).Close();
			}
			if (content.GetType() == typeof(string)) {
				string out_content = (string)content;
			//From SO: File.WriteAllLines takes a sequence of strings - you've only got a single string. If you only want your file to contain that single string, just use File.WriteAllText.
				if (Append == true) {
					File.AppendAllText(path, out_content, Encoding.ASCII);//string
				} else {
					File.WriteAllText(path, out_content, Encoding.ASCII);//string
				}
			} else {
				IEnumerable<string> out_content = (IEnumerable<string>)content;
				if (Append == true) {
					File.AppendAllLines(path, out_content, Encoding.ASCII);//IEnumerable<string>'
				} else {
					File.WriteAllLines(path, out_content, Encoding.ASCII);//string[]
				}				
			}
		}

		public void RemoveItem(string Path,bool remake = false){
			if (TestPath(Path) == "File") {
				File.Delete(Path);
				if (remake) {
					File.Create(Path);
				}
			} else if (TestPath(Path) == "Directory") {
				Directory.Delete(Path, true);
				if (remake) {
					Directory.CreateDirectory(Path);
				}
			}
		}

		public string TestPath(string path) {
				string string_out = "";
				if (path != null) {
						path = path.Trim();
					if (Directory.Exists(path)) {
						string_out = "Directory";
					} else if (File.Exists(path)) {
						string_out = "File";
					} else {// neither file nor directory exists. guess intention
						string_out = "None";
					}
				} else {// neither file nor directory exists. guess intention
					string_out = "Empty";
				}
				return string_out;
			}
		//Web
		public string InvokeWebRequest(string Url, string Method = WebRequestMethods.Http.Get, string Body = "",bool Authorization = false,bool JSON = false){ 
			string response_out = "";

				// SSL stuff
				//ServicePointManager.Expect100Continue = true;
				ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
				
				if (JSON == true) {
					request.ContentType = "application/json";
				}
				if (Authorization == true) {
					//request.Headers.Add("Authorization", "Bearer "+webHook);
					//request.Headers.Add("X-GitHub-Api-build", "2022-11-28");
					request.PreAuthenticate = true;
				}

				request.Method = Method;
				request.ContentType = "application/json;charset=utf-8";
				request.Accept = "application/vnd.github+json";
				request.UserAgent = "WinGetApprovalPipeline";

			 //Check Headers
			 // for (int i=0; i < response.Headers.Count; ++i)  {
				// outBox_msg.AppendText(Environment.NewLine + "Header Name : " + response.Headers.Keys[i] + "Header value : " + response.Headers[i]);
			 // }

			try {
				if ((Body == "") || (Method ==WebRequestMethods.Http.Get)) {
				} else {
					var data = Encoding.Default.GetBytes(Body); // note: choose appropriate encoding
					request.ContentLength = data.Length;
					var newStream = request.GetRequestStream(); // get a ref to the request body so it can be modified
					newStream.Write(data, 0, data.Length);
					newStream.Close();
				} 

				} catch (Exception e) {
					outBox.Text = "(InvokeWebRequest) Request Error: " + e.Message + Environment.NewLine + outBox.Text;
				}
				
				try {
					HttpWebResponse response = (HttpWebResponse)request.GetResponse();
					StreamReader sr = new StreamReader(response.GetResponseStream());
					if (Method == "Head") {
						string response_text = response.StatusCode.ToString();
						response_out = response_text;

					} else {
						string response_text = sr.ReadToEnd();
						response_out = response_text;
					}
					sr.Close();
				} catch (Exception e) {
					outBox.Text = "(InvokeWebRequest) Response Error: " + e.Message + Environment.NewLine + outBox.Text;
				}
		return response_out;
		}// end InvokeWebRequest	
		//Discord
		public void SendMessageToWebhook (string content) {
			string payload = "{\"content\": \"" + content + "\"}";
			if (webHook.Contains("http")) {
				InvokeWebRequest(webHook, WebRequestMethods.Http.Post, payload,false,true);
			}
		}

        public string ReadSetting(string key) {
			string result = "Not Found";
            try {
                var appSettings = ConfigurationManager.AppSettings;
                result = appSettings[key] ?? "Not Found";
            } catch (ConfigurationErrorsException) {
				outBox.Text = "Error reading app settings" + Environment.NewLine + outBox.Text;
            }
			return result;
        }

        public void DeleteSetting(string key) {
            try {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null) {
                } else {
                    settings.Remove(key);
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            } catch (ConfigurationErrorsException) {
				outBox.Text = "Error reading app settings" + Environment.NewLine + outBox.Text;
            }
        }

        public void AddOrUpdateSetting(string key, string value) {
            try {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null) {
                    settings.Add(key, value);
                } else {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            } catch (ConfigurationErrorsException) {
				outBox.Text = "Error reading app settings" + Environment.NewLine + outBox.Text;
            }
        }
		
        public void LoadSetting(string key, ref string value, string defaultValue) {
			if (value == "Not Loaded") {
				value = ReadSetting(key);
				if (value == "Not Found") {
					value = defaultValue;
					AddOrUpdateSetting(key, value);
				}
			}
        }








//////////////////////////////////////////====================////////////////////////////////////////
//////////////////////====================--------------------====================////////////////////
//====================--------------------   UI templates    --------------------====================//
//////////////////////====================--------------------====================////////////////////
//////////////////////////////////////////====================////////////////////////////////////////
		public void drawButton(ref Button button, int pointX, int pointY, int sizeX, int sizeY,string buttonText, EventHandler buttonOnclick){
			button = new Button();
			button.Text = buttonText;
			button.Location = new Point(pointX, pointY);
			button.Size = new Size(sizeX, sizeY);
			//button.BackColor = color_DefaultBack;
			//button.ForeColor = color_DefaultText;
			button.Click += new EventHandler(buttonOnclick);
			// button.Font = new Font(buttonFont, buttonFontSIze);
			Controls.Add(button);
		}// end drawButton

		public void drawRichTextBox(ref RichTextBox richTextBox, int pointX,int pointY,int sizeX,int sizeY,string text, string name){
			richTextBox = new RichTextBox();
			richTextBox.Text = text;
			richTextBox.Name = name;
			richTextBox.Multiline = true;
			richTextBox.AcceptsTab = true;
			richTextBox.WordWrap = true;
			richTextBox.ReadOnly = true;
			richTextBox.DetectUrls = true;
			// richTextBox.BackColor = color_DefaultBack;
			// richTextBox.ForeColor = color_DefaultText;
			// richTextBox.Font = new Font(AppFont, AppFontSIze);
			richTextBox.Location = new Point(pointX, pointY);
			//richTextBox.LinkClicked  += new LinkClickedEventHandler(Link_Click);
			richTextBox.Width = sizeX;
			richTextBox.Height = sizeY;
			//richTextBox.Dock = DockStyle.Fill;
			richTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;


			//richTextBox.BackColor = Color.Red;
			//richTextBox.ForeColor = Color.Blue;
			//richTextBox.RichTextBoxScrollBars = ScrollBars.Both;
			//richTextBox.AcceptsReturn = true;

			Controls.Add(richTextBox);
		}// end drawRichTextBox
		
		public void drawTextBox(ref TextBox urlBox, int pointX, int pointY, int sizeX, int sizeY,string text){
			urlBox = new TextBox();
			urlBox.Text = text;
			urlBox.Name = "urlBox";
			// urlBox.Font = new Font(AppFont, urlBoxFontSIze);
			urlBox.Location = new Point(pointX, pointY);
			// urlBox.BackColor = color_InputBack;
			// urlBox.ForeColor = color_DefaultText;
			urlBox.Width = sizeX;
			urlBox.Height = sizeY;
			Controls.Add(urlBox);
		}
		
		public void drawLabel(ref Label newLabel, int pointX, int pointY, int sizeX, int sizeY,string text){
			newLabel = new Label();
			newLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			//newLabel.ImageList = imageList1;
			newLabel.ImageIndex = 1;
			newLabel.ImageAlign = ContentAlignment.TopLeft;
			// newLabel.BackColor = color_DefaultBack;
			// newLabel.ForeColor = color_DefaultText;
			newLabel.Name = "newLabel";
			// newLabel.Font = new Font(AppFont, AppFontSIze);
			newLabel.Location = new Point(pointX, pointY);
			newLabel.Width = sizeX;
			newLabel.Height = sizeY;
			//newLabel.KeyUp += newLabel_KeyUp;

			newLabel.Text = text;

			//newLabel.Size = new Size (label1.PreferredWidth, label1.PreferredHeight);
			Controls.Add(newLabel);
		}

		public void drawDataGrid(ref DataGridView dataGridView, int startX, int startY, int sizeX, int sizeY){
			dataGridView = new DataGridView();
			dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
			dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Single;
			dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			
			// dataGridView.ForeColor = color_DefaultText;//Selected cell text color
			// dataGridView.BackColor = color_DefaultBack;//Selected cell BG color
			// dataGridView.DefaultCellStyle.SelectionForeColor  = color_DefaultText;//Unselected cell text color
			// dataGridView.DefaultCellStyle.SelectionBackColor = color_DefaultBack;//Unselected cell BG color
			// dataGridView.BackgroundColor = color_DefaultBack;//Space underneath/between cells
			dataGridView.GridColor = SystemColors.ActiveBorder;//Gridline color
			
			dataGridView.Name = "dataGridView";
			// dataGridView.Font = new Font(AppFont, AppFontSize);
			dataGridView.Location = new Point(startX, startY);
			dataGridView.Size = new Size(sizeX, sizeY);
			// dataGridView.KeyUp += dataGridView_KeyUp;
			// dataGridView.Text = text;
			Controls.Add(dataGridView);


		
			dataGridView.EditMode = DataGridViewEditMode.EditProgrammatically;
			dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
			dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dataGridView.AllowUserToDeleteRows = false;
			dataGridView.RowHeadersVisible = false;
			dataGridView.MultiSelect = false;
			//dataGridView.Dock = DockStyle.Fill;

/*
			dataGridView.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridView_CellFormatting);
			dataGridView.CellParsing += new DataGridViewCellParsingEventHandler(dataGridView_CellParsing);
			addNewRowButton.Click += new EventHandler(addNewRowButton_Click);
			deleteRowButton.Click += new EventHandler(deleteRowButton_Click);
			ledgerStyleButton.Click += new EventHandler(ledgerStyleButton_Click);
			dataGridView.CellValidating += new DataGridViewCellValidatingEventHandler(dataGridView_CellValidating);
*/
		}// end drawDataGrid

		public void drawToolTip(ref ToolTip toolTip, ref Button button, string DisplayText, int AutoPopDelay = 5000, int InitialDelay = 1000, int ReshowDelay = 500){
			toolTip = new ToolTip();

			// Set up the delays for the ToolTip.
			toolTip.AutoPopDelay = AutoPopDelay;
			toolTip.InitialDelay = InitialDelay;
			toolTip.ReshowDelay = ReshowDelay;
			// Force the ToolTip text to be displayed whether or not the form is active.
			toolTip.ShowAlways = true;
			 
			// Set up the ToolTip text for the Button and Checkbox.
			toolTip.SetToolTip(button, DisplayText);
			//toolTip.SetToolTip(this.checkBox1, "My checkBox1");
		}

		public void drawStatusStrip (StatusStrip statusStrip,ToolStripStatusLabel toolStripStatusLabel) {
			statusStrip = new System.Windows.Forms.StatusStrip();
			statusStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            statusStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            
			toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			toolStripStatusLabel.Name = "toolStripStatusLabel";
            toolStripStatusLabel.Size = new System.Drawing.Size(109, 17);
            toolStripStatusLabel.Text = "toolStripStatusLabel";
			statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripStatusLabel });
            
            statusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            statusStrip.Location = new System.Drawing.Point(0, 0);
            statusStrip.Name = "statusStrip";
            statusStrip.ShowItemToolTips = true;
            statusStrip.Size = new System.Drawing.Size(292, 22);
            statusStrip.SizingGrip = false;
            statusStrip.Stretch = false;
            statusStrip.TabIndex = 0;
            statusStrip.Text = "statusStrip";
			
			Controls.Add(statusStrip);
		}
		
		public static DialogResult drawInputDialog(ref string input, string boxTitle) {
			System.Drawing.Size size = new System.Drawing.Size(200, 70);
			Form inputBox = new Form();

			inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			inputBox.ClientSize = size;
			inputBox.Text = boxTitle;

			System.Windows.Forms.TextBox textBox = new TextBox();
			textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
			textBox.Location = new System.Drawing.Point(5, 5);
			textBox.Text = input;
			inputBox.Controls.Add(textBox);

			Button okButton = new Button();
			okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			okButton.Name = "okButton";
			okButton.Size = new System.Drawing.Size(75, 23);
			okButton.Text = "&OK";
			okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 39);
			inputBox.Controls.Add(okButton);

			Button cancelButton = new Button();
			cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			cancelButton.Name = "cancelButton";
			cancelButton.Size = new System.Drawing.Size(75, 23);
			cancelButton.Text = "&Cancel";
			cancelButton.Location = new System.Drawing.Point(size.Width - 80, 39);
			inputBox.Controls.Add(cancelButton);

			inputBox.AcceptButton = okButton;
			inputBox.CancelButton = cancelButton; 

			DialogResult result = inputBox.ShowDialog();
			input = textBox.Text;
			return result;
		}
    }// end TribalMultiLauncher
	
	public class StockItem  {
	  public string ItemName;
	  public int XLoc;
	  public int YLoc;
	  public int ZLoc;
	  public int StockQty;
	  public string Event;
	  public string Timestamp;
	  public string PlayerName;
	  public decimal Earnings;
	}
}// end TribalMultiLauncherNamespace

