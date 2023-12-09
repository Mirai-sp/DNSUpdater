# DNSUpdater

DNSUpdater is an application that updates your IP address in DDNS services.
When configuring strategies to obtain the WAN IP address, if successful, the recovered address will be submitted for update.

The application is fully customizable and allows you to implement new strategy classes as needed.

## Config Model

For the application to work, a configuration file **config.json** must exist in the application directory with the following model.

~~~json
[
{
    "ServiceName" : "UpdaterDDNSByHttpRequest",
    "DomainName" : "myddnsdomain.com",
    "Enabled" : true,
    "Interval" : 7000,
    "Properties" : [
        {
            "Name" : "ServiceUrl",
            "Value" : "http://ddnsUpdaterEndpoint.com"
        },
	{
            "Name" : "HTTPVerb",
            "Value" : "Get"
        }
    ],
    "WorkStrategy" : [
        {
            "StrategyName" : "StrategyECHO_IP",
            "Enabled" : true,
            "Properties" : [
                {
                    "Name" : "GETURL",
                    "Value" : "http://serviceThatRetrieveWanIPThatReturnAJsonObject"
                },
                {
                    "Name" : "PATHPROPERTIE",
                    "Value" : "object.myIPPropertie"
                },
                {
                    "Name" : "RETRY",
                    "Value" : 3
                },
                {
                    "Name" : "DELAY",
                    "Value" : 2000
                },
           ]
        }
   ]
},
{
    "ServiceName" : "UpdaterDDNSByHttpRequest",
    "DomainName" : "myAnotherddnsdomain.com",
    "Enabled" : true,
    "Interval" : 20000,
    "Properties" : [
        {
            "Name" : "ServiceUrl",
            "Value" : "http://ddnsUpdaterEndpointThatReturn.com"
        },
	    {
            "Name" : "HTTPVerb",
            "Value" : "Post"
        },
	    {
            "Name" : "UserName",
            "Value" : "MyUserName"
        },
	    {
            "Name" : "Password",
            "Value" : "MyPassword"
        },
	    {
            "Name" : "ContentType",
            "Value" : "application/json"
        }
    ],
    "WorkStrategy" : [
        {
            "StrategyName" : "StrategyECHO_IP",
            "Enabled" : true,
            "Properties" : [
                {
                    "Name" : "GETURL",
                    "Value" : "http://serviceThatRetrieveWanIPThatReturnAJsonObject"
                },
                {
                    "Name" : "PATHPROPERTIE",
                    "Value" : "object.myIPPropertie"
                },
                {
                    "Name" : "RETRY",
                    "Value" : 3
                },
                {
                    "Name" : "DELAY",
                    "Value" : 3000
                },
           ]
        },
        {
            "StrategyName" : "StrategyECHO_IP",
            "Enabled" : true,
            "Properties" : [
                {
                    "Name" : "GETURL",
                    "Value" : "http://serviceThatRetrieveWanIP2IfFirstFails"
                },
                {
                    "Name" : "RETRY",
                    "Value" : 3
                },
                {
                    "Name" : "DELAY",
                    "Value" : 3000
                }
           ]
        }
   ]
}
]
~~~

## PHP Echo Wan IP

If you want to host your own service that returns an IP address, you can create a **PHP file** with the following content.
~~~php
<?php
    header('Content-Type: application/json; charset=utf-8');
    $ip_address['origin'] = $_SERVER["REMOTE_ADDR"];
    echo json_encode($ip_address);
?>
~~~

## Features

- DDNS updater with different types of update strategies and update method.
- Activate strategies as necessary.
- Multiple update schedules

## Changelog

- v1.0 First public release


## TODO

- In planning