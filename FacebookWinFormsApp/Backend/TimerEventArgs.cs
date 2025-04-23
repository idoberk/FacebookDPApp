using System;

namespace FacebookDPApp.Backend
{
    public class TimerEventArgs : EventArgs
    {
        public int RemainingSeconds { get; }
        public bool IsTimeRunningOut { get; }

        public TimerEventArgs(int i_RemainingSeconds, bool i_IsTimeRunningOut)
        {
            RemainingSeconds = i_RemainingSeconds;
            IsTimeRunningOut = i_IsTimeRunningOut;
        }
    }
}
