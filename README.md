# Coding Exercise Web Api
Fictional customer registration web api with some validations and persistence to database with requirements taken from a previous companies interview coding exercise which I had to do a few years ago.

This is a very simple Web API to allow new Policy holders to register for an insurance policy

The API contains just one endpoint. The endpoint supports some basic validation of the policy holder/customers information
and returns a unique online Customer ID. The registration data supported in this scenario is:
1) Policy holder’s first name
2) Policy holder’s surname
3) Policy Reference number
4) Either the policy holders DOB OR the policy holder’s email

The API has been implemented in .Net using Visual Studio 2022 and does not support any authentication.
The submitted data is stored along with the customer ID using Entity Framework to a local database

## Validation Requirements
In reality the registration data would be validated against a back-office policy system but for ease of understanding the follow type of validation is supported:

1) Policy holder’s first name and surname are both required and should be between 3 and 50
chars.
2) Policy Reference number is required and should match the following format XX-999999.
Where XX are any capitalised alpha character followed by a hyphen and 6 numbers.
3) If supplied the policy holders DOB should mean the customer is at least 18 years old at the
point of registering.
4) If supplied the policy holders email address should contain a string of at least 4 alpha
numeric chars followed by an ‘@’ sign and then another string of at least 2 alpha numeric
chars. The email address should end in either ‘.com’ or ‘.co.uk’.

## Installation notes ##

If you used git clone to download the source code, then it definitely builds ok  in Visual Studio 2022 Community Edition so hopefully will be ok in other versions or Rider.

To create the local database where it saves the new customer details, double click on the top level folder file: CreateLocalDatabase.bat

It will tell you the name of the database and where it plans to install it ... hopefully you have a local instance (localdb)\\MSSQLLocalDB.

## Usage notes ##

When you Debug the solution, it should open a Swagger UI page where you can exercise the Web API endpoint

Can also use POSTMAN if you prefer:

```
POST https://localhost:7270/api/customers
{
    "firstName": "Simon", 
    "lastName": "Evans",
    "policyNumber": "AB-223454",
    "dateOfBirth": "1972-08-16",
    "email": "simon.evans@test.com"
}
```
## Development notes ##

This section is to explain some of the decisions made.

I named the endpoint Customers as that is what it is being added to. Even though the requirements say it is for Registering a customer, I think REST guidelines say don't use the describing verbs, try to stick to the HTTP verbs. So in effect Registering is really 'Creating' a customer and Create's should be done via HTTP POST verb. Some people call the method name which handles the POST verb 'Post' but I called it 'Create'.

### Layers ###

For a Web service this small I would normally keep everything except the unit tests in a single Web API project but have sub folders for Models, Data (layer), Controllers, Validators to show logical separation. However I've put these into different assemblies to show how a bigger application might be structured. Excluding testing, I've gone with a compromise of just 3 layers although lots of projects i've worked on have 7 or more layers or have gone with different approach like a domain driven design for larger systems or even CQRS where the extra complexity could be justified. It's difficult to justify it here, so for now going for separate Web API and Core logic separated out to allow for different use cases.  Here's a brief projects description in terms of how they provide separation of concerns:

#### 1. Web.Api #### 
Just the REST API layer with knowledge of how to receive the input and perform model binding and return the output including correct HTTP status codes.  

#### 2. Web.Api.Core #### 
More of pure C# assembly with no references to HTTP layer above and no knowledge of persistence layer. This layer includes the DTO objects in folder Models, validation rules in folder Validators and Business logic in folder Services. Probably a bit over the top but choose to allow service to receive a DTO rather than the Entity used by the persistence layer as you don't normally expose the data entities to the UI or Web Service layer directly. Normally a 'ViewModel' or 'DTO' is used for that purpose. In the Service layer, the Adapt extension method from Mapster library is used to convert DTO to Entity.

#### 3. Web.Api.Infrastructure ####
This is anything which is not Web.Api and not 'Core' logic e.g. things which would access the database, network, file system. For these requirements though, there is just a single collection of concerns in subfolder 'Data'. This contains the data entities for things like Customer, and a repository for Customer. Migrations folder is creating by the Entity Framework tooling

#### 4. Web.Api.Core.Tests ####
As the name implies, this is just the unit tests of classes from Web.Api.Core. Only had time to implement validation tests 

### Implementation of the validation requirements ###

You can use the built in model data annotations such as Required, Length to implement some of the validation requirements, but I went with the FluentValidation library as they have helpers to make Unit testing easier. When I came to add Swagger later on, the only downside is that using plain data annotations would have improved the swagger documentation so would indicate which fields were required automatically. I guess you could go with both approaches but then it is not very DRY.

I have tested that if the model submitted does not pass the validation rules, then you get a HTTP 400 error with the JSON array of model errors. Some people go with HTTP 422 I think as the model may be 'correct' in terms of properties submitted but just invalid in terms of validation.

Likewise when the model is correct and the customer can be created, I just went with HTTP 200, but again some people use HTTP 201 Created.

Finally if exception occurs like if you rename the database in appsettings.json, then checked it returns http 500 and the controller does catch the exception to pass it to the ILogger.

### Areas for improvement ###

Note I treated the  Age check requirement in the most simplistic manner by just subtracting 18 years from current date and doing date comparison but ran out of time to think about people born on Feb 29th in Leap years so didn't write any unit tests for that. As the age check is delegated to a function in the validator class, I did create an overload with the intention of allowing a set of unit tests to pass in different 'today dates' but ran out of time to write any such tests.

I treated the Email check with a regular expression in the end. The FluentValidation library does offer a EmailAddress built in function but it only seems to enforce "a@b" type addresses. The requirements might also have been wrong, in that they said at least 4 alphanumerics followed by @, followed by least 2 alphanumerics but email addresses should be able to contain more than alphanumerics before the @ symbol e.g. dash, hyphen, period, etc so in the end i went with regular expression which allows any character except whitespace before an @ symbol following by at least two non whitespace characters. I didn't allow for leading or trailing whitespace in the emails too which probably needs to be improved.

Probably need to revisit where I just blindly added nullable reference type operator to few properties in one or two dto's to get the nullable warning check to go away and perhaps default them to empty strings within a constructor or with individual property defaulters and remove the IsNotNull checks from the validation rules and unit tests

Finally with a bit more time, could have added some tests for the service class with moq library to check a few of the paths through the method to make sure the right things were being called.

### Non requirements (but maybe needed?) ###
The requirements did not ask for this but when I was originally asked to do this a few years ago for an interview, they said try to read in between the lines, so I added some business logic in the CustomerService class to perform a check to see if the customer  has already registered with the same email before and if so it does not create a new customer. This is detected in the CustomerController and returned as HTTP 400 Bad Request like any other validation problem. 
