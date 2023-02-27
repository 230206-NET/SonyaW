
namespace Services;

public class ManagerService {

    private readonly ManagerDB _repo;

    public ManagerService(ManagerDB repo) {
        _repo = repo;
    }

    public bool DoesManagerExist(string email) {
        return _repo.DoesManagerExist(email);
    }
    // display all managers

    public Manager GetSingleManager(string email) {
        return _repo.GetSingleManager(email);
    }

    public List<Manager> GetAllManagers() {
        return _repo.GetAllManagers();
    }

    public Manager PromoteEmployee(Employee empl) {
        return _repo.PromoteEmployee(empl);
    }

    // public Employee DemoteManager(Manager mng, string mngEmail) {
    //     return _repo.DemoteManager(mng, mngEmail);
    // }

    public bool EditProfile(Manager mng) {
        return _repo.EditProfile(mng);
    }

}