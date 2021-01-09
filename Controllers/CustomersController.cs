using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proiect_Nohai.Models;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace Proiect_Nohai.Data
{
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly PharmacyContext _context;
        private string _baseUrl = "http://localhost:3734/api/Customers";

        public CustomersController(PharmacyContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index(string sortOrder, string searchString, int? pageNumber)
        {
            // var client = new HttpClient();
            //var response = await client.GetAsync(_baseUrl);

            //if (response.IsSuccessStatusCode)
            //{
            //  var customers = JsonConvert.DeserializeObject<List<Customer>>(await response.Content.ReadAsStringAsync());
            //return View(customers);
            //}
            //return NotFound();
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CurrentFilter"] = searchString;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = null;
            }

            var names = from b in _context.Customers select b;
            if (!String.IsNullOrEmpty(searchString))
            {
                names = names.Where(s => s.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    names = names.OrderByDescending(b => b.Name);
                    break;
                default:
                    names = names.OrderBy(b => b.Name);
                    break;
            }
            //return View(await names.AsNoTracking().ToListAsync());
            int pageSize = 4;
            return View(await PaginatedList<Customer>.CreateAsync(names.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            var client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}/{id.Value}");
            if (response.IsSuccessStatusCode)
            {
                var customer = JsonConvert.DeserializeObject<Customer>(
                await response.Content.ReadAsStringAsync());
                return View(customer);
            }
            return NotFound();
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerID,Name,Adress,Sex,BirthDate,Disease")] Customer customer)
        {
            if (!ModelState.IsValid) return View(customer);
            try
            {
                var client = new HttpClient();
                string json = JsonConvert.SerializeObject(customer);
                var response = await client.PostAsync(_baseUrl,
                new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to create record:{ ex.Message}");
            }
            return View(customer);

        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            var client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}/{id.Value}");
            if (response.IsSuccessStatusCode)
            {
                var customer = JsonConvert.DeserializeObject<Customer>(
                await response.Content.ReadAsStringAsync());
                return View(customer);
            }
            return new NotFoundResult();
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerID,Name,Adress,Sex,BirthDate,Disease")] Customer customer)
        {
            if (!ModelState.IsValid) return View(customer);
            var client = new HttpClient();
            string json = JsonConvert.SerializeObject(customer);
            var response = await client.PutAsync($"{_baseUrl}/{customer.CustomerID}",
            new StringContent(json, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            var client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}/{id.Value}");
            if (response.IsSuccessStatusCode)
            {
                var customer = JsonConvert.DeserializeObject<Customer>(await
               response.Content.ReadAsStringAsync());
                return View(customer);
            }
            return new NotFoundResult();
        }

        // POST: Customers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete([Bind("CustomerID")] Customer customer)
        {
            try
            {
                var client = new HttpClient();
                HttpRequestMessage request =
                new HttpRequestMessage(HttpMethod.Delete,
               $"{_baseUrl}/{customer.CustomerID}")
                {
                    Content = new StringContent(JsonConvert.SerializeObject(customer),
               Encoding.UTF8, "application/json")
                };
                var response = await client.SendAsync(request);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to delete record: { ex.Message} ");
            }
            return View(customer);
        }

    }
}
