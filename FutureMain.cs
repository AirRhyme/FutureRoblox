using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EasyExploits;
using System.IO;
using Microsoft.Win32;

namespace Future
{
    public partial class FutureMain : Form
    {
        Module api = new Module();

        public FutureMain()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (Process process in Process.GetProcessesByName("RobloxPlayerBeta"))
            {
                try
                {
                    process.Kill();
                }
                catch (Exception k)
                {
                    MessageBox.Show("An Error Occured" + k.Message);
                }
            }
        }

   

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();


        private async void FutureMain_Load(object sender, EventArgs e)
        {
            WebClient wc = new WebClient();
            wc.Proxy = null;
            try
            {
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION", true);
                string friendlyName = AppDomain.CurrentDomain.FriendlyName;
                bool flag2 = registryKey.GetValue(friendlyName) == null;
                if (flag2)
                {
                    registryKey.SetValue(friendlyName, 11001, RegistryValueKind.DWord);
                }
                registryKey = null;
                friendlyName = null;
            }
            catch (Exception)
            {
            }
            webBrowser1.Url = new Uri(string.Format("file:///{0}/Monaco/Monaco.html", Directory.GetCurrentDirectory()));
            await Task.Delay(500);
            webBrowser1.Document.InvokeScript("SetTheme", new string[]
            {
                   "Dark" 
                   /*
                    There are 2 Themes Dark and Light
                   */
            });
            addBase();
            addMath();
            addGlobalNS();
            addGlobalV();
            addGlobalF();
            webBrowser1.Document.InvokeScript("SetText", new object[]
            {
                 "-- Execute Scripts Here--"
            });
            listBox1.Items.Clear();
            Functions.addToScriptList(listBox1, "./Scripts", "*.txt");
            Functions.addToScriptList(listBox1, "./Scripts", "*.lua");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FutureMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        WebClient wc = new WebClient();
        private string defPath = Application.StartupPath + "//Monaco//";

        private void addIntel(string label, string kind, string detail, string insertText)
        {
            string text = "\"" + label + "\"";
            string text2 = "\"" + kind + "\"";
            string text3 = "\"" + detail + "\"";
            string text4 = "\"" + insertText + "\"";
            webBrowser1.Document.InvokeScript("AddIntellisense", new object[]
            {
                label,
                kind,
                detail,
                insertText
            });
        }

        private void addGlobalF()
        {
            string[] array = System.IO.File.ReadAllLines(this.defPath + "//globalf.txt");
            foreach (string text in array)
            {
                bool flag = text.Contains(':');
                if (flag)
                {
                    this.addIntel(text, "Function", text, text.Substring(1));
                }
                else
                {
                    this.addIntel(text, "Function", text, text);
                }
            }
        }

        private void addGlobalV()
        {
            foreach (string text in System.IO.File.ReadLines(this.defPath + "//globalv.txt"))
            {
                this.addIntel(text, "Variable", text, text);
            }
        }

        private void addGlobalNS()
        {
            foreach (string text in System.IO.File.ReadLines(this.defPath + "//globalns.txt"))
            {
                this.addIntel(text, "Class", text, text);
            }
        }

        private void addMath()
        {
            foreach (string text in System.IO.File.ReadLines(this.defPath + "//classfunc.txt"))
            {
                this.addIntel(text, "Method", text, text);
            }
        }

        private void addBase()
        {
            foreach (string text in System.IO.File.ReadLines(this.defPath + "//base.txt"))
            {
                this.addIntel(text, "Keyword", text, text);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HtmlDocument document = webBrowser1.Document;
            string scriptName = "GetText";
            object[] args = new string[0];
            object obj = document.InvokeScript(scriptName, args);
            string script = obj.ToString();
            api.ExecuteScript(script);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            api.LaunchExploit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (openfiledialog.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    string MainText = File.ReadAllText(openfiledialog.FileName);
                    webBrowser1.Document.InvokeScript("SetText", new object[]
 {
                          MainText
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file. Full Error: " + ex.Message);
                }
            }
        }

             public static OpenFileDialog openfiledialog = new OpenFileDialog
             {
                 Filter = "Text File (*.txt)|*.txt| Lua Script (*.lua) |*.lua|All files (*.*)|*.*",
                 FilterIndex = 1,
                 RestoreDirectory = true,
                 Title = "Future - Open File"
             };

        private void button6_Click(object sender, EventArgs e)
        {
            webBrowser1.Document.InvokeScript("SetText", new object[]
                        {
                ""
                        });
        }

        public void setText(String text)
        {
            webBrowser1.Document.InvokeScript("SetText", new object[]{

                text

});

           }

        public String getText()
        {
            HtmlDocument document = webBrowser1.Document;
            string scriptName = "GetText";
            object[] args = new string[0];
            object obj = document.InvokeScript(scriptName, args);
            string script = obj.ToString();

            return script;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex != -1)
            {
                webBrowser1.Document.InvokeScript("SetText", new object[1]
                {
          (object) System.IO.File.ReadAllText("scripts\\" + this.listBox1.SelectedItem.ToString())
                });
            }
            else
            {
                int num = (int)MessageBox.Show("Please select a script from the script list to load.", "Future");
            }
        }

        private void refreshToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            Functions.addToScriptList(listBox1, "./Scripts", "*.txt");
            Functions.addToScriptList(listBox1, "./Scripts", "*.lua");
        }
    }
}

