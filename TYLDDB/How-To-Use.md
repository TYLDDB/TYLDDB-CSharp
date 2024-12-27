# How to use TYLDDB

## Basic content

### Read database

#### Instantiate the LDDB class

```c#
LDDB lddb = new LDDB();
```

If you are using a higher version .NET, you can also write a little simpler.

```c#
LDDB lddb = new();
```

#### Read file

```c#
lddb.FilePath = "./example.lddb";
lddb.ReadingFile();
```

The contents of the file are then loaded into memory.

#### Select the database to read

```c#
lddb.LoadDatabase("database1");
```

This allows you to select the contents of the database for the next operation

If you want to get all the contents of this database, you can use `lddb.GetLoadingDatabaseContent()` , which returns a string.

#### Gets all database names

```c#
lddb.ReadAllDatabaseName();
```

