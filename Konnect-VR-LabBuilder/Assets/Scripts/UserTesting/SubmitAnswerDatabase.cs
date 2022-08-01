using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Microsoft.Data;
using Microsoft.Data.SqlClient;
/*using SqlCommand = Microsoft.Data.SqlClient.SqlCommand;
using SqlConnection = Microsoft.Data.SqlClient.SqlConnection;
using SqlConnectionStringBuilder = Microsoft.Data.SqlClient.SqlConnectionStringBuilder;
using SqlException = Microsoft.Data.SqlClient.SqlException;
using SqlDataReader = Microsoft.Data.SqlClient.SqlDataReader;*/

public class SubmitAnswerDatabase : MonoBehaviour
{
    public int number_of_questions;
    private string [] userAnswer;
    private SqlCommand command;
    public Button submit;
    public InputField [] input;
    //public GameObject MCInputPanel;
    private SqlConnection conn;

    public void setUserAnswer(string ans, int index)
    {
        userAnswer[index] = ans;
    }

    public string[] getUserAnswers()
    {
        return userAnswer;
    }

    public void setNumberOfQuestions(int num)
    {
        userAnswer = new string[num];
        //input = new InputField[num];
        number_of_questions = num;
    }

    public int getNumberOfQuestions()
    {
        return number_of_questions;
    }


    void Start()
    {
        //Debug.Log("You have sucessfully started the program!");
        connectDB();

        submit.onClick.AddListener(SubmitOnClick);
        //Debug.Log("You have connected to the database!");

        //setNumberOfQuestions(number_of_questions)
    }

    void connectDB()
    {
        /*Debug.Log("You are about to create the connection string.");
        String connectionString = "Server=LAPTOP-FAM1VF56; " + // in the future, use localhost    // "Data Source=[::]:1434; " +
                                "Database=KonnectVR; " +
                                "Integrated Security=true;";// +
                                //"PortNumber=1434";

        
        //SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        //builder.DataSource = "127.0.0.1,1434";
        //builder.Server = "LAPTOP-FAM1VF56";
        //builder.InitialCatalog = "KonnectVR";

        Debug.Log("You have created the connection string and are creating a new SQL connection");
        //SqlConnection conn = new SqlConnection(builder.ConnectionString);
        SqlConnection conn = new SqlConnection(connectionString);
        Debug.Log("You have created a new SQL connection and are generating a string to run a query");

        // create a SqlCommand object for this connection

        string initRun = "INSERT INTO Assessment_Results (Student_ID, Assessment_ID, Question_ID) VALUES (" +
            "(SELECT Student_ID FROM Students WHERE Student_ID='0'), " +
            "(SELECT Assessment_ID FROM Assessments WHERE Assessment_ID='11'), " +
            "(SELECT Question_ID FROM Questions WHERE Question_ID='101')" +
            ");";
        Debug.Log("You have generated a string to run a query are about to attempt to open the database connection");

        using (conn)
        {
            try
            {
                conn.Open();
            }
            catch (SocketException sock)
            {
                Debug.Log(sock.Source);
            }

            Debug.Log("You have opened the database connection and are about to run the query");

            using (SqlCommand myCommand = new SqlCommand(initRun, conn))
            {
                string Result = (string)myCommand.ExecuteScalar(); // returns the first column of the first row
            }
            Debug.Log("You run the query and are about to close the connection");

            conn.Close();
            Debug.Log("You have closed the connection!");
        }*/
        try
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "se491konnectvr.database.windows.net";
            builder.UserID = "KonnectVRAdmin";
            builder.Password = "SE491KonnectVR";
            builder.InitialCatalog = "Konnect VR";

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                Debug.Log("\nQuery data example:");
                //Debug.Log("=========================================\n");

                string initRun = "INSERT INTO Assessment_Result (Student_ID, Assessment_ID, Question_ID) VALUES (" +
                                    "(SELECT Student_ID FROM Student WHERE Student_ID='1'), " +
                                    "(SELECT Assessment_ID FROM Assessment WHERE Assessment_ID='11'), " +
                                    "(SELECT Question_ID FROM Question WHERE Question_ID='101')" +
                                    ");";

                using (SqlCommand command = new SqlCommand(initRun, connection))
                {
                    connection.Open();
                    Debug.Log("Connection Sucessful");
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Debug.Log(reader.GetString(0) +", "+ reader.GetString(1));
                        }
                    }
                }
            }
        }
        catch (SqlException e)
        {
            Debug.Log(e.ToString());
        }
        Console.ReadLine();

    }


    void SubmitOnClick()
    {
        Debug.Log("You have clicked the button!");

        DateTime now = DateTime.Now;

        //Pressing the button will take the text in the input field and set it to answerString

        for (int i = 1; i < number_of_questions; i++)
        {
            setUserAnswer(input[i].text.ToString(), i);
        

        // Runs SQL query to put response and time of response into database (I hope????)
            /*using (SqlConnection conn = new SqlConnection(builder.ConnectionString))
            {
                conn.Open();
                using (SqlCommand myCommand = new SqlCommand("INSERT INTO Assessment_Results (Date_Submitted, Question_Response) VALUES (" + now + ", " + userAnswer[i] +
                                                                ") WHERE Student_ID = 0, Assessment_ID = 11, Question_ID = 101;", conn))
                {
                    string Result = (string)myCommand.ExecuteScalar(); // returns the first column of the first row
                }

                conn.Close();
            }*/
        }
    }
}
