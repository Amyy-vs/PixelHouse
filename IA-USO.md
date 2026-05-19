# Declaração de Uso de IA/LLM

**Nome:** Amanda Vieira da Silva
**Data:** 18/05/2026
**Modelo(s) de IA/LLM utilizado(s):** Claude Sonnet 4 (Anthropic), Blackbox AI
**Ferramentas auxiliares:** claude.ai, Blackbox (extensão Visual Studio Code)

## 1) Nível de uso por parte do desafio
- Parte A (SQL): ☐ Não usei IA  ☑ Consultei IA  ☐ Usei IA para gerar parte do código
- Parte B (C#): ☐ Não usei IA  ☐ Consultei IA  ☑ Usei IA para gerar parte do código

## 2) O que a IA produziu (3–6 linhas por parte)

A) Utilizei para validar a cláusula NOT EXISTS como estratégia de deduplicação e confirmar que o intervalo de datas com DATEADD cobria corretamente o mês corrente sem excluir o último dia.

B) Utilizei para gerar o repositório in-memory, o controller refatorado com DI e async/await, os DTOs de entrada e saída, e o novo endpoint GET /api/turmas/{id}. Em seguida, utilizei o Blackbox no VS Code para as correções: registro Scoped no lugar de Singleton, retorno 201 CreatedAtAction no POST, e validações via DataAnnotations no DTO de entrada. 

## 3) Prompts principais (cole abaixo)

[Claude — ]
"Tenho este controller legado em ASP.NET Core com os seguintes problemas: SQL Injection
por concatenação de strings, connection string hardcoded, sem injeção de dependência e sem async/await. Caso identifique novos problemas, me sinalize.
Quero refatorar usando a interface ITurmaRepository, com uma implementação in-memory para
não depender de banco real na avaliação. Gere o InMemoryTurmaRepository implementando os métodos
GetPedidosAprovadosAsync, CriarTurmaAsync e GetTurmaDetalheAsync com dados mockados."

"Gere o TurmasController refatorado injetando ITurmaRepository no construtor, mantendo os endpoints GET /{id}/pedidos e POST /, e adicionando o novo GET /{id} que retorna idTurma, nomeTurma, anoFormatura, status, totalPedidosAprovados e receitaTotal — com 404 caso a turma não exista. Todos os métodos devem ser async."

[Blackbox — revisão no VS Code]
"Mapeie os pontos faltantes e oportunidades de melhoria no projeto."
