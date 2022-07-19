using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Model;
using SqlimportProducer.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SqlimportProducer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private readonly IPublishEndpoint publishEndpoint;

        /// <summary>
        /// It's a constructor of ProducerController class
        /// </summary>
        /// <remarks>
        /// Created by  :   Sunil Thakur,
        /// Created on  :   15/07/2022
        /// Purpose     :   It's a constructor of ProducerController class
        /// </remarks>
        /// <param name="publishEndpoint></param>
        /// <returns></returns>
        public ProducerController(IPublishEndpoint publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint;
        }   //ProducerController

        /// <summary>
        /// It's a api end point that will import data from sql to elastic search
        /// </summary>
        /// <remarks>
        /// Created by  :   Sunil Thakur,
        /// Created on  :   15/07/2022
        /// Purpose     :   It's a api end point that will import data from sql to elastic search
        /// </remarks>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet, Route("TableService/CreateEmployeeTableInElasticSearch")]
        public async Task<ActionResult> CreateEmployeeTableInElasticSearch()
        {
            ElasticDataBaseService elasticDataBaseService = new ElasticDataBaseService(publishEndpoint);
            elasticDataBaseService.GetAndSendTableRecordToElastic();

            return Ok();
        }   //  CreateEmployeeTableInElasticSearch

    }
}
