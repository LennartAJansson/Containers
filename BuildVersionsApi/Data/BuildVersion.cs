namespace BuildVersionsApi.Data
{
    public class BuildVersion// : BaseLoggedEntity
    {
        private int major;
        private int minor;
        private int build;

        //https://devopsnet.com/2011/06/09/build-versioning-strategy/
        public int Id { get; set; }
        public int Major
        {
            get => major;
            set
            {
                if (value > major)
                {
                    major = value;
                    Minor = 0;
                    Build = 0;
                    Revision = 0;
                }
            }
        }
        public int Minor
        {
            get => minor;
            set
            {
                if (value > minor)
                {
                    minor = value;
                    Build = 0;
                    Revision = 0;
                }
            }
        }
        public int Build
        {
            get => build;
            set
            {
                if (value > build)
                {
                    build = value;
                    Revision = 0;
                }
            }
        }
        public int Revision { get; set; }
        public Version Version => new(Major, Minor, Build, Revision);
        public string Release => $"{Major}.{Minor}";
        public string SemanticVersion => SemanticVersionPre == string.Empty ? $"{Major}.{Minor}.{Build}" : $"{Major}.{Minor}.{Build}-{SemanticVersionPre}.{Revision}";
        public string SemanticRelease => SemanticVersionPre == string.Empty ? $"{Major}.{Minor}" : $"{Major}.{Minor}-{SemanticVersionPre}.{Build}.{Revision}";
        public string SemanticVersionPre { get; set; } = "";

        public Binary? Binary { get; set; }
    }
}
