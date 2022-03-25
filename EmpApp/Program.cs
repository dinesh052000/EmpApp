/*Employee application
DESCRIPTION:Details of Employee Application
DATE:16/10/2021
AUTHOR:DINESH KUMAAR B
MODIFIED ON:23/12/2021 TIME:3:00 PM
REVISED BY: Jaya Ethiraj, Akshaya Rajagopal
*/
using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Data.SqlClient;


namespace EmployeeDetails
{
    class Employee
    {
        private string EmployeeName;
        private string EmployeeID;
        private string EmployeeEmail;
        private string EmployeePhone;
        private DateTime DOB;
        private DateTime DOJ;
        List<string> EmpName = new List<string>();
        List<string> EmpID = new List<string>();
        List<string> EmpMailID = new List<string>();
        List<string> EmpPhoneNo = new List<string>();
        List<DateTime> EmpDOB = new List<DateTime>();
        List<DateTime> EmpDOJ = new List<DateTime>();

        public void Select()
        {
        SwitchStatement:
            Console.WriteLine("Choose the number to select the task given");
            Console.WriteLine("1.Adding Employee details\n" +
                              "2.Displaying Employee details\n" +
                              "3.Updating Employee details\n" +
                              "4.Deleting Employee details\n");
            int option = Convert.ToInt32(Console.ReadLine());
            switch (option)
            {
                case 1:
                    Details();
                    break;
                case 2:
                    Display();
                    break;
                case 3:
                    Update();
                    break;
                case 4:
                    Delete();
                    break;
                default:
                    Console.WriteLine("Please select the option from 1 to 4");
                    goto SwitchStatement;

            }
        }


