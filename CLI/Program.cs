using StudentskaSluzba.DAO;
using StudentskaSluzba.Console;


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
