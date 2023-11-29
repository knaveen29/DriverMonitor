# Synopsis

Driver activity monitor

# Pre-requisites:
1. Docker desktop has to be installed 
2. Optional : 		
     .Net 6 framework 
     Node - 16.x
     NPM - 8.x

# Installation

```
git clone https://github.com/knaveen29/DriverMonitor.git
```

## Build
```
dotnet build
```

## Run
Navigate to Project root folder (where .sln file is there) and in terminal :

1. To Run all the components in docker:

```
docker-compose build (for first time)
docker-compose up (always)
```

OR

2. Run components individually

a. Running api

```
dotnet run --project /DriverMonitorBackend
```

b. Running react (UI)

```
npm i
npm run start
```


# URLs
UI : http://localhost:3000/
API : http://localhost:5000/swagger

# To run & query Postgres data
1. In Docker, click on the DB container and open the CLI.
2. Type -> psql -U postgres 
   This will enable psql with the username as postgres
3. Now to navigate to this project's DB, type -> \c driver_monitor
4. Now that you are in the DB, you can query the data. To start with list of tables -> \dt 

Help : To learn more on the commands : https://www.enterprisedb.com/postgres-tutorials/postgresql-query-introduction-explanation-and-50-examples


# Tests

Run Unit Tests for dotnet

```
dotnet test
```

Run Tests in React
```
npm run test
```


# Additional Links

1. [React-Bootstrap UI](https://react-bootstrap.netlify.app/)
2. [Postgres handy command](https://www.postgresqltutorial.com/postgresql-administration/psql-commands/)
3. [Jest tests](https://jestjs.io/docs/getting-started)
4. 
