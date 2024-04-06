# Desafio de Desenvolvimento BackEnd

Bem-vindo à documentação da API Community IoT Device (CIoTD). 

A CIoTD é uma plataforma colaborativa para compartilhamento de acesso à dados de dispositivos IoT.

Através dela, colaboradores podem cadastrar seus dispositivos, permitindo que qualquer pessoa possa coletar os dados 
desses dispositivos e utilizar em suas aplicações.

## 🚀 Começando

Essas instruções permitirão que você obtenha uma cópia do projeto em operação na sua máquina local para fins de desenvolvimento e teste.

Disponibilizei o projeto de duas formas:

# Docker

- Se o seu objetivo for apenas utilizar esta API, poderá fazer isso através de uma imagem Docker que disponibilizei em:

https://hub.docker.com/r/itfrancisconeto/ciotd/tags

- Para utilizar essa imagem instale o Docker em seu Sistema Operacional e, através de um Terminal, execute o seguinte comando:

docker run -p 8000:80  itfrancisconeto/ciotd:latest

Este comando irá baixar a imagem e executar o container localmente em sua máquina

- Com o container em execução, abra o navegador e digite o seguinte endereço:

http://localhost:8000/swagger/index.html

# Github

- Se o seu objetivo for analisar e testar o código desta API, poderá fazer isso através do Github.

Uma forma de fazer isso é criar e acessar uma pasta na sua máquina local.

Depois basta digitar o seguinte comando:

git clone https://github.com/itfrancisconeto/CIoTD.git .

Lembre-se de ter instalado o Git em seu Sistema Operacional.

## ⚙️ Construção da API

No texto de descrição deste desafio foi disponibilizado um anexo de documentação da API no padrão de especificação OpenAPI.

A utilização desse padrão torna a documentação da API legível tanto por seres humanos quanto por máquinas.

Dessa forma é possível salvar a especificação em arquivos JSON ou YAML e importá-lo em uma ferramenta de code generator para obter
o código fonte de maneira automática. O problema dessa abordagem é que o código fonte gerado automáticamente pode conter funcionalidades
imprecisas e que nem sempre estarão de acordo com as boas práticas da programação.

Diante do exposto, optei construir a API do zero e apenas me basear na documentação disponibilizada para entender as funcionalidades necessárias.

# Arquitetura

Após a leitura da documentação e entendimento das regras de negócio, meu primeiro passo foi definir qual seria a arquitetura da API.

A fim de desenvolver uma API facilmente escalável e alinhada com as boas práticas do mercado, optei por me basear na Clean Architecture.

Dessa forma dividi o projeto em cinco camadas:

1. **Domain (Domínio)**: para definição das entidades e especificação das estruturas de dados.

2. **Infrastructure (Infraestrutura)**: para tratar a recuperação e persistência dos dados.

3. **Application (Aplicação)**: para coordenar a interação entre a camada de apresentação (externa) e a camada de infraestrutura (interna).

4. **Presentation (Apresentação)**: para viabilizar a interação com os usuários e apresentar as informações para eles.

5. **Security (Segurança)**: Considerando a grande relevância do tema Segurança da Informação, optei por criar essa camada adicional dedicada
a funcionalidades que tornem esta API mais segura. Inicialmente implementei nesta camada a autenticação por token JWT.

## 🛠️ Ferramentas

Desenvolvi essa API com a linguagem C# versão 12 e plataforma Dot Net versão 8.0.

Como IDE (Integrated Development Environment) utilizei o Visual Studio Community 2022.

Swagger para documentar a API e servir de interface para testes dos recursos.

## 📌 Estrutura

A API é composta por dois recursos denominados Device e Login. O recurso Device representa o dispositivo IoT (Internet of Things) contextualizado.
E o recurso Login representa a autenticação do usuário para poder utilizar a API.

Buscando seguir o estilo de arquitetura REST (Representational State Transfer), estruturei a API através dos seguintes endpoints para esse recurso:

**GET /Device**: utilizado para listar todos os dispositivos cadastrados na plataforma a fim de atender o requisito funcional 3 da documentação.
Considerei os seguintes códigos de status nesse endpoint:

- Status200OK para requisição realizada com sucesso e resposta retornada
- Status401Unauthorized para tentativa de acesso não autorizado
- Status500InternalServerError para o caso de ocorrer um erro interno no servidor

Neste exemplo listei a resposta para três dispositivos cadastrados com suas respectivas informações.

