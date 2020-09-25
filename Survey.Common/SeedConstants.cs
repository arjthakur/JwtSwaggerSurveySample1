using System;

namespace Survey.Common
{
    public class SeedConstants
    {
        #region Users seed
        public const string DomainName = "MySurvey";
        public const string SuperAdminEmail = "SuperAdmin@" + DomainName + ".com";
        public static DateTime SeedDate = new DateTime(2020, 1, 1);
        public const string UserName = "MySurveyAdmin";
        public const string PasswordHash = "AQAAAAEAACcQAAAAEMeA4fPsgwZQc/VKrVDZeqMr+E5fUOY8JN0ZU3CqCgzuj0KyH00Te5QVv6vGAvGh3A==";//Admin@Survey10001          //public const string PasswordHash = "AQAAAAEAACcQAAAAEJcHsXfnnl5+JTYLUVwSfrB2CyGfw6fyakgcsIuuqDfnIkx2bliMl2L4bCD6zhUgBw==";//Admin@Survey1
        public const string FirstName = "Arjun";
        public const string LaststName = "Thakur";
        public const string PhoneNumber = "+919977333980";
        public const int SeedId = 10001;
        #endregion

        #region Roles
        public const string Roles = "SuperAdmin,Admin,User";
        #endregion
    }
}
