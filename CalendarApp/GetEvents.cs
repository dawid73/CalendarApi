using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CalendarApp
{
    class GetEvents
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/calendar-dotnet-quickstart.json
        static string[] Scopes = { CalendarService.Scope.Calendar };
        static string ApplicationName = "Google Calendar API .NET Quickstart";

        static void Main(string[] args)
        {

           


            UserCredential credential;

            using (var stream =
                new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/calendar-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                //Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 3;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            //var newEventRequest = service.Events.Insert(calEvent, "primary");
            //newEventRequest.Execute();

            //Lista przechowująca zdarzenia
           // List<string> googleEvents = new List<string>();
            string[,] googleEventsTab = new string[2, 3];
            int tabStart = 0;

            // List events.
            Events events = request.Execute();
            Console.WriteLine("Natchodzące wydarzenia:");
            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    string when = eventItem.Start.DateTime.ToString();
                    string a = eventItem.End.DateTime.ToString();
                    
                    if (String.IsNullOrEmpty(when))
                    {
                        when = eventItem.Start.Date;
                        a = eventItem.End.Date;
                    }
                    String ev = eventItem.Summary + " " + when + a;
                    Console.WriteLine(ev);
                    //googleEvents.Add(ev);
                    googleEventsTab[0, tabStart] = eventItem.Summary;
                    googleEventsTab[1, tabStart] = when + " - " + a;
                    tabStart++;
                }
            }
            else
            {
                Console.WriteLine("No upcoming events found.");
            }



            //create pdf - TEST
            GeneratePDF generatePDF = new GeneratePDF();
            generatePDF.pdfCreate(googleEventsTab);



            Console.Read();

        }
    }
}