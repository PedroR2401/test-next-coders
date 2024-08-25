# Desafio Técnico - Aplicação de Gerenciamento de Tarefas

### Como configurar o banco de dados:
O banco de dados deverá ser configurado na linha 11 do appsettings.json.

Exemplo: "DefaultConnection": "Server=localhost;Database=GerenciadorTarefas;User=root;Password=123;"

### Para criação do banco de dados:
Foi utilizado o entity framework, utilizando os seguintes comandos no terminal e dentro da pasta Gerenciador_de_Tarefas seguindo a ordem:

dotnet ef migrations add Criandotabelas

dotnet ef database update

### Para rodar o projeto:

No terminal e dentro da pasta Gerenciador_de_Tarefas utilize o comando:

dotnet watch


