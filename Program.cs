﻿using System.Data;
using Scheduler;

try
{
    Utilities.DisplayMessage(
@"Tony's Brevium homework
***********************

Here's a log of my development steps:
* Understand the assignment, create the initial code wrappers (with error handling, etc)
* Using NSWAG (through Visual Studio 2022) auto-generate API connections, contract objects, etc
* Wrap the auto-generated code in a to add error handling, logging, and any other customizations needed. This allows the autogenerated code to remain untouched
* Add code to GitHub
* Create scheduling logic

NOTE: Due to this being a small simple time constrained assignment, unit tests and GUI interfaces will be skipped for now

***********************
"
		);

    await new Scheduler.Scheduler().RunTest();

	// Done
	Utilities.DisplayMessage(
@"

***************
COMPLETED RUN!!
***************"
        );
}
catch(Exception exception)
{
    Utilities.LogErrorException(exception);
}
