# DNSUpdater

DNSUpdater is an application that updates your IP address in DDNS services.
When configuring strategies to obtain the WAN IP address, if successful, the recovered address will be submitted for update.

The application has support for configuring different strategies in an update service and in case of failure, the next strategy is executed and so on until the execution is successful. In case of general failure, the retry is started from the beginning respecting the configuration retry and execution delay.

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
                    "Name" : "ServiceUrl",
                    "Value" : "http://serviceThatRetrieveWanIPThatReturnAJsonObject"
                },
                {
                    "Name" : "HTTPVerb",
                    "Value" : "Get"
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
            "Value" : "http://ddnsUpdaterEndpointThatHavePostFields.com"
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
                    "Name" : "ServiceUrl",
                    "Value" : "http://serviceThatRetrieveWanIPThatReturnAJsonObject"
                },
                {
                    "Name" : "HTTPVerb",
                    "Value" : "Get"
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
                    "Name" : "ServiceUrl",
                    "Value" : "http://serviceThatRetrieveWanIPThatReturnAJsonObject"
                },
                {
                    "Name" : "HTTPVerb",
                    "Value" : "Get"
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
},
{
    "ServiceName" : "UpdaterDDNSByHttpRequest",
    "DomainName" : "myAnotherddnsdomain.com",
    "Enabled" : true,
    "Interval" : 20000,
    "Properties" : [
        {
            "Name" : "ServiceUrl",
            "Value" : "http://ddnsUpdaterEndpointThatPostFieldsAndHeaders.com"
        },
	    {
            "Name" : "HTTPVerb",
            "Value" : "Post"
        },
	    {
            "Name" : "CustomField1",
            "Value" : "CustomValue1"
        },
	    {
            "Name" : "CustomField2",
            "Value" : "CustomField2"
        },
	    {
            "Name" : "ContentType",
            "Value" : "application/json"
        },
	    {
            "Name" : "RequestHeaders",
            "Value" : [
                {
                    "Name" : "HeaderAttribute1",
                    "Value" : "HeaderValue1"
                },
                {
                    "Name" : "HeaderAttribute2",
                    "Value" : "HeaderValue2"
                }
            ]
        }
    ],
    "WorkStrategy" : [
        {
            "StrategyName" : "StrategyECHO_IP",
            "Enabled" : true,
            "Properties" : [
                {
                    "Name" : "ServiceUrl",
                    "Value" : "http://serviceThatRetrieveWanIPThatReturnAJsonObject"
                },
                {
                    "Name" : "HTTPVerb",
                    "Value" : "Get"
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
                    "Name" : "ServiceUrl",
                    "Value" : "http://serviceThatRetrieveWanIPThatReturnAJsonObject"
                },
                {
                    "Name" : "HTTPVerb",
                    "Value" : "Get"
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
- v1.0.1 Add support for providing headers in requests and support for other HTTPs verbs (Post, Delete, Put, Patch) in the "StrategyECHO_IP" strategy.


## TODO

- In planning
