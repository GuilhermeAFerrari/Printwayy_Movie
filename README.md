# PrintWayyMovie
### Aplicação para gerenciamento de um cinema
![Build Status](https://travis-ci.org/joemccann/dillinger.svg)
TROCAR SHIELDS

Um dos objetivos do projeto é ser necessário o minimo de configuração para a execução.
No Back-end foi utilizado Minimals APIs, SQLite e Entity Framework, com regions para organização das rotas e as validações dos campos.
No Front-end o uso do axios, reduzindo a complexidade do codigo, aumentando a escalabilidade, mantendo uso apenas dos recursos essenciais e as boas práticas.

## Conteúdo do repositório
- Printwayy_BancoDeDados: Contendo a modelagem do banco de dados
- Printwayy_Backend: Contendo a solução e arquivos da aplicação back-end (APIs)
- Printwayy_Frontend: Contendo os arquivos que compõem o front-end (.html, .css, .js)

## 🛠 Tecnologias e Frameworks
#### Back-end
- [.NET 6.0](https://visualstudio.microsoft.com/pt-br/vs/community/)
- [SQLite](https://www.sqlite.org/index.html)
- Swashbuckle.AspNetCore
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Sqlite
#### Front-end
- [HTML](https://developer.mozilla.org/pt-BR/docs/Web/HTML)
- [CSS](https://developer.mozilla.org/pt-BR/docs/Web/CSS)
- [JavaScript](https://developer.mozilla.org/pt-BR/docs/Web/JavaScript)
- [Axios](https://axios-http.com/ptbr/docs/intro)
- [Toastr](https://github.com/CodeSeven/toastr)

## Pré-requisitos
- Visual Studio versão 17.0 ou superior de 2022 para compilação e execução do back-end - [VisualStudio](https://visualstudio.microsoft.com/pt-br/vs/community/)
- Para o front-end apenas o mecanismo JavaScript do navegador, indicado chrome ou edge.
- Opcionalmente você pode usar o editor de código Visual Studio Code - [VSCode](https://code.visualstudio.com/)

## Executando a aplicação
### 🎲 Rodando o Back End (Server)
```bash
# Clone este repositório ou faça o download .zip e extraia os arquivos
$ git clone <https://github.com/tgmarinho/nlw1>

# Acesse a pasta PrintwayyMovie_Backend do projeto e abra o arquvio PrintwayyMovie_API.sln com o Visual Studio

# Execute a aplicação (CRTL+F5)

# O servidor inciará naa portaa: 7098 para HTTPS e 5098 para HTTP, ambas configurações sem econtram no arquivo "Properties/launchSettings.json"

# O servidor iniciará o banco de dados SQLite com a carga inicial de Salas e todas entidades presente na modelagem do banco.
```
### 🎲 Rodando o Front End (Client)

```bash
# Clone este repositório ou faça o download .zip e extraia os arquivos
$ git clone <https://github.com/tgmarinho/nlw1>

# Acesse a pasta PrintwayyMovie_Frontend do projeto e execute o arquvio index.html presente na raiz da pasta

# O navegador abrira a tela de load e em seguida o formulário de login iniciando o fluxo do sistema

# Para realizar o login no sistema utilize as credenciais: gerencia@printwayy.com | gerencia (Com acesso a todas operações) ou admin@printwayy.com | admin (Com acesso apenas de visualização das telas).
```
### 🎲 Rodando os Testes unitários (Server)
```bash

```

## Features
- Gerenciamento de Salas
- Gerenciamento de Filmes
- Gerenciamento de Sessões

## 😎 Autor
Guilherme Ferrari - guile.ferrari@hotmail.com
[<img src="https://img.shields.io/badge/linkedin-%230077B5.svg?&style=for-the-badge&logo=linkedin&logoColor=white" />](https://www.linkedin.com/in/guilherme-antonio-ferrari/)