[![.NET](https://github.com/Prigan/dotnet-webapp-deepti/actions/workflows/Basic%20CI%20and%20CD.yml/badge.svg)](https://github.com/Prigan/dotnet-webapp-deepti/actions/workflows/Basic%20CI%20and%20CD.yml)

# Dot - A Developer Search System
In this coding challenge you will build a simple webapp called "Dot" that uses the Github REST API to load user data.
The challenge is designed to test your thinking, process, and ability to put together an application.

# Requirements

## Source Control - Git
We will be using git to manage our repositories and integrate with our pipeline. In this challenge, we are looking to see your ability to:

1. Fork this github repo.
2. Create branches for new features/ideas. You are free to organize as you see fit. We want to see your process.
3. Create commits with comments
4. Push changes
5. Merge branches to master

## Building the Web App
In this challenge, you will:

1. Generate a new dotnet app.
2. Use webapp or mvc (your choice)
3. Add all 5 CRUD operations in the webapp (use localStorage, no need for a DB unless you want to - Bonus)
4. You have an option to leverage a frontend framework such as Vue/Angular or keep things simple with dotnet views/razor pages. 
5. Leverage the Github REST API for your data.

## Github REST API
Our applications are always in need of intergration, you will leverage the Github API in this case (focus on Users) to get the data you need.
[Github API Docs](https://docs.github.com/en/rest)

## Documentation
At the end you will commit a file to this repo called "MyThinking.md". Please briefly explain at a high level how you approached each requirement. 
You can also include a section of things you would do if you had more time or would like to learn and explore.

## Sample Idea (you are not limited to this)
An example of using this data efficiently:
- I can search for a User (Home Page with a Form, for example) - you can use AJAX if you want.
- I can save the User profile to my favorites (or similar feature to save) [CREATE]
- I can view the User profile [READ]
- When I view the User Profile from my favorites (or similar area), I can add a note (with ability to edit) to the User I have saved in my favorites [UPDATE]
- I can delete the User from my favorites [DELETE]
- I can view a list of  repos from the user [BONUS - API call with large datasets, how will you handle loading/caching?) 

## Checklist

- [ ] Did you use git?
- [ ] Generate a dotnet app with all CRUD operations?
- [ ] Add proper code styling and linting?
- [ ] Use the Github API?
- [ ] Complete the documentation?
