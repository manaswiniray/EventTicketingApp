using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using EventTicketingApp;
using System.Text;
using System.Net.Http.Headers;

namespace EventTicketingWebApplication.Controllers
{
    public class Home_Controller : Controller
    {
        string BaseURL = "http://localhost:61452/";
        public async Task<IActionResult> GetAllEvents()
        {
            List<EventTicketingApp.EventTicket> eventList = new List<EventTicketingApp.EventTicket>();

            using(var client=new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = await client.GetAsync("api/EventTickets");
                if(responseMessage.IsSuccessStatusCode)
                {
                    var result = responseMessage.Content.ReadAsStringAsync().Result;
                    eventList = JsonConvert.DeserializeObject<List<EventTicketingApp.EventTicket>>(result);
                }
            }
            return View(eventList);
        }

        [HttpGet]
        public async Task<IActionResult> GetEvent(int id)
        {
            EventTicket eventTicket = new EventTicket();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = await client.GetAsync("api/EventTickets/"+id);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var result = responseMessage.Content.ReadAsStringAsync().Result;
                    eventTicket = JsonConvert.DeserializeObject<EventTicketingApp.EventTicket>(result);
                }
            }
            return View(eventTicket);
        }

        public IActionResult AddEvent()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> AddEvent(EventTicket eventTicket)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));


                var eventTicketJson = JsonConvert.SerializeObject(eventTicket);

                var content = new StringContent(eventTicketJson, Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = await client.PostAsync("api/EventTickets", content);


                if (responseMessage.IsSuccessStatusCode)
                {
                    ViewBag.msg = "Submitted";
                    ModelState.Clear();
                }
                else
                {
                    ViewBag.msg = "Some error happened";
                }
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateEventAsync(int id)
        {
            EventTicket eventTicket = new EventTicket();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = await client.GetAsync("api/EventTickets/" + id);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var result = responseMessage.Content.ReadAsStringAsync().Result;
                    eventTicket = JsonConvert.DeserializeObject<EventTicketingApp.EventTicket>(result);
                }
            }
            return View(eventTicket);
        }

        [HttpPost]
        public IActionResult UpdateEvent(EventTicket eventTicket)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage responseMessage = client.PutAsJsonAsync(BaseURL + "api/EventTickets/"+eventTicket.EventId, eventTicket).Result;


                if (responseMessage.IsSuccessStatusCode)
                {
                    ViewBag.msg = "Updated";
                    ModelState.Clear();
                }
                else
                {
                    ViewBag.msg = "Some error happened";
                }
                return View();
            }
        }

        //doubt

        [HttpGet]
        public async Task<IActionResult> DeleteEventAsync(int id)
        {
            EventTicket eventTicket = new EventTicket();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = await client.GetAsync("api/EventTickets/" + id);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var result = responseMessage.Content.ReadAsStringAsync().Result;
                    eventTicket = JsonConvert.DeserializeObject<EventTicketingApp.EventTicket>(result);
                }
            }
            return View(eventTicket);
        }

        [HttpDelete]
        public IActionResult DeleteEvent(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage responseMessage = client.DeleteAsync(BaseURL + "api/EventTickets/" +id).Result;


                if (responseMessage.IsSuccessStatusCode)
                {
                    ViewBag.msg = "Deleted";
                    ModelState.Clear();
                }
                else
                {
                    ViewBag.msg = "Some error happened";
                }
                return View();
            }
        }

    }
}
