using System;
using System.IO;
using RestSharp;
using System.Net;
using Newtonsoft.Json;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace RestApiTestTool
{
    class Program
    {
        static Method Convert(string value)
        {
            value = value.ToUpper();
            switch (value)
            {
                case "POST":
                    return Method.Post;
                case "GET":
                    return Method.Get;
                case "PUT":
                    return Method.Put;
                case "DELETE":
                    return Method.Delete;
                case "PATCH":
                    return Method.Patch;
                default:
                    return Method.Get;
            }
        }

        static bool Request(Method method, string uri, string dataJson, out string result)
        {
            var request = new RestRequest()
            {
                Method = method
            };
            if (!string.IsNullOrEmpty(dataJson))
            {
                request.AddParameter("application/json", dataJson, ParameterType.RequestBody);
                request.RequestFormat = DataFormat.Json;
            }
            var client = new RestClient(uri);
            var response = client.ExecuteAsync(request).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                result = response.Content.ToString();
                return true;
            }
            else
            {
                result = "HTTP Status: " + response.StatusCode + " " + response.StatusDescription
                         + "\r\nResponse:" + response.Content;
                return false;
            }
        }

        static void Main(string[] args)
        {
            bool success = true;
            try
            {
                string[] testFiles;
                if (args != null && args.Length == 1)
                {
                    testFiles = Directory.GetFiles(args[0]);
                }
                else 
                {
                    testFiles = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "\\TestSuit");
                }

                foreach (string testFile in testFiles)
                {
                    string testContent = File.ReadAllText(testFile);
                    XElement xTestCase = XElement.Parse(testContent);

                    ConLog.Info(new string('-', 64));
                    ConLog.Info("TestCase: " + Path.GetFileNameWithoutExtension(testFile));

                    int stepCounter = 1;
                    foreach (XElement xTestStep in xTestCase.Elements("step"))
                    {
                        string uri = xTestStep.Attribute("uri").Value;
                        Method method = Convert(xTestStep.Attribute("method").Value);
                        string requestJson = xTestStep.Element("request").Value;
                        string expectedJson = xTestStep.Element("expected").Value;
                        
                        ConLog.Alert("Request: " + xTestStep.Attribute("method").Value + " URI:" + uri);
                        
                        if (Request(method, uri, requestJson, out string result))
                        {
                            JObject jOExpected = JObject.Parse(expectedJson);
                            string expectedValue = JsonConvert.SerializeObject(jOExpected, Formatting.None);

                            JObject jOActual = JObject.Parse(result);
                            string actualValue = JsonConvert.SerializeObject(jOActual, Formatting.None);

                            if (expectedValue == actualValue)
                            {
                                ConLog.Success("Successfully step. Checked response.");
                            }
                            else
                            {
                                ConLog.Erro("Fail: response: ");
                                ConLog.Alert(actualValue);
                                ConLog.Erro("Step: " + stepCounter.ToString());
                                ConLog.Alert("Expected:\n" + expectedValue);
                                ConLog.Erro("end\n");
                                success = false;
                                break;
                            }
                        }
                        else
                        {
                            ConLog.Erro("API not response.");
                            success = false;
                            break;
                        }
                        stepCounter++;
                    }
                }
            }
            catch (Exception e)
            {
                ConLog.Erro("Exception: " + e.Message);
                success = false;
            }

            ConLog.Info("\nend execution.");

            if (success) ConLog.Success("All tests run successfully.");
            else ConLog.Erro("There were failures in the execution of tests.");

            Console.ReadKey();
        }
    }
}
