using System.Management.Automation;
using ServiceStack.Redis;
using System.Collections.Generic;
using System;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.Collections;

namespace PowerRedis2
{
    #region Server
    
    [Cmdlet(VerbsCommon.Get, "RedisInfo")]
    public class GetRedisInfoCommand : Cmdlet
    {
        protected override void BeginProcessing()
        {
            if (!Globals.IsConnected)
            {
                WriteError(new ErrorRecord(new RedisException("Not Connected"), "Not Connected", ErrorCategory.NotSpecified, null));
            }
        }

        protected override void ProcessRecord()
        {
            Dictionary<string, string> info = Globals.rc.Info;

            string json = @"{{
  ""prtg"": {{
    ""result"": [
      {{
        ""channel"": ""Connected Clients"",
        ""value"": ""{0}"",
        ""unit"": ""Count""
      }},
      {{
        ""channel"": ""Blocked Clients"",
        ""value"": ""{1}"",
        ""unit"": ""Count""
      }},
      {{
        ""channel"": ""Used Memory"",
        ""value"": ""{2}"",
        ""unit"": ""BytesMemory""
      }},
      {{
        ""channel"": ""Total Connections Received"",
        ""value"": ""{3}"",
        ""unit"": ""Count""
      }},
      {{
        ""channel"": ""Instantaneous Operations per Second"",
        ""value"": ""{4}"",
        ""unit"": ""Count""
      }},
      {{
        ""channel"": ""Expired Keys"",
        ""value"": ""{5}"",
        ""unit"": ""Count""
      }}
    ]
  }}
}}";
            string result = string.Format(json, info["connected_clients"]
  , info["blocked_clients"]
  , info["used_memory"]
  , info["total_connections_received"]
  , info["instantaneous_ops_per_sec"]
  , info["expired_keys"]);


            WriteObject(result);
        }

        protected override void EndProcessing()
        {

        }
    }

    #endregion

}
