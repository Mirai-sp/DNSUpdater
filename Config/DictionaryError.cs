namespace DNSUpdater.Config
{
    public static class DictionaryError
    {
        public static string ERROR_CONFIG_FILE_DOES_NOT_EXISTS(string fileName) { return $"Configuration file does not exists.\r\n\r\n{fileName}"; }
        public static string ERROR_CONFIG_FILE_IS_EMPTY(string fileName) { return $"Configuration file is empty.\r\n\r\n{fileName}"; }
        public static string ERROR_CONFIG_FILE_IS_INVALID(string fileName) { return $"The configuration file content is invalid.\r\n\r\n{fileName}"; }
        public static string ERROR_NO_SCHEDULED_TASK_SELECTED() { return "Please select a scheduled task."; }
        public static string ERROR_NOT_WAS_POSSIBLE_LOAD_SELECTED_SCHEDULED_JOB(string serviceName) { return $"Not was possible to retrieve information for Service Name({serviceName} try reload the schedule job list."; }
    }
}
