using System;

namespace BreederStationBussinessLayer.Domain.Enums
{
        public enum SexEnum { samec, samice };
        public class SexEnumUtils
        {
            public static SexEnum getSexEnum(string type)
            {
                switch (type)
                {
                    case "samec":
                        return SexEnum.samec;
                    case "samice":
                        return SexEnum.samice;
                }
                throw new ArgumentException();
            }
        }
}
