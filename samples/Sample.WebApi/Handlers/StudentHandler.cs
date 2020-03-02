﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Sample.WebApi.Repositories;

namespace Sample.WebApi.Handlers
{
    public class StudentHandler : INotificationHandler<Person>, IEventHandler
    {
        private readonly IPersonPepository _personPepository;

        public StudentHandler(IPersonPepository personPepository)
        {
            _personPepository = personPepository;
        }

        public void Dispose()
        {
        }

        public async Task Handle(Person request, CancellationToken cancellationToken)
        {
            //Console.WriteLine("Student handle start");
            Console.WriteLine("2");
            await Task.Delay(5000, cancellationToken);
            //var result = await _personPepository.InsertAsync(request);
            //return new Person { Id = 2, Name = "test" };
        }
    }
}