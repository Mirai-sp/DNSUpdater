namespace DNSUpdater.Config
{
    public static class BusinessConfig
    {
        public static string DATETIME_OUTPUT = "dd/MM/yyyy HH:mm:ss";
        public static string ERROR_TITLE = "Error";
        public static string SERVICE_NAME = "Service Name";
        public static string ENABLED = "Enabled";
        public static string INTERVAL = "Interval";
        public static string LAST_UPDATED = "Last Updated";
        public static string NEXT_UPDATE = "Next Update";
        public static string STATUS = "Status";
        public static string ENABLE = "Enable";
        public static string DISABLE = "Disable";
        public static string TRUE = "True";
        public static string FALSE = "False";
        public static string NOT_RUNED_YET = "Not Run Yet";
        public static string NOT_SCHEDULED = "Not Scheduled";
        public static string PENDING = "Pending";
        public static string UPDATING = "Updating";
        public static string PROPERTY_RETRY = "Retry";
        public static string PROPERTY_DELAY = "Delay";

        public static string FAILED(string err) { return $"Failed: {err}"; }
        public static string NO_CHANGED(string ip) { return $"Update Not Necessary to: {ip}"; }
        public static string SUCCESS(string ip) { return $"Success. DNS Was Updated to: {ip}"; }
    }
}
