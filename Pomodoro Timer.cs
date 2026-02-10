using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pomodoro_Timer_Project
{
    public partial class PomodoroTimer : Form
    {
        public PomodoroTimer()
        {
            InitializeComponent();
        }
        enum enTitle
        {
            Work,
            Break,
            LongBreak
        };
        enTitle Title = enTitle.Work;

        private int TimeMinutes = 0;
        private int TimeSeconds = 0;
        private byte BreakCounter = 0;

        void Reset()
        {
            lblMinutes.Text = "00";
            lblSeconds.Text = "00";
            labTitle.Text = "Timer";
            timer1.Stop();
            TimeMinutes = 0;
            TimeSeconds = 0;
            BreakCounter = 0;
            Title = enTitle.Work;
            toolStripMenuItem3.Text = "00:00";
            btnStart.Enabled = true;
            btnStop.Enabled = true;
            startToolStripMenuItem.Enabled = true;
            stopToolStripMenuItem.Enabled = true;
            SetTime();
        }


        void SetTime()
        {
            SetTitle();
            switch (Title)
            {
                case enTitle.Work:
                    TimeMinutes = 25;
                    break;

                case enTitle.Break:
                    TimeMinutes = 5;
                    break;

                case enTitle.LongBreak:
                    TimeMinutes = 20;
                    break;

            }

            TimeSeconds = 0;
        }

        void ShowTimerNotifyIcon()
        {
            notifyIcon1.Icon = SystemIcons.Information;
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.BalloonTipTitle = "Timer";

            if (Title == enTitle.Work)
               notifyIcon1.BalloonTipText= "Great job! Time for a break 😌 \nWork session finished";
          
            else
                notifyIcon1.BalloonTipText = "Break finished! Back to work 💪";

            notifyIcon1.ShowBalloonTip(10000);   
        }

        void ShowNotifyIcon(string Title, string Message)
        {
            notifyIcon1.Icon = this.Icon;
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.BalloonTipTitle = Title;
            notifyIcon1.BalloonTipText = Message;
            notifyIcon1.ShowBalloonTip(10000);
        }
        void FinishWork()
        {  
            if (BreakCounter == 3)
            {
                Title = enTitle.LongBreak;
                BreakCounter = 0;
            }
            else
            {
                Title = enTitle.Break;
            }        
        }

        void FinishBreak()
        {    
            if(Title==enTitle.Break)
                  BreakCounter++;

            Title = enTitle.Work;        
        }

        void ChangeTimer()
        {
          
            if (Title == enTitle.Work)
            {
                FinishWork();
            }
            else
            {
                FinishBreak();
            }     
            SetTime();
        }

        void SetTitle()
        {
            if (Title == enTitle.LongBreak)
            {
                labTitle.Text = "Long Break";
                labTitle.Font = new Font(labTitle.Font.FontFamily, 28);
            }
            else
            {
                labTitle.Text = Title.ToString();
                labTitle.Font = new Font(labTitle.Font.FontFamily, 48);
            }
        }

        void ShowTime()
        {
            lblMinutes.Text = $"{ TimeMinutes:D2}";
            lblSeconds.Text = $"{ TimeSeconds:D2}";
            toolStripMenuItem3.Text = $"{lblMinutes.Text}:{lblSeconds.Text}";
        }
  
       void Timer()
        {
            ShowTime();
            TimeSpan TimeS = new TimeSpan(0, TimeMinutes, TimeSeconds);

            TimeS = TimeS.Subtract(new TimeSpan(0, 0, 1));
            TimeMinutes = TimeS.Minutes;
            TimeSeconds = TimeS.Seconds;

           
            if (TimeMinutes == 0 && TimeSeconds < 0)
            {
                ShowTimerNotifyIcon();
                ChangeTimer();
            }
                                
        }


        void Start()
        {
            timer1.Start();
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            startToolStripMenuItem.Enabled = false;
            stopToolStripMenuItem.Enabled = true;
        }
        void Stop()
        {
            timer1.Stop();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            startToolStripMenuItem.Enabled = true;
            stopToolStripMenuItem.Enabled = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

      
        private void timer1_Tick(object sender, EventArgs e)
        {        
            Timer();
        }

        private void Form1_Load(object sender, EventArgs e)
        {   
            Reset();
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.Hide();
            ShowNotifyIcon("Timer", "Hidden\nThe Timer is still running in the tray");
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reset();
        }
    }
}
