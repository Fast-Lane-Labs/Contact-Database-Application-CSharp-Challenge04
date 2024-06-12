using ContactApplication.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ContactApplication.Controllers
{
    public class UserController : Controller
    {
        
        public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User> 
        { 
            new User 
            {
                Id = 1,
                Name = "John Doe",
                Email = "John.Doe@fastlane-israel.com"
            } 
        };
        // GET: User
        public ActionResult Index()
        {
            // Implement the Index method here
            return View(userlist);
        }
 
        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            // Implement the details method here
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
 
        // GET: User/Create
        public ActionResult Create()
        {
            //Implement the Create method here
            return View();

        }
 
      // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            // Implement the Create method (POST) here
            if (ModelState.IsValid)
            {
                userlist.Add(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }
 
        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            // This method is responsible for displaying the view to edit an existing user with the specified ID.
            // It retrieves the user from the userlist based on the provided ID and passes it to the Edit view.
            
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
 
        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User user)
        {
            // This method is responsible for handling the HTTP POST request to update an existing user with the specified ID.
            // It receives user input from the form submission and updates the corresponding user's information in the userlist.
            // If successful, it redirects to the Index action to display the updated list of users.
            // If no user is found with the provided ID, it returns a NotFoundResult.
            // If an error occurs during the process, it returns the Edit view to display any validation errors.
        
            var userInDb = userlist.FirstOrDefault(u => u.Id == id);
            if (userInDb == null)
            {
                return NotFound();
            }

            userInDb.Name = user.Name;
            userInDb.Email = user.Email;

            return RedirectToAction("Index");
        }
 
        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            // Implement the Delete method here
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
 
        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            // Implement the Delete method (POST) here
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            userlist.Remove(user);
            
            return RedirectToAction("Index");
        } 


        // GET: User/ExportUsersToCSV
        public IActionResult ExportUsersToCSV()
        {
        var users = userlist;
        var builder = new StringBuilder();
        builder.AppendLine("Name,Email");

        foreach (var user in users)
        {
            builder.AppendLine($"{user.Name},{user.Email}");
        }

        return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "Users.csv");
    }

    // GET: User/ImportUsersFromCSV
    public ActionResult ImportUsersFromCSV()
    {
        return View();
    }
    
    // POST: User/ImportUsersFromCSV
    [HttpPost]
    public ActionResult ImportUsersFromCSV(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return Content("Please select a CSV file to import.");
        }

        using (var reader = new StreamReader(file.OpenReadStream()))
        {
            // Skip the first row
            reader.ReadLine();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');

                var user = new User
                {
                    Name = values[0],
                    Email = values[1]
                };

                userlist.Add(user);
            }
        }

        return RedirectToAction("Index");
    }
   
    }


}