[
  {
    "identifier": "88afc829-777e-4331-b961-c5d517acf5dc",
    "description": "string",
    "manufacturer": "PredictWeater",
    "url": "string",
    "commands": [
      {
        "comand": "get_rainfall_intensity",
        "parameters": [
          {
            "name": "string",
            "description": "string"
          }
        ]
      }
    ],
    "rainFallIntensities": [
      {
        "dateTime": "2024-04-04T19:22:08.224Z",
        "volumetry": 0
      },
      {
        "dateTime": "2024-04-04T21:14:06.9265477-03:00",
        "volumetry": 310.53
      },
      {
        "dateTime": "2024-04-05T18:44:46.4492289-03:00",
        "volumetry": 758.74
      }
    ]
  },
  {
    "identifier": "f0ffb744-39ff-4e94-9792-a76e7dd346a4",
    "description": "string",
    "manufacturer": "PredictWeater",
    "url": "string",
    "commands": [
      {
        "comand": "get_rainfall_intensity",
        "parameters": [
          {
            "name": "string",
            "description": "string"
          }
        ]
      }
    ],
    "rainFallIntensities": [
      {
        "dateTime": "2024-04-05T23:31:07.401Z",
        "volumetry": 0
      }
    ]
  },
  {
    "identifier": "df71a032-a90d-495f-9934-94a52b30799d",
    "description": "string",
    "manufacturer": "PredictWeater",
    "url": "string",
    "commands": [
      {
        "comand": "get_rainfall_intensity",
        "parameters": [
          {
            "name": "string",
            "description": "string"
          }
        ]
      }
    ],
    "rainFallIntensities": [
      {
        "dateTime": "2024-04-05T23:31:07.401Z",
        "volumetry": 0
      }
    ]
  }
]

**POST /Device**: utilizado para cadastrar um novo dispositivo na plataforma considerando as regras descritas no requisito funcional 2 da documentação.
Tomei a liberdade de assumir para o campo identifier o valor GUID (Globally Unique Identifier).
Considerei os seguintes códigos de status nesse endpoint:

- Status201OK para requisição criada com sucesso e resposta retornada
- Status400BadRequest caso fabricante (manufacturer) seja diferente de PredictWeater ou comando (comand) seja diferente de get_rainfall_intensity
- Status401Unauthorized para tentativa de acesso não autorizado
- Status500InternalServerError para o caso de ocorrer um erro interno no servidor

Exemplo de resposta para cadastro realizado com sucesso:

{
  "identifier": "fa421bbc-e19c-418c-a9db-429584abf96d",
  "description": "string",
  "manufacturer": "PredictWeater",
  "url": "string",
  "commands": [
    {
      "comand": "get_rainfall_intensity",
      "parameters": [
        {
          "name": "string",
          "description": "string"
        }
      ]
    }
  ],
  "rainFallIntensities": [
    {
      "dateTime": "2024-04-05T23:31:07.401Z",
      "volumetry": 0
    }
  ]
}

Exemplo de resposta para a tentativa de cadastro do novo dispositivo com fabricante diferente de PredictWeater:

{
  "message": "Fabricante diferente de PredictWeater não permitido"
}

Exemplo de resposta para a tentativa de cadastro do novo dispositivo com comando diferente de get_rainfall_intensity:

{
  "message": "Comando diferente de get_rainfall_intensity não permitido"
}

**GET /Device/{id}**: utilizado para listar o dispositivo cadastrado na plataforma de acordo com o id informado.
Considerei os seguintes códigos de status nesse endpoint:

- Status200OK para requisição realizada com sucesso e resposta retornada
- Status401Unauthorized para tentativa de acesso não autorizado
- Status404NotFound para dispositivo não encontrado
- Status500InternalServerError para o caso de ocorrer um erro interno no servidor

Neste exemplo listei a resposta para a busca do dispositivo cadastrado com id "88afc829-777e-4331-b961-c5d517acf5dc":

{
  "identifier": "88afc829-777e-4331-b961-c5d517acf5dc",
  "description": "string",
  "manufacturer": "PredictWeater",
  "url": "string",
  "commands": [
    {
      "comand": "get_rainfall_intensity",
      "parameters": [
        {
          "name": "string",
          "description": "string"
        }
      ]
    }
  ],
  "rainFallIntensities": [
    {
      "dateTime": "2024-04-04T19:22:08.224Z",
      "volumetry": 0
    },
    {
      "dateTime": "2024-04-04T21:14:06.9265477-03:00",
      "volumetry": 310.53
    },
    {
      "dateTime": "2024-04-05T18:44:46.4492289-03:00",
      "volumetry": 758.74
    }
  ]
}

E este é um exemplo da resposta para a busca de um id não encontrado:

{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.5",
  "title": "Not Found",
  "status": 404,
  "traceId": "00-c49e979e5facf6100a05b8af6e565f18-23fde627d0e0bf73-00"
}

**PUT /Device/{id}**: utilizado para atualizar os dados de um dispositivo.
Considerei os seguintes códigos de status nesse endpoint:

- Status200OK para requisição realizada com sucesso e resposta retornada
- Status401Unauthorized para tentativa de acesso não autorizado
- Status404NotFound para dispositivo não encontrado
- Status500InternalServerError para o caso de ocorrer um erro interno no servidor

Neste exemplo listei a resposta para o dispositivo id "88afc829-777e-4331-b961-c5d517acf5dc" após a alteração de sua descrição.

{
  "identifier": "88afc829-777e-4331-b961-c5d517acf5dc",
  "description": "testando o a atualização dos dados deste dispositivo",
  "manufacturer": "PredictWeater",
  "url": "string",
  "commands": [
    {
      "comand": "get_rainfall_intensity",
      "parameters": [
        {
          "name": "string",
          "description": "string"
        }
      ]
    }
  ],
  "rainFallIntensities": [
    {
      "dateTime": "2024-04-05T23:55:34.29Z",
      "volumetry": 0
    }
  ]
}

