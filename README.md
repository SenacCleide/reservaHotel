# Hotel API

###### Api para gerenciamento de hoteis


## Sobre o projeto:

Este projeto consiste em uma API RESTful que possibilita a criação de reservas em hoteis.

A API foi construída utilizando .Net Core 6.0

Utilizamos o banco de dados MongoDB para armazenar as informações das reservas e usuários e o banco de dados Sql para armazenar os dados de créditos do cliente.

## Funcionalidades:

- Autenticação do usuário

- Cadastro de usuários

- Criar ou Atualizar saldo da carteira digital do usuário

- Criar reserva do usúario

- Criar reserva do Hotel

- Listar Reserva do Hotel

- Download da Reserva

- Download de Pagamento

## EndPoints:

Cadastro de Usuários:

POST  
User/Add

{  
     "name": "Teste",    
      "email": "teste@teste",   
      "password": "XXXXXX",   
      "createdAt": "2023-04-18T19:02:54.767Z"       
} 

Autenticação do Usuário:

POST  
Auth/Login  
{  
  "email": "Teste@teste",  
  "password": "XXXXXX"  
}

Adicionar saldo na carteira digital:

POST  
User/AddPurse

{  
  "tennant": "string",  // Corresponde ao Id do usuário  
  "value": 0  
}

Criar uma reserva para o usuário:  
Nesse endpoint é realizado algumas validações;
- Verifica as datas da reserva se não não está maior ou menor do que o hotel disponibilizou para a estadia;
- Verifica se a reserva está disponivel;
- Verifica se o valor do saldo do cliente é suficiente para a reserva e ou adiciona mais saldo;
- Se todas a validações estiverem correta retorna o comprovante de pagamento da reserva;

POST  
User/AddUserStay

{  
  "id": "string",  
  "name": "string",  
  "idStayHotel": "string",  
  "tennant": "string",  
  "value": 0,  
  "status": "string",  
  "createdAt": "2023-04-18T19:30:51.098Z",  
  "checkin": "2023-04-18T19:30:51.098Z",  
  "checkout": "2023-04-18T19:30:51.098Z"  
}

Hotel cria as reservas para vendas  

POST  
Hotel/AddStayHotel
{    
  "name": "Nome do Hotel",  
  "checkin": "2023-04-18T19:02:44.981Z",  
  "checkout": "2023-04-18T19:02:44.981Z",  
  "bedrooms": 2,  
  "value": 30000,  
  "isReserved": true,  
  "createdAt": "2023-04-18T19:02:44.981Z",  
  "updatedAt": null  
}

Lista as reservas para vendas

GET  
Hotel/List

Download em Pdf (Pagamento ou reserva do cliente)  
Exemplos:
- id_Pay  // Exemplo para impressão do pagamento
- id  // Exemplo para impressão da reserva

GET  
Hotel/DownloadStay?file=1254

