﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Peppy;
using Peppy.Core;
using Peppy.EntityFrameworkCore;
using Peppy.RabbitMQ;
using Peppy.Redis;
using Sample.WebApi.Repositories;

namespace Sample.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        //private readonly ICapPublisher _capBus;
        private readonly ILogger<WeatherForecastController> _logger;

        private readonly IPersonPepository _personPepository;
        private readonly AppDbContext _appDbContext;
        private readonly IRedisManager _redisManager;
        private readonly IRabbitMQManager _rabbitMQManager;
        private readonly ClientRegister _clientRegister;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            //ICapPublisher capPublisher,
            //IRedisManager redisManager,
            IPersonPepository personPepository,
            AppDbContext appDbContext,
            ClientRegister clientRegister,
            IRabbitMQManager rabbitMQManager)
        {
            _logger = logger;
            //_capBus = capPublisher;
            //_redisManager = redisManager;
            _clientRegister = clientRegister;
            _rabbitMQManager = rabbitMQManager;
            _appDbContext = appDbContext;
            _personPepository = personPepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Person>> Get([FromServices]AppDbContext dbContext)
        {
            //await _personPepository.BatchDeleteAsync(x => x.Id > 1);
            //await _personPepository.Query().Where(x => x.Name == "test").DeleteFromQueryAsync();
            var persons = await _personPepository.QueryListAsync();
            //_rabbitMQManager.SendMsgAsync("test", "test", new TestModel { Date = DateTime.Now });
            //_redisManager.Add("test", "test");
            //_client.OnConsumerReceived(null, new EventArgs());
            //using (var trans = dbContext.Database.BeginTransaction(_capBus, autoCommit: false))
            //{
            //    dbContext.Persons.Add(new Person() { Name = DateTime.Now.ToString() });

            //    _capBus.Publish("sample.rabbitmq.qq", DateTime.Now);

            //    dbContext.SaveChanges();
            //    trans.Commit();
            //}
            return persons;
        }

        [NonAction]
        [RabbitMQReceive("test", "test")]
        public void Test(TestModel msg)
        {
            _logger.LogInformation(msg.ToJson());
        }

        //[CapSubscribe("sample.rabbitmq.mysql")]
        public void Subscriber(DateTime p)
        {
            Console.WriteLine($@"{DateTime.Now} Subscriber invoked, Info: {p}");
        }
    }

    public class TestModel
    {
        public DateTime Date { get; set; }
    }
}