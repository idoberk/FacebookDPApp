using System;

namespace FacebookDPApp.Backend
{
    public class PostWrapper 
    {
        private static readonly Random sr_RandomLikesCount = new Random();
        public string Message { get; }
        public DateTime CreatedTime { get;}
        public int LikesCount { get;}

        public PostWrapper (string i_Message, DateTime i_DateTime)
        {
            Message = i_Message;
            CreatedTime = i_DateTime;
            LikesCount = sr_RandomLikesCount.Next(0, 1000);
        }
    }
}
