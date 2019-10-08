# HelloWorldMicroservice

Olá!

Neste projeto estou utilizando RabbitMQ para mensageria e .net core console app
para instalar o RabbitMQ basta seguir as instruções messe site: https://www.rabbitmq.com/download.html

Para rodar o projeto é muito simples, basta executar o HelloWorldService.service.

No appsettings.json temos uma configuração "readOwnMessage" caso queira que ser serviço leia as próprias messagens enviadas bastar deixar essa opção marcada como true, caso não queira marque como false.

O projeto também pode ser iniciado por linha de comando, para habilitar ou não a opção de ler as próprias mensagens basta apois iniciar colocar o primeiro args sendo true ou false.

Você pode iniciar quantas instâncias quiserem do service, as mensagens vão aparecer no console.

# proposta

O serviço ao ser iniciado enviar um mensagem "Hello World!" a cada 5 segundos, todos os serviços que se conectarem na mesma exchange receberam as menssagens automaticamente.

Foi utilizado o conceito de exchange do RabbitMQ onde é criado uma exchange e várias filas são criadas e lincadas a essa exchange, feito isso quando se envia uma mensagem para a exchange todas as filas lincadas receberão ao mesmo tempo a mensagem.

Quando o serviço é parado o próprio RabbitMQ se encarrega de deletar a fila.
