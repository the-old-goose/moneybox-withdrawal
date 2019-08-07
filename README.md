# Moneybox Money Withdrawal

The solution contains a .NET core library (Moneybox.App) which is structured into the following 3 folders:

* Domain - this contains the domain models for a user and an account, and a notification service.
* Features - this contains two operations, one which is implemented (transfer money) and another which isn't (withdraw money)
* DataAccess - this contains a repository for retrieving and saving an account (and the nested user it belongs to)

## The task

The task is to implement a money withdrawal in the WithdrawMoney.Execute(...) method in the features folder. For consistency, the logic should be the same as the TransferMoney.Execute(...) method i.e. notifications for low funds and exceptions where the operation is not possible. 

As part of this process however, you should look to refactor some of the code in the TransferMoney.Execute(...) method into the domain models, and make these models less susceptible to misuse. We're looking to make our domain models rich in behaviour and much more than just plain old objects, however we don't want any data persistance operations (i.e. data access repositories) to bleed into our domain. This should simplify the task of implementing WithdrawMoney.Execute(...).

## Guidelines

* You should spend no more than 1 hour on this task, although there is no time limit
* You should fork or copy this repository into your own public repository (Github, BitBucket etc.) before you do your work
* Your solution must compile and run first time
* You should not alter the notification service or the the account repository interfaces
* You may add unit/integration tests using a test framework (and/or mocking framework) of your choice
* You may edit this README.md if you want to give more details around your work (e.g. why you have done something a particular way, or anything else you would look to do but didn't have time)

Once you have completed your work, send us a link to your public repository.

Good luck!

## Comments

1. [Commit #1:](https://github.com/the-old-goose/moneybox-withdrawal/commit/3b67c855920922d68f389b602c0a67f25dc7fec5)
* Added private setter to some properties as I feel that unless for testing (could make a specific constructor),balance should not be set outside the class. If this is done in error, it would have a drastic effect! 
* I also considered refactoring the notification service into the `account` class however I feel it's better suited to the feature classes as I feel it would clutter the `Account` class when further notification logic is added. 
* I have also added flags and configured the logic as there is no point notifying the user if they will have a low balance after the transaction yet the transaction fails to succeed.

2.[Commit #2:](https://github.com/the-old-goose/moneybox-withdrawal/commit/f9fde68ca5d18fea1cd81e5197a3eab1b3e91ee5)
* The `WithdrawMoney` follows the same logic as `TransferMoney`

3.[Commit #3:](https://github.com/the-old-goose/moneybox-withdrawal/commit/043ec583acc6a8a34f28711b0e4d8c8765609c35)
* I would definitely implement a base class called `Money` which wraps `decimal amount`. This would implement validation to check that amount is positive and also specify a currency.

4.[Commit #4:](https://github.com/the-old-goose/moneybox-withdrawal/commit/ed266ae8285783eb9ea6c12da5c005a6ad837e97)
* I have been strict with the 1 hour time frame and have managed to implement an example test case using MSTest. Full test coverage would be the next steps I would take.

> " There is 100 ways to skin a cat,
>     hopefully this is the way you want the cat to be skinned" -The-old-goose
