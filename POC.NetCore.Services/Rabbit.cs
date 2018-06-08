using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace POC.NetCore.Services
{
    public class Rabbit
    {
        private ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _model;

        public Rabbit()
        {
            setupRabbit();
        }

        public void setupRabbit()
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = "localhost",
                Password = "guest",
                UserName = "guest"
            };


            _connection = _connectionFactory.CreateConnection();
            _model = _connection.CreateModel();
        }


        public void Sender(string message)
        {

            var properties = _model.CreateBasicProperties();
            properties.SetPersistent(true);

            //serailize
            byte[] mensagem = Encoding.Default.GetBytes(message);

            _model.BasicPublish("", "AVI", properties, mensagem);

        }
    }
}
