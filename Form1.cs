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
    public partial class Form1 : Form
    {
        public Form1()
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

        private int TimeWork = 25;
        private int TimeBreak =5;
        private int TimeLongBreak = 20;

        private int TimeMinutes = 0;
        private int TimeSeconds = 0;
        private byte BreakCounter = 0;
 

        void Reset()
        {
            labTimer.Text = " 00:00";
            labTitle.Text = "Timer";
            timer1.Enabled = false;
            TimeMinutes = 0;
            TimeSeconds = 0;
            BreakCounter = 0;
        }


        void SetTime()
        {
            if (Title == enTitle.Work)
            {
                TimeMinutes = TimeWork;
            }
            if (Title == enTitle.Break)
            {
                TimeMinutes = TimeBreak;
            }
            if(Title==enTitle.LongBreak)
            {
                TimeMinutes = TimeLongBreak;
                BreakCounter = 0;
            }
        }

        void ShowAMessage()
        {
            timer1.Enabled = false;
           

            if(Title==enTitle.Work)
            {
                MessageBox.Show("Great job! Time for a break 😌", "Work session finished");
                   

            }
            else
            {
                MessageBox.Show("Break finished! Back to work 💪");
                   
            }
            timer1.Enabled = true ;
        }
        void FinishWork()
        {
            if (TimeMinutes == 0 && TimeSeconds == 0 && Title == enTitle.Work)
            {
                ShowAMessage();
                if (BreakCounter ==3)
                {
                    Title = enTitle.LongBreak;
                   
                }
                else
                {
                    Title = enTitle.Break;
                }
                SetTime();
            }
        }

        void FinishBreak()
        {
            if (TimeMinutes == 0 && TimeSeconds == 0 && (Title == enTitle.Break || Title == enTitle.LongBreak))
            {
               ShowAMessage();
               BreakCounter++;
               Title = enTitle.Work;
               SetTime();
            }
        }

        void SetTitle()
        {
            if(Title==enTitle.LongBreak)
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
       void Timer()
        {
            SetTitle();

            if (TimeMinutes > 0 || TimeSeconds> 0)
            {
                if (TimeSeconds < 10 || TimeMinutes < 10)
                {
                    string Minutes = TimeMinutes.ToString();
                    string Seconds = TimeSeconds.ToString();

                    if (TimeSeconds < 10)
                    {
                        Seconds = $"0{TimeSeconds.ToString()}" ;
                    }
                    if (TimeMinutes < 10)
                    {
                        Minutes = $"0{TimeMinutes.ToString()}" ;
                    }
                    labTimer.Text = Minutes + " : " + Seconds;

                 }

                else
                {
                    labTimer.Text =  $"{ TimeMinutes.ToString()} : { TimeSeconds.ToString()}" ;
                }

                TimeSeconds--;

                if(TimeSeconds < 0)
                {
                    TimeSeconds = 59;

                    if(TimeMinutes > 0)
                         TimeMinutes--;
                }
            }


            if (Title == enTitle.Work)
            {
                FinishWork();
            }
            else
            {
                FinishBreak();
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
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
            SetTime();
        }
    }
}
