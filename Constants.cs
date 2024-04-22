namespace webapi
{
    public static class Constants
    {
        public static readonly string serverUrl = "https://localhost:7262";

        public static readonly string localPathToStorages = @"Resources/";
        public static readonly string localPathToProfileIcons = $"{localPathToStorages}ProfileIcons/";
        public static readonly string localPathToModuleFiles = $"{localPathToStorages}ModuleFiles/";
        public static readonly string localPathToSessionRecordingFiles = $"{localPathToStorages}SessionRecording/";

        public static readonly string webPathToProfileIcons = $"{serverUrl}/api/upload/profileIcon/";
        public static readonly string webPathToModuleFile = $"{serverUrl}/api/upload/module/";
        public static readonly string webPathToSessionRecordingFile = $"{serverUrl}/api/upload/session/";

    }
}