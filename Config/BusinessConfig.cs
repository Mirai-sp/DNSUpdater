﻿namespace DNSUpdater.Config
{
    public static class BusinessConfig
    {
        public static string DATETIME_OUTPUT = "dd/MM/yyyy HH:mm:ss";
        public static string ERROR_TITLE = "Error";
        public static string SERVICE_NAME = "Service Name";
        public static string DOMAIN_NAME = "Domain Name";
        public static string ENABLED = "Enabled";
        public static string INTERVAL = "Interval";
        public static string LAST_UPDATED = "Last Updated";
        public static string NEXT_UPDATE = "Next Update";
        public static string WAN_IP = "WAN IP";
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
        public static string PROPERTY_SERVICEURL = "ServiceUrl";
        public static string PROPERTY_HTTP_VERB = "HttpVerb";
        public static string PROPERTY_CONTENT_TYPE = "ContentType";
        public static string PROPERTY_PATH_PROPERTIE = "PathPropertie";
        public static string PROPERTY_REQUEST_HEADERS = "RequestHeaders";
        public const string HTTP_GET = "Get";
        public const string HTTP_POST = "Post";
        public const string HTTP_DELETE = "Delete";
        public const string HTTP_PUT = "Put";
        public const string HTTP_PATCH = "PATCH";

        public static string INITIALIZING_UPDATE(string serviceName) { return $"Starting update process to {serviceName}"; }
        public static string FAILED_RUN_STRATEGY(string err) { return $"Failed Run Strategy: {err}"; }
        public static string FAILED_UPDATE(string err) { return $"Failed Update: {err}"; }
        public static string NO_CHANGED(string ip) { return $"Update Not Necessary to: {ip}"; }
        public static string SUBMITED(string response) { return $"Request Submited: {response}"; }
        public static string STRATEGY_RETRIEVE_IP_SUCESSFULL(string serviceName, string IP) { return $"Strategy was executed for service {serviceName} with success and the retrieved IP is {IP}, now we will continue and check if is necessary send a update request IP"; }
    }
}
