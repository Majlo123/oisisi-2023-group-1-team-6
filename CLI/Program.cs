using StudentskaSluzba.Console;
using StudentskaSluzba.DAO;


namespace StudentskaSluzba;

class Program
{
    static void Main()
    {
        StudentsDAO students = new StudentsDAO();
        ConsoleView view = new ConsoleView();
        view.RunMenu();

    }
}
