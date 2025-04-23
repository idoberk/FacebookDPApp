using System;

namespace FacebookDPApp.Backend
{
    public class MyPost 
    {
        private static readonly Random sr_RandomLikesCount = new Random();
        public string Message { get; }
        public DateTime CreatedTime { get;}
        public int LikesCount { get;}

        public MyPost (string i_Message, DateTime i_DateTime)
        {
            Message = i_Message;
            CreatedTime = i_DateTime;
            LikesCount = sr_RandomLikesCount.Next(0, 1000);
        }
    }
}
