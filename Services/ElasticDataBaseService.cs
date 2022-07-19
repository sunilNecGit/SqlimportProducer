using MassTransit;
using Model;
using System.Data.SqlClient;

namespace SqlimportProducer.Services
{
    public class ElasticDataBaseService
    {
        private readonly IPublishEndpoint publishEndpoint;

        /// <summary>
        /// It's a constructor of ElasticDataBaseService class
        /// </summary>
        /// <remarks>
        /// Created by  :   Sunil Thakur,
        /// Created on  :   15/07/2022
        /// Purpose     :   It's a constructor of ElasticDataBaseService class
        /// </remarks>
        /// <param name="publishEndpoint></param>
        /// <returns></returns>
        public ElasticDataBaseService(IPublishEndpoint publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint;
        }   //ElasticDataBaseService

        /// <summary>
        /// It's a method that gets and send data from sql to rabbitmq.
        /// </summary>
        /// <remarks>
        /// Created by  :   Sunil Thakur,
        /// Created on  :   15/07/2022
        /// Purpose     :   It's a method that gets and send data from sql to rabbitmq.
        /// </remarks>
        /// <param name=""></param>
        /// <returns></returns>
        public void GetAndSendTableRecordToElastic()
        {
            List<Employee> items = GetEmployeesAndSend();

            foreach (var item in items)
            {
                Console.WriteLine($"employee {item.ID}, Done!");
            }
        }   //GetAndSendTableRecordToElastic

        /// <summary>
        /// It's a method that will fetch the records from the sql database and also publish message using rabbitmq masstransit.
        /// </summary>
        /// <remarks>
        /// Created by  :   Sunil Thakur,
        /// Created on  :   15/07/2022
        /// Purpose     :   It's a method that will fetch the records from the sql database and also publish message using rabbitmq masstransit.
        /// </remarks>
        /// <param name=""></param>
        /// <returns></returns>
        private List<Employee> GetEmployeesAndSend()
        {
            List<Employee> items = new List<Employee>();

            using (SqlConnection conn = new SqlConnection(@"Server=DLLT-4YBDW93; Initial Catalog=mobileposUWP; User ID=sa; Password=abc@1234; MultipleActiveResultSets=True;"))
            {
                using (SqlCommand cmd = new SqlCommand("select * from employee", conn))
                {
                    conn.Open();
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            Employee employeeitem = new Employee()
                            {
                                ID = Convert.ToInt32(r["id"]),
                                Name = r["name"].ToString(),
                            };

                            items.Add(employeeitem);
                            publishEndpoint.Publish<Employee>(employeeitem);
                        }
                    }
                }
            }

            return items;
        }   //GetEmployees
    }
}
