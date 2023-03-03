using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Models;
using Services;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

    // Expense Service
    builder.Services.AddScoped<TicketDB>();
    builder.Services.AddScoped<TicketService>();
    // Employee Service
    builder.Services.AddScoped<EmployeeDB>();
    builder.Services.AddScoped<EmployeeService>();
    // Manager Service
    builder.Services.AddScoped<ManagerDB>();
    builder.Services.AddScoped<ManagerService>();

    // builder.Services.AddControllers();
    // builder.Services.AddDbContext<ExpenseDB>();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    bool loggedin = false;
    Employee EmplLoggedIn = null;
    Manager ManagerLoggedIn = null;

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// app.Urls.Add("http://localhost:5268/");

string uriLink = "http://localhost:5268/";


// ----------------------------------------------------

app.MapGet("/", () => { 
    loggedin = false;
    return " Expense Reimbursement Application\n***************************************\n Log in or create an account"; 
} );

app.MapPost("create", (EmployeeService employeeService, [FromBody] Employee empl) => {
    return employeeService.CreateEmployee(empl) == null ? "That email is already registered" : "Account has been created successfully";
});

// create account


// ----------------------- EMPLOYEE HTTP REQUESTS -----------------------------

// get all employees
app.MapGet("empl", (EmployeeService _emplService) => {
    loggedin = false;
    EmplLoggedIn = null;
    ManagerLoggedIn = null;

    List<Employee> AllEmpl = _emplService.GetAllEmployees();
    if(AllEmpl != null) return Results.Ok(AllEmpl);
    return Results.NotFound();
});

// login to employee
app.MapGet("empl/login", ([FromQuery] string email, [FromQuery] string password, [FromServices] EmployeeService emplService) => {
    ManagerLoggedIn = null;
    Employee emplLogIn = emplService.EmplLogIn(email, password);
    if(emplLogIn != null){
        EmplLoggedIn = emplLogIn;
        loggedin = true;
        return Results.Ok(EmplLoggedIn);
    }
    return Results.NotFound("Employee credentials invalid.");
});

// get all of an employee's expenses 
app.MapGet("empl/login/tickets", ([FromServices] EmployeeService emplService, [FromServices] TicketService ticketService) => {
    if(loggedin && EmplLoggedIn != null){
        List<Expense> expenses = ticketService.GetExpenseForEmpl(EmplLoggedIn.email);
        return Results.Ok(expenses);
    }
    return Results.Unauthorized();
});

// create a ticket
app.MapPost("empl/login/tickets/create", ([FromBody] Expense expense, [FromServices] TicketService ticketService) => {
    if(loggedin && EmplLoggedIn != null){
        return Results.Ok(ticketService.CreateExpense(expense, EmplLoggedIn));
    }
    return Results.Unauthorized();
});
 //new Uri(uriLink + "empl/login/tickets/create"), 
 
// delete a ticket
app.MapPut("empl/login/tickets/delete", ([FromQuery] int idNum, [FromServices] TicketService ticketService) => {
    if(loggedin && EmplLoggedIn != null) {
        bool success = ticketService.DeleteExpense(idNum);
        if(success) return Results.Ok("Ticket has been deleted");
        else return Results.NotFound("Ticket not found"); // Status 404
    }
    return Results.Unauthorized();
});

// edit profile
app.MapPut("empl/login/edit", ([FromBody] Employee emplToEdit, EmployeeService emplService) => {
    if(loggedin && EmplLoggedIn != null && emplToEdit.email == EmplLoggedIn.email) {
        return Results.Ok(emplService.EditProfile(emplToEdit));
    }
    return Results.Unauthorized();
});



// ---------------------- MANAGER HTTP REQUESTS ------------------------------

// display all managers
app.MapGet("mngr/", (ManagerService _mngrService) => {
    loggedin = false;
    EmplLoggedIn = null;
    ManagerLoggedIn = null;

    List<Manager> AllMngr = _mngrService.GetAllManagers();
    if(AllMngr != null) return Results.Ok(AllMngr);
    return Results.NotFound();
});

// // does manager exist
// app.MapGet("mngr/system", (ManagerService mngrService, [FromQuery] string email) => {
//     if(mngrService.DoesManagerExist(email)) {
//         return Results.Ok();
//     }
//     return Results.NotFound();
// });

// manager log in
app.MapGet("mngr/login", ([FromQuery] string email, [FromQuery] string password, ManagerService managerService) => {
    EmplLoggedIn = null;
    Manager mngrLogIn = managerService.ManagerLogIn(email, password);
    if(mngrLogIn != null){
        ManagerLoggedIn = mngrLogIn;
        loggedin = true;
        return Results.Ok(ManagerLoggedIn);
    }
    return Results.NotFound("Manager credentials invalid.");
});

app.MapGet("mngr/viewAll", ([FromServices] TicketService ticketService) => {
    if(loggedin && ManagerLoggedIn != null) {
        return Results.Ok(ticketService.ManagersPendingTickets(ManagerLoggedIn.email));
    }
    return Results.Unauthorized();
});

// view all employees
app.MapGet("mngr/login/empl", (EmployeeService emplService) => {
    if(loggedin && ManagerLoggedIn != null) {
        return Results.Ok(emplService.GetAssociatedEmployees(ManagerLoggedIn.email));
    }
    return Results.Unauthorized();
});

// view all of an employee's tickets
app.MapGet("mngr/login/empl/tickets", ([FromServices] TicketService ticketService, [FromQuery] string email) => {
    if(loggedin && ManagerLoggedIn != null) {
        return Results.Ok(ticketService.GetExpenseForEmpl(email));
    }
    return Results.Unauthorized();
});

// accept employee ticket: MAKE SURE the idNum being passed in is a ticket under an employee under this manager
app.MapPut("mngr/login/empl/tickets/accept", ([FromQuery]int idNum, [FromServices] TicketService ticketService) => {
    if(loggedin && ManagerLoggedIn != null) return Results.Ok(ticketService.AcceptTicket(idNum));
    return Results.Unauthorized();
});

// reject employee ticket
app.MapPut("mngr/login/empl/tickets/reject", ([FromQuery]int idNum, [FromServices] TicketService ticketService) => {
    if(loggedin && ManagerLoggedIn != null) return Results.Ok(ticketService.RejectTicket(idNum));
    return Results.Unauthorized();
});

// remove an employee
app.MapPut("mngr/login/empl/remove", ([FromBody] Employee emplToRemove, [FromServices] EmployeeService employeeService) => {
    if(loggedin && ManagerLoggedIn != null) return Results.Ok(employeeService.DeleteEmployee(emplToRemove));
    return Results.Unauthorized();
});

// edit profile
app.MapPut("mngr/login/edit", ([FromBody] Manager mngrToEdit, ManagerService managerService) => {
    if(loggedin && ManagerLoggedIn != null && mngrToEdit.email == ManagerLoggedIn.email) {
        return Results.Ok(managerService.EditProfile(mngrToEdit));
    }
    return Results.Unauthorized();
});



// ----------------------------------------------------

app.Run();
