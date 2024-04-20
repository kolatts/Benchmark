# efbench (Benchmark)
A simple set of projects to run performance testing, especially with EF Core and SQL.

# Getting Started

1. Install with the following command:
   ```ps
   dotnet tool install -g efbench
   ```
1. Run against `localdb` (Windows only)
    ```
    efbench primary-keys --db localdb
    ```
1. Run against `Sqlite in-memory`
    ```
    efbench primary-keys --db sqliteinmemory
    ```
1. Run against `SQL Server in a container`
    1. Ensure Rancher Desktop / Docker Desktop is installed along with docker engine.
    1. Copy the contents of the docker-compose.yml to a directory
        1. Run `docker compose up -d` from this directory to get container running
    ```
    efbench primary-keys --db sqlservercontainer
    ```
1. Run against any SQL server (optimized for AzureSql, but other SQL Servers will work also):
    ```
    efbench primary-keys --connection-string <your-connectionstring>
    ```