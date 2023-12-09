namespace DNSUpdater.Config
{
    public static class DictionaryError
    {
        public static string ERROR_CONFIG_FILE_DOES_NOT_EXISTS(string fileName) { return $"Configuration file does not exists.\r\n\r\n{fileName}"; }
        public static string ERROR_CONFIG_FILE_IS_EMPTY(string fileName) { return $"Configuration file is empty.\r\n\r\n{fileName}"; }
        public static string ERROR_CONFIG_FILE_IS_INVALID(string fileName) { return $"The configuration file content is invalid.\r\n\r\n{fileName}"; }
        public static string ERROR_NO_SCHEDULED_TASK_SELECTED() { return "Please select a scheduled task."; }
        public static string ERROR_NOT_WAS_POSSIBLE_LOAD_SELECTED_SCHEDULED_JOB(string serviceName) { return $"Not was possible to retrieve information for Service Name({serviceName} try reload the schedule job list."; }
        public static string ERROR_DUPLICATED_PROPERTIES(string serviceName, string duplicatedProperties) { return $"Duplicated propertie was found and this is not allowed, please fix it and reload configuration.\r\n\r\nService Name:{serviceName}\r\nDuplicated Properties:{duplicatedProperties}"; }
        public static string ERROR_REQUIRED_PROPERTIES(string serviceName, string requiredPropertyes) { return $"Propertyes({requiredPropertyes}) is required, please fix it and reload configuration.\r\n\r\nService Name:{serviceName}"; }
        public static string ERROR_PROPERTIE_VALUE_IS_INVALID(string serviceName, string propertieName, string propertyeValue) { return $"Propertie Value {propertyeValue} is invalid for {propertieName} context. Service name {serviceName}"; }
        public static string ERROR_UPDATER_CLASS_NOT_FOUND(string className) { return $"Updater Class({className}) not found."; }
        public static string ERROR_UPDATER_ALL_ATTEMPTS_WAS_TRIED(string strategyName, string serviceName) { return $"All attempts was tried for strategy name {strategyName} in service name {serviceName}, if another strategy is seted, it will be tested now."; }
        public static string ERROR_UNABLE_TO_CONVERT_VALUE(string value, string desireType) { return $"Unable to convert {value} to {desireType}."; }
        public static string ERROR_UNABLE_TO_UPDATE = "Unable to update.";
        public static string ERROR_UNABLE_TO_REACH_THE_REQUESTED_URL(string requestedURL) { return $"Unable to reach the requested url {requestedURL}."; }
        public static string ERROR_ATTEMPT_RESEND(string actualAttempt, string totalAttempt, string strategyName, string url, string error) { return $"Attempt {actualAttempt} of {totalAttempt} Strategy {strategyName} URL:{url} Error: {error}"; }

        public static string ERROR_UNABLE_GET_PROPERTIE_VALUE_FROM_RESPONSE(string serviceName, string response, string value) { return $"Unable to get the propertie value {value} from response({response}). Service name {serviceName}"; }
        public static string ERROR_RESPONSE_VALUE_IS_NOT_A_VALID_IP_ADDRESS(string serviceName, string response) { return $"Response value {response} is not a valid ip address. Service Name {serviceName}"; }
        public static string ERROR_INVALID_HTTP_VERB_PROVIDED(string providedMethod) { return $"Invalid HTTP Verb Provided({providedMethod}"; }
    }
}
