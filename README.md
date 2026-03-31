# Joyería ZAFIRIA — Backend API REST

Catálogo de joyas desarrollado con Arquitectura Limpia, .NET 9, PostgreSQL y Keycloak.

## Tecnologías
- .NET 9 — Backend API REST
- PostgreSQL 15 — Base de datos
- Keycloak 23 — Autenticación JWT
- Entity Framework Core — Migraciones
- Docker — Contenedores
- Swagger — Documentación API

## Requisitos
- Docker Desktop instalado

## Cómo ejecutar
```bash
docker compose up --build
```

## URLs
| Servicio | URL |
|---|---|
| API Swagger | http://localhost:5025/swagger |
| Keycloak Admin | http://localhost:8910 |
| PostgreSQL | localhost:5455 |

## Credenciales
**Keycloak:**
- Usuario: `zafiria-admin`
- Contraseña: `Zafiria2026`

**PostgreSQL:**
- Base de datos: `zafiria_db`
- Usuario: `zafiria_user`
- Contraseña: `Zafiria2026`

## Endpoints públicos
- GET /api/joyas
- GET /api/joyas/{id}
- GET /api/joyas/categoria/{id}
- GET /api/joyas/ocasion/{id}
- POST /api/reservas

## Endpoints protegidos (requieren token Admin)
- POST /api/joyas
- PUT /api/joyas/{id}
- DELETE /api/joyas/{id}
- GET /api/reservas
- PUT /api/reservas/{id}