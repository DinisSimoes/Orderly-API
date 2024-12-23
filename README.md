# Orderly-API

"corção" do projeto

## Projetos Relacionados

Este projeto está relacionado a outros repositórios.

 - [Orderly Consumer](https://github.com/DinisSimoes/Orderly-Consumer): Job para consumir mensagens do kafka e escrever no mongoDB
 - [Orderly WEB](https://github.com/DinisSimoes/Orderly-WEB): front-end da aplicação

## Pontos a ter em conta
 - Este é o projeto principal do teste.
 - Para simplificar, criei testes unitários apenas para os serviços das orders. Em um cenário real, testaria muitos mais casos, pois busco manter um padrão de cobertura de código em torno de 90% (incluindo o frontend).
 - O projeto foi desenvolvido com as bases de dados configuradas para rodar localmente. As conexões estão definidas no arquivo appsettings.
 - Tanto o projeto quanto o consumidor estão prontos para rodar em Docker. O único componente que não está configurado para Docker é o Angular. Embora não tenha priorizado isso como parte do teste, poderia facilmente criar o Dockerfile para o frontend também.
 - O sistema está estruturado para permitir uma rápida extensão para outras entidades. Já criei os repositórios CRUD e os respectivos serviços para todas as entidades.
 - Apenas a entidade orders possui a implementação completa de CQRS e controladores, pois foram o foco do teste.
 - As migrações precisam ser executadas antes de iniciar o projeto.
 - O Kafka foi configurado e está rodando em Docker.

## Tomadas de Decisão Durante o Desenvolvimento

Durante o desenvolvimento deste sistema, tomei várias decisões arquiteturais e de design para garantir que o projeto fosse modular, escalável e de fácil manutenção, tanto a curto quanto a longo prazo. As decisões foram baseadas em princípios de Clean Architecture, DDD (Domain-Driven Design) e SRP (Single Responsibility Principle). Abaixo estão as principais decisões tomadas:

 - **Princípio SRP (Single Responsibility Principle):** Busquei manter cada módulo e serviço com uma única responsabilidade, para garantir que mudanças em um componente não impactem outros de forma desnecessária. Seguir o SRP contribui para a simplicidade, manutenção e teste do sistema, além de garantir que cada parte do código tenha uma função bem definida e clara.

 - **Pré-preparação para Novas Funcionalidades:** O objetivo foi estruturar o projeto de forma que a implementação de novas funcionalidades fosse o mais simples possível. Por exemplo, para adicionar funcionalidades como a criação de usuários, alteração de produtos ou exclusão de itens de um pedido, o processo é simples: basta criar o handler correspondente, o comando ou a consulta necessária, e o controlador da entidade. Essa estrutura facilita a adição de novas funcionalidades sem a necessidade de reestruturar o código base.

 - **Separação de Repositórios (Repos):** Embora, no estado atual da aplicação, a separação de repositórios para cada entidade não se justifique completamente e possa ser considerada um pouco de "over-engineering", preparei o sistema para que, caso a aplicação cresça, essa separação possa ser facilmente implementada para o caso de as outras entidades precisarem das funcionalidades, rapidez e fluides que a entidade "orders" já precisa ter.

 - **Criação de Serviços:** Dado o escopo atual do projeto, não havia uma necessidade imediata de separar os serviços, podendo (inclusive) os repositórios ser chamados diretamente nos handlers. No entanto, pensando na escalabilidade do sistema, implementei a separação de responsabilidades, seguindo o princípio de responsabilidade única (SRP). Isso garante que, conforme o sistema cresça, cada componente tenha uma função bem definida e o código permaneça limpo e fácil de manter. A separação de responsabilidades também facilita a realização de testes unitários e a manutenção a longo prazo.

