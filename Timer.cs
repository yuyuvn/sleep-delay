using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SleepTimer
{
    public enum StatusType { StandBy, Running }

    class Timer : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private uint _defaultDelay;
        public string TimeLeft { get; set; }
        public string ActionName
        {
            get
            {
                if (status == StatusType.StandBy) return "Run";
                else return "Exit";
            }
        }
        public StatusType status { get; private set; }

        public Timer(uint defaultDelay)
        {
            _defaultDelay = defaultDelay;
            TimeLeft = defaultDelay.ToString();
        }
        public void Reset()
        {
            TimeLeft = _defaultDelay.ToString();
            OnPropertyChanged("TimeLeft");
        }

        public void Plus(int minutes)
        {
            int time = Int32.Parse(TimeLeft) + minutes;
            if (time < 0) time = 0;

            TimeLeft = time.ToString();
            OnPropertyChanged("TimeLeft");
        }

        public void Toogle()
        {
            if (status == StatusType.StandBy) Start();
            else Stop();
            if (this.PropertyChanged != null)
            {
                var e = new PropertyChangedEventArgs("ActionName");
                this.PropertyChanged(this, e);
            }
        }

        private void Start()
        {
            status = StatusType.Running;
            System.Threading.Tasks.Task.Factory.StartNew(async () =>
            {
                Thread.Sleep(Int32.Parse(TimeLeft) *60000);
                HttpClient client = new HttpClient();
                var values = new Dictionary<string, string>
                {
                    { "value1", "" },
                    { "value2", "" },
                    { "value3", "" }
                };
                var content = new FormUrlEncodedContent(values);
                await client.PostAsync(Properties.Settings.Default.Endpoint, content);
                Process.Start("shutdown", "/f /s /t 0");
            });
        }

        private void Stop()
        {
            System.Windows.Application.Current.Shutdown();
        }

        protected void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
