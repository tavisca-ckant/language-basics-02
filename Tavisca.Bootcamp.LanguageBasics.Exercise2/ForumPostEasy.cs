using System;

namespace GradeBook
{
    public class ForumPostEasy
    {
        public static string GetCurrentTime(string[] exactPostTime, string[] showPostTime)
        {
            int length = exactPostTime.Length;

            var minTime = "";
            var maxTime = "";

            string[,] minMaxTime = new string [length,2];

            for (int i = 0; i < length; i++) 
            {
                string[] currentMinMaxTime = GetCurrentMinAndMaxTime (exactPostTime[i], showPostTime[i]);
                minTime = currentMinMaxTime[0];
                maxTime = currentMinMaxTime[1];

                minMaxTime[i,0] = Format(minTime);
                minMaxTime[i,1] = Format(maxTime);
            }

            minTime = GetLeastTimeInMinMax (minTime, minMaxTime, length);
            maxTime = GetMaximumTimeInMinMax (maxTime, minMaxTime, length);
            
            return LexicographicallySmallest (minTime, maxTime);     
        }

        private static string[] GetCurrentMinAndMaxTime (string exactPostTime, string showPostTime)
        {

            string [] time = exactPostTime.Split(":"); // HH:MM:SS
            var hours = int.Parse(time[0]);
            var minutes = int.Parse(time[1]);
            var seconds = int.Parse(time[2]);

            string[] xMomentAgo = showPostTime.Split(" ");

            return CurrentMinMaxTime (hours, minutes, seconds, xMomentAgo[1], xMomentAgo[0]);
        }

        private static string[] CurrentMinMaxTime (int hours, int minutes, int seconds, string moment, string x)
        {
            var minTime = "";
            var maxTime = ""; 

            switch (moment) 
            {
                    case "seconds":
                        minTime = hours + ":" + minutes + ":" + seconds;
                        maxTime = GetTime59SecondsAgo (hours, minutes, seconds);
                        break;
                    
                    case "minutes":       
                        minTime = GetTimeXMinutesAgo (hours, minutes, seconds, int.Parse(x));
                        maxTime = GetTimeXMinutes59SecondsAgo (hours, minutes, seconds, int.Parse(x));
                        break;

                    case "hours":      
                        minTime = GetTimeXHoursAgo (hours, minutes, seconds, int.Parse(x));
                        maxTime = GetTimeXHours59Minute59SecondsAgo (hours, minutes, seconds, int.Parse(x));
                        break;
            }

            return new string [] {minTime, maxTime};
        }

        private static string GetTime59SecondsAgo (int hours, int minutes, int seconds)
        {
            if (seconds == 59) 
            {
                seconds = 0;
            } 
            else 
            {
                minutes++;
                seconds--;
            }
            if (minutes > 59) 
            {
                minutes %= 60;
                hours = ++hours % 24;
            }
            return hours + ":" + minutes + ":" + seconds;
        }

        private static string GetTimeXMinutesAgo (int hours, int minutes, int seconds, int X)
        {
            // X minutes ago
            minutes += X;
            if (minutes > 59) 
            {
                minutes %= 60;
                hours = ++hours % 24;
            }
            return hours + ":" + minutes + ":" + seconds;
        }

        private static string GetTimeXMinutes59SecondsAgo (int hours, int minutes, int seconds, int X)
        {
            string[] time = GetTimeXMinutesAgo (hours, minutes, seconds, X).Split(":");
            hours = int.Parse (time[0]);
            minutes = int.Parse (time[1]);
            seconds = int.Parse (time[2]);
            
            // (X minutes) + 59 seconds ago
            seconds += 59;
            if (seconds > 59) 
            {
                seconds %= 60;
                minutes++;
                if (minutes > 59) 
                {
                    minutes %= 60;
                    hours = ++hours % 24;
                }
            }
            return hours + ":" + minutes + ":" + seconds;
        }

        private static string GetTimeXHoursAgo (int hours, int minutes, int seconds, int X)
        {
            hours += X;
            if (hours > 23) 
            {
                hours %= 24;
            }
            return hours + ":" + minutes + ":" + seconds;
        }

        private static string GetTimeXHours59Minute59SecondsAgo (int hours, int minutes, int seconds, int X)
        {
            // (X hours) +  59 minutes 59 seconds ago => (X+1 hours) - 1 second.
            string[] time = GetTimeXHoursAgo (hours, minutes, seconds, X + 1).Split(":");
            hours = int.Parse (time[0]);
            minutes = int.Parse (time[1]);
            seconds = int.Parse (time[2]);
        
            seconds++;
            if (seconds == 60) 
            {
                seconds = 0;
                minutes++;
                if (minutes == 60) 
                {
                    minutes = 0;
                    hours++;
                    if (hours == 24) 
                    {
                        hours = 0;
                    }
                }
            }
            return hours + ":" + minutes + ":" + seconds;   
        }

        // Formats string in HH:MM:SS format.
        private static string Format (string time) 
        {
            string[] temp = time.Split(":");
            for (int i = 0; i < 3; i++) 
            {
                if (temp[i].Length == 1) 
                {
                     temp[i] = "0" + temp[i];
                }
            }
            return temp[0] + ":" + temp[1] + ":" + temp[2];
        }

        private static string GetLeastTimeInMinMax (string minTime, string[,] minMaxTime, int length)
        {
            minTime = minMaxTime[0,0];

            for (int i = 1; i < length; i++) 
            {
                // min time
                if (minTime.CompareTo(minMaxTime[i,0]) < 0) 
                {
                    minTime = minMaxTime[i,0];
                }
            }

            return minTime;
        }

        private static string GetMaximumTimeInMinMax (string maxTime, string[,] minMaxTime, int length)
        {
            maxTime = minMaxTime[0,1];

            for (int i = 1; i < length; i++) 
            {
                // max time
                if (maxTime.CompareTo(minMaxTime[i,1]) > 0) 
                {
                     maxTime = minMaxTime[i,1];
                }
            }

            return maxTime;
        }

        private static string LexicographicallySmallest (string minTime, string maxTime)
        {
            // Find Lexicographically smallest between Min and Max time.
            if (minTime.CompareTo(maxTime) > 0) 
            {
                return "impossible";
            } 
            else 
            {
                return minTime;
            }
        }
    }
}
