using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace PayCare.Repository
{
    public class Store
    {
        public static string ActiveForm;
        public static int ActiveMonth;
        public static int ActiveYear;
        public static string ActiveUser;
        public static bool IsAdministrator;
        public static int CutOffDate;

        public static bool IsPeriodClosed;
        public static bool IsThrClosed;

        public static string ActiveReport;
        public static string ConnStr = ConfigurationManager.AppSettings["ConnectionString"];
        public static string ReportPath = ConfigurationManager.AppSettings["ReportPath"];




        public static int GetTotalOverTimeInMinute(string startHour, string endHour)
        {
            string strStartHour = string.Empty;
            string strEndHour = string.Empty;
           
            TimeSpan startTime = Convert.ToDateTime(startHour).TimeOfDay;
            TimeSpan endTime = Convert.ToDateTime(endHour).TimeOfDay;
            TimeSpan diff = endTime > startTime ? endTime - startTime : endTime - startTime + TimeSpan.FromDays(1);
            int minutes = (int)diff.TotalMinutes;

            return minutes;
        }




        public static int GetTotalOverTimeInMinute(int startHour, int startMinute,
         int endHour, int endMinute)
        {
            string strStartHour = string.Empty;
            string strStartMinute = string.Empty;
            string strEndHour = string.Empty;
            string strEndMinute = string.Empty;

            strStartHour = startHour < 10 ? "0" + startHour.ToString() : startHour.ToString();
            strStartMinute = startMinute < 10 ? "0" + startMinute.ToString() : startMinute.ToString();

            strEndHour = endHour < 10 ? "0" + endHour.ToString() : endHour.ToString();
            strEndMinute = endMinute < 10 ? "0" + endMinute.ToString() : endMinute.ToString();

            TimeSpan startTime = Convert.ToDateTime(strStartHour + ":" + strStartMinute).TimeOfDay;
            TimeSpan endTime = Convert.ToDateTime(strEndHour + ":" + strEndMinute).TimeOfDay;
            TimeSpan diff = endTime > startTime ? endTime - startTime : endTime - startTime + TimeSpan.FromDays(1);
            int minutes = (int)diff.TotalMinutes;

            return minutes;
        }


        public static string GetTotalInHour(int totalMinute)
        {
            TimeSpan ts = TimeSpan.FromMinutes(Convert.ToDouble(totalMinute));
            return ts.Hours.ToString() + " Jam " + ts.Minutes.ToString() + " Menit";
        }


        public static string GetAmounInWords(int amount)
        {
            string[] strDigits = { "", "satu", "dua", "tiga", "empat", "lima", "enam", "tujuh", "delapan", "sembilan", "sepuluh", "sebelas" };
            string words = "";

            if (amount < 12)
            {
                words = " " + strDigits[amount];
            }
            else if (amount < 20)
            {
                words = GetAmounInWords(amount - 10).ToString() + " belas";
            }
            else if (amount < 100)
            {
                words = GetAmounInWords(amount / 10) + " puluh" + GetAmounInWords(amount % 10);
            }
            else if (amount < 200)
            {
                words = " seratus" + GetAmounInWords(amount - 100);
            }
            else if (amount < 1000)
            {
                words = GetAmounInWords(amount / 100) + " ratus" + GetAmounInWords(amount % 100);
            }
            else if (amount < 2000)
            {
                words = " seribu" + GetAmounInWords(amount - 1000);
            }
            else if (amount < 1000000)
            {
                words = GetAmounInWords(amount / 1000) + " ribu" + GetAmounInWords(amount % 1000);
            }
            else if (amount < 1000000000)
            {
                words = GetAmounInWords(amount / 1000000) + " juta" + GetAmounInWords(amount % 1000000);
            }
            else if (amount < 1000000000000)
            {
                words = GetAmounInWords(amount / 1000000000) + " milyar" + GetAmounInWords(amount % 1000000000);
            }

            return words;

        }

        public static string GetMonthName(int monthCode)
        {
            string monthName = "";

            if (monthCode == 1)
            {
                monthName = "Januari";
            }
            else if (monthCode == 2)
            {
                monthName = "Februari";
            }
            else if (monthCode == 3)
            {
                monthName = "Maret";
            }
            else if (monthCode == 4)
            {
                monthName = "April";
            }
            else if (monthCode == 5)
            {
                monthName = "Mei";
            }
            else if (monthCode == 6)
            {
                monthName = "Juni";
            }
            else if (monthCode == 7)
            {
                monthName = "Juli";
            }
            else if (monthCode == 8)
            {
                monthName = "Agustus";
            }
            else if (monthCode == 9)
            {
                monthName = "September";
            }
            else if (monthCode == 10)
            {
                monthName = "Oktober";
            }
            else if (monthCode == 11)
            {
                monthName = "November";
            }
            else
            {
                monthName = "Desember";
            }


            return monthName;

        }


        public static int GetMonthCode(string monthName)
        {
            int monthCode = 0;

            if (monthName == "Januari")
            {
                monthCode = 1;
            }
            else if (monthName == "Februari")
            {
                monthCode = 2;
            }
            else if (monthName == "Maret")
            {
                monthCode = 3;
            }
            else if (monthName == "April")
            {
                monthCode = 4;
            }
            else if (monthName == "Mei")
            {
                monthCode = 5;
            }
            else if (monthName == "Juni")
            {
                monthCode = 6;
            }
            else if (monthName == "Juli")
            {
                monthCode = 7;
            }
            else if (monthName == "Agustus")
            {
                monthCode = 8;
            }
            else if (monthName == "September")
            {
                monthCode = 9;
            }
            else if (monthName == "Oktober")
            {
                monthCode = 10;
            }
            else if (monthName == "November")
            {
                monthCode = 11;
            }
            else
            {
                monthCode = 12;
            }


            return monthCode;

        }


        public static string GetDay(int day)
        {
            string strDay = string.Empty;

            switch (day)
            {
                case 0:
                    strDay = "Minggu";
                    break;
                case 1:
                    strDay = "Senin";
                    break;
                case 2:
                    strDay = "Selasa";
                    break;
                case 3:
                    strDay = "Rabu";
                    break;
                case 4:
                    strDay = "Kamis";
                    break;
                case 5:
                    strDay = "Jumat";
                    break;
                case 6:
                    strDay = "Sabtu";
                    break;

            }

            return strDay;
        }

    }
}
