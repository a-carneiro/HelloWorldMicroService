# HelloWorldMicroservice

Olá!

Neste projeto estou utilizando RabbitMQ para menssageria e .net core console app
para instalar o RabbitMQ basta seguir as instruções messe site: https://www.rabbitmq.com/download.html

Para rodar o projeto é muito simples, basta executar o HelloWorldService.service.

No appsettings.json temos uma configuração "readOwnMessage" caso queira que ser serviço leia as próprias messagens enviadas bastar deixar essa opção marcada como true, caso não queira marque como false.

O projeto também pode ser iniciado por linha de comando, para abilitar ou não a opção de ler as próprias messagens basta apois iniciar colocar o primeiro args sendo true ou false.
