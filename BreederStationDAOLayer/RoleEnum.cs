using System;

namespace BreederStationDataLayer
{
    public enum RoleEnum { ADMIN = 1, REDITEL = 2, CHOVATEL = 3, UKLIZEC = 4, ROLE_INSERT_DELETE_TEST = 5 };

    public class RoleEnumUtils
    {
        public static RoleEnum getRoleType(string type)
        {
            switch (type)
            {
                case "ADMIN":
                    return RoleEnum.ADMIN;
                case "UKLIZEC":
                    return RoleEnum.UKLIZEC;
                case "CHOVATEL":
                    return RoleEnum.CHOVATEL;
                case "REDITEL":
                    return RoleEnum.REDITEL;
                case "ROLE_INSERT_DELETE_TEST":
                    return RoleEnum.ROLE_INSERT_DELETE_TEST;
            }
            throw new ArgumentException();
        }

    }

}
