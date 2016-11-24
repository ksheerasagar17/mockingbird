# Synopsis

Mockingbird is a simple ASP.Net web application to help developer’s setup mock endpoints quickly for development and testing purposes. After deploying this ASP.Net website on a web server; developers can quickly create new RESTful api endpoints which spit out the same response as original endpoint. These are useful especially when downstream applications are down because of maintenance or in case of any other failures.

# Usage example

## Register Interception handler in WebApiConfig ##

Handler **MockRequestsInterceptor** does the interception for every requests that comes into this application with the exception for portal. Portal requests are routed even before webapi and are sent to MVC controller to enable adding new endpoints.

```csharp
	// Web API configuration and services
    config.MessageHandlers.Add(new MockRequestsInterceptor());
```

## Interception handler ##

**Getting** endpoints from datasbase those are started (Active) and request payload matches that of mocked uri and VERB in database

```csharp
    var mockEndpointApiData = _db.MockApiHttpDatas.FirstOrDefault(x => x.ApiStatus == ApiStatus.Started
        && x.Verb.ToString() == incomingRequestData.RequestMethod.ToString()
        && x.Path == incomingRequestData.RequestUriPathAndQuery);
```


## Create new api endpoint and consume ##

![](Createnewapiendpoint.png)

## Mock api endpoints ##

![](mockapiendpoints.png)

# Motivation

Working in a Multi-tent architectures i have found myself blocked by unavailable downstream api endpoints due to maintenance and/or other issues in production and develop environments. Not able to do a complete round of integration testing because of **blockers** prompted me to cook up this simple web application. I found mocking useful especially in companies with Hybrid SaaS infrastructures where applications are spread over in Cloud and OnPremise. By mocking OnPremise application endpoints, i was able to **bridge** the broken connectivity to unblock myself. I was also blindsided by frequent api changes on dependency development servers. It is quick to mock up a endpoint and continue with the rest of the development tasks.

# Installation

You can download this asp.net web application and publish it to a web server as it is.

# License

GENERAL PUBLIC LICENSE
