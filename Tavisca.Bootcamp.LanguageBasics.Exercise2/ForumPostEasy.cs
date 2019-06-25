using System;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public class ForumPostEasy
    {
        public static string GetCurrentTime(string[] exactPostTime, string[] showPostTime)
        {
            int len = exactPostTime.Length;

            var minTime = "";
            var maxTime = "";

            string[,] minMaxTime = new string [len,2];

            for (int i = 0; i < len; i++) {
                string[] s = showPostTime[i].Split(" ");
                string [] time = exactPostTime[i].Split(":"); // HH:MM:SS
                var hours = Convert.ToInt32(time[0]);
                var minutes = Convert.ToInt32(time[1]);
                var seconds = Convert.ToInt32(time[2]);
                var X =  0;

                switch (s[1]) {
                    case "seconds":
                        // GetLeastTime
                        // less 59 seconds.
                        if (seconds == 59) {
                            seconds = 0;
                        } else {
                            minutes++;
                            seconds--;
                        }
                        if (minutes == 60) {
                            hours = ++hours % 24;
                            minutes = 0;
                        }
                        maxTime = hours + ":" + minutes + ":" + seconds;
                        minTime = exactPostTime[i];
                        break;
                    case "minutes":  
                        X =  Convert.ToInt32(s[0]);  
                        // X minutes ago
                        minutes += X;
                        if (minutes > 59) {
                            minutes %= 60;
                            hours = ++hours % 24;
                        }
                        minTime = hours + ":" + minutes + ":" + seconds;
                        // (X minutes) + 59 seconds ago
                        seconds += 59;
                        if (seconds > 59) {
                            seconds %= 60;
                            minutes++;
                            if (minutes > 59) {
                                minutes %= 60;
                                hours = ++hours % 24;
                            }
                        }
                        maxTime = hours + ":" + minutes + ":" + seconds;
                        break;
                    case "hours":
                        X =  Convert.ToInt32(s[0]);
                        // X hours ago.
                        hours += X;
                        if (hours > 23) {
                            hours %= 24;
                        }
                        minTime = hours + ":" + minutes + ":" + seconds;
                        // (X hours) +  59 minutes 59 seconds ago => 1 sec 
                        // reset hour
                        hours = Convert.ToInt32(time[0]);
                        seconds++;
                        if (seconds == 60) {
                            seconds = 0;
                            minutes++;
                            if (minutes == 60) {
                                minutes = 0;
                                hours++;
                                if (hours == 24) {
                                    hours = 0;
                                }
                            }
                        }
                        maxTime = hours + ":" + minutes + ":" + seconds;   
                        break;
                }
                minMaxTime[i,0] = Format(minTime);
                minMaxTime[i,1] = Format(maxTime);
            }

            minTime = minMaxTime[0,0];
            maxTime = minMaxTime[0,1];

            for (int i = 1; i < len; i++) {
                // min time
                if (minTime.CompareTo(minMaxTime[i,0]) < 0) {
                    minTime = minMaxTime[i,0];
                }
                // max time
                if (maxTime.CompareTo(minMaxTime[i,1]) > 0) {
                    maxTime = minMaxTime[i,1];
                }
            }
            // Find Lexicographically smallest between Min and Max time.
            if (minTime.CompareTo(maxTime) > 0) {
                return "impossible";
            } else {
                return minTime;
            }
        }

        // Formats string in HH:MM:SS format.
        static string Format (string time) {
            string[] temp = time.Split(":");
            for (int i = 0; i < 3; i++) {
                 if (temp[i].Length == 1) {
                     temp[i] = "0" + temp[i];
                }
            }
            return temp[0] + ":" + temp[1] + ":" + temp[2];
        }
    }
}