**DELETE /Device/{id}**: utilizado para remover um dispositivo.
Considerei os seguintes códigos de status nesse endpoint:

- Status200OK para requisição realizada com sucesso e resposta retornada
- Status401Unauthorized para tentativa de acesso não autorizado
- Status404NotFound para dispositivo não encontrado
- Status500InternalServerError para o caso de ocorrer um erro interno no servidor

Neste exemplo listei a resposta para o dispositivo id "fa421bbc-e19c-418c-a9db-429584abf96d" removido com sucesso.

{
  "identifier": "fa421bbc-e19c-418c-a9db-429584abf96d",
  "description": "string",
  "manufacturer": "PredictWeater",
  "url": "string",
  "commands": [
    {
      "comand": "get_rainfall_intensity",
      "parameters": [
        {
          "name": "string",
          "description": "string"
        }
      ]
    }
  ],
  "rainFallIntensities": [
    {
      "dateTime": "2024-04-05T23:31:07.401Z",
      "volumetry": 0
    }
  ]
}

**GET /Device/{id},{command}**: utilizado para recuperar a volumetria de chuva por dispositivo.
Tomei a liberdade de simular o resultado da comunicação telnet solicitada no requisito 4 através de uma função que gera números aleatórios para a medição da volumetria de chuva e inclui a data e hora da medição. Considerei os seguintes códigos de status nesse endpoint:

- Status200OK para requisição realizada com sucesso e resposta retornada
- Status401Unauthorized para tentativa de acesso não autorizado
- Status404NotFound para dispositivo não encontrado
- Status500InternalServerError para o caso de ocorrer um erro interno no servidor

Neste exemplo listei a resposta para a volumetria do dispositivo id "df71a032-a90d-495f-9934-94a52b30799d" realizada com sucesso.

{
  "identifier": "df71a032-a90d-495f-9934-94a52b30799d",
  "description": "string",
  "manufacturer": "PredictWeater",
  "url": "string",
  "commands": [
    {
      "comand": "get_rainfall_intensity",
      "parameters": [
        {
          "name": "string",
          "description": "string"
        }
      ]
    }
  ],
  "rainFallIntensities": [
    {
      "dateTime": "2024-04-05T23:31:07.401Z",
      "volumetry": 0
    },
    {
      "dateTime": "2024-04-05T21:14:28.8101827-03:00",
      "volumetry": 381.06
    }
  ]
}

Este é um exemplo de resposta para a tentativa de listar a volumetria do dispositivo com comando diferente de get_rainfall_intensity:

{
  "message": "Comando diferente de get_rainfall_intensity não permitido"
}

**POST /Login/login**: utilizado para gerar o token de autenticação na API.
Para atender o requisito 1 da documentação, implementei um recurso para autenticação via token. 
Na parte de sugestões para melhorias futuras eu descrevo melhor ações que podem ser aplicadas nesse recurso.
Neste exemplo listei a resposta para a tentativa de uso de um endpoint sem estar autenticado:

Error: Unauthorized
content-length: 0 
date: Sat,06 Apr 2024 00:20:26 GMT 
server: Kestrel 
www-authenticate: Bearer 

## 📌 Testes unitários

Implementei na solução um projeto com uma classe dedicada para testes unitários dos endpoints da API.

Utilizei de forma bem simples ASP.NET Core MVC Testing em conjunto com XUnit.

## 🖇️ Sugestões para melhorias futuras desta API

- Aumentar a cobertura de testes unitários e implementar testes de integração assim que esta solução tiver um Frontend.

- Na parte que trata a recuperação da volumetria de chuva do dispositivo, futuramente pode ser incluída a recuperação dessa volumetria
a partir de um serviço de mensageria preparado para receber dados de vários sensores separados em tópicos utilizando a arquitetura Publish/Subscribe.
Outro ponto de melhoria nesse tópico seria a implementação da utilização do protocolo MQTT na comunicação com os dispositivos para redução do tamanho do payload de dados trafegados e otimização da utilização em conexões de baixa qualidade.

- Na parte que trata a geração do token de autenticação, pode ser incluída futuramente a recuperação do usuário e senha
a partir de um banco de dados para fazer uma checagem prévia se usuário é válido ou não para a geração do token.

- Para a persistência dos dados eu fiz a utilização de um arquivo JSON. Com a perpectiva de utilização em larga escala faz-se necessário
implementar a utilização de um banco de dados. Em um cenário com a possibilidade de utilização de diferentes sensores, eu implementaria
uma solução de banco de dados NoSQL para ter mais flexibilidade e não depender de um esquema rígido de modelagem.

- Nesta API fiz a utilização do design pattern Repository para criar uma interface de interação com os dados persistidos no arquivo JSON. 
Na medida em que a complexidade desta API aumentar outros design patterns podem ser adotados como, por exemplo, o padrão
Facade para fornecer uma interface única e simplificada de acesso às funcionalidades de um ou mais subsistemas de modo a ocultar a complexidade.
























