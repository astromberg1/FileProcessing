using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileProcess
{
    public static class InitValues
    {
        public static DateTime Referensdatum { get; set; }

        public static string UtfilSokVag { get; set; }

        public static string InfilSokvagOchNamn { get; set; }

        private static bool _AllaVardenSatta;

        public static bool CheckAllaVardenSatta()
        {
            if ((Referensdatum != DateTime.MinValue) && (UtfilSokVag != null) && (InfilSokvagOchNamn != null))
                _AllaVardenSatta = true;
            else
            {
                _AllaVardenSatta = false;
            }


            return _AllaVardenSatta;
        }

    }
}
