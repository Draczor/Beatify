﻿using UserService.Models;

namespace UserService.Producer
{
    public interface IRabbitMQProducer
    {
        public Task SendUserCreated(User user);
    }
}
