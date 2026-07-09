using CoreDotnet.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace CoreDotnet.Controllers
{
    public class SupportController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public SupportController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult CreateTicket()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateTicket(CreateTicketViewModel createTicketViewModel)
        {
            //if (ModelState.IsValid)
            //{
                // Save the ticket to the database or perform any other necessary actions
                // For example, you can use a service to save the ticket
                // await _ticketService.CreateTicketAsync(createTicketViewModel);
                // Redirect to a success page or display a success message
                var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
                createTicketViewModel.Userid = userid;
                var client = _httpClientFactory.CreateClient();
                var response = await client.PostAsJsonAsync("https://localhost:7263/api/Tickets/Create", createTicketViewModel);
                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        // Ticket created successfully
                        return RedirectToAction("TicketsList"); // Redirect to a success page
                    }
                    else
                    {
                        // Handle error response from the API
                        ModelState.AddModelError(string.Empty, "An error occurred while creating the ticket.");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while creating the ticket.");
                }
                return View(createTicketViewModel);
            //}
            return View(createTicketViewModel);
        }

        public async Task<IActionResult> TicketsList()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7263/api/Tickets/GetTickets");
            if (response.IsSuccessStatusCode)
            {
                var tickets = await response.Content.ReadFromJsonAsync<List<CreateTicketViewModel>>();
                return View(tickets);
            }
            else
            {
                // Handle error response from the API
                ModelState.AddModelError(string.Empty, "An error occurred while retrieving the tickets.");
                return View(new List<CreateTicketViewModel>());
            }
        }
        public async Task<IActionResult> Edit(int Id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7263/api/Tickets/{Id}");
            if (response.IsSuccessStatusCode)
            {
                var tickets = await response.Content.ReadFromJsonAsync<CreateTicketViewModel>();
                return View(tickets);
            }
            else
            {
                // Handle error response from the API
                ModelState.AddModelError(string.Empty, "An error occurred while retrieving the tickets.");
                return View(new List<CreateTicketViewModel>());
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CreateTicketViewModel createTicketViewModel)
        {
            //if (ModelState.IsValid)
            //{
            // Save the ticket to the database or perform any other necessary actions
            // For example, you can use a service to save the ticket
            // await _ticketService.CreateTicketAsync(createTicketViewModel);
            // Redirect to a success page or display a success message
            var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
            createTicketViewModel.Userid = userid;
            var client = _httpClientFactory.CreateClient();
            var response = await client.PutAsJsonAsync($"https://localhost:7263/api/Tickets/{ createTicketViewModel.Id}", createTicketViewModel);
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    // Ticket created successfully
                    return RedirectToAction("TicketsList"); // Redirect to a success page
                }
                else
                {
                    // Handle error response from the API
                    ModelState.AddModelError(string.Empty, "An error occurred while creating the ticket.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the ticket.");
            }
            //return View(createTicketViewModel);
            //}
            return View(createTicketViewModel);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:7263/api/Tickets/{Id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("TicketsList");
            }
            else
            {
                // Handle error response from the API
                ModelState.AddModelError(string.Empty, "An error occurred while retrieving the tickets.");
                return View(new List<CreateTicketViewModel>());
            }
        }
    }
}
