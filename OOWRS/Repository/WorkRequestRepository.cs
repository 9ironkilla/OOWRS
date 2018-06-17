using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OOWRS.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace OOWRS.Repository
{
    public interface IWorkRepository
    {
         List<WorkRequestModel> GetAllWorkRequests();
         WorkRequestModel GetWorkRequest(int id);
         void DeleteWorkRequest(int id);
         void SaveWorkRequest(WorkRequestModel workRequest);

    }

    public class WorkRequestRepository: IWorkRepository
    {

  

        private SqlConnection _WorkRequestConnection;
        private string _FindWorkRequestQuery = "SELECT Id, OwnerName, Description, Status, CreatedDate from [WorkRequests] where [Id] = @Id";
        private string _GetAllWorkRequests = "SELECT Id, OwnerName, Description, Status, CreatedDate from [WorkRequests]";
        private string _DeleteWorkRequest = "DELETE from [WorkRequests] where [Id] = @Id";
        private string _SaveWorkRequest = "Insert INTO [WorkRequests] (OwnerName, Status, Description, CreatedDate) values (@OwnerName, @Status, @Description, @CreatedDate)";
        private string _UpdateWorkRequest = "UPDATE [WorkRequests] SET Status = @Status, Description = @Description where [Id] = @Id";



        public WorkRequestRepository()
        {
           
        }



        public List<WorkRequestModel> GetAllWorkRequests()
        {
            List<WorkRequestModel> workRequests = new List<WorkRequestModel>();

            _WorkRequestConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionStringName"].ConnectionString);
            using (_WorkRequestConnection)
            {
                SqlCommand command = new SqlCommand(_GetAllWorkRequests, _WorkRequestConnection);

                try
                {
                     _WorkRequestConnection.Close();
                    _WorkRequestConnection.Open();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var workRequest = new WorkRequestModel
                        {
                            Id = Convert.ToInt32(reader[0].ToString()),
                            OwnerName = reader[1].ToString(),
                            Description = reader[2].ToString(),
                            Status = reader[3].ToString(),
                            CreatedDate = Convert.ToDateTime(reader[4].ToString()),
                        };
                        workRequests.Add(workRequest);
                    }

                }
                catch (SqlException e)
                {
                    Console.Out.Write($"SQL Exception {e.Message}");
                    return workRequests;
                }
                finally
                {
                    _WorkRequestConnection.Close();
                }


            }
            return workRequests;

        }





        public WorkRequestModel GetWorkRequest(int id)
        {

            WorkRequestModel workRequest = new WorkRequestModel();
            _WorkRequestConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionStringName"].ConnectionString);
            using (_WorkRequestConnection)
            {
                SqlCommand command = new SqlCommand(_FindWorkRequestQuery, _WorkRequestConnection);
                command.Parameters.AddWithValue("@Id", id);

                try
                {
                    _WorkRequestConnection.Open();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        workRequest.Id = Convert.ToInt32(reader[0].ToString());
                        workRequest.OwnerName = reader[1].ToString();
                        workRequest.Description = reader[2].ToString();
                        workRequest.Status = reader[3].ToString();
                        workRequest.CreatedDate = Convert.ToDateTime(reader[4].ToString());
                    }

                }
                catch (SqlException e)
                {
                    Console.Out.Write($"SQL Exception {e.Message}");
                    return workRequest;
                }
                finally
                {
                    _WorkRequestConnection.Close();
                }


            }
            return workRequest;


        }

        public void DeleteWorkRequest(int id)
        {

            _WorkRequestConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionStringName"].ConnectionString);
            using (_WorkRequestConnection)
            {
                SqlCommand command = new SqlCommand(_DeleteWorkRequest, _WorkRequestConnection);
                command.Parameters.AddWithValue("@Id", id);

                try
                {
                    _WorkRequestConnection.Open();
                    var reader = command.ExecuteReader();
                }
                catch (SqlException e)
                {
                    Console.Out.Write($"SQL Exception {e.Message}");
                    return;
                }
                finally
                {
                    _WorkRequestConnection.Close();
                }


            }
            return;
        }
        public void SaveWorkRequest(WorkRequestModel workRequest)
        {

            SqlCommand command;
            _WorkRequestConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionStringName"].ConnectionString);
            using (_WorkRequestConnection)
            {
                if ((workRequest.Id != 0))
                {
                    command = new SqlCommand(_UpdateWorkRequest, _WorkRequestConnection);
                    command.Parameters.AddWithValue("@Status", workRequest.Status);
                    command.Parameters.AddWithValue("@Description", workRequest.Description);
         
                }
                else
                {
                    command = new SqlCommand(_SaveWorkRequest, _WorkRequestConnection);
                    command.Parameters.AddWithValue("@OwnerName", workRequest.OwnerName);
                    command.Parameters.AddWithValue("@Status", workRequest.Status);
                    command.Parameters.AddWithValue("@Description", workRequest.Description);
                    command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                    

                }


                try
                {
                    _WorkRequestConnection.Open();
                    var reader = command.ExecuteReader();
           
                }
                catch (SqlException e)
                {
                    Console.Out.Write($"SQL Exception {e.Message}");
                    return;
                }
                finally
                {
                    _WorkRequestConnection.Close();
                }


            }
            return;
        
        }



    }
}