using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;

namespace WindowsFormsApplication5
{
    public partial class FormMain : Form
    {
        private const string UserRoot = "HKEY_CURRENT_USER";
        private const string Subkey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings";
        private const string KeyName = UserRoot + "\\" + Subkey;

        public FormMain()
        {
            InitializeComponent();

            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var result = Registry.GetValue(KeyName, "ProxyEnable", 0);

            if (result.ToString() == "1")
            {
                timer1.Enabled = false;

                notifyIcon1.Visible = true;
                notifyIcon1.Icon = SystemIcons.Information;
                notifyIcon1.Click += NotifyIcon1_Click;

                notifyIcon1.BalloonTipTitle = @"Proxy foi ativado";
                notifyIcon1.BalloonTipText = @"Atenção seu proxy de rede foi ativado, clique para desativar";

                notifyIcon1.ShowBalloonTip(5000); // 5 segundos
            }
        }

        private void NotifyIcon1_Click(object sender, EventArgs e)
        {
            Registry.SetValue(KeyName, "ProxyEnable", 0, RegistryValueKind.DWord);
            timer1.Enabled = true;
            notifyIcon1.Visible = false;
        }
    }
}
