# pizza-place-API

This is a sample Back-end API for a fictitious pizza place,
Dataset csvs are located here
```
https://www.kaggle.com/datasets/mysarahmadbhat/pizza-place-sales/data?select=pizza_types.csv
```
While this API's Database schema can be found in ./Database Schema .


## Dependencies

Below are the following Required Dependencies for this to run properly

- Visual Studio 2022
- .net 8.0
- Microsoft.EntityFrameworkCore.8.0.4
- Microsoft.EntityFrameworkCore.SqlServer.8.0.4

## Usage

- Clone the repo
  ```
  https://github.com/jmc010796/pizza-place-API.git
  ```
- Open Solution Install Nugget Packages
  ```
  Microsoft.EntityFrameworkCore.8.0.4
  Microsoft.EntityFrameworkCore.SqlServer.8.0.4
  ```
- Run HTTP

## API Endpoints

The following are the currently implemented Endpoints usable upon running this software

| Endpoint | Function | Parameters |
| --- | --- | --- |
| POST api/Data/UploadDataSet | Loads Dataset from CSV and Imports into the Databse | type = Indicates what type of csv are being uploaded |
| | | file = CSV file to be imported |
| GET api/Menu/GetAllPizza/{page}?pageSize={pageSize} | Get's All Pizza | page = Page number |
| | | pageSize = Number of Pizza per page |
| GET api/Menu/GetCategory/{page}?categId={categId}&pageSize={pageSize} | Get's All Pizza under a category | page = Page number |
| | | pageSize = Number of Pizza per page |
| | | categId = Category ID |
| POST api/Menu/SearchPizza/{page}?pageSize={pageSize} | Returns All Pizza that match all condition in query | page = Page number |
| | | pageSize = Number of Pizza per page |
| | | query = JSON object containing search conditions, See Model file for reference |
| PUT api/Menu/AddOrder | Takes in a list of pizza and quantity and adds it to an Order | items = List of Menu Item containing pizza ids and quantities |
