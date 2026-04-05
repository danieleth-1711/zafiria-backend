-- Crear base para Keycloak
CREATE DATABASE keycloak_db;

-- Conectarse a keycloak_db
\c keycloak_db

GRANT ALL PRIVILEGES ON DATABASE keycloak_db TO zafiria_user;
GRANT ALL ON SCHEMA public TO zafiria_user;
ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT ALL ON TABLES TO zafiria_user;

-- Conectarse a la base de Zafiria creada por docker
\c zafiria_db

GRANT ALL PRIVILEGES ON DATABASE zafiria_db TO zafiria_user;
GRANT ALL ON SCHEMA public TO zafiria_user;
ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT ALL ON TABLES TO zafiria_user;
