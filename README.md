## Como executar
``` bash
dotnet run - Swagger disponível em: https://localhost:{porta}/swagger 
```
## Decisões de design
- Repositório in-memory (Scoped) sem dependência de banco; substituível por Dapper/EF Core.
- DI via construtor — controller desacoplado e testável.
- async/await em todos os endpoints para suportar I/O real sem bloquear threads.
- DTOs separados dos models; serialização camelCase via JsonNamingPolicy em Program.cs.
- Validação de entrada com [Required], [Range] e [StringLength]; [ApiController] retorna 400 automaticamente.