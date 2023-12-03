namespace DNSUpdater.Config
{
    public static class DictionaryError
    {
        public static string ERROR_CONFIG_FILE_DOES_NOT_EXISTS(string fileName) { return $"Configuration file does not exists.\r\n\r\n{fileName}"; }
        public static string ERROR_CONFIG_FILE_IS_EMPTY(string fileName) { return $"Configuration file is empty.\r\n\r\n{fileName}"; }
        public static string ERROR_CONFIG_FILE_IS_INVALID(string fileName) { return $"The configuration file content is invalid.\r\n\r\n{fileName}"; }
    }
}
