using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LINQ_CONSOLE_APP
{
    class Program
    {
        public static List<Employee> employees = new List<Employee>();
        public static List<Project> projects = new List<Project>();


        static void Main(string[] args)
        {
            IntializeEmployees();
            IntializeProjects();

            ///Filter out the employees whose names start with "T"
            //Where
            Console.WriteLine("Where in querySyntax------");
            var QuerySyntax = from employee in employees
                    where employee.EmployeeName.StartsWith("S")
                    select employee.EmployeeName;

            foreach (var item in QuerySyntax)
                Console.WriteLine(item);

            Console.WriteLine("Where in MethodSyntax------");
            var MethodSyntax = employees.Where(e => e.EmployeeName.StartsWith("V"));

            foreach (var item in MethodSyntax)
                Console.WriteLine(item);

            Console.WriteLine('\n');

            // order employees name by ascending
            Console.WriteLine("Order by ascending in querySyntax------");
            var QuerySyntax1 = from employee in employees
                               orderby employee.EmployeeName
                               select employee.EmployeeName;

            foreach (var item in QuerySyntax1)
                Console.WriteLine(item);

            Console.WriteLine("Order by ascending in methodSyntax------");
            var MethodSyntax1 = employees.OrderBy(o => o.EmployeeName);

            
            foreach (var item in MethodSyntax1)
                Console.WriteLine(item.EmployeeName);

            Console.WriteLine('\n');

            Console.WriteLine("Order by descending in querySyntax------");

            var QuerySyntax2 = from employee in employees
                               orderby employee.EmployeeName descending
                               select employee.EmployeeName;

            foreach(var item in QuerySyntax2)
                Console.WriteLine(item);

            Console.WriteLine("Order by descending in methodSyntax------");

            var MethodSyntax2 = employees.OrderByDescending(m => m.EmployeeName);

            foreach(var item in MethodSyntax2)
               Console.WriteLine(item.EmployeeName);

            //GROUP
            //Let’s group our data according to project id.
            var QuerySyntax10 = from employee in employees
                                group employee by employee.ProjectId;

            Console.WriteLine("Group in querySyntax------");
            foreach (var item in QuerySyntax10)
                Console.WriteLine(item.Key+" : "+item.Count());

            var MethodSyntax10 = employees.GroupBy(g => g.ProjectId);
            Console.WriteLine('\n');

            Console.WriteLine("Group in methodSyntax------");
            foreach (var item in MethodSyntax10)
                Console.WriteLine(item.Key + " : " + item.Count());

            //Let’s order all the entries by projectid ascending.
            Console.WriteLine("ThenBy Ascending QuerySyntax-----------------------");

            var QuerySyntax3 = from employee in employees
                               orderby employee.ProjectId , employee.EmployeeName
                               select employee;

            foreach(var item in QuerySyntax3)
                Console.WriteLine(item.EmployeeName+" : "+item.ProjectId);

            var MethodSyntax3 = employees.OrderBy(o => o.ProjectId).ThenBy(m => m.EmployeeName);

            Console.WriteLine("ThenBy Ascending MethodSyntax-----------------------");
            foreach (var item in MethodSyntax3)
                Console.WriteLine(item.EmployeeName+" : "+item.ProjectId);

            Console.WriteLine("ThenBy Descending-----------------------");

            var MehtodSyntax3 = employees.OrderByDescending(o => o.ProjectId).ThenBy(m => m.EmployeeName);
            foreach(var item in MehtodSyntax3)
                Console.WriteLine(item.EmployeeName+" : "+item.ProjectId);

        //Now, we want that for projectid 102, the employees should get ordered by name descending;
        //ie. Shubham should come above Bhushan. Let’s check out the syntax in both the types for the same.
            Console.WriteLine("ThenBy Descending-------------");

            var QuerySyntax4 = from employee in employees
                               orderby employee.ProjectId, employee.EmployeeName descending
                               select employee;

            foreach (var item in QuerySyntax3)
                Console.WriteLine(item.EmployeeName + " : " + item.ProjectId);

            Console.WriteLine("ThenBy Dscending----------------------");

            var MehtodSyntax4 = employees.OrderBy(o => o.ProjectId).ThenByDescending(e => e.EmployeeName);
            foreach (var item in MehtodSyntax4)
                Console.WriteLine(item.EmployeeName + " : " + item.ProjectId);

            //Take Operator
            //If we want to select a particular number of rows, we use the ‘Take’ method.
            //put the query syntax in a bracket and then use Take.

            Console.WriteLine("QuerySyntax Take Operator-----------------");
            var QuerySyntax5 = (from employee in employees select employee).Take(2) ;

            foreach(var item in QuerySyntax5)
                Console.WriteLine(item.EmployeeName);

            Console.WriteLine("Take Operator--------------");
            var MethodSyntax5 = employees.Take(2);

            foreach (var item in MethodSyntax5)
                Console.WriteLine(item.EmployeeName);

            //Skip : Similarly to the ‘TAKE’ operator we have the ‘SKIP’ operator.
            //When we use skip(2),
            //the query will skip the first two records from the result set and display the results.

            Console.WriteLine("QuerySyntax Skip Operator-------------");
            var QuerySyntax6 = (from employee in employees select employee).Skip(2);

            foreach(var item in QuerySyntax6)
                Console.WriteLine(item.EmployeeName);

            Console.WriteLine("MethodSyntax Skip Operator----------------");
            var MethodSyntax6 = employees.Skip(2);

            foreach (var item in MethodSyntax6)
                Console.WriteLine(item.EmployeeName);

            //Now suppose we want to display employees with their project name. This can be done with a join
            //JOINS
          
            var QuerySyntax7 = from employee in employees
                               join project in projects on employee.ProjectId equals project.ProjectId
                               select new { employee.EmployeeName, project.ProjectName };

            Console.WriteLine("QuerySyntax using Joins-----------");
            foreach (var item in QuerySyntax7)
                Console.WriteLine(item.EmployeeName + " : " + item.ProjectName);

          
            var MethodSyntax7 = employees.Join(projects, e => e.EmployeeName, p => p.ProjectName,
            (e, p) => new { e.EmployeeName, p.ProjectName });

            Console.WriteLine("MethodSyntax using Joins----------");
            foreach (var item in MethodSyntax7)
                Console.WriteLine(item.EmployeeName + " : " + item.ProjectName);

            
            var QuerySyntax8 = from employee in employees
                               join project in projects on employee.ProjectId equals project.ProjectId
                               into group1 from project in group1.DefaultIfEmpty()
                               select new { employee.EmployeeName, ProjectName = project?.ProjectName ?? "Null" };

            Console.WriteLine("Left Join in querySyntax------");
            foreach (var item in QuerySyntax8)
            {
                Console.WriteLine(item.EmployeeName + ":" + item.ProjectName);
            }

            Console.WriteLine("First");

            var QuerySyntax9 = (from employee in employees select employee).First();

            if(QuerySyntax9!=null)
                Console.WriteLine(QuerySyntax9.EmployeeName);
        }



        public static void IntializeEmployees()
        {
            employees.Add(new Employee
            {
                EmployeeId=1,
                EmployeeName = "Pratik",
                ProjectId = 103

            });

            employees.Add(new Employee
            {
                EmployeeId = 2,
                EmployeeName="Viraj",
                ProjectId = 101
            });

            employees.Add(new Employee
            {
                EmployeeId = 3,
                EmployeeName = "Shubham",
                ProjectId = 102
            });

            employees.Add(new Employee
            {
                EmployeeId = 3,
                EmployeeName = "Bhushan",
                ProjectId = 102
            });


        }
        public static void IntializeProjects()
        {
            projects.Add(new Project
            {
                ProjectId = 101,
                ProjectName = "ABC",
            });

            projects.Add(new Project
            {
                ProjectId = 102,
                ProjectName = "PQR"
            });

            projects.Add(new Project
            {
                ProjectId = 103,
                ProjectName = "XYZ"
            });
        }

      

      
        
    }
}