        public void Details()
        {
            int NoOfEmployee;
            Console.WriteLine("Enter the number of employee details going to be entered");
            NoOfEmployee = Convert.ToInt32(Console.ReadLine());
            for (int EmployeeNo = 0; EmployeeNo < NoOfEmployee; EmployeeNo++)
            {
                EmployeeNameDetails();
                EmpName.Add(EmployeeName);
                EmployeeIDDetails();
                EmpID.Add(EmployeeID);
                EmployeeEmailDetails();
                EmpMailID.Add(EmployeeEmail);
                EmployeeNumberDetails();
                EmpPhoneNo.Add(EmployeePhone);
                EmployeeDOBDetails();
                EmpDOB.Add(DOB);
                EmployeeDOJDetails();
                EmpDOJ.Add(DOJ);

                Console.WriteLine("Connecting....\n");
                SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DIN;Integrated Security=True");
                try
                {


                    connection.Open();


                    if (connection.State == System.Data.ConnectionState.Open)
                    {

                        SqlCommand command = new SqlCommand("insert into Table_2(EmployeeID,EmployeeName,EmployeeMailID,EmployeePhoneNo,EmployeeDOB,EmployeeDOJ)" +
                            "values" + "('" + EmployeeID + "','" + EmployeeName + "','" + EmployeeEmail + "','" + EmployeePhone + "','" + DOB.ToString("yyyy-MM-dd") + "','" + DOJ.ToString("yyyy-MM-dd") + "')", connection);

                        command.ExecuteNonQuery();

                        Console.WriteLine("Insertion successful");


                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    connection.Close();
                    Select();
                }


            }
        }
           public void EmployeeNameDetails()
            {
            try
            {
                Console.WriteLine("ENTER THE EMPLOYEE NAME(First letter should be capital): ");
                EmployeeName = Console.ReadLine();//Gets Employee name input
              
                
                Regex Name = new Regex(@"[^A-Za-z]+$");
                Regex invalidName = new Regex(@"\b([A-Za-z]{4,})\b");

                bool hasSpecialChars = Name.IsMatch(EmployeeName.ToString());
                bool invalid = invalidName.IsMatch(EmployeeName.ToString());
                Console.WriteLine(invalid);

                if (string.IsNullOrEmpty(EmployeeName) == true || hasSpecialChars == true)//Checks is number or other characters
                {                                                            //Checks whether the input given is null or not
                    Console.WriteLine("Please enter a valid character(Don't enter any special character)\n");//If condition is false returns Error message
                    EmployeeNameDetails();
                }
                


                else if (EmployeeName.Length < 3)
                {
                        Console.WriteLine("Name character shuld not be less than 3");
                        EmployeeNameDetails();
                }

                else if(invalid == false)
                {
                    Console.WriteLine("Don't write repeated characters more than 4 times");
                }

                }
            

            catch (SystemException ex)
            {
                Console.WriteLine(ex);
            }

            }

            public void EmployeeIDDetails()
            {

                try
                {
                    Console.WriteLine("Enter the Employee ID: ");
                    EmployeeID = Console.ReadLine();
                    Regex ID = new Regex("(ACE)[0-9]{4}");//Used to validate correct ID Format
                    bool IsvalidID = ID.IsMatch(EmployeeID);


                    if (IsvalidID != true || EmployeeID == "ACE0000" || EmployeeID.Length > 7)

                    {
                        Console.WriteLine("Invalid enter in the correct format(ACE____)");
                        EmployeeIDDetails();
                    }


                }

                catch (SystemException ex)
                {
                    Console.WriteLine(ex);
                }
            }

            public void EmployeeEmailDetails()
            {
                try
                {
                    Console.WriteLine("ENTER THE EMPLOYEE EMAIL: ");
                    EmployeeEmail = Console.ReadLine();
                    Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                    RegexOptions.CultureInvariant | RegexOptions.Singleline);
                    bool isValidEmail = regex.IsMatch(EmployeeEmail);//Used to Validate Email
                    if (!isValidEmail)
                    {
                        Console.WriteLine($"The email is invalid Please enter correct format");
                        EmployeeEmailDetails();
                    }

                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

           public void EmployeeNumberDetails()
            {
                try
                {
                    Console.WriteLine("ENTER THE PHONE NUMBER :");

                    EmployeePhone = Console.ReadLine();

                    Regex r = new Regex("^[6-9][0-9]{9}$");
                    bool isValidPhone = r.IsMatch(EmployeePhone);
                    if (!isValidPhone)
                    {
                        Console.WriteLine($"Invalid Please enter the correct 10 digit number and First Number should start with (6-9) \n");
                        EmployeeNumberDetails();
                    }
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

           public void EmployeeDOBDetails()
            {

            DOBStatement:
                Console.Write("ENTER THE EMPLOYEE DATE OF BIRTH(MM/dd/yyyy): ");
                try
                {
                    DOB = DateTime.Parse(Console.ReadLine());
                    int age = 0;
                    age = DateTime.Now.Year - DOB.Year;//Used to calcualte age
                    age = age - 1;
                    if (age < 18 || age>60)
                    {
                        Console.WriteLine("The age should be between 18-60");
                        goto DOBStatement;
                    }
                }

                catch (SystemException)
                {
                    SystemException systemException = new SystemException();
                    Console.Write("Enter a correct Date format (e.g. 10/22/1987): \n");
                    EmployeeDOBDetails();
                }
            }

            public void EmployeeDOJDetails()
            {
            DOJStatement:
                Console.Write("ENTER THE EMPLOYEE DATE OF JOINING(MM/dd/yyyy): ");
                try
                {
                    DOJ = DateTime.Parse(Console.ReadLine());


                    if (DOJ > DateTime.Now)
                    {
                        Console.WriteLine("Should not enter Future dates");
                        goto DOJStatement;
                    }

                }

                catch (SystemException e)
                {
                    SystemException systemException = new SystemException();
                    Console.Write("Enter a correct Date format (e.g. 10/22/1987): \n", e);
                    EmployeeDOJDetails();
                }
                  
        }

            

        

        public void Update()
        {
            int choose;
            Console.Write("Please select which detail you want to update\n");
            Console.Write("1)Update the Phone number in the console \n");
            Console.Write("2)Update the Employee details in SQL Server \n");
            choose = Convert.ToInt32(Console.ReadLine());

            switch (choose)
            {
                case 1:
                    try
                    {
                        Console.WriteLine("Enter the Employee ID");

                        int phone = EmpID.IndexOf(Console.ReadLine());
                        Console.Write("Now write the new phone number");
                        EmpPhoneNo[phone] = Console.ReadLine();
                        Regex r = new Regex("^[6-9][0-9]{9}$");//Used to check the phone number
                        bool isValidPhone = r.IsMatch(EmpPhoneNo[phone]);
                        if (!isValidPhone)
                        {
                            Console.WriteLine("Enter the 10 digit Number");
                            goto case 1;
                        }


                    }

                    catch (ArgumentOutOfRangeException e)
                    {
                        Console.WriteLine("PrimaryKey(ID) should not be empty", e);
                        goto case 1;
                    }

                    break;

                case 2:
                    Console.WriteLine("Connecting....");
                    SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DIN;Integrated Security=True");
                    try
                    {
                        con.Open();
                        if (con.State == System.Data.ConnectionState.Open)
                        {
                            string NewEmployeeID;
                            string NewEmployeeName;
                            string NewEmployeeEmail;
                            string NewEmployeePhone;
                            DateTime NewDOB;
                            DateTime NewDOJ;


                            
                            EmployeeIDDetails();
                            NewEmployeeID = EmployeeID;

                            
                            EmployeeNameDetails();
                            NewEmployeeName = EmployeeName;

                            
                            EmployeeEmailDetails();
                            NewEmployeeEmail = EmployeeEmail;

                            
                            EmployeeNumberDetails();
                            NewEmployeePhone = EmployeePhone;
                            
                            EmployeeDOBDetails();
                            NewDOB = DOB;

                            EmployeeDOJDetails();
                            NewDOJ = DOJ;

                            SqlCommand cmd = new SqlCommand("UPDATE Table_2 SET EmployeeName = '" + NewEmployeeName + "'  WHERE EmployeeID = '" + EmployeeID + "'", con);
                            SqlCommand cmd2 = new SqlCommand("UPDATE Table_2 SET EmployeeMailID = '" + NewEmployeeEmail + "' WHERE EmployeeID = '" + EmployeeID + "'", con);
                            SqlCommand cmd3 = new SqlCommand("UPDATE Table_2 SET EmployeePhoneNo = " + NewEmployeePhone + " WHERE EmployeeID = '" + EmployeeID + "'", con);
                            SqlCommand cmd4 = new SqlCommand("UPDATE Table_2 SET EmployeeDOB = '" + NewDOB + "' WHERE EmployeeID = '" + EmployeeID + "'", con);
                            SqlCommand cmd5 = new SqlCommand("UPDATE Table_2 SET EmployeeDOJ = '" + NewDOJ + "' WHERE EmployeeID = '" + EmployeeID + "'", con);
                            cmd.ExecuteNonQuery();
                            cmd2.ExecuteNonQuery();
                            cmd3.ExecuteNonQuery();
                            cmd4.ExecuteNonQuery();
                            cmd5.ExecuteNonQuery();
                            Console.WriteLine("Data Upadated successfully");

                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    finally
                    {
                        con.Close();
                    }

                    break;



                default:
                    Console.WriteLine("Choose the right option");
                    break;
            }
            Select();


        }

        public void Delete()
        {
            int choose;
            Console.Write("Please select which detail you want to Delete\n");
            Console.Write("1)Delete entire Table \n");
            choose = Convert.ToInt32(Console.ReadLine());

            switch (choose)
            {
                case 1:
                    try
                    {
                        Console.WriteLine("Enter the Employee ID");

                        //int phone = EmpID.IndexOf(Console.ReadLine());

                        //Console.Write("The phone number that previously stored is " + EmpPhoneNo[phone] + "\n");
                        //Console.Write("Now write the phone number to be removed from the list");
                        //EmpName.Remove(Console.ReadLine());
                        EmployeeID = Console.ReadLine();
                        SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DIN;Integrated Security=True");
                        con.Open();
                        if (con.State == System.Data.ConnectionState.Open)
                        {
                            SqlCommand cmd = new SqlCommand("DELETE FROM Table_2 WHERE EmployeeID='" + EmployeeID + "'", con);

                            cmd.ExecuteNonQuery();

                            Console.WriteLine("Successfully Deleted");
                            con.Close();
                        }
                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine("PrimaryKey(ID) should not be empty", ex);

                        goto case 1;
                    }

                    break;
            

                default:
                    Console.WriteLine("Choose the right option");
                    break;
            }
            Select();


        }


        public void Display()
        {
        ChooseStatement:

            int choose;
            Console.Write("Please select which OUTPUT detail you want to display\n");
            Console.Write("1) Display all the Employee details in the list\n" +
                          "2) Display from the SQL server \n");
            choose = Convert.ToInt32(Console.ReadLine());

            switch (choose)
            {
                case 1:
                    Console.Write("There are " + EmpID.Count + " employees" + "\n");


                    for (int start = 0; start < EmpID.Count; start++)
                    {
                        Console.WriteLine("EmployeeName: " + EmpName[start] + "\n");
                        Console.WriteLine("EmployeeID: " + EmpID[start] + "\n");
                        Console.WriteLine("EmployeeEmailID: " + EmpMailID[start] + "\n");
                        Console.WriteLine("EmployeePhone: " + EmpPhoneNo[start] + "\n");
                        Console.WriteLine("EmployeeDOB: " + EmpDOB[start].ToString("MM/dd/yyyy") + "\n");
                        Console.WriteLine("EmployeeDOB: " + EmpDOJ[start].ToString("MM/dd/yyyy") + "\n");


                    }
                    break;
                case 2:
                    SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DIN;Integrated Security=True");
                    connection.Open();
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        SqlCommand command = new SqlCommand("SELECT * FROM TABLE_2", connection);
                        SqlDataReader dataReader=command.ExecuteReader();
                        Console.WriteLine("Displaying....");
                        while(dataReader.Read())
                        {
                            Console.WriteLine("EmployeeID: " + dataReader.GetString(0));
                            Console.WriteLine("EmployeeName: " + dataReader.GetString(1));
                            Console.WriteLine("EmployeeMailID: " + dataReader.GetString(2));
                            Console.WriteLine("EmployeePhoneNo: " + dataReader.GetString(3));
                            Console.WriteLine("EmployeeDOB: " + dataReader.GetDateTime(4).ToString("yyyy-MM-dd"));
                            Console.WriteLine("EmployeeDOJ: " + dataReader.GetDateTime(5).ToString("yyyy-MM-dd"));
                        }
                        dataReader.Close();
                        connection.Close();
                        
                        
                    }

                    break;
                default:
                    Console.WriteLine("Please select the correct option");
                    goto ChooseStatement;
            }

            Select();

        }
        public static void Main(string[] args)
        {
            Employee employee = new Employee();
            Console.WriteLine("EMPLOYEE APPLICATION MANAGEMENT SYSTEM");
            Console.WriteLine("----------------------------------------");
            employee.Select();//Object created to select the options

        }


    }


}

