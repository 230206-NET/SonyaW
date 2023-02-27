
namespace Services;

public class EmployeeService {
    private readonly EmployeeDB _repo;

    public EmployeeService(EmployeeDB repo) {
        _repo = repo;
    }

    public List<Employee> GetAllEmployees() {
        return _repo.GetAllEmployees();
    }

    // display all employees associated with specified manager
    public List<Employee> GetAssociatedEmployees(string managerEmail) {
        return _repo.GetAssociatedEmployees(managerEmail);
    }

    public bool DoesEmployeeExist(string email) {
        return _repo.DoesEmployeeExist(email);
    }

    // create an employee account
    public int CreateEmployee(Employee newEmp) {
        return _repo.CreateEmployee(newEmp);
    }

    // edit profile
    public bool EditProfile(Employee editEmployee) {
        return _repo.EditProfile(editEmployee);
    }

    // delete employee data
    public bool DeleteEmployee(Employee emplToDelete) {
        return _repo.DeleteEmployee(emplToDelete);
    }

    // get one employee
    public Employee GetSingleEmployee(string email) {
        return _repo.GetSingleEmployee(email);
    }

 
}