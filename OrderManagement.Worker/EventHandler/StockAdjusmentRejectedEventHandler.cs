﻿using InventoryManagement.Contract;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Worker.EventHandler
{
    internal class StockAdjusmentRejectedEventHandler : IConsumer<StockAdjusmentRejectedEvent>
    {
        public Task Consume(ConsumeContext<StockAdjusmentRejectedEvent> context)
        {
            //Order-> Rejected
            // publish event
            throw new NotImplementedException();
        }
    }
}