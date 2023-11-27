Run RabbitMQ via docker:

 docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.12-management

 Open UI for RabbitMQ:
 http://localhost:15672/
 guest guest