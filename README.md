# API Netcon - Viabilidade de Ativos

API REST em .NET para verificar ativos (equipamentos de telecomunicações) dentro de um raio geográfico específico.

## 🚀 Como Rodar com Docker

### Pré-requisitos

- Docker instalado ([Download aqui](https://www.docker.com/))

### Passo a Passo

1. **Clone o repositório:**

```bash
git clone https://github.com/hcscaue/Netcon-Test.git
cd Netcon-Test
```

2. **Build da imagem Docker:**

```bash
docker build -t netcon-api -f NetconTest.Api/Dockerfile .
```

3. **Rodar o container:**

```bash
docker run -d -p 8080:8080 --name netcon-api netcon-api
```

4. **Acesse a API:**

   - Swagger UI: `http://localhost:8080/swagger`
   - API Base: `http://localhost:8080`

5. **Parar o container:**

```bash
docker stop netcon-api
docker rm netcon-api
```

---

## 📖 Endpoints

### 1. Autenticação (Obter Token JWT)

**Endpoint:** `POST /authorization`

**Request Body:**

```json
{
	"name": "admin",
	"password": "admin"
}
```

**Response (200 OK):**

```json
{
	"token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

**Exemplo com cURL:**

```bash
curl -X POST http://localhost:8080/authorization \
  -H "Content-Type: application/json" \
  -d '{"name":"admin","password":"admin"}'
```

---

### 2. Buscar Ativos no Raio

**Endpoint:** `GET /api/feasibility`

**Query Parameters:**
| Parâmetro | Tipo | Obrigatório | Descrição | Validação |
|-----------|------|-------------|-----------|-----------|
| `latitude` | float | Sim | Latitude do ponto central | Entre -90 e 90 |
| `longitude` | float | Sim | Longitude do ponto central | Entre -180 e 180 |
| `radius` | integer | Sim | Raio de busca em metros | Entre 10 e 1000 |

**Headers:**

```
Authorization: Bearer {seu-token-jwt}
```

**Response (200 OK):**

```json
[
	{
		"id": 75,
		"name": "CTO-RJO-023",
		"latitude": -22.910159,
		"longitude": -43.182978,
		"radius": 15.56
	},
	{
		"id": 79,
		"name": "CTO-RJO-040",
		"latitude": -22.910302,
		"longitude": -43.184067,
		"radius": 98.45
	}
]
```

**Response (200 OK - Nenhum ativo encontrado):**

```json
[]
```

**Response (400 Bad Request):**

```json
{
	"code": "400",
	"reason": "empty field",
	"message": "latitude is mandatory",
	"status": "bad request",
	"timestamp": "2025-01-13T14:25:00Z"
}
```

**Response (401 Unauthorized):**

```json
{
	"message": "Unauthorized"
}
```

**Exemplo com cURL:**

```bash
# Substitua YOUR_TOKEN pelo token recebido na autenticação
curl -X GET "http://localhost:8080/api/feasibility?latitude=-22.910159&longitude=-43.182978&radius=500" \
  -H "Authorization: Bearer YOUR_TOKEN"
```

---

## 🧪 Testes Unitários

### Rodar todos os testes:

```bash
dotnet test
```

### Rodar com detalhes:

```bash
dotnet test --verbosity detailed
```

### Cobertura de Testes:

- ✅ **AssetService**: Testa lógica de cálculo de distância e filtro por raio
- ✅ **AuthService**: Testa geração e validação de token JWT
- ✅ **FeasibilityController**: Testa validações de entrada e retorno de erros

---

## 🏗️ Arquitetura (Domain-Driven Design)

```
┌─────────────────┐
│   Api Layer     │  ← Controllers, JWT Config, Swagger
├─────────────────┤
│ Application     │  ← Services, DTOs (Business Logic)
├─────────────────┤
│ Infrastructure  │  ← Repositories (Data Access)
├─────────────────┤
│    Domain       │  ← Entities (Core Models)
└─────────────────┘
       ▲
       │
   Tests Layer
```

### Estrutura de Pastas:

```
NetconTest/
├── NetconTest.Api/          # Camada de apresentação (Controllers)
├── NetconTest.Application/  # Lógica de negócio (Services)
├── NetconTest.Domain/       # Entidades e DTOs
├── NetconTest.Infra/        # Acesso a dados (Repositories)
└── NetconTest.Tests/        # Testes unitários
```

### Responsabilidades:

- **Domain**: Entidades (`Asset`, `Geometry`) e DTOs (contratos de dados)
- **Infrastructure**: Leitura do arquivo JSON com coordenadas dos ativos
- **Application**: Cálculo de distância (Haversine) e filtragem por raio
- **Api**: Validação de entrada, autenticação JWT, serialização HTTP
- **Tests**: Cobertura da lógica de negócio e autenticação

---

## 🔧 Rodar Localmente (Sem Docker)

### Pré-requisitos:

- .NET 8.0 SDK ([Download aqui](https://dotnet.microsoft.com/download))

### Comandos:

```bash
# Restaurar dependências
dotnet restore

# Compilar
dotnet build

# Rodar testes
dotnet test

# Executar API
cd NetconTest.Api
dotnet run

# A API estará disponível em http://localhost:5000
```

---

## 📦 Tecnologias Utilizadas

- **.NET 8.0** - Framework principal
- **ASP.NET Core Web API** - API REST
- **System.Text.Json** - Serialização JSON
- **JWT Bearer Authentication** - Autenticação
- **xUnit** - Framework de testes
- **Moq** - Mock para testes unitários
- **FluentAssertions** - Assertions expressivas
- **Docker** - Containerização

---

## 📝 Observações Técnicas

### Cálculo de Distância

Utiliza a **fórmula de Haversine** para calcular a distância geodésica entre duas coordenadas geográficas (latitude/longitude), retornando o resultado em metros.

### Dados

O arquivo `dataset.json` contém as coordenadas dos ativos no formato WGS84. Alguns registros podem ter coordenadas nulas e são automaticamente filtrados.

### Segurança

- Token JWT com expiração de 2 horas
- Validação rigorosa de parâmetros de entrada
- Headers de segurança configurados (Cache-Control, X-Request-Id)

---

## 👨‍💻 Autor

Desenvolvido como parte do processo seletivo para Desenvolvedor de Software na Netcon Americas.

**Contato:** [hcs.caue@gmail.com]

---

## 📄 Licença

Este projeto foi desenvolvido para fins de avaliação técnica.
