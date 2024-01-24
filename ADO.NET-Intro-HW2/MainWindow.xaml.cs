using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows;

namespace ADO.NET_INTRO_HW2;


public partial class MainWindow : Window
{
    SqlConnection? sqlConnection = null;
    SqlCommand? sqlCommand = null;
    SqlDataReader? sqlDataReader = null;
    DataTable? dataTable = null;
    SqlDataAdapter? sqlDataAdapter = null;

    public MainWindow()
    {

        InitializeComponent();
        string conSql = "Data Source=DESKTOP-QOMBEIP;Initial Catalog=Library;Integrated Security=True;" +
            "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        sqlConnection = new SqlConnection(conSql);
       
    }


    public void Click_DataSelect()
    {
        try
        {
            sqlConnection?.Open();

            var selectQuery = TextBox.Text;
            sqlCommand = new SqlCommand(selectQuery,sqlConnection);
            sqlDataReader = sqlCommand.ExecuteReader();
            dataTable = new DataTable();

            bool isLine = true;

            while (sqlDataReader.Read())
            {

                if (isLine)
                {
                    for (int i = 0; i < sqlDataReader.FieldCount; i++)
                    {
                        dataTable.Columns.Add(sqlDataReader.GetName(i), sqlDataReader[i].GetType());
                    }
                    isLine = false;
                    DataRow dataRow = dataTable.NewRow();

                    for (int i = 0; i < sqlDataReader.FieldCount; i++)
                    {
                        dataRow[i] = sqlDataReader[i];
                    }


                }
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        finally
        {
            sqlConnection?.Close();

        }

    }


    public void Click_DataUpdate()
    {
        SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);
        if(dataTable is not null)
        {
            sqlDataAdapter?.Update(dataTable!);
        }
    }

    public void Click_DataDelete()
    {
        SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);
        if (dataTable is not null)
        {
            sqlDataAdapter?.Fill(dataTable!);
        }
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        Click_DataSelect();
    }

    private void Update_Button_Click(object sender, RoutedEventArgs e)
    {
        Click_DataUpdate();
    }

    private void delect_Button_Click(object sender, RoutedEventArgs e)
    {
        Click_DataDelete();
    }
}
