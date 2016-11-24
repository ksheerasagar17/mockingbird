using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Web;

namespace mockingbird.Models
{
    /// <summary>
    /// Model to save and use to check all the incoming data.
    /// Each of the fields here are relevant to http request and response parameters in C#.
    /// We will need to use these fields to save http request and response in databaes.
    /// Later we will intercept each incoming request and check for the data in database for mock api response.s
    /// </summary>
    public class MockApiHttpData
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Path")]
        public string Path { get; set; }

        [DisplayName("Verb")]
        public RequestMethod Verb { get; set; }

        [DisplayName("Headers")]
        public string Headers { get; set; }

        [DisplayName("Response status")]
        public HttpStatusCode ResponseStatus { get; set; }

        [DisplayName("Content-Type")]
        public string ContentType { get; set; }

        [DisplayName("Content-Encoding")]
        public string ContentEncoding { get; set; }

        [DisplayName("Response body")]
        public string ResponseBody { get; set; }

        [DisplayName("Set response delay")]
        public string ResponseDelay { get; set; }

        [DisplayName("Status")]
        public ApiStatus ApiStatus { get; set; }

    }


    public enum ApiStatus
    {
        Started,
        Stopped,
        Active,
        Disabled
    }

    public enum RequestMethod
    {
        GET,
        HEAD,
        POST,
        PUT,
        PATCH,
        DELETE,
        OPTIONS,
        TRACE
    }

    public enum ApiHttpStatusCode
    {
        Continue = 100,        // Section 10.1.1: Continue
        SwitchingProtocols = 101,        // Section 10.1.2: Switching Protocols

        OK = 200,                // Section 10.2.1: OK
        Created = 201,        // Section 10.2.2: Created
        Accepted = 202,        // Section 10.2.3: Accepted
        NonAuthoritativeInformation = 203,    // Section 10.2.4: Non-Authoritative Information
        NoContent = 204,        // Section 10.2.5: No Content
        ResetContent = 205,    // Section 10.2.6: Reset Content
        PartialContent = 206,    // Section 10.2.7: Partial Content

        MultipleChoices = 300,        // Section 10.3.1: Multiple Choices
        MovedPermanently = 301,        // Section 10.3.2: Moved Permanently
        Found = 302,            // Section 10.3.3: Found
        SeeOther = 303,        // Section 10.3.4: See Other
        NotModified = 304,    // Section 10.3.5: Not Modified
        UseProxy = 305,        // Section 10.3.6: Use Proxy
        TemporaryRedirect = 307,        // Section 10.3.8: Temporary Redirect

        BadRequest = 400,        // Section 10.4.1: Bad Request
        Unauthorized = 401,    // Section 10.4.2: Unauthorized
        PaymentRequired = 402,        // Section 10.4.3: Payment Required
        Forbidden = 403,        // Section 10.4.4: Forbidden
        NotFound = 404,        // Section 10.4.5: Not Found
        MethodNotAllowed = 405,        // Section 10.4.6: Method Not Allowed
        NotAcceptable = 406,    // Section 10.4.7: Not Acceptable
        ProxyAuthenticationRequired = 407,    // Section 10.4.8: Proxy Authentication Required
        RequestTimeout = 408,    // Section 10.4.9: Request Time-out
        Conflict = 409,        // Section 10.4.10: Conflict
        Gone = 410,            // Section 10.4.11: Gone
        LengthRequired = 411,    // Section 10.4.12: Length Required
        PreconditionFailed = 412,    // Section 10.4.13: Precondition Failed
        RequestEntityTooLarge = 413,    // Section 10.4.14: Request Entity Too Large
        RequestUriTooLarge = 414,    // Section 10.4.15: Request-URI Too Large
        UnsupportedMediaType = 415,    // Section 10.4.16: Unsupported Media Type
        RequestedRangeNotSatisfiable = 416,    // Section 10.4.17: Requested range not satisfiable
        ExpectationFailed = 417,        // Section 10.4.18: Expectation Failed

        InternalServerError = 500,    // Section 10.5.1: Internal Server Error
        NotImplemented = 501,    // Section 10.5.2: Not Implemented
        BadGateway = 502,        // Section 10.5.3: Bad Gateway
        ServiceUnavailable = 503,    // Section 10.5.4: Service Unavailable
        GatewayTimeout = 504,        // Section 10.5.5: Gateway Time-out
        HttpVersionNotSupported = 505,        // Section 10.5.6: HTTP Version not supported
    }
}