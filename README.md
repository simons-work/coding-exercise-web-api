# Coding Exercise Web Api
Fictional customer registration web api with some validations and persistence to database with requirements taken from a previous companies interview coding exercise which I had to do a few years ago.

This is a very simple Web API to allow new Policy holders to register for an insurance policy

The API contains just one endpoint. The endpoint supports some basic validation of the policy holder/customers information
and returns a unique online Customer ID. The registration data supported in this scenario are:
1) Policy holder’s first name
2) Policy holder’s surname
3) Policy Reference number
4) Either the policy holders DOB OR the policy holder’s email

The API has been implemented in .Net using Visual Studio 2022 and does not support any authentication.
The submitted data is stored along with the customer ID using Entity Framework to a local database

### Validation implemented
In reality the registration data would be validated against a back-office policy system but for ease of understanding the follow type of validation is supported

1) Policy holder’s first name and surname are both required and should be between 3 and 50
chars.
2) Policy Reference number is required and should match the following format XX-999999.
Where XX are any capitalised alpha character followed by a hyphen and 6 numbers.
3) If supplied the policy holders DOB should mean the customer is at least 18 years old at the
point of registering.
4) If supplied the policy holders email address should contain a string of at least 4 alpha
numeric chars followed by an ‘@’ sign and then another string of at least 2 alpha numeric
chars. The email address should end in either ‘.com’ or ‘.co.uk’.
