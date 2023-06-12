namespace _0.Framework.Infrastructure
{
    public static class Roles
    {
        public const int Administrator = 1;
        public const int Operator = 2;
        public const int User = 3;

        public static string GetRoleNameBy(long id)
        {
            switch (id)
            {
                case 1:
                    {
                        return "مدیر سیستم";
                    }
                case 2:
                    {
                        return "اپراتور سیستم";
                    }
                case 3:
                    {
                        return "کاربر";
                    }
                default:
                    return "";
            }
        }
    }
}
