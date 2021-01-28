using RabbitMQ.Client;
using System;
using System.Text;

namespace rabbmitMQServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory factory = new ConnectionFactory
            {
                UserName = "guest",//用户名
                Password = "guest",//密码
                HostName = "127.0.0.1"//rabbitmq ip
            };

            //创建连接
            var connection = factory.CreateConnection();
            //创建通道
            var channel = connection.CreateModel();
            //声明一个队列
            channel.QueueDeclare("test", false, false, false, null);

            Console.WriteLine("RabbitMQ连接成功，请输入消息，输入exit退出！");

            string input;
            do
            {
                input = Console.ReadLine();

                var sendBytes = Encoding.UTF8.GetBytes(input);
                //发布消息
                channel.BasicPublish("", "test", null, sendBytes);

            } while (input.Trim().ToLower() != "exit");
            channel.Close();
            connection.Close();
        }
    }
}
