using System;
using System.Collections.Generic;
using System.Text;
using MPLATFORMLib;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MControls
{
    class MHelpers
    {
        public static Color ColorActive
        {
            get
            {
                return Color.Red;// Color.FromArgb(255, 72, 0);
            }
        }

        public static Color ColorReady
        {
            get
            {
                return Color.Green;// FromArgb(122, 192, 36);
            }
        }

        public static Color ColorDarkBlue
        {
            get
            {
                return Color.FromArgb(0, 92, 144);
            }
        }

        public static Color ColorLightBlue
        {
            get
            {
                return Color.FromArgb(52, 152, 212);
            }
        }

        public static Color ColorBGBlue
        {
            get
            {
                return Color.FromArgb(222, 232, 254);
            }
        }

        public static string ToString(M_VID_PROPS vidProps)
        {
            return vidProps.nWidth.ToString() + "x" + vidProps.nHeight + "@" + vidProps.dblRate.ToString("00.00") + " " +
                vidProps.nAspectX.ToString() + ":" + vidProps.nAspectY.ToString() + " " +
                vidProps.eInterlace.ToString();
        }

        public static string ToString(M_AUD_PROPS audProps)
        {
            return ((double)audProps.nSamplesPerSec / 1000).ToString("0.00") + "KHz " + audProps.nChannels.ToString() + "Ch " + audProps.nBitsPerSample.ToString() + "Bits";
        }

        public static string ToString(M_DATETIME mTime)
        {
            string strRes = "";
            if (mTime.nYear > 0)
            {
                strRes = string.Format("{0,0:00}.{1,0:00}.{2,0:0000} {3,0:00}:{4,0:00}:{5,0:00}", mTime.nDay, mTime.nMonth, mTime.nYear, mTime.nHour, mTime.nMinute, mTime.nSecond);
            }
            else
            {
                strRes = string.Format("{0,0:00}:{1,0:00}:{2,0:00}", mTime.nHour, mTime.nMinute, mTime.nSecond);
            }         
            return strRes;
            //return mTime.nHour.ToString("00") + ":" + mTime.nMinute.ToString("00") + ":" + mTime.nSecond.ToString("00");
        }

        public static string ToString(M_DATETIME mTime, bool _bMSec)
        {
            return mTime.nHour.ToString("00") + ":" + mTime.nMinute.ToString("00") + ":" + mTime.nSecond.ToString("00") + "." +
                mTime.nMilliseconds.ToString("000");
        }

        public static string PosToString(double _dblPos)
        {
            return PosToString(_dblPos, -1);
        }

        // _nUseMsec = 0 -> No msec
        // _nUseMsec = 1 -> Use msec
        // _nUseMsec = -1 -> Auto
        public static string PosToString(double _dblPos, int _nUseMsec)
        {
            bool bReverse = _dblPos >= 0 ? false : true;
            if (bReverse)
                _dblPos *= -1;

            int nHour = (int)_dblPos / 3600;
            int nMinutes = ((int)_dblPos % 3600) / 60;
            int nSec = ((int)_dblPos % 60);
            _dblPos -= (int)_dblPos;
            int nMsec = (int)(_dblPos * 1000 + 0.01); // For rounding
            nMsec = nMsec >= 1000 ? 999 : nMsec;

            if (_nUseMsec < 0)
                _nUseMsec = nMsec > 0 ? 1 : 0;

            string strRes = (bReverse ? "-" : "") + nHour.ToString("00") + ":" + nMinutes.ToString("00") + ":" + nSec.ToString("00")
                + (_nUseMsec > 0 ? "." + nMsec.ToString("000") : "");

            return strRes;
        }

        public static M_DATETIME ParseTime(string _strTime)
        {
            return ParseTime(_strTime, 25.0);
        }

        public static M_DATETIME ParseTime(string _strTime, double _dblRate)
        {
            return ParseTime(_strTime, _dblRate, false );
        }

        public static M_DATETIME ParseDateTime(string _strDateTime)
        {
            M_DATETIME m_dtRes = new M_DATETIME();
            DateTime dtParse;
            bool valid = DateTime.TryParse(_strDateTime, null, System.Globalization.DateTimeStyles.NoCurrentDateDefault, out dtParse);
            if (valid)
            {
                m_dtRes.nYear = dtParse.Year > 1 ? dtParse.Year : 0;
                m_dtRes.nMonth = dtParse.Month > 1 ? dtParse.Month : 0;
                m_dtRes.nDay = dtParse.Day > 1 ? dtParse.Day : 0;
                m_dtRes.nHour = dtParse.Hour;
                m_dtRes.nMinute = dtParse.Minute;
                m_dtRes.nSecond = dtParse.Second;
            }
            return m_dtRes;

        }

        public static M_DATETIME ParseTime(string _strTime, double _dblRate, bool _bForceSec )
        {
            M_DATETIME mTime = new M_DATETIME();
            // Check for negative time
            _strTime = _strTime.Trim();
            bool bNegative = false;
            if(_strTime.Length > 0 && _strTime[0] == '-')
            {
                // Remove -1
                bNegative = true;
                _strTime = _strTime.Substring(1);
            }

            // Get milliseconds
            int nMsecPos = _strTime.LastIndexOf(".");
            if (nMsecPos >= 0)
            {
                double dblMsec = 0;
                double.TryParse(_strTime.Substring(nMsecPos), out dblMsec);
                mTime.nMilliseconds = (int)(dblMsec * 1000);
                _strTime = _strTime.Substring(0, nMsecPos);
            }

            // Parse other type
            string [] arrParts = _strTime.Split(':');
            if (arrParts.Length == 1)
            {
                // e.g. 10 or 10.100
                Int32.TryParse(arrParts[0], out mTime.nSecond);
            }
            else if (arrParts.Length == 2)
            {
                if (nMsecPos >= 0 || _bForceSec)
                {
                    // e.g. 12:10.100 -> 12 min 10 sec 100 msec
                    Int32.TryParse(arrParts[0], out mTime.nMinute);
                    Int32.TryParse(arrParts[1], out mTime.nSecond);
                }
                else 
                {
                    // e.g. 12:10 -> 12h 10 min
                    Int32.TryParse(arrParts[0], out mTime.nHour);
                    Int32.TryParse(arrParts[1], out mTime.nMinute);
                }
            }
            else if (arrParts.Length >= 3)
            {
                // e.g. 12:10:05 -> 12h 10 min 5 sec
                Int32.TryParse(arrParts[0], out mTime.nHour);
                Int32.TryParse(arrParts[1], out mTime.nMinute);
                Int32.TryParse(arrParts[2], out mTime.nSecond);

                if (arrParts.Length >= 4)
                {
                    // 12:10:05:10  -> timecode (ignore .xxx if have e.g. 12:10:05:10.100 -> 12:10:05:10)
                    Int32.TryParse(arrParts[3], out mTime.nMilliseconds);

                    // Update milliseconds according to rate
                    _dblRate = _dblRate > 0 ? _dblRate : 25.0;
                    mTime.nMilliseconds = (int)(mTime.nMilliseconds * 1000 / _dblRate);
                }
            }

            if (bNegative)
            {
                mTime.nHour *= -1;
                mTime.nMinute *= -1;
                mTime.nSecond *= -1;
                mTime.nMilliseconds *= -1;
            }

            return mTime;
        }

        public static double ParsePos(string _strTime)
        {
            M_DATETIME mTime = ParseTime(_strTime, 25.0, true );
            return mTime.nHour * 3600 + mTime.nMinute * 60 + mTime.nSecond + (double)mTime.nMilliseconds / 1000.0;
        }

        // Conversion
        public static DateTime MTime2DateTime(M_DATETIME _mTime)
        {
            DateTime dtTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                _mTime.nHour, _mTime.nMinute, _mTime.nSecond, _mTime.nMilliseconds);

            return dtTime;
        }

        public static double MTime2Sec(M_DATETIME mTime)
        {
            return mTime.nHour * 3600 + mTime.nMinute * 60 + mTime.nSecond + (double)mTime.nMilliseconds / 1000.0;
        }

        public static long MTime2FileTime(M_DATETIME mTime)
        {
            if (mTime.nDay > 0)
            {
                DateTime dtTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                    mTime.nHour, mTime.nMinute, mTime.nSecond, mTime.nMilliseconds);

                return dtTime.ToFileTime();
            }

            return ((mTime.nHour * 3600 + mTime.nMinute * 60 + mTime.nSecond) * 1000 + mTime.nMilliseconds) * 10000L;
        }

        
        
    }
}
