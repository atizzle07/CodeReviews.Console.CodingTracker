## Coding Tracker Requirements

This app should be very similar to the Habit Logger you’ve previously completed. It will serve the purpose of reinforcing what you’ve learned with a bit of repetition and building on that knowledge with slightly more challenging requirements.

This time you’ll have to deal with the complexity of handling Dates and Times, which is a real challenge in any application. You’ll also be using your first external library. Often times in professional environments programmers don’t reinvent the wheel and save time by using public solutions provided by other coders. That’s the beauty of the internet. You have access to an amazing coding community!

In the first app we also didn’t have requirements for coding organization. This time you’ll have to use Separation of Concerns, one of the most important principles in modern programming. We recommend you go through our Object Oriented Programming Crash Course prior to completing this project so you're better equipped to make your code more organized.

### Requirements
- This application has the same requirements as the previous project, except that now you'll be logging your daily coding time.


- To show the data on the console, you should use the "Spectre.Console" library.


- You're required to have separate classes in different files (ex. UserInput.cs, Validation.cs, CodingController.cs)


- You should tell the user the specific format you want the date and time to be logged and not allow any other format.


- You'll need to create a configuration file that you'll contain your database path and connection strings.


- You'll need to create a "CodingSession" class in a separate file. It will contain the properties of your coding session: Id, StartTime, EndTime, Duration


- The user shouldn't input the duration of the session. It should be calculated based on the Start and End times, in a separate "CalculateDuration" method.


- The user should be able to input the start and end times manually.


- You need to use Dapper ORM for the data access instead of ADO.NET. (This requirement was included in Feb/2024)


- When reading from the database, you can't use an anonymous object, you have to read your table into a List of Coding Sessions.


- ollow the **DRY Principle**, and avoid code repetition.

### Resources
If you have learned the basics of C# following the C# Foundations article, and completed the Habit Logger project, you should know all the basic techniques needed to complete this project. Here’s a list of extra resources you might need:

- [Spectre Console](https://spectreconsole.net/) Documentation

- [Using Configuration Manager](https://docs.microsoft.com/en-us/troubleshoot/dotnet/csharp/store-custom-information-config-file)
- [Parsing DateTime in C#](https://medium.com/@Has_San/datetime-in-c-1aef47db4feb)
- [Dapper Tutorial](https://www.learndapper.com/)

### Tips

- It's up to you the order in which you'll build, but we recommend you do it in this order: configuration file, model, database/table creation, CRUD controller (where the operations will happen), TableVisualisationEngine (where the consoleTableExt code will be run) and finally: validation of data.

- Sqlite doesn't support dates. We recommend you store the datetime as a string in the database and then parse it using C#. You'll need to parse it to calculate the duration of your sessions.

- Don't forget to push your changes to github every time you stop working.

- Don't forget the user input's validation: Check for incorrect dates. What happens if a menu option is chosen that's not available? What happens if the users input a string instead of a number? Remember that the end date can't be before the start date.

### Challenges

- Add the possibility of tracking the coding time via a stopwatch so the user can track the session as it happens.

- Create reports where the users can see their total and average coding session per period.

- Create the ability to set coding goals and show how far the users are from reaching their goal, along with how many hours a day they would have to code to reach their goal. You can do it via SQL queries or with C#.


### Creating a Configuration File
In advanced applications, configuration properties are stored in an xml file. This practice makes it easier to configure your application in production. It’s not absolutely necessary now, but it’s not hard to learn and you should get used to it from the beginning of your coding journey. It makes your code cleaner and more organised. [Check out the documentation](https://docs.microsoft.com/en-us/troubleshoot/dotnet/csharp/store-custom-information-config-file) and if necessary search for “configuration file C#” on Youtube.

### Creating a Desktop App
If you have watched the [entire C# Foundation course](https://www.youtube.com/watch?v=o81wpRuOGjE&list=PL4G0MUH8YWiAMypwveH2LlLK_o8Jto9CE), you have already created a Math Game Desktop app using the amazing .NET MAUI. It will be great practice to build a desktop Coding Tracker App with the same functionality you’ve created for this console app. There will be some challenges, especially if you want to create a timer, but you’ve already got all the skills necessary. And remember, if you get stuck, reach out on our Discord community and we will help!


### To Do List

- Need to add *.db to the gitignore list
- After creating the database need to change properties of the db to "copy if newer"