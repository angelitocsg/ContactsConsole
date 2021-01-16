# Contacts

.NET CRUD study model project without database used in training.

## Prerequisites

* .NET 5.0
* Visual Studio Code

## Create project and folder structure

Create default `.gitignore` file:

```shell
dotnet new gitignore
```

Create `README.md` file:

```shell
echo # Contacts > README2.md
```

Create `src` directory:

```shell
mkdir src
```

Create a .NET solution file:

```shell
dotnet new sln -n Contacts
```

Enter src directory and create .NET console project:

```shell
cd src
dotnet new console -n Contacts
```

Add project to solution:

```shell
dotnet sln ..\Contacts.sln add Contacts\
```

## Create classes

1. Entities
2. ValueObjects
3. Interfaces
4. Repositories
5. Services
6. Controllers